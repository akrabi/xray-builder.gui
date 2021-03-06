﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;
using HtmlAgilityPack;
using Newtonsoft.Json;
using XRayBuilderGUI.Model;

namespace XRayBuilderGUI.Libraries
{
    public static class Functions
    {
        public static TObject JsonDeserialize<TObject>(string value, bool strict = true)
            => JsonConvert.DeserializeObject<TObject>(value, new JsonSerializerSettings
            {
                MissingMemberHandling = strict ? MissingMemberHandling.Error : MissingMemberHandling.Ignore
            });

        public static TObject JsonDeserializeFile<TObject>(string filename, bool strict = true)
            => JsonDeserialize<TObject>(ReadFromFile(filename), strict);

        public static string ReadFromFile(string file)
        {
            using var streamReader = new StreamReader(file, Encoding.UTF8);
            return streamReader.ReadToEnd();
        }

        // TODO: Clean this up more cause it still sucks
        public static string Clean(this string str)
        {
            (string[] searches, string replace)[] replacements =
            {
                (new[] {"&#169;", "&amp;#169;", "&#174;", "&amp;#174;", "&mdash;", @"</?[a-z]+>" }, ""),
                (new[] { "“", "”", "\"" }, "'"),
                (new[] { "&#133;", "&amp;#133;", @" \. \. \." }, "…"),
                (new[] { " - ", "--" }, "—"),
                (new[] { @"\t|\n|\r|•", @"\s+"}, " "),
                (new[] { @"\. …$"}, "."),
                (new[] {"@", "#", @"\$", "%", "_", }, "")
            };
            foreach (var (s, r) in replacements)
            {
                str = Regex.Replace(str, $"({string.Join("|", s)})", r, RegexOptions.Multiline);
            }
            return str.Trim();
        }

        public static string GetBookOutputDirectory(string author, string title, bool create)
        {
            var newAuthor = RemoveInvalidFileChars(author);
            var newTitle = RemoveInvalidFileChars(title);
            var path = Path.Combine(Properties.Settings.Default.outDir, $"{newAuthor}\\{newTitle}");
            if (create)
                Directory.CreateDirectory(path);
            return path;
        }

        public static string RemoveInvalidFileChars(string filename)
        {
            var fileChars = Path.GetInvalidFileNameChars();
            return new string(filename.Where(x => !fileChars.Contains(x)).ToArray());
        }

        public static bool ExtrasExist(string location, string asin)
            => File.Exists(location + $"\\AuthorProfile.profile.{asin}.asc") && File.Exists(location + $"\\EndActions.data.{asin}.asc");

        //public static string GetTempDirectory()
        //{
        //    string path;
        //    do
        //    {
        //        path = Path.Combine(Properties.Settings.Default.tmpDir, Path.GetRandomFileName());
        //    } while (Directory.Exists(path));
        //    Directory.CreateDirectory(path);
        //    return path;
        //}

        public static string TimeStamp()
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var time = string.Format("{0:HH:mm:ss}", DateTime.Now);
            var date = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            return $"Running X-Ray Builder GUI v{version}. Log started on {date} at {time}.\r\n";
        }

        public static string GetPageCount(string rawML, BookInfo bookInfo)
        {
            string output;
            double lineCount = 0;
            if (!File.Exists(rawML) || bookInfo == null)
            {
                output = "Error: RawML could not be found, aborting.\r\nPath: " + rawML;
                return output;
            }
            var bookDoc = new HtmlDocument { OptionAutoCloseOnEnd = true };
            bookDoc.Load(rawML, Encoding.UTF8);
            var booklineNodes = bookDoc.DocumentNode.SelectNodes("//p") ?? bookDoc.DocumentNode.SelectNodes("//div");
            if (booklineNodes == null)
            {
                output = "An error occurred while estimating page count!";
                return output;
            }
            foreach (var line in booklineNodes)
            {
                var lineLength = line.InnerText.Length + 1;
                if (lineLength < 70)
                {
                    lineCount++;
                    continue;
                }
                lineCount += Math.Ceiling((double)lineLength / 70);
            }
            var pageCount = Convert.ToInt32(Math.Ceiling(lineCount / 31));
            if (pageCount == 0)
            {
                output = "An error occurred while estimating page count!";
                return output;
            }
            var minutes = pageCount * 1.2890625;
            var span = TimeSpan.FromMinutes(minutes);
            bookInfo.PagesInBook = pageCount;
            bookInfo.ReadingHours = span.Hours;
            bookInfo.ReadingMinutes = span.Minutes;
            output = $"Typical time to read: {span.Hours} hours and {span.Minutes} minutes ({bookInfo.PagesInBook} pages)";
            return output;
        }

