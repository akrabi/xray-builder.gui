using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using XRayBuilderGUI.Libraries.Enumerables.Extensions;
using XRayBuilderGUI.Libraries.Logging;
using XRayBuilderGUI.XRay.Artifacts;

namespace XRayBuilderGUI.XRay.Logic.Aliases
{
    public sealed class AliasesService : IAliasesService
    {
        // TODO: Load from file, possibly localized, or anything better than this
        #region CommonTitles
        private readonly string[] _commonTitles = { "Mr", "Mrs", "Ms", "Miss", "Dr", "Herr", "Monsieur", "Hr", "Frau",
            "A V M", "Admiraal", "Admiral", "Alderman", "Alhaji", "Ambassador", "Baron", "Barones", "Brig",
            "Brigadier", "Brother", "Canon", "Capt", "Captain", "Cardinal", "Cdr", "Chief", "Cik", "Cmdr", "Col",
            "Colonel", "Commandant", "Commander", "Commissioner", "Commodore", "Comte", "Comtessa", "Congressman",
            "Conseiller", "Consul", "Conte", "Contessa", "Corporal", "Councillor", "Count", "Countess", "Air Cdre",
            "Air Commodore", "Air Marshal", "Air Vice Marshal", "Brig Gen", "Brig General", "Brigadier General",
            "Crown Prince", "Crown Princess", "Dame", "Datin", "Dato", "Datuk", "Datuk Seri", "Deacon", "Deaconess",
            "Dean", "Dhr", "Dipl Ing", "Doctor", "Dott", "Dott Sa", "Dr", "Dr Ing", "Dra", "Drs", "Embajador",
            "Embajadora", "En", "Encik", "Eng", "Eur Ing", "Exma Sra", "Exmo Sr", "Father", "First Lieutient",
            "First Officer", "Flt Lieut", "Flying Officer", "Fr", "Frau", "Fraulein", "Fru", "Gen", "Generaal",
            "General", "Governor", "Graaf", "Gravin", "Group Captain", "Grp Capt", "H E Dr", "H H", "H M", "H R H",
            "Hajah", "Haji", "Hajim", "Her Highness", "Her Majesty", "Herr", "High Chief", "His Highness",
            "His Holiness", "His Majesty", "Hon", "Hr", "Hra", "Ing", "Ir", "Jonkheer", "Judge", "Justice",
            "Khun Ying", "Kolonel", "Lady", "Lcda", "Lic", "Lieut", "Lieut Cdr", "Lieut Col", "Lieut Gen", "Lord",
            "Madame", "Mademoiselle", "Maj Gen", "Major", "Master", "Mevrouw", "Miss", "Mlle", "Mme", "Monsieur",
            "Monsignor", "Mstr", "Nti", "Pastor", "President", "Prince", "Princess", "Princesse", "Prinses", "Prof",
            "Prof Dr", "Prof Sir", "Professor", "Puan", "Puan Sri", "Rabbi", "Rear Admiral", "Rev", "Rev Canon",
            "Rev Dr", "Rev Mother", "Reverend", "Rva", "Senator", "Sergeant", "Sheikh", "Sheikha", "Sig", "Sig Na",
            "Sig Ra", "Sir", "Sister", "Sqn Ldr", "Sr", "Sr D", "Sra", "Srta", "Sultan", "Tan Sri", "Tan Sri Dato",
            "Tengku", "Teuku", "Than Puying", "The Hon Dr", "The Hon Justice", "The Hon Miss", "The Hon Mr",
            "The Hon Mrs", "The Hon Ms", "The Hon Sir", "The Very Rev", "Toh Puan", "Tun", "Vice Admiral",
            "Viscount", "Viscountess", "Wg Cdr", "Jr", "Sr", "Sheriff", "Special Agent", "Detective", "Lt" };
        #endregion

        private readonly ILogger _logger;

