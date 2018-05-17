using BattleTech;
using HBS.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleTech.SimGameSpaceController;

namespace ConsoleApplication1 {
    class Program {
        static void Main(string[] args) {
            newmap();
            /* string[] lines = System.IO.File.ReadAllLines(@"C:\Program Files (x86)\Steam\steamapps\common\BATTLETECH\mods\OldData\is3025_planets.txt");
             foreach (string line in lines) {
                 string[] words = line.Split(' ');
                 string name = words[9];
                 if (words.Length == 11) {
                     name += " " + words[10];
                 }
                 FakeVector3 vector = new FakeVector3();
                 string x = words[4].Replace("<", "").Replace(",", "");
                 vector.x = float.Parse(x, CultureInfo.InvariantCulture);
                 string y = words[6].Replace(">", "");
                 vector.y = float.Parse(y, CultureInfo.InvariantCulture);
                 vector.z = 0;
                 DescriptionDef desc = new DescriptionDef("starsystemdef_" + name, name, " ", "", 0, 0, false, "", "", "");
                 Faction faction;
                 string folder = "";
                 switch (words[2]) {
                     case "LCPlanet":
                         faction = Faction.Steiner;
                         folder = "Steiner";
                         break;
                     case "FWLPlanet":
                         faction = Faction.Marik;
                         folder = "Marik";
                         break;
                     case "DCPlanet":
                         faction = Faction.Kurita;
                         folder = "Kurita";
                         break;
                     case "FSPlanet":
                         faction = Faction.Davion;
                         folder = "Davion";
                         break;
                     case "CCPlanet":
                         faction = Faction.Liao;
                         folder = "Liao";
                         break;
                     default:
                         faction = Faction.Locals;
                         folder = "Locals";
                         break;
                 }
                 string beginjson = File.ReadAllText("C:/Users/morph/Desktop/Neuer Ordner (4)/starsystemdef_Terra.json");
                 StarSystemDef def = new StarSystemDef();
                 def.FromJSON(beginjson);

                 StarSystemDef def2 = new StarSystemDef(desc, vector, def.Tags, false, 7, faction, def.ContractEmployers, def.ContractTargets, def.SystemInfluence, def.TravelRequirements);
                 string json = def2.ToJSON();
                 JObject jsonObject = JObject.Parse(json);
                 JObject descriptionjson = (JObject)jsonObject["Description"];
                 descriptionjson.Add("Id", "starsystemdef_" + name);
                 descriptionjson.Add("Name", name);
                 descriptionjson.Add("Details", " ");
                 // string json = JsonConvert.SerializeObject(def2, new Newtonsoft.Json.Converters.StringEnumConverter());
                 string path = "C:/Program Files (x86)/Steam/steamapps/common/BATTLETECH/mods/OldData/" + folder + "/starsystemdef_" + name + ".json";
                 File.WriteAllText(path, jsonObject.ToString());*/
        }

