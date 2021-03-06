﻿// Based on KFX handling from jhowell's KFX in/output plugins (https://www.mobileread.com/forums/showthread.php?t=272407)

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using IonDotnet;
using IonDotnet.Internals;
using IonDotnet.Tree;
using XRayBuilderGUI.Libraries.Enumerables.Extensions;
using XRayBuilderGUI.Libraries.IO.Extensions;

namespace XRayBuilderGUI.Unpack.KFX
{
    public class YjContainer : IMetadata
    {
        private static HashSet<string> YjSymbols { get; } = new HashSet<string>
        {
            "$10",
            "$11",
            "$12",
            "$13",
            "$14?",
            "$15",
            "$16",
            "$17?",
            "$18?",
            "$19",
            "$20?",
            "$21",
            "$22?",
            "$23",
            "$24",
            "$25?",
            "$26?",
            "$27",
            "$28?",
            "$29?",
            "$30?",
            "$31",
            "$32",
            "$33",
            "$34",
            "$35?",
            "$36",
            "$37?",
            "$38?",
            "$39?",
            "$40?",
            "$41",
            "$42",
            "$43?",
            "$44",
            "$45",
            "$46?",
            "$47",
            "$48",
            "$49",
            "$50",
            "$51",
            "$52",
            "$53",
            "$54",
            "$55",
            "$56",
            "$57",
            "$58",
            "$59",
            "$60",
            "$61",
            "$62",
            "$63",
            "$64?",
            "$65",
            "$66",
            "$67",
            "$68",
            "$69",
            "$70",
            "$71?",
            "$72",
            "$73?",
            "$74?",
            "$75",
            "$76",
            "$77",
            "$78?",
            "$79?",
            "$80?",
            "$81?",
            "$82?",
            "$83",
            "$84",
            "$85",
            "$86",
            "$87",
            "$88",
            "$89",
            "$90",
            "$91",
            "$92",
            "$93",
            "$94",
            "$95",
            "$96",
            "$97",
            "$98",
            "$99?",
            "$100",
            "$101?",
            "$102",
            "$103?",
            "$104",
            "$105",
            "$106",
            "$107",
            "$108",
            "$109?",
            "$110?",
            "$111?",
            "$112",
            "$113?",
            "$114?",
            "$115?",
            "$116?",
            "$117?",
            "$118",
            "$119?",
            "$120?",
            "$121?",
            "$122?",
            "$123?",
            "$124?",
            "$125",
            "$126",
            "$127",
            "$128?",
            "$129?",
            "$130?",
            "$131?",
            "$132?",
            "$133",
            "$134?",
            "$135?",
            "$136?",
            "$137?",
            "$138?",
            "$139?",
            "$140",
            "$141",
            "$142",
            "$143",
            "$144",
            "$145",
            "$146",
            "$147?",
            "$148",
            "$149",
            "$150",
            "$151",
            "$152",
            "$153",
            "$154",
            "$155",
            "$156",
            "$157",
            "$158?",
            "$159",
            "$160?",
            "$161",
            "$162",
            "$163",
            "$164",
            "$165",
            "$166",
            "$167",
            "$168?",
            "$169",
            "$170",
            "$171",
            "$172?",
            "$173",
            "$174",
            "$175",
            "$176",
            "$177?",
            "$178",
            "$179",
            "$180",
            "$181",
            "$182",
            "$183",
            "$184",
            "$185",
            "$186",
            "$187?",
            "$188?",
            "$189?",
            "$190?",
            "$191?",
            "$192",
            "$193?",
            "$194?",
            "$195?",
            "$196?",
            "$197?",
            "$198?",
            "$199",
            "$200",
            "$201",
            "$202",
            "$203",
            "$204?",
            "$205",
            "$206",
            "$207",
            "$208",
            "$209",
            "$210",
            "$211",
            "$212",
            "$213",
            "$214",
            "$215",
            "$216",
            "$217",
            "$218",
            "$219",
            "$220",
            "$221?",
            "$222",
            "$223?",
            "$224",
            "$225?",
            "$226?",
            "$227?",
            "$228?",
            "$229?",
            "$230",
            "$231",
            "$232",
            "$233",
            "$234?",
            "$235",
            "$236",
            "$237",
            "$238",
            "$239",
            "$240",
            "$241",
            "$242?",
            "$243?",
            "$244",
            "$245",
            "$246",
            "$247",
            "$248",
            "$249",
            "$250",
            "$251",
            "$252",
            "$253",
            "$254",
            "$255",
            "$256?",
            "$257?",
            "$258",
            "$259",
            "$260",
            "$261?",
            "$262",
            "$263?",
            "$264",
            "$265",
            "$266",
            "$267",
            "$268?",
            "$269",
            "$270",
            "$271",
            "$272",
            "$273",
            "$274",
            "$275?",
            "$276",
            "$277",
            "$278",
            "$279",
            "$280?",
            "$281",
            "$282?",
            "$283",
            "$284",
            "$285",
            "$286",
            "$287",
            "$288?",
            "$289?",
            "$290?",
            "$291?",
            "$292?",
            "$293?",
            "$294",
            "$295?",
            "$296",
            "$297?",
            "$298",
            "$299",
            "$300?",
            "$301?",
            "$302?",
            "$303?",
            "$304",
            "$305",
            "$306",
            "$307",
            "$308",
            "$309?",
            "$310",
            "$311",
            "$312",
            "$313?",
            "$314",
            "$315?",
            "$316?",
            "$317?",
            "$318",
            "$319",
            "$320",
            "$321",
            "$322",
            "$323",
            "$324",
            "$325",
            "$326",
            "$327?",
            "$328",
            "$329",
            "$330",
            "$331",
            "$332?",
            "$333?",
            "$334",
            "$335",
            "$336",
            "$337",
            "$338?",
            "$339?",
            "$340",
            "$341",
            "$342",
            "$343",
            "$344",
            "$345",
            "$346",
            "$347",
            "$348",
            "$349",
            "$350",
            "$351",
            "$352",
            "$353?",
            "$354?",
            "$355",
            "$356",
            "$357",
            "$358?",
            "$359",
            "$360",
            "$361",
            "$362",
            "$363",
            "$364?",
            "$365?",
            "$366?",
            "$367?",
            "$368?",
            "$369",
            "$370",
            "$371",
            "$372",
            "$373",
            "$374",
            "$375",
            "$376",
            "$377",
            "$378",
            "$379?",
            "$380?",
            "$381",
            "$382",
            "$383",
            "$384",
            "$385",
            "$386",
            "$387",
            "$388?",
            "$389",
            "$390",
            "$391",
            "$392",
            "$393",
            "$394",
            "$395",
            "$396",
            "$397?",
            "$398?",
            "$399?",
            "$400?",
            "$401?",
            "$402?",
            "$403",
            "$404?",
            "$405?",
            "$406?",
            "$407?",
            "$408?",
            "$409",
            "$410",
            "$411",
            "$412",
            "$413",
            "$414",
            "$415",
            "$416",
            "$417",
            "$418",
            "$419",
            "$420?",
            "$421",
            "$422",
            "$423",
            "$424",
            "$425?",
            "$426",
            "$427",
            "$428",
            "$429",
            "$430?",
            "$431?",
            "$432?",
            "$433",
            "$434",
            "$435?",
            "$436",
            "$437",
            "$438",
            "$439",
            "$440?",
            "$441",
            "$442",
            "$443?",
            "$444?",
            "$445?",
            "$446?",
            "$447",
            "$448?",
            "$449",
            "$450?",
            "$451?",
            "$452?",
            "$453",
            "$454",
            "$455",
            "$456",
            "$457",
            "$458?",
            "$459",
            "$460",
            "$461",
            "$462",
            "$463?",
            "$464",
            "$465",
            "$466",
            "$467?",
            "$468",
            "$469?",
            "$470?",
            "$471?",
            "$472",
            "$473?",
            "$474",
            "$475",
            "$476",
            "$477",
            "$478",
            "$479",
            "$480",
            "$481",
            "$482",
            "$483",
            "$484",
            "$485",
            "$486",
            "$487",
            "$488",
            "$489",
            "$490",
            "$491",
            "$492",
            "$493?",
            "$494?",
            "$495",
            "$496",
            "$497",
            "$498",
            "$499",
            "$500",
            "$501",
            "$502",
            "$503",
            "$504?",
            "$505",
            "$506?",
            "$507?",
            "$508?",
            "$509",
            "$510?",
            "$511?",
            "$512?",
            "$513?",
            "$514?",
            "$515?",
            "$516?",
            "$517?",
            "$518?",
            "$519?",
            "$520?",
            "$521?",
            "$522?",
            "$523?",
            "$524?",
            "$525",
            "$526",
            "$527?",
            "$528",
            "$529?",
            "$530?",
            "$531?",
            "$532?",
            "$533?",
            "$534?",
            "$535?",
            "$536?",
            "$537?",
            "$538",
            "$539?",
            "$540?",
            "$541?",
            "$542?",
            "$543?",
            "$544?",
            "$545?",
            "$546",
            "$547",
            "$548",
            "$549?",
            "$550",
            "$551",
            "$552",
            "$553",
            "$554",
            "$555?",
            "$556?",
            "$557",
            "$558?",
            "$559",
            "$560",
            "$561?",
            "$562?",
            "$563?",
            "$564",
            "$565",
            "$566?",
            "$567?",
            "$568?",
            "$569",
            "$570",
            "$571?",
            "$572?",
            "$573",
            "$574?",
            "$575?",
            "$576",
            "$577",
            "$578?",
            "$579?",
            "$580",
            "$581",
            "$582?",
            "$583",
            "$584",
            "$585",
            "$586",
            "$587",
            "$588",
            "$589",
            "$590",
            "$591",
            "$592",
            "$593",
            "$594",
            "$595",
            "$596",
            "$597",
            "$598",
            "$599?",
            "$600?",
            "$601",
            "$602",
            "$603?",
            "$604",
            "$605",
            "$606",
            "$607?",
            "$608",
            "$609",
            "$610",
            "$611",
            "$612?",
            "$613",
            "$614",
            "$615",
            "$616",
            "$617",
            "$618",
            "$619",
            "$620?",
            "$621",
            "$622",
            "$623",
            "$624?",
            "$625",
            "$626?",
            "$627?",
            "$628",
            "$629",
            "$630",
            "$631?",
            "$632",
            "$633",
            "$634?",
            "$635",
            "$636",
            "$637",
            "$638",
            "$639",
            "$640",
            "$641",
            "$642",
            "$643",
            "$644",
            "$645",
            "$646",
            "$647",
            "$648",
            "$649",
            "$650",
            "$651?",
            "$652",
            "$653?",
            "$654?",
            "$655",
            "$656",
            "$657",
            "$658",
            "$659",
            "$660",
            "$661?",
            "$662?",
            "$663?",
            "$664?",
            "$665",
            "$666",
            "$667?",
            "$668",
            "$669?",
            "$670?",
            "$671",
            "$672",
            "$673",
            "$674",
            "$675",
            "$676",
            "$677",
            "$678",
            "$679",
            "$680?",
            "$681?",
            "$682",
            "$683",
            "$684",
            "$685?",
            "$686",
            "$687",
            "$688",
            "$689",
            "$690",
            "$691?",
            "$692",
            "$693",
            "$694?",
            "$695?",
            "$696",
            "$697",
            "$698",
            "$699?",
            "$700",
            "$701",
            "$702",
            "$703",
            "$704",
            "$705",
            "$706?",
            "$707?",
            "$708?",
            "$709?",
            "$710?",
            "$711?",
            "$712?",
            "$713?",
            "$714?",
            "$715?",
            "$716?",
            "$717?",
            "$718?",
            "$719?",
            "$720?",
            "$721?",
            "$722?",
            "$723?",
            "$724?",
            "$725?",
            "$726?",
            "$727?",
            "$728?",
            "$729?",
            "$730?",
            "$731?",
            "$732?",
            "$733?",
            "$734?",
            "$735?",
            "$736?",
            "$737?",
            "$738?",
            "$739?",
            "$740?",
            "$741?",
            "$742?",
            "$743?",
            "$744?",
            "$745?",
            "$746?",
            "$747?",
            "$748?",
            "$749?",
            "$750?",
            "$751?",
            "$752?",
            "$753?",
            "$754?",
            "$755?",
            "$756?",
            "$757?",
            "$758?",
            "$759?",
            "$760?",
            "$761?"
        };
        public static ISymbolTable YjSymbolTable { get; } = SharedSymbolTable.NewSharedSymbolTable("YJ_symbols", 10, null, YjSymbols.Select(sym => sym.Replace("?", "")));

