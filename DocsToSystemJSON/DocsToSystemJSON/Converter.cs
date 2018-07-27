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

        public Converter(string dataPath, string arrayName, string OutputPath, string BlueprintPath, bool is3040) {

            JObject jobject = JObject.Parse(File.ReadAllText(dataPath));
            this.universeDataJArray = (JArray)jobject[arrayName];
            this.OutputPath = OutputPath;
            this.BlueprintPath = BlueprintPath;
            this.is3040 = is3040;
        }

        public void newMap() {
            string beginjson = File.ReadAllText(BlueprintPath);
            JObject originalJObject = JObject.Parse(beginjson);
            foreach (JObject systemJObject in universeDataJArray) {
                JObject newSystemJObject = originalJObject;
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
                newSystemJObject["SupportedBiomes"] = JArray.FromObject(getBiomes(systemJObject));
                (new FileInfo(OutputPath + "/Locals/" + newSystemJObject["Description"]["Id"] + ".json")).Directory.Create();
                File.WriteAllText(OutputPath + "/Locals/" + newSystemJObject["Description"]["Id"] + ".json", newSystemJObject.ToString());
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
                case "Free Worlds League":
                    return "Marik";
                case "Draconis Combine":
                    return "Kurita";
                case "Federated Suns":
                    return "Davion";
                case "Capellan Confederation":
                    return "Liao";
                case "Aurigan Coalition":
                    return "AuriganRestoration";
                case "ComStar":
                    return "ComStar";
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
                    return "AuriganMercenaries";
                case "Abandoned":
                    return "NoFaction";
                case "Undiscovered":
                    return "NoFaction";
                default:
                   return "Locals";
            }
        }
        public List<string> createTags(JObject systemJObject) {
            List<string> tagList = new List<string>();

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
                tagList.Add("planet_climate_moon");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Rocky World")) {
                tagList.Add("planet_climate_rocky");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Terran World")) {
                tagList.Add("planet_climate_terran");
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
                    }
                    else {
                        tagList.Add("planet_feature_moon02");
                    }
                }
                else {
                    tagList.Add("planet_feature_moon01");
                }
            }

            //Population
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

            //MegaCity
            if ((bool)systemJObject["MegaCity"]) {
                tagList.Add("planet_other_megacity");
            }

            //CapitalSystem
            if ((bool)systemJObject["CapitalSystem"]) {
                tagList.Add("planet_other_capital");
            }

            //TechLevel
            if (((string)systemJObject["TechLevel"]).Equals("Inner Sphere-Level Civilization")) {
                tagList.Add("planet_civ_innersphere");
            }
            else if (((string)systemJObject["TechLevel"]).Equals("Periphery - Level Civilization")) {
                tagList.Add("planet_civ_periphery");
            }
            else if (((string)systemJObject["TechLevel"]).Equals("Primitive Civilization")) {
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
            if ((bool)systemJObject["Recreation"]) {
                tagList.Add("planet_industry_recreation");
            }

            //Mining
            if ((bool)systemJObject["Mining"]) {
                tagList.Add("planet_industry_mining");
            }

            //Agriculture
            if ((bool)systemJObject["Agriculture"]) {
                tagList.Add("planet_industry_agriculture");
            }

            //Aquaculture
            if ((bool)systemJObject["Aquaculture"]) {
                tagList.Add("planet_industry_aquaculture");
            }

            //ResearchFacility
            if ((bool)systemJObject["ResearchFacility"]) {
                tagList.Add("planet_industry_research");
            }

            //Manufactoring
            if ((bool)systemJObject["Manufactoring"]) {
                tagList.Add("planet_industry_manufacturing");
            }

            //ComstarBase
            if ((bool)systemJObject["ComstarBase"]) {
                tagList.Add("planet_other_comstar");
            }

            //StarleagueRemnants
            if ((bool)systemJObject["StarleagueRemnants"]) {
                tagList.Add("planet_other_starleague");
            }

            //TradeHub
            if ((bool)systemJObject["TradeHub"]) {
                tagList.Add("planet_other_hub");
            }

            //AlienVegetation
            if ((bool)systemJObject["AlienVegetation"]) {
                tagList.Add("planet_other_alienvegetation");
            }

            //BlackMarket
            if ((bool)systemJObject["BlackMarket"]) {
                tagList.Add("planet_other_blackmarket");
            }

            //GeothermalBoreholes
            if ((bool)systemJObject["GeothermalBoreholes"]) {
                tagList.Add("planet_other_boreholes");
            }

            //GeothermalBoreholes
            if ((bool)systemJObject["RecentlyColonized"]) {
                tagList.Add("planet_other_newcolony");
            }

            //GeothermalBoreholes
            if ((bool)systemJObject["PiratePresence"]) {
                tagList.Add("planet_other_pirate");
            }

            //PrisonPlanet
            if ((bool)systemJObject["PrisonPlanet"]) {
                tagList.Add("planet_other_prison");
            }

            //Ruins
            if ((bool)systemJObject["Ruins"]) {
                tagList.Add("planet_ruins");
            }

            //FactionTag
            string year = "Faction3025";
            if (is3040) {
                year = "Faction3040";
            }
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
                default:
                    break;
            }
            return tagList;
        }

    }
}