        public static void RunNotepad(string filename)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "notepad",
                Arguments = filename,
                UseShellExecute = false
            };
            try
            {
                using var process = Process.Start(startInfo);
                process?.WaitForExit();
            }
            catch (Exception ex)
            {
                throw new Exception("Error trying to launch notepad.", ex);
            }
        }

        public static void Save<T>(T output, string fileName) where T : class
        {
            using var writer = new StreamWriter(fileName, false, Encoding.UTF8);
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(writer, output);
            writer.Flush();
        }
        
        // todo move to xml library
        //http://stackoverflow.com/questions/14562415/xml-deserialization-generic-method
        public static T XmlDeserialize<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new Exception($"File not found: {filePath}");

            var serializer = new XmlSerializer(typeof(T));
            using var reader = new StreamReader(filePath, Encoding.UTF8);

            try
            {
                return (T) serializer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"Error processing XML file: {ex.Message}"
                                               + "\r\nIf the error contains a (#, #), the first number is the line the error occurred on.", ex);
            }
        }

        /// <summary>
        /// Fix author name if in last, first format or if multiple authors present (returns first author)
        /// </summary>
        public static string FixAuthor(string author)
        {
            if (author == null) return null;
            if (author.Contains(';'))
                author = author.Split(';')[0];
            if (author.Contains(','))
            {
                var parts = author.Split(',');
                author = parts[1].Trim() + " " + parts[0].Trim();
            }
            return author;
        }

        public static string ExpandUnicode(string input)
        {
            var output = new StringBuilder(input.Length);
            for (var i = 0; i < input.Length; i++)
            {
                if (input[i] > 127)
                {
                    var uniBytes = Encoding.Unicode.GetBytes(input.Substring(i, 1));
                    output.AppendFormat(@"\u{0:X2}{1:X2}", uniBytes[1], uniBytes[0]);
                }
                else
                    output.Append(input[i]);
            }
            return output.ToString();
        }

        public static bool CleanUp(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                return false;

            var files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
            var dirs = Directory.GetDirectories(folderPath, "*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (var dir in dirs)
            {
                CleanUp(dir);
            }
            Thread.Sleep(1);
            Directory.Delete(folderPath, false);
            return true;
        }

        /// <summary>
        /// Process GUID. If in decimal form, convert to hex.
        /// </summary>
        public static string ConvertGuid(string guid)
        {
            if (Regex.IsMatch(guid, "/[a-zA-Z]/", RegexOptions.Compiled))
                guid = guid.ToUpper();
            else
            {
                long.TryParse(guid, out var guidDec);
                guid = guidDec.ToString("X");
            }

            if (guid == "0")
                throw new ArgumentException("An error occurred while converting the GUID.");

            return guid;
        }

        public static bool ValidateFilename(string author, string title)
        {
            var newAuthor = RemoveInvalidFileChars(author);
            var newTitle = RemoveInvalidFileChars(title);
            return author.Equals(newAuthor) && title.Equals(newTitle);
        }

        public static long UnixTimestampSeconds()
        {
            return (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }

        public static long UnixTimestampMilliseconds()
        {
            return (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
    }

    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    }
}