        public EntityCollection Entities { get; set; } = new EntityCollection();

        public enum ContainerFormat
        {
            KfxUnknown = 1,
            Kpf,
            KfxMain,
            KfxMetadata,
            KfxAttachable
        }

        public long RawMlSize { get; private set; }
        public Image CoverImage { get; private set; }

        public string Asin => Metadata.Asin;
        public string Author => Metadata.Author;
        public string CdeContentType => Metadata.CdeContentType;
        public string DbName => Metadata.AssetId;
        public string Title => Metadata.Title;
        public string UniqueId => null;

        private KfxMetadata Metadata { get; set; }
        private class KfxMetadata
        {
            public string Asin { get; set; }
            public string AssetId { get; set; }
            public string Author { get; set; }
            public string CdeContentType { get; set; }
            public string ContentId { get; set; }
            public string CoverImage { get; set; }
            public string IssueDate { get; set; }
            public string Language { get; set; }
            public string Publisher { get; set; }
            public string Title { get; set; }
        }

        protected void SetMetadata()
        {
            // This is definitely going to break
            // TODO: Handle other ids too, also consider multiple authors
            var metadata = Entities.Where(entity => entity.FragmentType == "$490")
                .Select(entity => entity.Value).OfType<IonStruct>()
                .Select(s => s.First()).OfType<IonList>()
                .SelectMany(list => list).OfType<IonStruct>()
                .Where(s => ((IonString) s.First()).StringValue == "kindle_title_metadata")
                .Select(md => md.GetById<IonList>(258))
                .Single()
                .Cast<IonStruct>()
                .Where(s => s.GetById<IonValue>(307) is IonString)
                .ToDictionary(s => s.GetById<IonString>(492).StringValue, s => s.GetById<IonString>(307).StringValue);

            Metadata = new KfxMetadata
            {
                Asin = metadata.GetOrDefault("ASIN"),
                AssetId = metadata.GetOrDefault("asset_id"),
                Author = metadata.GetOrDefault("author"),
                CdeContentType = metadata.GetOrDefault("cde_content_type"),
                ContentId = metadata.GetOrDefault("content_id"),
                CoverImage = metadata.GetOrDefault("cover_image"),
                IssueDate = metadata.GetOrDefault("issue_date"),
                Language = metadata.GetOrDefault("language"),
                Publisher = metadata.GetOrDefault("publisher"),
                Title = metadata.GetOrDefault("title")
            };
        }