        public static void newmap() {
            JArray jarray = JArray.Parse(File.ReadAllText(@"C:\Program Files (x86)\Steam\steamapps\common\BATTLETECH\mods\OldData\systems.json"));
            foreach (JObject system in jarray) {
                if (!((string)system["affiliation"]).Equals("Clans") && !((string)system["affiliation"]).Equals("Inhabited system") &&
                    !((string)system["affiliation"]).Equals("No record") && !((string)system["affiliation"]).Equals("Clan") &&
                    !((string)system["affiliation"]).Equals("New Delphi Compact") && !((string)system["affiliation"]).Equals("Alexandrian Covenant") &&
                    !((string)system["affiliation"]).Equals("Society of St.Andreas") && !((string)system["affiliation"]).Equals("Hidden system") &&
                    !((string)system["affiliation"]).Equals("Society of St. Andreas") && !((string)system["affiliation"]).Equals("Tortuga Dominions") &&
                    !((string)system["affiliation"]).Equals("Fiefdom of Randis") && !((string)system["name"]).Equals("New St. Andrews") &&
                    !((string)system["name"]).Equals("Mica I") && !((string)system["name"]).Equals("Mica V") && !((string)system["name"]).Equals("Novo Franklin")
                    && !((string)system["name"]).Equals("New Vandenberg") && !((string)system["name"]).Equals("Mica VII")) {
                    FakeVector3 vector = new FakeVector3();
                    vector.x = (float)system["x"];
                    vector.y = (float)system["y"];
                    vector.z = 0;
                    Faction faction;
                    string folder = "";
                    switch ((string)system["affiliation"]) {
                        case "Lyran Commonwealth":
                            faction = Faction.Steiner;
                            folder = "Steiner";
                            break;
                        case "Free Worlds League":
                            faction = Faction.Marik;
                            folder = "Marik";
                            break;
                        case "Draconis Combine":
                            faction = Faction.Kurita;
                            folder = "Kurita";
                            break;
                        case "Federated Suns":
                            faction = Faction.Davion;
                            folder = "Davion";
                            break;
                        case "Capellan Confederation":
                            faction = Faction.Liao;
                            folder = "Liao";
                            break;
                        case "Aurigan Coalition":
                            faction = Faction.AuriganRestoration;
                            folder = "Aurigan";
                            break;
                        case "ComStar":
                            faction = Faction.ComStar;
                            folder = "ComStar";
                            break;
                        case "Magistracy of Canopus":
                            faction = Faction.MagistracyOfCanopus;
                            folder = "Magistracy";
                            break;
                        case "Taurian Concordat":
                            faction = Faction.TaurianConcordat;
                            folder = "Taurian";
                            break;
                        case "Outworlds Alliance":
                            faction = Faction.Betrayers;
                            folder = "Outworld";
                            break;
                        case "Marian Hegemony":
                            faction = Faction.AuriganDirectorate;
                            folder = "Hegemony";
                            break;
                        case "Oberon Confederation":
                            faction = Faction.MagistracyCentrella;
                            folder = "Oberon";
                            break;
                        case "Lothian League":
                            faction = Faction.MajestyMetals;
                            folder = "Lothian";
                            break;
                        case "Circinus Federation":
                            faction = Faction.Nautilus;
                            folder = "Circinus";
                            break;
                        case "Illyrian Palatinate":
                            faction = Faction.AuriganMercenaries;
                            folder = "Illyrian";
                            break;
                        default:
                            faction = Faction.Locals;
                            folder = "Locals";
                            break;
                    }
                    string beginjson = File.ReadAllText("C:/Users/morph/Desktop/Neuer Ordner (4)/starsystemdef_Detroit.json");
                    string name = (string)system["name"];

                    StarSystemDef def = new StarSystemDef();
                    def.FromJSON(beginjson);
                    TagSet tags = new TagSet();
                    string details = " ";
                    
                    DescriptionDef desc = new DescriptionDef(("starsystemdef_" + system["name"]).Replace(" ", string.Empty).Replace("'", string.Empty), (string)system["name"], details, "", 0, 0, false, "", "", "");
                    StarSystemDef def2 = new StarSystemDef(desc, vector, def.Tags, false, 7, faction, getAllies(faction), getEnemies(faction), def.SystemInfluence, def.TravelRequirements);
                    
                            List<Biome.BIOMESKIN> biomes = new List<Biome.BIOMESKIN>();
                    biomes.Add(Biome.BIOMESKIN.highlandsSpring);
                            ReflectionHelper.InvokePrivateMethode(def2, "set_Difficulty", new object[] { 5 });
                            ReflectionHelper.InvokePrivateMethode(def2, "set_StarType", new object[] { StarType.G });
                            ReflectionHelper.InvokePrivateMethode(def2, "set_JumpDistance", new object[] { 7 });
                            ReflectionHelper.InvokePrivateMethode(def2, "set_ShopMaxSpecials", new object[] { 7 });
                            ReflectionHelper.InvokePrivateMethode(def2, "set_SupportedBiomes", new object[] { biomes });
        
                    string json = def2.ToJSON();
                    JObject jsonObject = JObject.Parse(json);
                    JObject descriptionjson = (JObject)jsonObject["Description"];
                    descriptionjson.Add("Id", "starsystemdef_" + ((string)system["name"]).Replace(" ", string.Empty).Replace("'", string.Empty));
                    descriptionjson.Add("Name", (string)system["name"]);
                    descriptionjson.Add("Details", details);
                    // string json = JsonConvert.SerializeObject(def2, new Newtonsoft.Json.Converters.StringEnumConverter());
                    string path = "C:/Program Files (x86)/Steam/steamapps/common/BATTLETECH/mods/OldData/" + folder + "/starsystemdef_" + ((string)system["name"]).Replace(" ", string.Empty).Replace("'", string.Empty) + ".json";
                    (new FileInfo(path)).Directory.Create();
                    File.WriteAllText(path, jsonObject.ToString());
                }
            }
        }

