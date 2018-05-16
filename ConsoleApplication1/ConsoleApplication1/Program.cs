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
                    DescriptionDef desc = new DescriptionDef(("starsystemdef_" + system["name"]).Replace(" ", string.Empty).Replace("'", string.Empty), (string)system["name"], " ", "", 0, 0, false, "", "", "");
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
                    descriptionjson.Add("Id", "starsystemdef_" + ((string)system["name"]).Replace(" ", string.Empty).Replace("'", string.Empty));
                    descriptionjson.Add("Name", (string)system["name"]);
                    descriptionjson.Add("Details", " ");
                    // string json = JsonConvert.SerializeObject(def2, new Newtonsoft.Json.Converters.StringEnumConverter());
                    string path = "C:/Program Files (x86)/Steam/steamapps/common/BATTLETECH/mods/OldData/" + folder + "/starsystemdef_" + ((string)system["name"]).Replace(" ", string.Empty).Replace("'", string.Empty) + ".json";
                    File.WriteAllText(path, jsonObject.ToString());
                }
            }
        }
    }
}