        /// <summary>
        /// Not needed as we will crash during load if DRM is detected
        /// </summary>
        public void CheckDrm() { }

        public byte[] GetRawMl()
        {
            throw new NotSupportedException();
        }

        public Stream GetRawMlStream() => new MemoryStream(new byte[0]);

        public void SaveRawMl(string path)
        {
            throw new NotSupportedException();
        }

        public void UpdateCdeContentType(FileStream fs)
        {
            throw new NotSupportedException();
        }

        public bool RawMlSupported { get; } = false;

        public void GetBookNavigation()
        {
            var bookNav = Entities.ValueOrDefault<IonList>("$389");
            var readingOrderId = GetReadingOrderIds().Single();
            if (bookNav != null)
            {
                foreach (var nav in bookNav.OfType<IonStruct>())
                {
                    if (nav.GetSymbolIdById(178) != readingOrderId)
                        continue;
                    var navContainers = nav.GetById<IonList>(392);
                    if (navContainers == null)
                        continue;

                    //if isinstance(nav_container, IonSymbol):
                    //  nav_container = self.fragments[YJFragmentKey(ftype = "$391", fid = nav_container)].value
                    //  inline_nav_containers = False

                    foreach (var navContainer in navContainers.OfType<IonStruct>())
                    {
                        if (navContainer.GetSymbolIdById(235) != 212)
                            continue;
                        //var containerName = navContainer.GetById<IonSymbol>(239);
                        var chapterList = navContainer.GetById<IonList>(247);

                    }
                }


            }

            throw new Exception($"Unable to locate book navigation for reading order {readingOrderId}");
        }

