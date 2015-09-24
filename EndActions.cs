﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

using HtmlAgilityPack;

namespace XRayBuilderGUI
{
    class EndActions
    {
        private Properties.Settings settings = Properties.Settings.Default;
        private frmMain main;

        private string EaPath = "";
        private string SaPath = "";
        private long _erl = 0;

        public List<BookInfo> custAlsoBought = new List<BookInfo>();

        private AuthorProfile authorProfile = null;
        private BookInfo curBook = null;

        public bool complete = false; //Set if constructor succeeds in gathering data
        
        //Requires an already-built AuthorProfile and the BaseEndActions.txt file
        public EndActions(AuthorProfile ap, BookInfo book, long erl, frmMain frm)
        {
            authorProfile = ap;
            curBook = book;
            _erl = erl;
            main = frm;

            main.Log("Attempting to find book on Amazon...");
            //Generate Book search URL from book's ASIN
            string ebookLocation = @"http://www.amazon.com/dp/" + book.asin;

            // Search Amazon for book
            main.Log("Book found on Amazon!");
            main.Log(string.Format("Book's Amazon page URL: {0}", ebookLocation));
            
            HtmlDocument bookHtmlDoc = new HtmlDocument {OptionAutoCloseOnEnd = true};
            try
            {
                bookHtmlDoc.LoadHtml(HttpDownloader.GetPageHtml(ebookLocation));
            }
            catch (Exception ex)
            {
                main.Log(string.Format("An error ocurred while downloading book's Amazon page: {0}\r\nYour ASIN may not be correct.", ex.Message));
                return;
            }
            if (Properties.Settings.Default.saveHtml)
            {
                try
                {
                    File.WriteAllText(Environment.CurrentDirectory +
                                      string.Format(@"\dmp\{0}.bookHtml.txt", book.asin),
                        ap.authorHtmlDoc.DocumentNode.InnerHtml);
                }
                catch (Exception ex)
                {
                    main.Log(string.Format("An error ocurred saving bookHtml.txt: {0}", ex.Message));
                    return;
                }
            }

            try
            {
                curBook.GetAmazonInfo(bookHtmlDoc);
            }
            catch (Exception ex)
            {
                main.Log(string.Format("An error ocurred parsing Amazon info: {0}", ex.Message));
                return;
            }

            main.Log("Gathering recommended book info...");
            //Parse Recommended Author titles and ASINs
            try
            {
                HtmlNodeCollection recList = bookHtmlDoc.DocumentNode.SelectNodes("//li[@class='a-carousel-card a-float-left']");
                if (recList == null)
                    main.Log("Could not find related book list page on Amazon.\r\nUnable to create End Actions.");
                if (recList != null)
                    foreach (HtmlNode item in recList.Where(item => item != null))
                    {
                        HtmlNode nodeTitle = item.SelectSingleNode(".//div/a");
                        string nodeTitleCheck = nodeTitle.GetAttributeValue("title", "");
                        string nodeUrl = nodeTitle.GetAttributeValue("href", "");
                        string cleanAuthor = "";
                        if (nodeUrl != "")
                            nodeUrl = "http://www.amazon.com" + nodeUrl;
                        if (nodeTitleCheck == "")
                        {
                            nodeTitle = item.SelectSingleNode(".//div/a");
                            //Remove CR, LF and TAB
                            nodeTitleCheck = Functions.CleanString(nodeTitle.InnerText);
                        }
                        cleanAuthor = Functions.CleanString(item.SelectSingleNode(".//div/div").InnerText);
                        BookInfo newBook = new BookInfo(nodeTitleCheck, cleanAuthor,
                            item.SelectSingleNode(".//div").GetAttributeValue("data-asin", ""));
                        try
                        {
                            //Gather book desc, image url, etc, if using new format
                            if (settings.useNewVersion)
                                newBook.GetAmazonInfo(nodeUrl);
                            custAlsoBought.Add(newBook);
                        }
                        catch (Exception ex)
                        {
                            main.Log(string.Format("{0}\r\n{1}\r\nContinuing anyway...", ex.Message, nodeUrl));
                        }
                    }
            }
            catch (Exception ex)
            {
                main.Log("An error occurred parsing the book's amazon page: " + ex.Message);
                return;
            }

            SetPaths();
            complete = true;
        }