        public static Faction getfaction(string faction) {
            switch (faction) {
                case "AuriganRestoration":
                    return Faction.AuriganRestoration;
                case "Betrayers":
                    return Faction.Betrayers;
                case "AuriganDirectorate":
                    return Faction.AuriganDirectorate;
                case "AuriganMercenaries":
                    return Faction.AuriganMercenaries;
                case "AuriganPirates":
                    return Faction.AuriganPirates;
                case "ComStar":
                    return Faction.ComStar;
                case "Davion":
                    return Faction.Davion;
                case "Kurita":
                    return Faction.Kurita;
                case "Liao":
                    return Faction.Liao;
                case "Locals":
                    return Faction.Locals;
                case "MagistracyCentrella":
                    return Faction.MagistracyCentrella;
                case "MagistracyOfCanopus":
                    return Faction.MagistracyOfCanopus;
                case "MajestyMetals":
                    return Faction.MajestyMetals;
                case "Marik":
                    return Faction.Marik;
                case "MercenaryReviewBoard":
                    return Faction.MercenaryReviewBoard;
                case "Nautilus":
                    return Faction.Nautilus;
                case "Steiner":
                    return Faction.Steiner;
                case "TaurianConcordat":
                    return Faction.TaurianConcordat;
                default:
                    return Faction.NoFaction;
            }
        }

        public static Biome.BIOMESKIN getBiome(string biome) {
            switch (biome) {


                case "highlandsSpring":
                    return Biome.BIOMESKIN.highlandsSpring;
                case "highlandsFall":
                    return Biome.BIOMESKIN.highlandsFall;

                case "lowlandsSpring":
                    return Biome.BIOMESKIN.lowlandsSpring;

                case "lowlandsFall":
                    return Biome.BIOMESKIN.lowlandsFall;

                case "desertParched":
                    return Biome.BIOMESKIN.desertParched;

                case "badlandsParched":
                    return Biome.BIOMESKIN.badlandsParched;

                case "lowlandsCoastal":
                    return Biome.BIOMESKIN.lowlandsCoastal;

                case "lunarVacuum":
                    return Biome.BIOMESKIN.lunarVacuum;

                case "martianVacuum":
                    return Biome.BIOMESKIN.martianVacuum;

                case "polarFrozen":
                    return Biome.BIOMESKIN.polarFrozen;

                case "tundraFrozen":
                    return Biome.BIOMESKIN.tundraFrozen;
                default:
                    return Biome.BIOMESKIN.highlandsSpring;
            }
        }

        public static StarType getStartype(string type) {
            switch (type) {
                case "M":
                    return StarType.M;
                case "K":
                    return StarType.K;

                case "G":
                    return StarType.G;

                case "F":
                    return StarType.F;

                case "A":
                    return StarType.A;

                case "B":
                    return StarType.B;

                case "O":
                    return StarType.O;
                default:
                    return StarType.M;
            }
        }

        public static List<Faction> getAllies(Faction faction) {
            switch (faction) {
                case Faction.Betrayers:
                    return new List<Faction>() { Faction.Betrayers, Faction.Locals, Faction.Davion, Faction.ComStar };
                case Faction.AuriganDirectorate:
                    return new List<Faction>() { Faction.AuriganDirectorate, Faction.Marik, Faction.Locals, Faction.Liao, Faction.Nautilus };
                case Faction.AuriganMercenaries:
                    return new List<Faction>() { Faction.AuriganMercenaries, Faction.Locals, Faction.Marik, Faction.MajestyMetals };
                case Faction.AuriganPirates:
                    return new List<Faction>() { Faction.AuriganPirates, Faction.Locals, Faction.MagistracyCentrella, Faction.Nautilus };
                case Faction.AuriganRestoration:
                    return new List<Faction>() { Faction.AuriganRestoration, Faction.MagistracyOfCanopus, Faction.Locals };
                case Faction.ComStar:
                    return new List<Faction>() { Faction.ComStar, Faction.MercenaryReviewBoard, Faction.Locals };
                case Faction.Davion:
                    return new List<Faction>() { Faction.Davion, Faction.ComStar, Faction.Locals, Faction.Betrayers };
                case Faction.Kurita:
                    return new List<Faction>() { Faction.Marik, Faction.Liao, Faction.ComStar, Faction.Locals, Faction.Kurita, Faction.MagistracyCentrella };
                case Faction.Liao:
                    return new List<Faction>() { Faction.Liao, Faction.Kurita, Faction.ComStar, Faction.Locals, Faction.AuriganDirectorate };
                case Faction.Locals:
                    return new List<Faction>() { Faction.Locals };
                case Faction.MagistracyCentrella:
                    return new List<Faction>() { Faction.Locals, Faction.Steiner, Faction.Kurita, Faction.AuriganPirates };
                case Faction.MagistracyOfCanopus:
                    return new List<Faction>() { Faction.Locals, Faction.AuriganRestoration, Faction.Marik, Faction.ComStar, Faction.MagistracyOfCanopus, Faction.MajestyMetals };
                case Faction.MajestyMetals:
                    return new List<Faction>() { Faction.Locals, Faction.MajestyMetals, Faction.MagistracyOfCanopus, Faction.TaurianConcordat };
                case Faction.Marik:
                    return new List<Faction>() { Faction.Locals, Faction.Marik, Faction.Kurita, Faction.ComStar, Faction.MagistracyOfCanopus, Faction.AuriganDirectorate };
                case Faction.MercenaryReviewBoard:
                    return new List<Faction>() { Faction.Locals, Faction.ComStar, Faction.MercenaryReviewBoard };
                case Faction.Nautilus:
                    return new List<Faction>() { Faction.Locals, Faction.Nautilus, Faction.AuriganPirates, Faction.ComStar, Faction.AuriganDirectorate };
                case Faction.NoFaction:
                    return new List<Faction>() { Faction.Locals, Faction.NoFaction };
                case Faction.Steiner:
                    return new List<Faction>() { Faction.Locals, Faction.Steiner, Faction.ComStar, Faction.Davion, Faction.MagistracyCentrella };
                case Faction.TaurianConcordat:
                    return new List<Faction>() { Faction.Locals, Faction.TaurianConcordat, Faction.ComStar, Faction.Marik, Faction.MajestyMetals };
                default:
                    return new List<Faction>() { Faction.NoFaction };
            }


        }

