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
            DirectoryInfo d = new DirectoryInfo("C:/Users/morph/Desktop/Jumppoints");
            foreach (var file in d.GetFiles("*.json")) {
                JObject systemJOBject = JObject.Parse(File.ReadAllText(file.FullName));
                if (((string)systemJOBject["Owner"]).Equals("AuriganDirectorate")) {
                    systemJOBject["Owner"] = "AuriganRestoration";
                    JArray tags = JArray.FromObject(systemJOBject["Tags"]["items"]);
                    List<string> stringtags = tags.ToObject<List<string>>();
                    stringtags.Remove("planet_faction_directorate");
                    tags = JArray.FromObject(stringtags);
                    tags.Add("planet_faction_restoration");
                    systemJOBject["Tags"]["items"] = tags;
                    string filepath = OutputPath + "/StarSystems/" + systemJOBject["Description"]["Id"] + ".json";
                    (new FileInfo(filepath)).Directory.Create();
                    File.WriteAllText(filepath, systemJOBject.ToString());
                }
            }
        }

        public static void newmap() {
            JArray jarray = JArray.Parse(File.ReadAllText(@"C:\Program Files (x86)\Steam\steamapps\common\BATTLETECH\mods\OldData\systems.json"));
            string newdata = File.ReadAllText(@"C:\Program Files (x86)\Steam\steamapps\common\BATTLETECH\mods\OldData\planetstest.json");
            JArray newdataArray = JArray.Parse(newdata);
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
                    TagSet tags = new TagSet();
                    int maxspecials = 0;
                    switch ((string)system["affiliation"]) {
                        case "Lyran Commonwealth":
                            maxspecials++;
                            faction = Faction.Steiner;
                            folder = "Steiner";
                            tags.Add("planet_civ_innersphere");
                            tags.Add("planet_faction_steiner");
                            break;
                        case "Free Worlds League":
                            maxspecials++;
                            faction = Faction.Marik;
                            folder = "Marik";
                            tags.Add("planet_civ_innersphere");
                            tags.Add("planet_faction_marik");
                            break;
                        case "Draconis Combine":
                            maxspecials++;
                            faction = Faction.Kurita;
                            folder = "Kurita";
                            tags.Add("planet_civ_innersphere");
                            tags.Add("planet_faction_kurita");
                            break;
                        case "Federated Suns":
                            maxspecials++;
                            faction = Faction.Davion;
                            folder = "Davion";
                            tags.Add("planet_civ_innersphere");
                            tags.Add("planet_faction_davion");
                            break;
                        case "Capellan Confederation":
                            maxspecials++;
                            faction = Faction.Liao;
                            folder = "Liao";
                            tags.Add("planet_civ_innersphere");
                            tags.Add("planet_faction_liao");
                            break;
                        case "Aurigan Coalition":
                            faction = Faction.AuriganRestoration;
                            folder = "Aurigan";
                            tags.Add("planet_civ_periphery");
                            tags.Add("planet_faction_restoration");
                            break;
                        case "ComStar":
                            maxspecials++;
                            faction = Faction.ComStar;
                            folder = "ComStar";
                            tags.Add("planet_civ_innersphere");
                            break;
                        case "Magistracy of Canopus":
                            faction = Faction.MagistracyOfCanopus;
                            folder = "Magistracy";
                            tags.Add("planet_civ_periphery");
                            tags.Add("planet_faction_magistracy");
                            break;
                        case "Taurian Concordat":
                            faction = Faction.TaurianConcordat;
                            folder = "Taurian";
                            tags.Add("planet_civ_periphery");
                            tags.Add("planet_faction_taurian");
                            break;
                        case "Outworlds Alliance":
                            faction = Faction.Betrayers;
                            folder = "Outworld";
                            tags.Add("planet_civ_periphery");
                            tags.Add("planet_faction_outworlds");
                            break;
                        case "Marian Hegemony":
                            faction = Faction.AuriganDirectorate;
                            folder = "Hegemony";
                            tags.Add("planet_civ_periphery");
                            tags.Add("planet_faction_marian");
                            break;
                        case "Oberon Confederation":
                            faction = Faction.MagistracyCentrella;
                            folder = "Oberon";
                            maxspecials++;
                            tags.Add("planet_civ_periphery");
                            tags.Add("planet_other_pirate");
                            tags.Add("planet_other_blackmarket");
                            tags.Add("planet_faction_oberon");
                            break;
                        case "Lothian League":
                            faction = Faction.MajestyMetals;
                            folder = "Lothian";
                            tags.Add("planet_civ_periphery");
                            tags.Add("planet_faction_lothian");
                            break;
                        case "Circinus Federation":
                            faction = Faction.Nautilus;
                            folder = "Circinus";
                            maxspecials++;
                            tags.Add("planet_civ_periphery");
                            tags.Add("planet_other_pirate");
                            tags.Add("planet_other_blackmarket");
                            tags.Add("planet_faction_circinus");
                            break;
                        case "Illyrian Palatinate":
                            faction = Faction.AuriganMercenaries;
                            folder = "Illyrian";
                            tags.Add("planet_civ_periphery");
                            tags.Add("planet_faction_illyrian");
                            break;
                        default:
                            faction = Faction.Locals;
                            folder = "Locals";
                            tags.Add("planet_civ_periphery");
                            break;
                    }
                    if (((string)system["name"]).Equals("Galatea")){
                        faction = Faction.MercenaryReviewBoard;
                        folder = "MRB";
                        tags.Remove("planet_faction_steiner");
                    }

                    string beginjson = File.ReadAllText(@"C:\Program Files (x86)\Steam\steamapps\common\BATTLETECH\mods\OldData\starsystemdef_Detroit.json");
                    string name = (string)system["name"];

                    StarSystemDef def = new StarSystemDef();
                    def.FromJSON(beginjson);


                    bool fueling = false;
                    string details = " ";
                    List<Biome.BIOMESKIN> biomes = new List<Biome.BIOMESKIN>(); ;
                    bool newdatafound = false;
                    foreach (JObject newdataObject in newdataArray) {
                        biomes = new List<Biome.BIOMESKIN>();
                        if (system["name"].Equals(newdataObject["Planet_Name"])) {
                            newdatafound = true;
                            if (!string.IsNullOrEmpty((string)newdataObject["Description"])) {
                                details = ((string)newdataObject["Description"]).Replace("\t", "").Replace("\\", "").Replace("</P>", "").Replace("<P>", "").Replace("\r", "").Replace("\n", "").Replace("</p>", "").Replace("<p>", "");
                                if (details.Length > 255) {
                                    details = details.Substring(0, 255);
                                }
                            }
                            if ((int)newdataObject["Industry"] != 0) {
                                tags.Add("planet_industry_mining");
                                if ((int)newdataObject["Industry"] >= 100000000) {
                                    maxspecials++;
                                    tags.Add("planet_industry_rich");
                                }
                                else {
                                    tags.Add("planet_industry_recreation");
                                }
                            }
                            else {
                                tags.Add("planet_industry_agriculture");
                                tags.Add("planet_industry_aquaculture");
                                tags.Add("planet_industry_poor");
                            }
                            if (!string.IsNullOrEmpty((string)newdataObject["comstar_facility"]) && !((string)newdataObject["comstar_facility"]).Equals("None")) {
                                tags.Add("planet_industry_research");
                                tags.Add("planet_other_comstar");
                                tags.Add("planet_other_starleague");
                                maxspecials++;
                            }
                            if ((int)newdataObject["Capital_Planet"] == 1) {
                                tags.Add("planet_other_capital");
                                maxspecials++;

                            }
                            if ((long)newdataObject["population"] > 1000000000) {
                                tags.Add("planet_pop_large");
                                maxspecials++;
                                maxspecials++;
                                if ((long)newdataObject["population"] > 5000000000) {
                                    tags.Add("planet_other_megacity");
                                    maxspecials++;
                                }
                            }
                            else if ((int)newdataObject["population"] > 100000000) {
                                tags.Add("planet_pop_medium");
                                maxspecials++;

                            }
                            else if ((int)newdataObject["population"] > 1000000) {
                                tags.Add("planet_pop_small");

                            }
                            else {
                                tags.Add("planet_pop_none");

                            }
                            if ((int)newdataObject["Charge_Station"] == 1) {
                                fueling = true;
                            }
                            if ((int)newdataObject["Factory"] == 123) {
                                tags.Add("planet_industry_manufacturing");
                                maxspecials++;
                                maxspecials++;
                            }
                            if ((int)newdataObject["hiringhall"] == 1) {
                                tags.Add("planet_other_hub");
                            }
                            switch ((int)newdataObject["terrain_class_ID"]) {
                                case 1: {
                                        tags.Add("planet_climate_terran");
                                        tags.Add("planet_other_megaforest");
                                        biomes.Add(Biome.BIOMESKIN.highlandsSpring);
                                        biomes.Add(Biome.BIOMESKIN.highlandsFall);
                                        biomes.Add(Biome.BIOMESKIN.lowlandsSpring);
                                        biomes.Add(Biome.BIOMESKIN.lowlandsFall);
                                        biomes.Add(Biome.BIOMESKIN.badlandsParched);
                                        break;
                                    }
                                case 2: {
                                        tags.Add("planet_climate_lunar");
                                        tags.Add("planet_other_moon");
                                        biomes.Add(Biome.BIOMESKIN.martianVacuum);
                                        biomes.Add(Biome.BIOMESKIN.lunarVacuum);
                                        break;
                                    }
                                case 3: {
                                        tags.Add("planet_climate_desert");
                                        tags.Add("planet_other_storms");
                                        biomes.Add(Biome.BIOMESKIN.desertParched);
                                        break;
                                    }
                                case 4: {
                                        tags.Add("planet_climate_arid");
                                        tags.Add("planet_other_mudflats");
                                        tags.Add("planet_other_fungus");
                                        biomes.Add(Biome.BIOMESKIN.lowlandsCoastal);
                                        biomes.Add(Biome.BIOMESKIN.badlandsParched);
                                        break;
                                    }
                                case 5: {
                                        tags.Add("planet_climate_arctic");
                                        biomes.Add(Biome.BIOMESKIN.polarFrozen);
                                        biomes.Add(Biome.BIOMESKIN.tundraFrozen);
                                        break;
                                    }
                                case 6: {
                                        tags.Add("planet_climate_tropical");
                                        biomes.Add(Biome.BIOMESKIN.lowlandsCoastal);
                                        biomes.Add(Biome.BIOMESKIN.lowlandsSpring);
                                        biomes.Add(Biome.BIOMESKIN.lowlandsFall);
                                        break;
                                    }
                                case 7: {
                                        tags.Add("planet_climate_rocky");
                                        biomes.Add(Biome.BIOMESKIN.highlandsSpring);
                                        biomes.Add(Biome.BIOMESKIN.highlandsFall);
                                        break;
                                    }
                                case 8: {
                                        tags.Add("planet_climate_mars");
                                        tags.Add("planet_other_volcanic");
                                        biomes.Add(Biome.BIOMESKIN.martianVacuum);
                                        biomes.Add(Biome.BIOMESKIN.badlandsParched);
                                        break;
                                    }
                                case 10: {
                                        tags.Add("planet_climate_terran");
                                        tags.Add("planet_other_megaforest");
                                        biomes.Add(Biome.BIOMESKIN.highlandsSpring);
                                        biomes.Add(Biome.BIOMESKIN.highlandsFall);
                                        biomes.Add(Biome.BIOMESKIN.lowlandsSpring);
                                        biomes.Add(Biome.BIOMESKIN.lowlandsFall);
                                        biomes.Add(Biome.BIOMESKIN.lowlandsCoastal);
                                        break;
                                    }
                                case 18: {
                                        tags.Add("planet_climate_water");
                                        biomes.Add(Biome.BIOMESKIN.lowlandsCoastal);
                                        biomes.Add(Biome.BIOMESKIN.highlandsSpring);
                                        break;
                                    }
                                default: {
                                        tags.Add("planet_climate_terran");
                                        biomes.Add(Biome.BIOMESKIN.highlandsSpring);
                                        biomes.Add(Biome.BIOMESKIN.highlandsFall);
                                        biomes.Add(Biome.BIOMESKIN.lowlandsSpring);
                                        biomes.Add(Biome.BIOMESKIN.lowlandsFall);
                                        biomes.Add(Biome.BIOMESKIN.desertParched);
                                        biomes.Add(Biome.BIOMESKIN.badlandsParched);
                                        biomes.Add(Biome.BIOMESKIN.lowlandsCoastal);
                                        biomes.Add(Biome.BIOMESKIN.lunarVacuum);
                                        biomes.Add(Biome.BIOMESKIN.martianVacuum);
                                        biomes.Add(Biome.BIOMESKIN.polarFrozen);
                                        biomes.Add(Biome.BIOMESKIN.tundraFrozen);
                                        break;
                                    }
                            }

                            break;
                        }

                    }
                    if (newdatafound) {
                        tags.Add("planet_size_medium");
                    }
                    else {
                        tags.Add("planet_climate_terran");
                        tags.Add("planet_size_medium");
                        biomes.Add(Biome.BIOMESKIN.highlandsSpring);
                        biomes.Add(Biome.BIOMESKIN.highlandsFall);
                        biomes.Add(Biome.BIOMESKIN.lowlandsSpring);
                        biomes.Add(Biome.BIOMESKIN.lowlandsFall);
                        biomes.Add(Biome.BIOMESKIN.desertParched);
                        biomes.Add(Biome.BIOMESKIN.badlandsParched);
                        biomes.Add(Biome.BIOMESKIN.lowlandsCoastal);
                        biomes.Add(Biome.BIOMESKIN.lunarVacuum);
                        biomes.Add(Biome.BIOMESKIN.martianVacuum);
                        biomes.Add(Biome.BIOMESKIN.polarFrozen);
                        biomes.Add(Biome.BIOMESKIN.tundraFrozen);
                    }



                    DescriptionDef desc = new DescriptionDef(("starsystemdef_" + system["name"]).Replace(" ", string.Empty).Replace("'", string.Empty), (string)system["name"], details, "", 0, 0, false, "", "", "");
                    StarSystemDef def2 = new StarSystemDef(desc, vector, tags, false, 7, faction, getAllies(faction), getEnemies(faction), def.SystemInfluence, def.TravelRequirements);

                    foreach (JObject newdataObject in newdataArray) {
                        if (system["name"].Equals(newdataObject["Planet_Name"])) {

                            break;
                        }
                    }


                    ReflectionHelper.InvokePrivateMethode(def2, "set_FuelingStation", new object[] { fueling });
                    ReflectionHelper.InvokePrivateMethode(def2, "set_Difficulty", new object[] { 0 });
                    ReflectionHelper.InvokePrivateMethode(def2, "set_StarType", new object[] { StarType.G });
                    ReflectionHelper.InvokePrivateMethode(def2, "set_JumpDistance", new object[] { 7 });
                    ReflectionHelper.InvokePrivateMethode(def2, "set_ShopMaxSpecials", new object[] { maxspecials });
                    ReflectionHelper.InvokePrivateMethode(def2, "set_SupportedBiomes", new object[] { biomes });

                    foreach (JObject newdataObject in newdataArray) {
                        if (system["name"].Equals(newdataObject["Planet_Name"])) {
                            ReflectionHelper.InvokePrivateMethode(def2, "set_StarType", new object[] { getStartype((string)newdataObject["SpecClass"]) });
                            if ((float)newdataObject["travel_time"] != 0f) {
                                ReflectionHelper.InvokePrivateMethode(def2, "set_JumpDistance", new object[] { (int)newdataObject["travel_time"] });
                            }
                            break;
                        }
                    }

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
                    return new List<Faction>() { Faction.Betrayers, Faction.Locals, Faction.ComStar };
                case Faction.AuriganDirectorate:
                    return new List<Faction>() { Faction.AuriganDirectorate, Faction.Locals };
                case Faction.AuriganMercenaries:
                    return new List<Faction>() { Faction.AuriganMercenaries, Faction.Locals };
                case Faction.AuriganPirates:
                    return new List<Faction>() { Faction.AuriganPirates, Faction.Locals };
                case Faction.AuriganRestoration:
                    return new List<Faction>() { Faction.AuriganRestoration, Faction.Locals };
                case Faction.ComStar:
                    return new List<Faction>() { Faction.ComStar, Faction.Locals };
                case Faction.Davion:
                    return new List<Faction>() { Faction.Davion, Faction.ComStar, Faction.Locals };
                case Faction.Kurita:
                    return new List<Faction>() { Faction.ComStar, Faction.Locals, Faction.Kurita };
                case Faction.Liao:
                    return new List<Faction>() { Faction.Liao, Faction.ComStar, Faction.Locals };
                case Faction.Locals:
                    return new List<Faction>() { Faction.Locals };
                case Faction.MagistracyCentrella:
                    return new List<Faction>() { Faction.Locals, Faction.MagistracyCentrella, Faction.AuriganPirates };
                case Faction.MagistracyOfCanopus:
                    return new List<Faction>() { Faction.Locals, Faction.ComStar, Faction.MagistracyOfCanopus };
                case Faction.MajestyMetals:
                    return new List<Faction>() { Faction.Locals, Faction.MajestyMetals };
                case Faction.Marik:
                    return new List<Faction>() { Faction.Locals, Faction.Marik, Faction.ComStar };
                case Faction.MercenaryReviewBoard:
                    return new List<Faction>() { Faction.Locals, Faction.ComStar, Faction.MercenaryReviewBoard };
                case Faction.Nautilus:
                    return new List<Faction>() { Faction.Locals, Faction.Nautilus, Faction.ComStar, Faction.AuriganPirates };
                case Faction.NoFaction:
                    return new List<Faction>() { Faction.Locals, Faction.NoFaction };
                case Faction.Steiner:
                    return new List<Faction>() { Faction.Locals, Faction.Steiner, Faction.ComStar };
                case Faction.TaurianConcordat:
                    return new List<Faction>() { Faction.Locals, Faction.TaurianConcordat, Faction.ComStar };
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
                    return new List<Faction>() { Faction.AuriganDirectorate,Faction.AuriganMercenaries,Faction.AuriganRestoration,
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