        public AliasesService(ILogger logger)
        {
            _logger = logger;

            //Try to load custom common titles from BaseSplitIgnore.txt
            try
            {
                using var streamReader = new StreamReader(Environment.CurrentDirectory + @"\dist\BaseSplitIgnore.txt", Encoding.UTF8);
                var CustomSplitIgnore = streamReader.ReadToEnd().Split(new[] { "\r\n" }, StringSplitOptions.None)
                    .Where(r => !r.StartsWith("//")).ToArray();
                if (CustomSplitIgnore.Length >= 1)
                    _commonTitles = CustomSplitIgnore;
                logger.Log("Splitting aliases using custom common titles file...");
            }
            catch (Exception ex)
            {
                logger.Log("An error occurred while opening the BaseSplitIgnore.txt file.\r\n" +
                    "Ensure you extracted it to the same directory as the program.\r\n" +
                    ex.Message + "\r\nUsing built-in default terms...");
            }
        }

        public void SaveCharacters(IEnumerable<Term> terms, string aliasFile)
        {
            // todo service should handle this
            if (!Directory.Exists(Environment.CurrentDirectory + @"\ext\"))
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\ext\");

            using var streamWriter = new StreamWriter(aliasFile, false, Encoding.UTF8);

            var characters = terms.Where(term => term.Type == "character");
            try
            {
                var aliasesByTermName = GenerateAliases(characters);
                foreach (var (name, aliases) in aliasesByTermName)
                    streamWriter.WriteLine($"{name}|{string.Join(",", aliases)}");
            }
            catch (Exception ex)
            {
                _logger.Log("An error occurred while generating the aliases.\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public IEnumerable<KeyValuePair<string, string[]>> GenerateAliases(IEnumerable<Term> characters)
        {
            var sanitizePattern = new Regex($@"( ?({string.Join("|", _commonTitles)})\.? )|(^[A-Z]\. )|( [A-Z]\.)|("")|(“)|(”)|(,)|(')");
            var aliasesByTermName = new Dictionary<string, string[]>();

            foreach (var character in characters)
            {
                // Short circuit if no alias is possible or splitting is disabled
                if (!character.TermName.Contains(" ") || !Properties.Settings.Default.splitAliases)
                {
                    aliasesByTermName.Add(character.TermName, new string[0]);
                    continue;
                }

                var aliases = new List<string>();
                var textInfo = new CultureInfo("en-US", false).TextInfo;

                var sanitizedName = sanitizePattern.Replace(character.TermName, string.Empty);
                if (sanitizedName != character.TermName)
                    aliases.Add(sanitizedName);

                sanitizedName = Regex.Replace(sanitizedName, @"\s+", " ");
                sanitizedName = Regex.Replace(sanitizedName, @"( ?V?I{0,3}$)", string.Empty);
                sanitizedName = Regex.Replace(sanitizedName, @"(\(aka )", "(");

                var bracketedName = Regex.Match(sanitizedName, @"(.*)(\()(.*)(\))");
                if (bracketedName.Success)
                {
                    aliases.Add(bracketedName.Groups[3].Value);
                    aliases.Add(bracketedName.Groups[1].Value.TrimEnd());
                    sanitizedName = sanitizedName.Replace(bracketedName.Groups[2].Value, "")
                        .Replace(bracketedName.Groups[4].Value, "");
                }

                if (sanitizedName.Contains(" "))
                {
                    sanitizedName = sanitizedName.Replace(" &amp;", "").Replace(" &", "");
                    var words = sanitizedName.Split(' ');
                    string FixAllCaps(string word) => word.ToUpper() == word ? textInfo.ToTitleCase(word.ToLower()) : word;
                    aliases.AddRange(words.Select(FixAllCaps));
                }

                // Filter out any already-existing aliases from other terms
                var dedupedAliases = aliases
                    .Where(alias => !aliasesByTermName.Values.SelectMany(existingAliases => existingAliases).Contains(alias))
                    // Aliases must be sorted by length, descending, to ensure they are matched properly
                    .OrderByDescending(alias => alias.Length)
                    .ToArray();

                if (dedupedAliases.Length > 0)
                    aliasesByTermName.Add(character.TermName, dedupedAliases);
            }

            return aliasesByTermName;
        }
    }
}