        public IEnumerable<int> GetReadingOrderIds()
        {
            var orders = GetReadingOrders();
            return orders.OfType<IonStruct>()
                .Select(order => order.GetById<IonSymbol>(178)?.SymbolValue.Sid)
                .Where(id => id != null)
                .Cast<int>();
        }

        public IonList GetReadingOrders()
        {
            var docData = Entities.SingleOrDefault("$538");
            if (docData?.Value is IonStruct docStruct)
                return docStruct.GetById<IonList>(169);

            throw new NotImplementedException();

            //metadata = self.fragments.get("$258", first = True)
            //return [] if metadata is None else metadata.value.get("$169", [])
        }

        public void Dispose()
        {
            CoverImage?.Dispose();
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Entity
    {
        public string FragmentId { get; set; }
        public string FragmentType { get; set; }
        public string Signature { get; }
        public ushort Version { get; }
        public uint Length { get; }
        public IonValue Value { get; }

        private const string EntitySignature = "ENTY";
        private const int MinHeaderLength = 10;
        private readonly int[] _allowedVersions = { 1 };

        public string[] RawFragmentTypes = { "$417", "$418" };

        private string DebuggerDisplay => $"{FragmentType} - {FragmentId}";

        public Entity(Stream stream, int id, int type, ISymbolTable symbolTable, IonDotnet.Systems.IonLoader loader)
        {
            using var reader = new BinaryReader(stream, Encoding.UTF8, true);
            Signature = Encoding.ASCII.GetString(reader.ReadBytes(4));
            if (Signature != EntitySignature)
                throw new Exception("Invalid signature");

            Version = reader.ReadUInt16();
            if (!_allowedVersions.Contains(Version))
                throw new Exception($"Version not supported ({Version})");

            Length = reader.ReadUInt32();
            if (Length < MinHeaderLength)
                throw new Exception("Header too short");

            // Duplicated in KfxContainer
            // 10 = number of bytes read so far
            var containerInfoData = new MemoryStream(stream.ReadBytes((int)Length - 10));
            var entityInfo = loader.LoadSingle<IonStruct>(containerInfoData);
            if (entityInfo == null)
                throw new Exception("Bad container or something");

            var compressionType = entityInfo.GetById<IonInt>(410).IntValue;
            if (compressionType != KfxContainer.DefaultCompressionType)
                throw new Exception($"Unexpected bcComprType ({compressionType})");

            var drmScheme = entityInfo.GetById<IonInt>(411).IntValue;
            if (drmScheme != KfxContainer.DefaultDrmScheme)
                throw new Exception($"Unexpected bcDRMScheme ({drmScheme})");

            FragmentId = symbolTable.FindKnownSymbol(id);
            FragmentType = symbolTable.FindKnownSymbol(type);

            Value = RawFragmentTypes.Contains(FragmentType)
                ? new IonBlob(new ReadOnlySpan<byte>(stream.ReadToEnd()))
                : loader.Load(stream.ReadToEnd()).Single();

            // Skipping annotation handling for now

            //if ftype == fid and ftype in ROOT_FRAGMENT_TYPES and not self.pure:

            //fid = "$348"

            //return YJFragment(fid = fid if fid != "$348" else None, ftype = ftype, value = self.value)
        }
    }
}
