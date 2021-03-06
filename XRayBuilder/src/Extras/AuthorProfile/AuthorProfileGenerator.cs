﻿using System;
using System.Collections.Async;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XRayBuilderGUI.DataSources.Amazon;
using XRayBuilderGUI.Libraries;
using XRayBuilderGUI.Libraries.Http;
using XRayBuilderGUI.Libraries.Images.Extensions;
using XRayBuilderGUI.Libraries.Logging;
using XRayBuilderGUI.Model;

namespace XRayBuilderGUI.Extras.AuthorProfile
{
    public class AuthorProfileGenerator : IAuthorProfileGenerator
    {
        private readonly IHttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly IAmazonClient _amazonClient;

        public AuthorProfileGenerator(IHttpClient httpClient, ILogger logger, IAmazonClient amazonClient)
        {
            _httpClient = httpClient;
            _logger = logger;
            _amazonClient = amazonClient;
        }

        // TODO: Review this...
        public async Task<Response> GenerateAsync(Request request, CancellationToken cancellationToken = default)
        {
            AuthorSearchResults searchResults = null;
            // Attempt to download from the alternate site, if present. If it fails in some way, try .com
            // If the .com search crashes, it will crash back to the caller in frmMain
            try
            {
                searchResults = await _amazonClient.SearchAuthor(request.Book.Author, request.Book.Asin, request.Settings.AmazonTld, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.Log("Error searching Amazon." + request.Settings.AmazonTld + ": " + ex.Message + "\r\n" + ex.StackTrace);
            }
            finally
            {
                if (searchResults == null)
                {
                    _logger.Log(string.Format("Failed to find {0} on Amazon." + request.Settings.AmazonTld, request.Book.Author));
                    if (request.Settings.AmazonTld != "com")
                    {
                        _logger.Log("Trying again with Amazon.com.");
                        request.Settings.AmazonTld = "com";
                        searchResults = await _amazonClient.SearchAuthor(request.Book.Author, request.Book.Asin, request.Settings.AmazonTld, cancellationToken);
                    }
                }
            }
            if (searchResults == null)
                return null; // Already logged error in search function

            var authorAsin = searchResults.Asin;

            //todo re-implement saving in a nicer way
//            if (Properties.Settings.Default.saveHtml)
//            {
//                try
//                {
//                    _logger.Log("Saving author's Amazon webpage...");
//                    File.WriteAllText(Environment.CurrentDirectory + string.Format(@"\dmp\{0}.authorpageHtml.txt", request.Book.Asin),
//                        searchResults.AuthorHtmlDoc.DocumentNode.InnerHtml);
//                }
//                catch (Exception ex)
//                {
//                    _logger.Log(string.Format("An error occurred saving authorpageHtml.txt: {0}", ex.Message));
//                }
//            }

            // Try to find author's biography

            string ReadBio(string file)
            {
                try
                {
                    var fileText = Functions.ReadFromFile(file);
                    if (string.IsNullOrEmpty(fileText))
                        _logger.Log("Found biography file, but it is empty!\r\n" + file);
                    else
                        _logger.Log("Using biography from " + file + ".");

                    return fileText;
                }
                catch (Exception ex)
                {
                    _logger.Log("An error occurred while opening " + file + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
                }

                return null;
            }

            // TODO: Separate out biography stuff
            string biography = null;
            var bioFile = Environment.CurrentDirectory + @"\ext\" + authorAsin + ".bio";
            var readFromFile = false;
            if (request.Settings.SaveBio && File.Exists(bioFile))
            {
                biography = ReadBio(bioFile);
                if (string.IsNullOrEmpty(biography))
                    return null;
                readFromFile = true;
            }

            if (string.IsNullOrEmpty(biography) && !string.IsNullOrEmpty(searchResults.Biography))
            {
                //Trim authour biography to less than 1000 characters and/or replace more problematic characters.
                if (searchResults.Biography.Trim().Length > 0)
                {
                    if (searchResults.Biography.Length > 1000)
                    {
                        var lastPunc = searchResults.Biography.LastIndexOfAny(new [] { '.', '!', '?' });
                        var lastSpace = searchResults.Biography.LastIndexOf(' ');

                        if (lastPunc > lastSpace)
                            biography = searchResults.Biography.Substring(0, lastPunc + 1);
                        else
                            biography = searchResults.Biography.Substring(0, lastSpace) + '\u2026';
                    }
                    else
                        biography = searchResults.Biography;

                    biography = biography.Clean();
                    if (request.Settings.SaveBio)
                        File.WriteAllText(bioFile, biography);
                    _logger.Log("Author biography found on Amazon!");
                }
            }

            var message = biography == null
                ? "No author biography found on Amazon!\r\nWould you like to create one?"
                : readFromFile
                    ? "Would you like to edit the existing biography?"
                    : "Author biography found on Amazon! Would you like to edit it?";

            // TODO: No dialogs here

            if (request.Settings.EditBiography
                && System.Windows.Forms.DialogResult.Yes ==
                System.Windows.Forms.MessageBox.Show(
                    message, "Biography",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question,
                    System.Windows.Forms.MessageBoxDefaultButton.Button2))
            {
                if (!File.Exists(bioFile))
                    File.WriteAllText(bioFile, string.Empty);
                Functions.RunNotepad(bioFile);
                biography = ReadBio(bioFile);
            }

            if (string.IsNullOrEmpty(biography))
            {
                biography = "No author biography found locally or on Amazon!";
                _logger.Log("An error occurred finding the author biography.");
            }

            if (request.Settings.SaveBio)
            {
                if (!File.Exists(bioFile))
                {
                    try
                    {
                        _logger.Log("Saving biography to " + bioFile);
                        using var streamWriter = new StreamWriter(bioFile, false, System.Text.Encoding.UTF8);
                        streamWriter.Write(biography);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log("An error occurred while writing biography.\r\n" + ex.Message + "\r\n" + ex.StackTrace);
                        return null;
                    }
                }
                if (System.Windows.Forms.DialogResult.Yes == System.Windows.Forms.MessageBox.Show("Would you like to open the biography file in notepad for editing?", "Biography",
                   System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2))
                {
                    Functions.RunNotepad(bioFile);
                    biography = ReadBio(bioFile);
                    if (string.IsNullOrEmpty(biography))
                        return null;
                }
            }

            // Try to download Author image
            request.Book.AuthorImageUrl = searchResults.ImageUrl;

            Bitmap ApAuthorImage = null;
            try
            {
                _logger.Log("Downloading author image...");
                ApAuthorImage = await _httpClient.GetImageAsync(request.Book.AuthorImageUrl, cancellationToken: cancellationToken);
                _logger.Log("Grayscale base64-encoded author image created!");
            }
            catch (Exception ex)
            {
                _logger.Log(string.Format("An error occurred downloading the author image: {0}", ex.Message));
            }

            var bookBag = new ConcurrentBag<BookInfo>();
            if (searchResults.Books != null && request.Settings.UseNewVersion)
            {
                _logger.Log("Gathering metadata for author's other books...");
                try
                {
                    await _amazonClient.EnhanceBookInfos(searchResults.Books).ForEachAsync(book =>
                    {
                        // todo progress
                        bookBag.Add(book);
                    }, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.Log($"An error occurred gathering metadata for other books: {ex.Message}");
                    throw;
                }
            }
            else
            {
                _logger.Log("Unable to find other books by this author. If there should be some, check the Amazon URL to ensure it is correct.");
            }

            _logger.Log("Writing Author Profile to file...");

            return new Response
            {
                Asin = authorAsin,
                Name = request.Book.Author,
                OtherBooks = bookBag.ToArray(),
                Biography = biography,
                Image = ApAuthorImage,
                ImageUrl = searchResults.ImageUrl,
                AmazonTld = request.Settings.AmazonTld
            };
        }

        public class Settings
        {
            public string AmazonTld { get; set; }
            public bool UseNewVersion { get; set; }
            public bool SaveBio { get; set; }
            public bool EditBiography { get; set; }
        }

        public class Request
        {
            public BookInfo Book { get; set; }
            public Settings Settings { get; set; }

        }

        public class Response
        {
            public string Asin { get; set; }
            public string Name { get; set; }
            public string Biography { get; set; }
            public Bitmap Image { get; set; }
            public string ImageUrl { get; set; }
            public BookInfo[] OtherBooks { get; set; }
            public string AmazonTld { get; set; }
        }

        public static Artifacts.AuthorProfile CreateAp(Response response, string bookAsin)
        {
            var authorOtherBooks = response.OtherBooks.Select(book => new Artifacts.AuthorProfile.Book
            {
                E = 1,
                Asin = book.Asin,
                Title = book.Title
            }).ToArray();

            return new Artifacts.AuthorProfile
            {
                Asin = bookAsin,
                CreationDate = Functions.UnixTimestampSeconds(),
                OtherBooks = authorOtherBooks,
                Authors = new[]
                {
                    new Artifacts.AuthorProfile.Author
                    {
                        Asin = response.Asin,
                        Bio = response.Biography,
                        ImageHeight = response.Image.Height,
                        Name = response.Name,
                        OtherBookAsins = response.OtherBooks.Select(book => book.Asin).ToArray(),
                        Picture = response.Image.ToBase64(ImageFormat.Jpeg)
                    }
                }
            };
        }
    }
}