        public static List<Faction> getEnemies(Faction faction) {
            switch (faction) {
                case Faction.Betrayers:
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganMercenaries,Faction.AuriganPirates,Faction.AuriganRestoration,
                        Faction.Kurita,Faction.Liao, Faction.MagistracyCentrella,
                        Faction.MagistracyOfCanopus,Faction.MajestyMetals, Faction.Marik,Faction.MercenaryReviewBoard,Faction.Nautilus,
                        Faction.Steiner,Faction.TaurianConcordat };
                case Faction.AuriganDirectorate:
                    return new List<Faction>() { Faction.AuriganMercenaries,Faction.AuriganPirates,Faction.AuriganRestoration,
                        Faction.Betrayers, Faction.ComStar, Faction.Davion,Faction.Kurita, Faction.MagistracyCentrella,
                        Faction.MagistracyOfCanopus,Faction.MajestyMetals,
                        Faction.Steiner,Faction.TaurianConcordat };
                case Faction.AuriganMercenaries:
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganPirates,Faction.AuriganRestoration,
                        Faction.Betrayers, Faction.ComStar, Faction.Davion,Faction.Kurita,Faction.Liao, Faction.MagistracyCentrella,
                        Faction.MagistracyOfCanopus,Faction.Nautilus,
                        Faction.Steiner,Faction.TaurianConcordat };
                case Faction.AuriganPirates:
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganMercenaries,Faction.AuriganRestoration,
                        Faction.Betrayers, Faction.ComStar, Faction.Davion,Faction.Kurita,Faction.Liao,
                        Faction.MagistracyOfCanopus,Faction.MajestyMetals, Faction.Marik,
                        Faction.Steiner,Faction.TaurianConcordat };
                case Faction.AuriganRestoration:
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganMercenaries,Faction.AuriganPirates,
                        Faction.Betrayers, Faction.ComStar, Faction.Davion,Faction.Kurita,Faction.Liao, Faction.MagistracyCentrella,
                        Faction.MajestyMetals, Faction.Marik,Faction.Nautilus,
                        Faction.Steiner,Faction.TaurianConcordat };
                case Faction.ComStar:
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganMercenaries,Faction.AuriganPirates,Faction.AuriganRestoration,
                        Faction.Betrayers, Faction.Davion,Faction.Kurita,Faction.Liao, Faction.MagistracyCentrella,
                        Faction.MagistracyOfCanopus,Faction.MajestyMetals, Faction.Marik,
                        Faction.Steiner,Faction.TaurianConcordat };
                case Faction.Davion:
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganMercenaries,Faction.AuriganPirates,Faction.AuriganRestoration,
                        Faction.Davion,Faction.Kurita,Faction.Liao, Faction.MagistracyCentrella,
                        Faction.MagistracyOfCanopus,Faction.MajestyMetals, Faction.Marik,Faction.Nautilus,
                        Faction.Steiner,Faction.TaurianConcordat };                    
                case Faction.Kurita:
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganMercenaries,Faction.AuriganPirates,Faction.AuriganRestoration,
                        Faction.Betrayers, Faction.Davion, 
                        Faction.MagistracyOfCanopus,Faction.MajestyMetals, Faction.Nautilus,
                        Faction.Steiner,Faction.TaurianConcordat };
                case Faction.Liao:
                    return new List<Faction>() { Faction.AuriganMercenaries,Faction.AuriganPirates,Faction.AuriganRestoration,
                        Faction.Betrayers, Faction.Davion, Faction.MagistracyCentrella,
                        Faction.MagistracyOfCanopus,Faction.MajestyMetals, Faction.Marik,Faction.Nautilus,
                        Faction.Steiner,Faction.TaurianConcordat };
                case Faction.Locals:
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganMercenaries,Faction.AuriganPirates,Faction.AuriganRestoration,
                        Faction.Betrayers, Faction.ComStar, Faction.Davion,Faction.Kurita,Faction.Liao, Faction.MagistracyCentrella,
                        Faction.MagistracyOfCanopus,Faction.MajestyMetals, Faction.Marik,Faction.Nautilus,
                        Faction.Steiner,Faction.TaurianConcordat };
                case Faction.MagistracyCentrella:
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganMercenaries,Faction.AuriganRestoration,
                        Faction.Betrayers, Faction.ComStar, Faction.Davion,Faction.Liao, 
                        Faction.MagistracyOfCanopus,Faction.MajestyMetals, Faction.Marik,Faction.Nautilus,Faction.TaurianConcordat };
                    
                case Faction.MagistracyOfCanopus:
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganMercenaries,Faction.AuriganPirates,
                        Faction.Betrayers, Faction.Davion,Faction.Kurita,Faction.Liao, Faction.MagistracyCentrella,
                        Faction.Nautilus,
                        Faction.Steiner,Faction.TaurianConcordat };
                case Faction.MajestyMetals:
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganMercenaries,Faction.AuriganPirates,Faction.AuriganRestoration,
                        Faction.Betrayers, Faction.ComStar, Faction.Davion,Faction.Kurita,Faction.Liao, Faction.MagistracyCentrella,
                        Faction.Marik,Faction.Nautilus,
                        Faction.Steiner};
                    
                case Faction.Marik:
                    return new List<Faction>() {Faction.AuriganMercenaries,Faction.AuriganPirates,Faction.AuriganRestoration,
                        Faction.Betrayers, Faction.Davion,Faction.Liao, Faction.MagistracyCentrella,
                        Faction.MajestyMetals,Faction.Nautilus,
                        Faction.Steiner,Faction.TaurianConcordat };
                case Faction.MercenaryReviewBoard:
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganMercenaries,Faction.AuriganPirates,Faction.AuriganRestoration,
                        Faction.Betrayers,  Faction.Davion,Faction.Kurita,Faction.Liao, Faction.MagistracyCentrella,
                        Faction.MagistracyOfCanopus,Faction.MajestyMetals, Faction.Marik,Faction.Nautilus,
                        Faction.Steiner,Faction.TaurianConcordat };
                case Faction.Nautilus:
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganMercenaries,Faction.AuriganPirates,Faction.AuriganRestoration,
                        Faction.Betrayers, Faction.ComStar, Faction.Davion,Faction.Kurita,Faction.Liao, Faction.MagistracyCentrella,
                        Faction.MagistracyOfCanopus,Faction.MajestyMetals, Faction.Marik,
                        Faction.Steiner,Faction.TaurianConcordat };
                case Faction.NoFaction:
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganMercenaries,Faction.AuriganPirates,Faction.AuriganRestoration,
                        Faction.Betrayers, Faction.ComStar, Faction.Davion,Faction.Kurita,Faction.Liao, Faction.MagistracyCentrella,
                        Faction.MagistracyOfCanopus,Faction.MajestyMetals, Faction.Marik,Faction.Nautilus,
                        Faction.Steiner,Faction.TaurianConcordat };

                case Faction.Steiner:
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganMercenaries,Faction.AuriganPirates,Faction.AuriganRestoration,
                        Faction.Betrayers,  Faction.Kurita,Faction.Liao, 
                        Faction.MagistracyOfCanopus,Faction.MajestyMetals, Faction.Marik,Faction.Nautilus,
                        Faction.TaurianConcordat };        
                case Faction.TaurianConcordat:
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganMercenaries,Faction.AuriganPirates,Faction.AuriganRestoration,
                        Faction.Betrayers,  Faction.Davion,Faction.Kurita,Faction.Liao, Faction.MagistracyCentrella,
                        Faction.MagistracyOfCanopus, Faction.Nautilus,
                        Faction.Steiner };
                default:
                    return new List<Faction>() { Faction.NoFaction };
            }
        }

    }
}