        public void GenerateOld()
        {
            //Create final EndActions.data.ASIN.asc
            string dt = DateTime.Now.ToString("s");
            string tz = DateTime.Now.ToString("zzz");
            XmlTextWriter writer = new XmlTextWriter(EaPath, Encoding.UTF8);
            try
            {
                main.Log("Writing End Actions to file...");
                writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"");
                writer.WriteStartElement("endaction");
                writer.WriteAttributeString("version", "0");
                writer.WriteAttributeString("guid", curBook.databasename + ":" + curBook.guid);
                writer.WriteAttributeString("key", curBook.asin);
                writer.WriteAttributeString("type", "EBOK");
                writer.WriteAttributeString("timestamp", dt + tz);
                writer.WriteElementString("treatment", "d");
                writer.WriteStartElement("currentBook");
                writer.WriteElementString("imageUrl", curBook.bookImageUrl);
                writer.WriteElementString("asin", curBook.asin);
                writer.WriteElementString("hasSample", "false");
                writer.WriteEndElement();
                writer.WriteStartElement("customerProfile");
                writer.WriteElementString("penName", settings.penName);
                writer.WriteElementString("realName", settings.realName);
                writer.WriteEndElement();
                writer.WriteStartElement("recs");
                writer.WriteAttributeString("type", "author");
                for (int i = 0; i < Math.Min(authorProfile.otherBooks.Count, 5); i++)
                {
                    writer.WriteStartElement("rec");
                    writer.WriteAttributeString("hasSample", "false");
                    writer.WriteAttributeString("asin", authorProfile.otherBooks[i].asin);
                    writer.WriteElementString("title", authorProfile.otherBooks[i].title);
                    writer.WriteElementString("author", curBook.author);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteStartElement("recs");
                writer.WriteAttributeString("type", "purchase");
                for (int i = 0; i < Math.Min(custAlsoBought.Count, 5); i++)
                {
                    writer.WriteStartElement("rec");
                    writer.WriteAttributeString("hasSample", "false");
                    writer.WriteAttributeString("asin", custAlsoBought[i].asin);
                    writer.WriteElementString("title", custAlsoBought[i].title);
                    writer.WriteElementString("author", custAlsoBought[i].author);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteElementString("booksMentionedPosition", "2");
                writer.WriteEndElement();
                writer.Flush();
                writer.Close();
                main.Log("EndActions file created successfully!\r\nSaved to " + EaPath);
                main.cmsPreview.Items[1].Enabled = true;
            }
            catch (Exception ex)
            {
                main.Log("An error occurred while writing the End Action file: " + ex.Message);
                return;
            }
        }

        public void GenerateNew()
        {
            string[] templates = GetBaseEndActions();
            if (templates == null) return;

            string bookInfoTemplate = templates[0];
            string widgetsTemplate = templates[1];
            string layoutsTemplate = templates[2];
            string finalOutput = "{{{0},{1},{2},{3}}}"; //bookInfo, widgets, layouts, data
            
            // Build bookInfo object
            // "bookInfo":{"class":"bookInfo","asin":"{0}","contentType":"EBOK","timestamp":{1},"refTagSuffix":"AAAgAAB","imageUrl":"{2}","embeddedID":"{3}:{4}","erl":{5}}
            TimeSpan timestamp = DateTime.Now - new DateTime(1970, 1, 1);
            bookInfoTemplate = string.Format(bookInfoTemplate, curBook.asin, Math.Round(timestamp.TotalMilliseconds), curBook.bookImageUrl, curBook.databasename, curBook.guid, _erl);

            // Build data object
            //string dataTemplate = @"""data"":{{""nextBook"":{0},{1},{2},""currentBookFeatured"":{3},{4},{5},{6},{7}}}";
            string dataTemplate = @"""data"":{{""nextBook"":{0},{1},{2},{3},{4},{5},{6}}}";
            string nextBook = "{}";
            string publicSharedRating = @"""publicSharedRating"":{""class"":""publicSharedRating"",""timestamp"":1430656509000,""value"":4}";
            string customerProfile = string.Format(@"""customerProfile"":{{""class"":""customerProfile"",""penName"":""{0}"",""realName"":""{1}""}}",
                settings.penName, settings.realName);
            string rating = @"""rating"":{""class"":""personalizationRating"",""timestamp"":1430656509000,""value"":4}";
            string authors = string.Format(@"""authorBios"":{{""class"":""authorBioList"",""authors"":[{0}]}}", authorProfile.ToJSON());
            string authorRecs = @"""authorRecs"":{{""class"":""featuredRecommendationList"",""recommendations"":[{0}]}}";
            string custRecs = @"""customersWhoBoughtRecs"":{{""class"":""featuredRecommendationList"",""recommendations"":[{0}]}}";
            BookInfo nextInSeries = null;
            try
            {
                nextInSeries = GetNextInSeries();
                if (nextInSeries != null)
                    nextBook = nextInSeries.ToJSON("recommendation", false);
            }
            catch (Exception ex)
            {
                main.Log("Error finding next book in series: " + ex.Message);
            }
            authorRecs = string.Format(authorRecs, String.Join(",", authorProfile.otherBooks.Select(bk => bk.ToJSON("featuredRecommendation", true)).ToArray()));
            custRecs = string.Format(custRecs, String.Join(",", custAlsoBought.Select(bk => bk.ToJSON("featuredRecommendation", true)).ToArray()));

            dataTemplate = string.Format(dataTemplate, nextBook, publicSharedRating, customerProfile,
                rating, authors, authorRecs, custRecs);

            finalOutput = string.Format(finalOutput, bookInfoTemplate, widgetsTemplate, layoutsTemplate, dataTemplate);

            using (StreamWriter streamWriter = new StreamWriter(EaPath, false))//, Encoding.UTF8))
            {
                streamWriter.Write(finalOutput);
                streamWriter.Flush();
            }
            main.Log("EndActions file created successfully!\r\nSaved to " + EaPath);
            main.cmsPreview.Items[1].Enabled = true;
        }

        public void GenerateStartAction()
        {
            string[] templates = GetBaseStartActions();
            if (templates == null) return;

            string bookInfoTemplate = templates[0];
            string widgetsTemplate = templates[1];
            string layoutsTemplate = templates[2];
            string welcomeTextTemplate = templates[3];

            string finalOutput = "{{{0},{1},{2},{3}}}"; //bookInfo, widgets, layouts, data

            // Build bookInfo object
            TimeSpan timestamp = DateTime.Now - new DateTime(1970, 1, 1);
            bookInfoTemplate = string.Format(bookInfoTemplate, curBook.asin, Math.Round(timestamp.TotalMilliseconds), curBook.bookImageUrl);
            string seriesPosition =
                string.Format(
                    @"""seriesPosition"":{{""class"":""seriesPosition"",""positionInSeries"":{0},""totalInSeries"":{1},""seriesName"":""{2}""}}",
                    curBook.seriesPosition, curBook.totalInSeries, curBook.seriesName);
            string popularHighlights = string.Format(@"""popularHighlightsText"":{{""class"":""dynamicText"",""localizedText"":{{""de"":""{0} Passagen wurden {1} mal markiert"",""en-US"":""{0} passages have been highlighted {1} times"",""ru"":""1\u00A0095 \u043E\u0442\u0440\u044B\u0432\u043A\u043E\u0432 \u0431\u044B\u043B\u043E \u0432\u044B\u0434\u0435\u043B\u0435\u043D\u043E 12\u00A0326 \u0440\u0430\u0437"",""pt-BR"":""{0} trechos foram destacados {1} vezes"",""ja"":""{0}\u7B87\u6240\u304C{1}\u56DE\u30CF\u30A4\u30E9\u30A4\u30C8\u3055\u308C\u307E\u3057\u305F"",""en"":""{0} passages have been highlighted {1} times"",""it"":""{0} brani sono stati evidenziati {1} volte"",""fr"":""{0}\u00A0095 passages ont \u00E9t\u00E9 surlign\u00E9s {1}\u00A0326 fois"",""zh-CN"":""{0} \u4E2A\u6BB5\u843D\u88AB\u6807\u6CE8\u4E86 {1} \u6B21"",""es"":""Se han subrayado {0} pasajes {1} veces"",""nl"":""{0} fragmenten zijn {1} keer gemarkeerd""}}}}", curBook.popularPassages, curBook.popularHighlights);
            string grokShelfInfo = string.Format(@"""grokShelfInfo"":{{""class"":""goodReadsShelfInfo"",""asin"":""{0}"",""shelves"":[""read""]}}", curBook.asin);
            string currentBook = curBook.ToExtraJSON("featuredRecommendation");
            string authors = string.Format(@"""authorBios"":{{""class"":""authorBioList"",""authors"":[{0}]}}", authorProfile.ToJSON());
            string authorRecs = @"""authorRecs"":{{""class"":""recommendationList"",""recommendations"":[{0}]}}";
            authorRecs = string.Format(authorRecs, String.Join(",", authorProfile.otherBooks.Select(bk => bk.ToJSON("recommendation", false)).ToArray()));
            string readingTime =
                string.Format(
                    @"""readingTime"":{{""class"":""time"",""hours"":{0},""minutes"":{1},""formattedTime"":{{""de"":""{0} Stunden und {1} Minuten"",""en-US"":""{0} hours and {1} minutes"",""ru"":""{0}\u00A0\u0447 \u043{0} {1}\u00A0\u043C\u043{0}\u043D"",""pt-BR"":""{0} horas e {1} minutos"",""ja"":""{0}\u6642\u9593{1}\u5206"",""en"":""{0} hours and {1} minutes"",""it"":""{0} ore e {1} minuti"",""fr"":""{0} heures et {1} minutes"",""zh-CN"":""{0} \u5C0F\u65F6 {1} \u5206\u949F"",""es"":""{0} horas y {1} minutos"",""nl"":""{0} uur en {1} minuten""}}}}",
                    curBook.readingHours, curBook.readingMinutes);
            string readingPages = string.Format(@"""readingPages"":{{""class"":""pages"",""pagesInBook"":{0}}}",
                curBook.pagesInBook);

            // Build data object
            string dataTemplate = @"""data"":{{{0},{1},{2},{3},""bookDescription"":{4},{5},{6},""currentBook"":{7},{8},{9}}}";
            dataTemplate = string.Format(dataTemplate, seriesPosition, welcomeTextTemplate, popularHighlights,
            grokShelfInfo, currentBook, authors, authorRecs, currentBook, readingTime, readingPages);

            finalOutput = string.Format(finalOutput, bookInfoTemplate, widgetsTemplate, layoutsTemplate, dataTemplate);

            using (StreamWriter streamWriter = new StreamWriter(SaPath, false))//, Encoding.UTF8))
            {
                streamWriter.Write(finalOutput);
                streamWriter.Flush();
            }
            main.Log("StartActions file created successfully!\r\nSaved to " + SaPath);
            main.cmsPreview.Items[3].Enabled = true;
        }

        private void SetPaths()
        {
            string outputDir;
            try
            {
                if (settings.android)
                {
                    outputDir = settings.outDir + @"\Android\" + curBook.asin;
                    Directory.CreateDirectory(outputDir);
                }
                else
                    outputDir = settings.useSubDirectories ? Functions.GetBookOutputDirectory(curBook.author, curBook.sidecarName) : settings.outDir;
            }
            catch (Exception ex)
            {
                main.Log("Failed to create output directory: " + ex.Message + "\r\nFiles will be placed in the default output directory.");
                outputDir = settings.outDir;
            }
            EaPath = outputDir + @"\EndActions.data." + curBook.asin + ".asc";
            SaPath = outputDir + @"\StartActions.data." + curBook.asin + ".asc";

            if (!Properties.Settings.Default.overwrite && File.Exists(EaPath))
            {
                main.Log("EndActions file already exists... Skipping!\r\n" +
                         "Please review the settings page if you want to overwite any existing files.");
                return;
            }
        }

        // Retrieve EndActions templates from BaseEndActions.txt.
        // Array will always be length 3. Index 0 will always be the bookInfo template.
        private string[] GetBaseEndActions()
        {
            string[] templates = null;
            try
            {
                using (StreamReader streamReader = new StreamReader("BaseEndActions.txt", Encoding.UTF8))
                {
                    templates = streamReader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    templates = templates.Where(r => !r.StartsWith("//")).ToArray(); //Remove commented lines
                    if (templates == null || templates.Length != 3 || !templates[0].StartsWith(@"""bookInfo"""))
                    {
                        main.Log("Error parsing BaseEndActions.txt. If you modified it, ensure you followed the specified format.");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                main.Log("An error occurred while opening the BaseEndActions.txt file.\r\n" +
                    "Ensure you extracted it to the same directory as the program.\r\n" +
                    ex.Message);
            }
            return templates;
        }

        // Retrieve StartActions templates from BaseStartActions.txt.
        // Array will always be length 4. Index 0 will always be the bookInfo template.
        private string[] GetBaseStartActions()
        {
            string[] templates = null;
            try
            {
                using (StreamReader streamReader = new StreamReader("BaseStartActions.txt", Encoding.UTF8))
                {
                    templates = streamReader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    templates = templates.Where(r => !r.StartsWith("//")).ToArray(); //Remove commented lines
                    if (templates == null || templates.Length != 4 || !templates[0].StartsWith(@"""bookInfo"""))
                    {
                        main.Log("Error parsing BaseStartActions.txt. If you modified it, ensure you followed the specified format.");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                main.Log("An error occurred while opening the BaseStartActions.txt file.\r\n" +
                    "Ensure you extracted it to the same directory as the program.\r\n" +
                    ex.Message);
            }
            return templates;
        }

        private BookInfo GetNextInSeries()
        {
            BookInfo nextBook = null;

            if (curBook.shelfariUrl == "") return null;

            // Get title of next book
            HtmlAgilityPack.HtmlDocument searchHtmlDoc = new HtmlAgilityPack.HtmlDocument();
            searchHtmlDoc.LoadHtml(HttpDownloader.GetPageHtml(curBook.shelfariUrl));
            string nextTitle = GetNextInSeriesTitle(searchHtmlDoc);
            // If search failed, try other method
            if (nextTitle == "")
                nextTitle = GetNextInSeriesTitle2(searchHtmlDoc);
            if (nextTitle != "")
            {
                // Search author's other books for the book (assumes next in series was written by the same author...)
                // Returns the first one found, though there should probably not be more than 1 of the same name anyway
                nextBook = authorProfile.otherBooks.FirstOrDefault(bk => bk.title == nextTitle);
                if (nextBook == null)
                {
                    // Attempt to search Amazon for the book instead
                    nextBook = Functions.AmazonSearchBook(nextTitle, curBook.author);
                    if (nextBook != null)
                        nextBook.GetAmazonInfo(nextBook.amazonUrl); //fill in desc, imageurl, and ratings
                }
                if (nextBook == null)
                    main.Log("Book was found to be part of a series, but next book could not be found.\r\n" +
                        "Please report this book and the Shelfari URL and output log to improve parsing.");

            } else
                main.Log("Unable to find next book in series, the book may not be part of one.");

            return nextBook;
        }

        // TODO: Un-yuckify all the return paths without nesting a ton of ifs
        // Search Shelfari page for possible series info, returning the next title in the series without downloading any other pages.
        private string GetNextInSeriesTitle(HtmlAgilityPack.HtmlDocument searchHtmlDoc)
        {
            //Added estimated reading time and page count from Shelfari, for now...
            HtmlAgilityPack.HtmlNode pageNode = searchHtmlDoc.DocumentNode.SelectSingleNode("//div[@id='WikiModule_FirstEdition']");
            if (pageNode == null)
                return "";
            HtmlAgilityPack.HtmlNode node1 = pageNode.SelectSingleNode(".//div/div");
            if (node1 == null)
                return "";
            //Parse page count and multiply by average reading time
            Match match1 = Regex.Match(node1.InnerText, @"Page Count: (\d+)");
            if (match1.Success)
            {
                double minutes = int.Parse(match1.Groups[1].Value) * 1.2890625;
                TimeSpan span = TimeSpan.FromMinutes(minutes);
                main.Log(string.Format("Typical time to read: {0} hours and {1} minutes ({2} pages)"
                    , span.Hours, span.Minutes, match1.Groups[1].Value));
                curBook.pagesInBook = match1.Groups[1].Value;
                curBook.readingHours = span.Hours.ToString();
                curBook.readingMinutes = span.Minutes.ToString();
            }

            //Added highlighted passage from Shelfari, dummy info for now...
            HtmlAgilityPack.HtmlNode members = searchHtmlDoc.DocumentNode.SelectSingleNode("//ul[@class='tabs_n tn1']");
            int highlights = 0;
            if (members != null)
            {
                Match match3 = Regex.Match(members.InnerText, @"Reviews \((\d+)\)");
                if (match3.Success)
                    curBook.popularPassages = match3.Groups[1].Value.ToString();
                match3 = Regex.Match(members.InnerText, @"Readers \((\d+)\)");
                if (match3.Success)
                {
                    curBook.popularHighlights = match3.Groups[1].Value.ToString();
                    highlights = int.Parse(match3.Groups[1].Value);
                }
                main.Log(string.Format("Popular Highlights: {0} passages have been highlighted {1} times"
                            , curBook.popularPassages, curBook.popularHighlights));
            }

            //If no "highlighted passages" found from Shelfari, add to log
            if (highlights == 0)
            {
                main.Log("Popular Highlights: No highlighted passages have been found for this book");
                curBook.popularPassages = "";
                curBook.popularHighlights = "";
            }

            HtmlAgilityPack.HtmlNode seriesNode = searchHtmlDoc.DocumentNode.SelectSingleNode("//span[@class='series']/a[@href]");
            if (seriesNode == null)
                return "";
            seriesNode.GetAttributeValue("href", "");
            curBook.seriesName = seriesNode.FirstChild.InnerText.Trim();
            HtmlAgilityPack.HtmlNode wikiNode = searchHtmlDoc.DocumentNode.SelectSingleNode("//div[@id='WikiModule_Series']");
            if (wikiNode == null)
                return "";
            HtmlAgilityPack.HtmlNode node3 = wikiNode.SelectSingleNode(".//div/div");
            if (node3 == null || !node3.InnerText.Contains("(standard series)"))
                return "";
            main.Log("About the series: " + node3.InnerText.Replace(" (standard series)", "").Replace(".", ""));
            Match match = Regex.Match(node3.InnerText, @"This is book (\d+) of (\d+)");
            if (!match.Success || match.Groups.Count != 3)
                return "";
            // Check if book is the last in the series
            if (!match.Groups[1].Value.Equals(match.Groups[2].Value))
            {
                curBook.seriesPosition = match.Groups[1].Value;
                curBook.totalInSeries = match.Groups[2].Value;

                node3 = wikiNode.SelectSingleNode(".//div/p");
                //Parse preceding book
                if (node3 != null && node3.InnerText.Contains("Preceded by ", StringComparison.OrdinalIgnoreCase))
                {
                    match = Regex.Match(node3.InnerText, @"Preceded by (.*),", RegexOptions.IgnoreCase);
                    if (match.Success && match.Groups.Count == 2)
                    {
                        curBook.previousBook = match.Groups[1].Value;
                        main.Log("Preceded by: " + curBook.previousBook);
                    }
                }
                //Parse following book
                if (node3 != null && node3.InnerText.Contains("followed by ", StringComparison.OrdinalIgnoreCase))
                {
                    match = Regex.Match(node3.InnerText, @"followed by (.*)\.", RegexOptions.IgnoreCase);
                    if (match.Success && match.Groups.Count == 2)
                    {
                        main.Log("Followed by: " + match.Groups[1].Value);
                        return match.Groups[1].Value;
                    }
                }
            }
            return "";
        }

        // Search Shelfari for series info, scrape series page, and return next title in series.
        private string GetNextInSeriesTitle2(HtmlAgilityPack.HtmlDocument searchHtmlDoc)
        {
            bool hasSeries = false;
            string series = "";
            string seriesShort = "";
            string seriesURL = "";
            int currentSeriesIndex = 0;
            int currentSeriesCount = 0;
            string nextTitle = "";
            //Check if book's Shelfari page contains series info
            HtmlAgilityPack.HtmlNode node = searchHtmlDoc.DocumentNode.SelectSingleNode("//span[@class='series']");
            if (node != null)
            {
                //Series name and book number
                series = node.InnerText.Trim();
                //Convert book number string to integer
                Int32.TryParse(series.Substring(series.LastIndexOf(" ") + 1), out currentSeriesIndex);
                //Parse series Shelfari URL
                seriesURL = node.SelectSingleNode("//span[@class='series']/a[@href]")
                    .GetAttributeValue("href", "");
                seriesShort = node.FirstChild.InnerText.Trim();
                //Add series name and book number to log, if found
                searchHtmlDoc.LoadHtml(HttpDownloader.GetPageHtml(string.Format(seriesURL)));
                //Parse number of books in series and convert to integer
                node = searchHtmlDoc.DocumentNode.SelectSingleNode("//h2[@class='f_m']");
                string test = node.FirstChild.InnerText.Trim();
                Match match = Regex.Match(test, @"\d+");
                if (match.Success)
                    Int32.TryParse(match.Value, out currentSeriesCount);
                hasSeries = true;
                //Check if there is a next book
                if (currentSeriesIndex < currentSeriesCount)
                {
                    //Add series name and book number to log, if found
                    main.Log(string.Format("This is book {0} of {1} in the {2} Series...",
                        currentSeriesIndex, currentSeriesCount, seriesShort));
                    foreach (HtmlAgilityPack.HtmlNode seriesItem in
                        searchHtmlDoc.DocumentNode.SelectNodes(".//ol/li"))
                    {
                        node = seriesItem.SelectSingleNode(".//div/span[@class='series bold']");
                        if (node != null)
                            if (node.InnerText.Contains((currentSeriesIndex + 1).ToString()))
                            {
                                node = seriesItem.SelectSingleNode(".//h3/a");
                                //Parse title of the next book
                                nextTitle = node.InnerText.Trim();
                                //Add next book in series to log, if found
                                main.Log($"The next book in this series is: {nextTitle}!");
                                //main.Log(string.Format("The next book in this series is {0}!", nextTitle));
                                return nextTitle;
                            }
                    }
                }
                if (hasSeries)
                    main.Log(string.Format("Unable to find the next book in this series!\r\n" +
                                      "This is the last title in the {0} series...", seriesShort));
                return "";
            }
            return "";
        }
    }
}