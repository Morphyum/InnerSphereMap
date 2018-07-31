using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace DocsToSystemJSON {


    class Converter {
        private JArray universeDataJArray;
        private string OutputPath;
        private string BlueprintPath;
        private bool is3040;
        private string yearFolder = "/IS3025/";
        private string OriginalData;
        private bool keepKamea;
        public static List<string> AllFactions = new List<string>() { "ComStar","Betrayers","AuriganDirectorate","AuriganMercenaries","AuriganPirates","AuriganRestoration",
                        "Davion","Kurita","Liao","Locals", "MagistracyCentrella",
                        "MagistracyOfCanopus","MajestyMetals", "Marik","Nautilus",
                        "Steiner","TaurianConcordat" };

        public Converter(string dataPath, string arrayName, string OutputPath, string BlueprintPath, bool is3040, string OriginalData, bool keepKamea) {

            JObject jobject = JObject.Parse(File.ReadAllText(dataPath));
            this.universeDataJArray = (JArray)jobject[arrayName];
            this.OutputPath = OutputPath;
            this.BlueprintPath = BlueprintPath;
            this.is3040 = is3040;
            this.OriginalData = OriginalData;
            this.keepKamea = keepKamea;
            if (is3040) {
                this.yearFolder = "/IS3040/";
            }
        }

        public void newMap() {
            string beginjson = File.ReadAllText(BlueprintPath);
            JObject originalJObject = JObject.Parse(beginjson);
            Random rand = new Random();
            foreach (JObject originalSystemJObject in universeDataJArray) {
                JObject systemJObject = originalSystemJObject;
                JObject newSystemJObject = originalJObject;
                if ((bool)systemJObject["Randomize"]) {
                    systemJObject = randomizeSystem(originalSystemJObject, rand);
                }
                newSystemJObject["Description"]["Id"] = "starsystemdef_" + ((string)systemJObject["PlanetName"]).Replace(" ", string.Empty).Replace("'", string.Empty);
                newSystemJObject["Description"]["Name"] = systemJObject["PlanetName"];
                newSystemJObject["Description"]["Details"] = systemJObject["Description"];
                newSystemJObject["Tags"]["items"] = JArray.FromObject(createTags(systemJObject));
                newSystemJObject["FuelingStation"] = systemJObject["RechargeStation"];
                newSystemJObject["JumpDistance"] = systemJObject["JumpDistance"];
                newSystemJObject["Difficulty"] = systemJObject["Difficulty"];
                newSystemJObject["StarType"] = systemJObject["StarType"];
                newSystemJObject["Position"]["x"] = systemJObject["x"];
                newSystemJObject["Position"]["y"] = systemJObject["y"];
                newSystemJObject["Owner"] = getOwner(systemJObject);
                newSystemJObject["ContractEmployers"] = JArray.FromObject(getEmployees((string)newSystemJObject["Owner"]));
                newSystemJObject["ContractTargets"] = JArray.FromObject(getTargets((string)newSystemJObject["Owner"]));
                newSystemJObject["SupportedBiomes"] = JArray.FromObject(getBiomes(systemJObject));
                string year = "Faction3025";
                if (is3040) {
                    year = "Faction3040";
                }
                string path = OutputPath + yearFolder + systemJObject[year] + "/" + newSystemJObject["Description"]["Id"] + ".json";
                (new FileInfo(path)).Directory.Create();
                File.WriteAllText(path, newSystemJObject.ToString());

                path = OriginalData + "/" + newSystemJObject["Description"]["Id"] + ".json";
                if (!File.Exists(OriginalData + "/" + newSystemJObject["Description"]["Id"] + ".json")) {
                    string filepath = OutputPath + "/StarSystems/" + newSystemJObject["Description"]["Id"] + ".json";
                    (new FileInfo(filepath)).Directory.Create();
                    File.WriteAllText(filepath, newSystemJObject.ToString());
                }
            }
            if (keepKamea) {
                switchToRestauration();
            }
        }

        public void switchToRestauration() {
            DirectoryInfo d = new DirectoryInfo(OriginalData);
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

        public JObject randomizeSystem(JObject systemJObject, Random rand) {
            //Startype
            switch (rand.Next(0, 7)) {
                case 0: {
                        systemJObject["StarType"] = "A";
                        break;
                    }
                case 1: {
                        systemJObject["StarType"] = "B";
                        break;
                    }
                case 2: {
                        systemJObject["StarType"] = "F";
                        break;
                    }
                case 3: {
                        systemJObject["StarType"] = "G";
                        break;
                    }
                case 4: {
                        systemJObject["StarType"] = "K";
                        break;
                    }
                case 5: {
                        systemJObject["StarType"] = "M";
                        break;
                    }
                default: {
                        systemJObject["StarType"] = "0";
                        break;
                    }
            }


            //Gravity
            switch (rand.Next(0, 3)) {
                case 0: {
                        systemJObject["Gravity"] = "High Gravity Planet";
                        break;
                    }
                case 1: {
                        systemJObject["Gravity"] = "Medium Gravity Planet";
                        break;
                    }
                default: {
                        systemJObject["Gravity"] = "Low Gravity Planet";
                        break;
                    }
            }

            //Climate
            switch (rand.Next(0, 11)) {
                case 0: {
                        systemJObject["ClimateBiome"] = "Arctic World";
                        break;
                    }
                case 1: {
                        systemJObject["ClimateBiome"] = "Arid World";
                        break;
                    }
                case 2: {
                        systemJObject["ClimateBiome"] = "Desert World";
                        break;
                    }
                case 3: {
                        systemJObject["ClimateBiome"] = "Ice World";
                        break;
                    }
                case 4: {
                        systemJObject["ClimateBiome"] = "Lunar World";
                        break;
                    }
                case 5: {
                        systemJObject["ClimateBiome"] = "Martian World";
                        break;
                    }
                case 6: {
                        systemJObject["ClimateBiome"] = "Rocky World";
                        break;
                    }
                case 7: {
                        systemJObject["ClimateBiome"] = "Terran World";
                        break;
                    }
                case 8: {
                        systemJObject["ClimateBiome"] = "Tropical World";
                        break;
                    }
                case 9: {
                        systemJObject["ClimateBiome"] = "Water World";
                        break;
                    }
                default: {
                        systemJObject["ClimateBiome"] = "Barren World";
                        break;
                    }
            }

            //Population
            switch (rand.Next(0, 4)) {
                case 0: {
                        systemJObject["Population"] = "Billions";
                        break;
                    }
                case 1: {
                        systemJObject["Population"] = "Hundreds Of Millions";
                        break;
                    }
                case 2: {
                        systemJObject["Population"] = "Millions";
                        break;
                    }
                default: {
                        systemJObject["Population"] = "Less Than A Million";
                        break;
                    }
            }

            //TechLevel
            switch (rand.Next(0, 3)) {
                case 0: {
                        systemJObject["TechLevel"] = "Inner Sphere-Level Civilization";
                        break;
                    }
                case 1: {
                        systemJObject["TechLevel"] = "Periphery-Level Civilization";
                        break;
                    }
                default: {
                        systemJObject["TechLevel"] = "Primitive Civilization";
                        break;
                    }
            }

            //Resources
            switch (rand.Next(0, 3)) {
                case 0: {
                        systemJObject["Resources"] = "Many";
                        break;
                    }
                case 1: {
                        systemJObject["Resources"] = "Normal";
                        break;
                    }
                default: {
                        systemJObject["Resources"] = "Few";
                        break;
                    }
            }

            systemJObject["Difficulty"] = rand.Next(-10, 11);
            systemJObject["JumpDistance"] = rand.Next(-15, 16);

            systemJObject["NumberOfMoons"] = rand.Next(0, 4);

            systemJObject["ExtensiveVulcanism"] = randomBool(rand);
            systemJObject["PlanetwideForest"] = randomBool(rand);
            systemJObject["PlanetwideMudflats"] = randomBool(rand);
            systemJObject["DenseCloudLayer"] = randomBool(rand);
            systemJObject["DominantFungus"] = randomBool(rand);
            systemJObject["HallucinatoryVegetation"] = randomBool(rand);
            systemJObject["PlanetwideStorms"] = randomBool(rand);
            systemJObject["TaintedAtmosphere"] = randomBool(rand);
            systemJObject["Asteroids"] = randomBool(rand);
            systemJObject["Comet"] = randomBool(rand);
            systemJObject["Gasgiant"] = randomBool(rand);
            systemJObject["PlanetRings"] = randomBool(rand);
            systemJObject["MegaCity"] = randomBool(rand);
            systemJObject["RechargeStation"] = randomBool(rand);
            systemJObject["Recreation"] = randomBool(rand);
            systemJObject["Mining"] = randomBool(rand);
            systemJObject["Agriculture"] = randomBool(rand);
            systemJObject["Aquaculture"] = randomBool(rand);
            systemJObject["ResearchFacility"] = randomBool(rand);
            systemJObject["Manufactoring"] = randomBool(rand);
            systemJObject["ComstarBase"] = randomBool(rand);
            systemJObject["StarleagueRemnants"] = randomBool(rand);
            systemJObject["TradeHub"] = randomBool(rand);
            systemJObject["AlienVegetation"] = randomBool(rand);
            systemJObject["BlackMarket"] = randomBool(rand);
            systemJObject["GeothermalBoreholes"] = randomBool(rand);
            systemJObject["RecentlyColonized"] = randomBool(rand);
            systemJObject["PiratePresence"] = randomBool(rand);
            systemJObject["PrisonPlanet"] = randomBool(rand);
            systemJObject["Ruins"] = randomBool(rand);

            return systemJObject;
        }
        public bool randomBool(Random rand) {
            if(rand.Next(0,2) == 1) {
                return true;
            } else {
                return false;
            }
        }
        public List<string> getBiomes(JObject systemJObject) {
            List<string> biomeList = new List<string>();
            if (((string)systemJObject["ClimateBiome"]).Equals("Arctic World")) {
                biomeList.Add("polarFrozen");
                biomeList.Add("tundraFrozen");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Arid World")) {
                biomeList.Add("lowlandsCoastal");
                biomeList.Add("badlandsParched");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Desert World")) {
                biomeList.Add("desertParched");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Ice World")) {
                biomeList.Add("polarFrozen");
                biomeList.Add("tundraFrozen");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Lunar World")) {
                biomeList.Add("lunarVacuum");
                biomeList.Add("martianVacuum");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Martian World")) {
                biomeList.Add("martianVacuum");
                biomeList.Add("badlandsParched");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Barren World")) {
                biomeList.Add("desertParched");
                biomeList.Add("badlandsParched");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Rocky World")) {
                biomeList.Add("highlandsSpring");
                biomeList.Add("highlandsFall");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Terran World")) {
                biomeList.Add("highlandsSpring");
                biomeList.Add("highlandsFall");
                biomeList.Add("lowlandsSpring");
                biomeList.Add("lowlandsFall");
                biomeList.Add("badlandsParched");
                biomeList.Add("lowlandsCoastal");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Tropical World")) {
                biomeList.Add("lowlandsCoastal");
                biomeList.Add("lowlandsSpring");
                biomeList.Add("lowlandsFall");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Water World")) {
                biomeList.Add("lowlandsCoastal");
                biomeList.Add("highlandsSpring");
            }
            return biomeList;
        }

        public string getOwner(JObject systemJObject) {
            string year = "Faction3025";
            if (is3040) {
                year = "Faction3040";
            }
            switch ((string)systemJObject[year]) {
                case "Lyran Commonwealth":
                    return "Steiner";
                case "Federated Commonwealth (LC)":
                    return "Steiner";
                case "Free Worlds League":
                    return "Marik";
                case "Draconis Combine":
                    return "Kurita";
                case "Federated Suns":
                    return "Davion";
                case "Federated Commonwealth (FS)":
                    return "Davion";
                case "Capellan Confederation":
                    return "Liao";
                case "Aurigan Coalition":
                    return "AuriganRestoration";
                case "ComStar":
                    if (is3040) {
                        return "Locals";
                    }
                    else {
                        return "ComStar";
                    }
                case "Magistracy of Canopus":
                    return "MagistracyOfCanopus";
                case "Taurian Concordat":
                    return "TaurianConcordat";
                case "Outworlds Alliance":
                    return "Betrayers";
                case "Marian Hegemony":
                    return "AuriganDirectorate";
                case "Oberon Confederation":
                    return "MagistracyCentrella";
                case "Lothian League":
                    return "MajestyMetals";
                case "Circinus Federation":
                    return "Nautilus";
                case "Illyrian Palatinate":
                    if (is3040) {
                        return "Locals";
                    }
                    else {
                        return "AuriganMercenaries";
                    }
                case "Free Rasalhague Republic":
                    if (is3040) {
                        return "AuriganMercenaries";
                    }
                    else {
                        return "Locals";
                    }
                case "St. Ives Compact":
                    if (is3040) {
                        return "ComStar";
                    }
                    else {
                        return "Locals";
                    }
                case "Abandoned":
                    return "NoFaction";
                case "Undiscovered":
                    return "NoFaction";
                default:
                    return "Locals";
            }
        }
        public List<string> createTags(JObject systemJObject) {
            string year = "Faction3025";
            if (is3040) {
                year = "Faction3040";
            }
            List<string> tagList = new List<string>();

            //Nametag
            tagList.Add("planet_name_" + ((string)systemJObject["PlanetName"]).Replace(" ","").ToLower());

            //planetSize
            if (((string)systemJObject["Gravity"]).Equals("Medium Gravity Planet")) {
                tagList.Add("planet_size_medium");
            }
            else if (((string)systemJObject["Gravity"]).Equals("High Gravity Planet")) {
                tagList.Add("planet_size_large");
            }
            else if (((string)systemJObject["Gravity"]).Equals("Low Gravity Planet")) {
                tagList.Add("planet_size_small");
            }

            //Climate
            if (((string)systemJObject["ClimateBiome"]).Equals("Arctic World")) {
                tagList.Add("planet_climate_arctic");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Arid World")) {
                tagList.Add("planet_climate_arid");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Desert World")) {
                tagList.Add("planet_climate_desert");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Ice World")) {
                tagList.Add("planet_climate_ice");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Lunar World")) {
                tagList.Add("planet_climate_lunar");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Martian World")) {
                tagList.Add("planet_climate_mars");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Barren World")) {
                tagList.Add("planet_climate_lunar");

                //Currently broken
                //tagList.Add("planet_climate_moon");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Rocky World")) {
                tagList.Add("planet_climate_rocky");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Terran World")) {
                tagList.Add("planet_climate_terran");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Tropical World")) {
                tagList.Add("planet_climate_tropical");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Water World")) {
                tagList.Add("planet_climate_water");
            }

            //Vulcans
            if ((bool)systemJObject["ExtensiveVulcanism"]) {
                tagList.Add("planet_other_volcanic");
            }

            //Forrest
            if ((bool)systemJObject["PlanetwideForest"]) {
                tagList.Add("planet_other_megaforest");
            }

            //Mudflats
            if ((bool)systemJObject["PlanetwideMudflats"]) {
                tagList.Add("planet_other_mudflats");
            }

            //Cloud Layer
            if ((bool)systemJObject["DenseCloudLayer"]) {
                tagList.Add("planet_other_floatingworld");
            }

            //Fungus
            if ((bool)systemJObject["DominantFungus"]) {
                tagList.Add("planet_other_fungus");
            }

            //Hallucinatory
            if ((bool)systemJObject["HallucinatoryVegetation"]) {
                tagList.Add("planet_other_stonedcaribou");
            }

            //Storms
            if ((bool)systemJObject["PlanetwideStorms"]) {
                tagList.Add("planet_other_storms");
            }

            //Poisoned Athmosphere
            if ((bool)systemJObject["TaintedAtmosphere"]) {
                tagList.Add("planet_other_taintedair");
            }

            //Asteroids
            if ((bool)systemJObject["Asteroids"]) {
                tagList.Add("planet_feature_asteroids");
            }

            //Comet
            if ((bool)systemJObject["Comet"]) {
                tagList.Add("planet_feature_comet");
            }

            //Gasgiant
            if ((bool)systemJObject["Gasgiant"]) {
                tagList.Add("planet_feature_gasgiant");
            }

            //PlanetRings
            if ((bool)systemJObject["PlanetRings"]) {
                tagList.Add("planet_feature_rings");
            }

            //NumberOfMoons
            if ((int)systemJObject["NumberOfMoons"] > 0) {
                if ((int)systemJObject["NumberOfMoons"] > 1) {
                    if ((int)systemJObject["NumberOfMoons"] > 2) {
                        tagList.Add("planet_feature_moon03");
                        tagList.Add("planet_other_moon");
                    }
                    else {
                        tagList.Add("planet_feature_moon02");
                        tagList.Add("planet_other_moon");
                    }
                }
                else {
                    tagList.Add("planet_feature_moon01");
                    tagList.Add("planet_other_moon");
                }
            }

            //Population
            if (!((string)systemJObject[year]).Equals("Abandoned") || !((string)systemJObject[year]).Equals("Undiscovered")) {
                if (((string)systemJObject["Population"]).Equals("Billions")) {
                    tagList.Add("planet_pop_large");
                }
                else if (((string)systemJObject["Population"]).Equals("Hundreds Of Millions")) {
                    tagList.Add("planet_pop_medium");
                }
                else if (((string)systemJObject["Population"]).Equals("Millions")) {
                    tagList.Add("planet_pop_small");
                }
                else if (((string)systemJObject["Population"]).Equals("Less Than A Million")) {
                    tagList.Add("planet_pop_none");
                }
            } else {
                tagList.Add("planet_pop_none");
            }

            //MegaCity
            if (!((string)systemJObject[year]).Equals("Abandoned") || !((string)systemJObject[year]).Equals("Undiscovered")) {
                if ((bool)systemJObject["MegaCity"]) {
                    tagList.Add("planet_other_megacity");
                }
            }

            //CapitalSystem
            if (!((string)systemJObject[year]).Equals("Abandoned") || !((string)systemJObject[year]).Equals("Undiscovered")) {
                if ((bool)systemJObject["CapitalSystem"]) {
                    tagList.Add("planet_other_capital");
                }
            }

            //TechLevel
            if (!((string)systemJObject[year]).Equals("Abandoned") || !((string)systemJObject[year]).Equals("Undiscovered")) {
                if (((string)systemJObject["TechLevel"]).Equals("Inner Sphere-Level Civilization")) {
                    tagList.Add("planet_civ_innersphere");
                }
                else if (((string)systemJObject["TechLevel"]).Equals("Periphery - Level Civilization")) {
                    tagList.Add("planet_civ_periphery");
                }
                else if (((string)systemJObject["TechLevel"]).Equals("Primitive Civilization")) {
                    tagList.Add("planet_civ_primitive");
                }
            } else {
                tagList.Add("planet_civ_primitive");
            }

            //Resources
            if (((string)systemJObject["Resources"]).Equals("Many")) {
                tagList.Add("planet_industry_rich");
            }
            else if (((string)systemJObject["Resources"]).Equals("Few")) {
                tagList.Add("planet_industry_poor");
            }

            //Recreation
            if (!((string)systemJObject[year]).Equals("Abandoned") || !((string)systemJObject[year]).Equals("Undiscovered")) {
                if ((bool)systemJObject["Recreation"]) {
                    tagList.Add("planet_industry_recreation");
                }
            }

            //Mining
            if (!((string)systemJObject[year]).Equals("Abandoned") || !((string)systemJObject[year]).Equals("Undiscovered")) {
                if ((bool)systemJObject["Mining"]) {
                    tagList.Add("planet_industry_mining");
                }
            }

            //Agriculture
            if (!((string)systemJObject[year]).Equals("Abandoned") || !((string)systemJObject[year]).Equals("Undiscovered")) {
                if ((bool)systemJObject["Agriculture"]) {
                    tagList.Add("planet_industry_agriculture");
                }
            }

            //Aquaculture
            if (!((string)systemJObject[year]).Equals("Abandoned") || !((string)systemJObject[year]).Equals("Undiscovered")) {
                if ((bool)systemJObject["Aquaculture"]) {
                    tagList.Add("planet_industry_aquaculture");
                }
            }

            //ResearchFacility
            if (!((string)systemJObject[year]).Equals("Abandoned") || !((string)systemJObject[year]).Equals("Undiscovered")) {
                if ((bool)systemJObject["ResearchFacility"]) {
                    tagList.Add("planet_industry_research");
                }
            }

            //Manufactoring
            if (!((string)systemJObject[year]).Equals("Abandoned") || !((string)systemJObject[year]).Equals("Undiscovered")) {
                if ((bool)systemJObject["Manufactoring"]) {
                    tagList.Add("planet_industry_manufacturing");
                }
            }

            //ComstarBase
            if (!((string)systemJObject[year]).Equals("Abandoned") || !((string)systemJObject[year]).Equals("Undiscovered")) {
                if ((bool)systemJObject["ComstarBase"]) {
                    tagList.Add("planet_other_comstar");
                }
            }

            //StarleagueRemnants
            if ((bool)systemJObject["StarleagueRemnants"]) {
                tagList.Add("planet_other_starleague");
            }

            //TradeHub
            if (!((string)systemJObject[year]).Equals("Abandoned") || !((string)systemJObject[year]).Equals("Undiscovered")) {
                if ((bool)systemJObject["TradeHub"]) {
                    tagList.Add("planet_other_hub");
                }
            }

            //AlienVegetation
            if ((bool)systemJObject["AlienVegetation"]) {
                tagList.Add("planet_other_alienvegetation");
            }

            //BlackMarket
            if (!((string)systemJObject[year]).Equals("Abandoned") || !((string)systemJObject[year]).Equals("Undiscovered")) {
                if ((bool)systemJObject["BlackMarket"]) {
                    tagList.Add("planet_other_blackmarket");
                }
            }

            //GeothermalBoreholes
            if ((bool)systemJObject["GeothermalBoreholes"]) {
                tagList.Add("planet_other_boreholes");
            }

            //RecentlyColonized
            if (!((string)systemJObject[year]).Equals("Abandoned") || !((string)systemJObject[year]).Equals("Undiscovered")) {
                if ((bool)systemJObject["RecentlyColonized"]) {
                    tagList.Add("planet_other_newcolony");
                }
            }

            //PiratePresence
            if ((bool)systemJObject["PiratePresence"]) {
                tagList.Add("planet_other_pirate");
            }

            //PrisonPlanet
            if (!((string)systemJObject[year]).Equals("Abandoned") || !((string)systemJObject[year]).Equals("Undiscovered")) {
                if ((bool)systemJObject["PrisonPlanet"]) {
                    tagList.Add("planet_other_prison");
                }
            }

            //Ruins
            if ((bool)systemJObject["Ruins"]) {
                tagList.Add("planet_ruins");
            }

            //FactionTag
            switch ((string)systemJObject[year]) {
                case "Lyran Commonwealth":
                    tagList.Add("planet_faction_steiner");
                    break;
                case "Federated Commonwealth (LC)":
                    tagList.Add("planet_faction_steiner");
                    break;
                case "Free Worlds League":
                    tagList.Add("planet_faction_marik");
                    break;
                case "Draconis Combine":
                    tagList.Add("planet_faction_kurita");
                    break;
                case "Federated Suns":
                    tagList.Add("planet_faction_davion");
                    break;
                case "Federated Commonwealth (FS)":
                    tagList.Add("planet_faction_davion");
                    break;
                case "Capellan Confederation":
                    tagList.Add("planet_faction_liao");
                    break;
                case "Aurigan Coalition":
                    tagList.Add("planet_faction_restoration");
                    break;
                case "ComStar":
                    tagList.Add("planet_civ_innersphere");
                    break;
                case "Magistracy of Canopus":
                    tagList.Add("planet_faction_magistracy");
                    break;
                case "Taurian Concordat":
                    tagList.Add("planet_faction_taurian");
                    break;
                case "Outworlds Alliance":
                    tagList.Add("planet_faction_outworlds");
                    break;
                case "Marian Hegemony":
                    tagList.Add("planet_faction_marian");
                    break;
                case "Oberon Confederation":
                    tagList.Add("planet_faction_oberon");
                    break;
                case "Lothian League":
                    tagList.Add("planet_faction_lothian");
                    break;
                case "Circinus Federation":
                    tagList.Add("planet_faction_circinus");
                    break;
                case "Illyrian Palatinate":
                    tagList.Add("planet_faction_illyrian");
                    break;
                case "Abandoned":
                    tagList.Add(" planet_other_empty");
                    break;
                case "Undiscovered":
                    tagList.Add(" planet_other_empty");
                    break;
                default:
                    break;
            }
            return tagList;
        }

        public static List<string> getEmployees(string faction) {
            List<string> employees = new List<string>();
            if (faction.Equals("NoFaction")) {
                return employees;
            }
            employees.Add(faction);
            employees.Add("Locals");
            return employees;
        }

        public static List<string> getTargets(string faction) {
            List<string> targets = AllFactions;
            if (faction.Equals("NoFaction")) {
                return targets;
            }
            targets.Remove(faction);
            return targets;
        }

    }
}
