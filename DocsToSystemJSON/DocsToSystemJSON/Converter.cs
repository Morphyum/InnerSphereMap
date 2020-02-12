using BattleTech;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace DocsToSystemJSON
{


    class Converter
    {
        private int JumpPointCount = 0;
        private JArray universeDataJArray;
        private string OutputPath;
        private string BlueprintPath;
        private int year;
        private bool createJumpPoints;
        private string yearFolder = "/IS3025/";
        private string OriginalData;
        private bool keepKamea;
        private string galaxyPath;

        public Converter(string dataPath, string arrayName, string OutputPath, string BlueprintPath, int year, string OriginalData, bool keepKamea, bool createJumpPoints, string galaxyPath) {

            JObject jobject = JObject.Parse(File.ReadAllText(dataPath));
            this.universeDataJArray = (JArray)jobject[arrayName];
            this.OutputPath = OutputPath;
            this.BlueprintPath = BlueprintPath;
            this.year = year;
            this.OriginalData = OriginalData;
            this.keepKamea = keepKamea;
            this.createJumpPoints = createJumpPoints;
            if (year == 1) {
                this.yearFolder = "/IS3040/";
            }
            if (year == 2) {
                this.yearFolder = "/IS3063/";
            }
            this.galaxyPath = galaxyPath;
        }

        public List<string> getAllFactions() {
            if (this.year == 0) {
                return new List<string>() { "Steiner","Marik","Kurita","Davion","Liao","AuriganRestoration","ComStar",
            "MagistracyOfCanopus","TaurianConcordat","Outworld","Marian","Oberon","Lothian","Circinus", "Illyrian","Rasalhague","Ives","Axumite",
            "Castile","Chainelane","ClanBurrock","ClanCloudCobra","ClanCoyote","ClanDiamondShark","ClanFireMandrill","ClanGhostBear","ClanGoliathScorpion",
            "ClanHellsHorses","ClanIceHellion","ClanJadeFalcon","ClanNovaCat","ClansGeneric","ClanSmokeJaguar","ClanSnowRaven","ClanStarAdder",
            "ClanSteelViper","ClanWolf","Delphi","Elysia","Hanse","JarnFolk","Tortuga","Valkyrate","NoFaction","Locals", "AuriganDirectorate", "AuriganPirates"  };
            }
            if (this.year == 1) {
                return new List<string>() { "Steiner","Marik","Kurita","Davion","Liao","AuriganRestoration","ComStar",
            "MagistracyOfCanopus","TaurianConcordat","Outworld","Marian","Oberon","Lothian","Circinus", "Illyrian","Rasalhague","Ives","Axumite",
            "Castile","Chainelane","ClanBurrock","ClanCloudCobra","ClanCoyote","ClanDiamondShark","ClanFireMandrill","ClanGhostBear","ClanGoliathScorpion",
            "ClanHellsHorses","ClanIceHellion","ClanJadeFalcon","ClanNovaCat","ClansGeneric","ClanSmokeJaguar","ClanSnowRaven","ClanStarAdder",
            "ClanSteelViper","ClanWolf","Delphi","Elysia","Hanse","JarnFolk","Tortuga","Valkyrate","NoFaction","Locals", "AuriganDirectorate", "AuriganPirates" };
            }
            if (this.year == 2) {
                return new List<string>() { "Steiner","Marik","Kurita","Davion","Liao","AuriganRestoration","ComStar",
            "MagistracyOfCanopus","TaurianConcordat","Outworld","Marian","Oberon","Lothian","Circinus", "Illyrian","Rasalhague","Ives","Axumite",
            "Castile","Chainelane","ClanBurrock","ClanCloudCobra","ClanCoyote","ClanDiamondShark","ClanFireMandrill","ClanGhostBear","ClanGoliathScorpion",
            "ClanHellsHorses","ClanIceHellion","ClanJadeFalcon","ClanNovaCat","ClansGeneric","ClanSmokeJaguar","ClanSnowRaven","ClanStarAdder",
            "ClanSteelViper","ClanWolf","Delphi","Elysia","Hanse","JarnFolk","Tortuga","Valkyrate","NoFaction","Locals", "AuriganDirectorate", "AuriganPirates","WordOfBlake","Rim"  };
            }
            return null;
        }


        public void makeGalaxyJson() {
            string beginjson = File.ReadAllText(galaxyPath);
            JObject modJson = JObject.Parse(beginjson);
            JObject BonusAttackResources = JObject.FromObject(modJson["Settings"]["BonusAttackResources"]);
            BonusAttackResources = new JObject();
            foreach (string faction in getAllFactions()) {
                BonusAttackResources.Add(faction, 100);
            }
            modJson["Settings"]["BonusAttackResources"] = BonusAttackResources;

            JObject BonusDefensiveResources = JObject.FromObject(modJson["Settings"]["BonusDefensiveResources"]);
            BonusDefensiveResources = new JObject();
            foreach (string faction in getAllFactions()) {
                BonusDefensiveResources.Add(faction, -200);
            }
            modJson["Settings"]["BonusDefensiveResources"] = BonusDefensiveResources;

            JObject LogoNames = JObject.FromObject(modJson["Settings"]["LogoNames"]);
            LogoNames = new JObject();
            foreach (string faction in getAllFactions()) {
                LogoNames.Add(faction, faction.ToLower() + "Logo");
            }
            modJson["Settings"]["LogoNames"] = LogoNames;

            JObject FactionNames = JObject.FromObject(modJson["Settings"]["FactionNames"]);
            FactionNames = new JObject();
            foreach (string faction in getAllFactions()) {
                FactionNames.Add(faction, "FULL NAME NEEDED");
            }
            modJson["Settings"]["FactionNames"] = FactionNames;

            JObject FactionStrings = JObject.FromObject(modJson["Settings"]["FactionStrings"]);
            FactionStrings = new JObject();
            foreach (string faction in getAllFactions()) {
                FactionStrings.Add(faction, faction);
            }
            modJson["Settings"]["FactionStrings"] = FactionStrings;

            JArray IncludedFactions = JArray.FromObject(modJson["Settings"]["IncludedFactions"]);
            IncludedFactions = new JArray();
            foreach (string faction in getAllFactions()) {
                IncludedFactions.Add(faction);
            }
            modJson["Settings"]["IncludedFactions"] = IncludedFactions;

            JObject FactionShopItems = JObject.FromObject(modJson["Settings"]["FactionShopItems"]);
            FactionShopItems = new JObject();
            foreach (string faction in getAllFactions()) {
                FactionShopItems.Add(faction, "itemCollection_faction_" + faction);
            }
            modJson["Settings"]["FactionShopItems"] = FactionShopItems;

            JObject FactionShops = JObject.FromObject(modJson["Settings"]["FactionShops"]);
            FactionShops = new JObject();
            foreach (string faction in getAllFactions()) {
                FactionShops.Add(faction, "itemCollection_minor_" + faction);
            }
            modJson["Settings"]["FactionShops"] = FactionShops;

            JObject FactionTags = JObject.FromObject(modJson["Settings"]["FactionTags"]);
            FactionTags = new JObject();
            foreach (string faction in getAllFactions()) {
                FactionTags.Add(faction, "planet_faction_" + faction.ToString().ToLower());
            }
            modJson["Settings"]["FactionTags"] = FactionTags;

            File.WriteAllText(galaxyPath, modJson.ToString());
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
                newSystemJObject["Description"]["Id"] = "starsystemdef_" + ((string)systemJObject["PlanetName"]).Replace(" ", string.Empty).Replace("'", string.Empty)
                    .Replace("#", string.Empty).Replace("ó", "o").Replace("á", "a").Replace("ì", "i").Replace("å", "a").Replace("é", "e").Replace("ä", "ae").Replace("ü", "ue")
                    .Replace("ö", "oe");
                newSystemJObject["CoreSystemID"] = "starsystemdef_" + ((string)systemJObject["PlanetName"]).Replace(" ", string.Empty).Replace("'", string.Empty)
                    .Replace("#", string.Empty).Replace("ó", "o").Replace("á", "a").Replace("ì", "i").Replace("å", "a").Replace("é", "e").Replace("ä", "ae").Replace("ü", "ue")
                    .Replace("ö", "oe");
                newSystemJObject["Description"]["Name"] = systemJObject["PlanetName"];
                newSystemJObject["Description"]["Details"] = systemJObject["Description"];
                newSystemJObject["Tags"]["items"] = JArray.FromObject(createTags(systemJObject));
                newSystemJObject["FuelingStation"] = (bool)systemJObject["RechargeStation"];
                newSystemJObject["JumpDistance"] = systemJObject["JumpDistance"];
                newSystemJObject["DefaultDifficulty"] = systemJObject["Difficulty"];
                newSystemJObject["DifficultyList"] = JArray.FromObject(new List<int>());
                newSystemJObject["DifficultyModes"] = JArray.FromObject(new List<string>());
                newSystemJObject["StarType"] = systemJObject["StarType"];
                newSystemJObject["Position"]["x"] = systemJObject["x"];
                newSystemJObject["Position"]["y"] = systemJObject["y"];
                newSystemJObject["ownerID"] = getOwner(systemJObject);
                newSystemJObject["factionShopOwnerID"] = getOwner(systemJObject);
                newSystemJObject["contractEmployerIDs"] = JArray.FromObject(getEmployees((string)newSystemJObject["ownerID"]));
                newSystemJObject["contractTargetIDs"] = JArray.FromObject(getTargets((string)newSystemJObject["ownerID"]));
                newSystemJObject["SupportedBiomes"] = JArray.FromObject(getBiomes(systemJObject));
                string year = "Faction3025";
                if (this.year == 1) {
                    year = "Faction3040";
                }
                if (this.year == 2) {
                    year = "Faction3063";
                }
                string path = OutputPath + yearFolder + systemJObject[year] + "/" + newSystemJObject["Description"]["Id"] + ".json";
                (new FileInfo(path)).Directory.Create();
                File.WriteAllText(path, newSystemJObject.ToString());

                path = OriginalData + "/" + newSystemJObject["Description"]["Id"] + ".json";
                if (!File.Exists(path) && !((string)newSystemJObject["Description"]["Id"]).Contains("(HBS)") && !((string)newSystemJObject["Description"]["Id"]).Contains("Alexandria(CC)") &&
                    !((string)newSystemJObject["Description"]["Id"]).Contains("Itsbur") &&
                    !((string)newSystemJObject["Description"]["Id"]).Contains("LiusMemory") &&
                    !((string)newSystemJObject["Description"]["Id"]).Contains("Murris")
                    && !((string)newSystemJObject["Description"]["Id"]).Contains("Tincalunas")
                    && !((string)newSystemJObject["Description"]["Id"]).Contains("Untran")
                    && !((string)newSystemJObject["Description"]["Id"]).Contains("Cruinne")
                    && !((string)newSystemJObject["Description"]["Id"]).Contains("WyethsGlory")
                    && !((string)newSystemJObject["Description"]["Id"]).Contains("Zathras")
                    && !((string)newSystemJObject["Description"]["Id"]).Contains("AmuDarya")
                    ) {
                    string filepath = OutputPath + "/StarSystems/" + newSystemJObject["Description"]["Id"] + ".json";
                    (new FileInfo(filepath)).Directory.Create();
                    File.WriteAllText(filepath, newSystemJObject.ToString());
                }
            }
            if (keepKamea) {
                switchToRestauration();
            }
            if (createJumpPoints) {
                CreateJumpPoints();
            }
        }
        private void CreateJumpPoints() {
            DirectoryInfo d = new DirectoryInfo(OriginalData);

            //Axumite Viroflay to Thala
            JObject startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Viroflay.json"));
            JObject goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Thala.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //Hanse ChaineCluster to Stralsund
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_ChaineCluster.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Stralsund.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //Castille _Novgorod to Asturias
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Novgorod.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Asturias.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //JarnFolk Nowhere to Ålborg
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Nowhere.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Ålborg.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //Delphi Rover to Dania
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Rover.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Dania.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //Tortuga Ulvskollen to NewPortRoyal
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Ulvskollen.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_NewPortRoyal.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //EXODUS1 Cabanatuan to Columbus
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Cabanatuan.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_EpsilonPegasus(Columbus).json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);
            //EXODUS1 Cabanatuan to Columbus
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Cabanatuan.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_EpsilonPegasus(Columbus).json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //EXODUS2 Columbus to Salonika
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_EpsilonPegasus(Columbus).json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Salonika.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);
            //EXODUS2 Columbus to Salonika
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_EpsilonPegasus(Columbus).json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Salonika.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //EXODUS3 Salonika to StarClusterA51
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Salonika.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_StarClusterA51.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);
            //EXODUS3 Salonika to StarClusterA51
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Salonika.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_StarClusterA51.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //EXODUS4 StarClusterA51 to StarClusterP12
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_StarClusterA51.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_StarClusterP12.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);
            //EXODUS4 StarClusterA51 to StarClusterP12
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_StarClusterA51.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_StarClusterP12.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //EXODUS5 StarClusterP12 to Arcadia
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_StarClusterP12.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Arcadia(Clan).json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);
            //EXODUS5 StarClusterP12 to Arcadia
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_StarClusterP12.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Arcadia(Clan).json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //Clans Arcadia to Shadow
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Arcadia(Clan).json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Shadow.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //Hanse Noise1 Wismar to Greifswald
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Wismar.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Greifswald.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //Hanse Noise2 Novgorod to Gateway
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Novgorod.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Gateway.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //SHARKROUTE1 Paxon to Colleen
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Paxon.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Colleen.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);
            //SHARKROUTE1 Paxon to Colleen
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Paxon.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Colleen.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //SHARKROUTE2 Colleen to EC821-387D
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Colleen.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_EC821-387D.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);
            //SHARKROUTE2 Colleen to EC821-387D
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Colleen.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_EC821-387D.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //SHARKROUTE3 EC821-387D to StarClusterP24
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_EC821-387D.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_StarClusterP24.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);
            //SHARKROUTE3 EC821-387D to StarClusterP24
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_EC821-387D.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_StarClusterP24.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //SHARKROUTE4 StarClusterP24 to Waystation531
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_StarClusterP24.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Waystation531.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);
            //SHARKROUTE4 StarClusterP24 to Waystation531
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_StarClusterP24.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Waystation531.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //SHARKROUTE5 Waystation531 to Granada
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Waystation531.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Granada.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);
            //SHARKROUTE5 Waystation531 to Granada
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Waystation531.json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Granada.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);

            //Wobby
            startJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Hope(Randis2988+).json"));
            goalJOBject = JObject.Parse(File.ReadAllText(OutputPath + "/StarSystems/starsystemdef_Baltar.json"));
            CreateJumpPointsToTarget((float)startJOBject["Position"]["x"], (float)startJOBject["Position"]["y"], (float)goalJOBject["Position"]["x"], (float)goalJOBject["Position"]["y"]);
        }

        private void CreateJumpPointsToTarget(float startx, float starty, float goalx, float goaly) {

            Random rand = new Random();
            while (GetDistanceInLY(startx, starty, goalx, goaly) > 50) {
                JumpPointCount++;
                bool created = false;
                while (!created) {
                    float newx;
                    float newy;
                    if (startx < goalx)
                        newx = startx + rand.Next(10, 51);
                    else
                        newx = startx - rand.Next(10, 51);
                    if (starty < goaly)
                        newy = starty + rand.Next(10, 51);
                    else
                        newy = starty - rand.Next(10, 51);
                    if (GetDistanceInLY(startx, starty, newx, newy) < 50) {
                        string beginjson = File.ReadAllText(BlueprintPath);
                        JObject originalJObject = JObject.Parse(beginjson);
                        JObject newSystemJObject = originalJObject;
                        newSystemJObject["Description"]["Id"] =
                        newSystemJObject["CoreSystemID"] = "starsystemdef_JumpPoint" + JumpPointCount;
                        newSystemJObject["Description"]["Name"] = "Jump Point" + JumpPointCount;
                        newSystemJObject["Description"]["Details"] = "This system contains near to nothing. It's whole purpose is to refill your jumpdrive.";
                        newSystemJObject["Tags"]["items"] = JArray.FromObject(new List<string> { "planet_size_small", "planet_climate_lunar", "planet_pop_none", "planet_name_" + "Jump Point" + JumpPointCount });
                        newSystemJObject["FuelingStation"] = false;
                        newSystemJObject["JumpDistance"] = 30;
                        newSystemJObject["DefaultDifficulty"] = 1;
                        newSystemJObject["DifficultyList"] = JArray.FromObject(new List<int>());
                        newSystemJObject["DifficultyModes"] = JArray.FromObject(new List<string>());
                        newSystemJObject["StarType"] = "M";
                        newSystemJObject["Position"]["x"] = newx;
                        newSystemJObject["Position"]["y"] = newy;
                        newSystemJObject["ownerID"] = "NoFaction";
                        newSystemJObject["contractEmployerIDs"] = JArray.FromObject(getEmployees("NoFaction"));
                        newSystemJObject["contractTargetIDs"] = JArray.FromObject(getTargets("NoFaction"));
                        newSystemJObject["SupportedBiomes"] = JArray.FromObject(new List<string> { "lunarVacuum", "martianVacuum" });
                        string path = OutputPath + yearFolder + "JumpPoints" + "/" + newSystemJObject["Description"]["Id"] + ".json";
                        (new FileInfo(path)).Directory.Create();
                        File.WriteAllText(path, newSystemJObject.ToString());

                        path = OriginalData + "/" + newSystemJObject["Description"]["Id"] + ".json";
                        if (!File.Exists(path)) {
                            string filepath = OutputPath + "/StarSystems/" + newSystemJObject["Description"]["Id"] + ".json";
                            (new FileInfo(filepath)).Directory.Create();
                            File.WriteAllText(filepath, newSystemJObject.ToString());
                        }
                        startx = newx;
                        starty = newy;
                        created = true;
                    }
                }
            }
        }

        public static double GetDistanceInLY(float x1, float y1, float x2, float y2) {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
        public void switchToRestauration() {
            DirectoryInfo d = new DirectoryInfo(OriginalData);
            foreach (var file in d.GetFiles("*.json")) {
                JObject systemJOBject = JObject.Parse(File.ReadAllText(file.FullName));
                if (((string)systemJOBject["ownerID"]).Equals("AuriganDirectorate")) {
                    systemJOBject["ownerID"] = "AuriganRestoration";
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

            systemJObject["Difficulty"] = rand.Next(1, 11);
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
            systemJObject["CapitalSystem"] = randomBool(rand);
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
            if (rand.Next(0, 2) == 1) {
                return true;
            }
            else {
                return false;
            }
        }

        //jungleTropical

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
                biomeList.Add("jungleTropical");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Tropical World")) {
                biomeList.Add("jungleTropical");
            }
            else if (((string)systemJObject["ClimateBiome"]).Equals("Water World")) {
                biomeList.Add("lowlandsCoastal");
                biomeList.Add("highlandsSpring");
            }
            if ((bool)systemJObject["MegaCity"]) {
                biomeList.Add("urbanHighTech");
            }
            return biomeList;
        }
        public string getOwner(string year, JObject systemJObject) {
            string faction = (string)systemJObject[year];
            switch (faction) {
                case "Lyran Commonwealth":
                case "Federated Commonwealth (LC)":
                case "Lyran Alliance":
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
                    return "ComStar";
                case "Magistracy of Canopus":
                    return "MagistracyOfCanopus";
                case "Taurian Concordat":
                    return "TaurianConcordat";
                case "Outworlds Alliance":
                    return "Outworld";
                case "Marian Hegemony":
                    return "Marian";
                case "Oberon Confederation":
                    return "Oberon";
                case "Lothian League":
                    return "Lothian";
                case "Circinus Federation":
                    return "Circinus";
                case "Illyrian Palatinate":
                    return "Illyrian";
                case "Free Rasalhague Republic":
                    return "Rasalhague";
                case "St. Ives Compact":
                    return "Ives";
                case "Axumite Providence":
                    return "Axumite";
                case "Nueva Castile":
                    return "Castile";
                case "Chainelane Isles":
                    return "Chainelane";
                case "Clan Burrock":
                    return "ClanBurrock";
                case "Clan Cloud Cobra":
                    return "ClanCloudCobra";
                case "Clan Coyote":
                    return "ClanCoyote";
                case "Clan Diamond Shark":
                    return "ClanDiamondShark";
                case "Clan Fire Mandrill":
                    return "ClanFireMandrill";
                case "Clan Ghost Bear":
                case "Ghost Bear Dominion":
                    return "ClanGhostBear";
                case "Clan Goliath Scorpion":
                    return "ClanGoliathScorpion";
                case "Clan Hell's Horses":
                    return "ClanHellsHorses";
                case "Clan Ice Hellion":
                    return "ClanIceHellion";
                case "Clan Jade Falcon":
                    return "ClanJadeFalcon";
                case "Clan Nova Cat":
                    return "ClanNovaCat";
                case "Clans":
                    return "ClansGeneric";
                case "Clan Smoke Jaguar":
                    return "ClanSmokeJaguar";
                case "Clan Snow Raven":
                    return "ClanSnowRaven";
                case "Clan Star Adder":
                    return "ClanStarAdder";
                case "Clan Steel Viper":
                    return "ClanSteelViper";
                case "Clan Wolf":
                    return "ClanWolf";
                case "New Delphi Compact":
                    return "Delphi";
                case "Elysian Fields":
                    return "Elysia";
                case "Hanseatic League":
                    return "Hanse";
                case "JarnFolk":
                    return "JarnFolk";
                case "Tortuga Dominions":
                    return "Tortuga";
                case "Greater Valkyrate":
                    return "Valkyrate";
                case "Rim Collection":
                    return "Rim";
                case "Word of Blake":
                    return "WordOfBlake";
                case "Independent":
                //3063
                case "Chaos March":
                case "Terracap Confederation":
                case "New Colony Region":
                case "Styk Commonality":
                case "Saiph Triumvirate":
                case "Sarna Supremacy":
                case "Interstellar Expeditions":
                //3040
                case "Clan Blood Spirit":
                //3025
                case "Morgraine's Valkyrate":
                    return "Locals";
                case "Abandoned":
                case "Undiscovered":
                    return "NoFaction";
                case "Disputed":
                    return getOwner("Faction3040", systemJObject);
                default:
                    Console.WriteLine(faction);
                    throw new Exception();

            }
        }
        public string getOwner(JObject systemJObject) {
            string year = "Faction3025";
            if (this.year == 1) {
                year = "Faction3040";
            }
            if (this.year == 2) {
                year = "Faction3063";
            }
            return getOwner(year, systemJObject);

        }
        public List<string> createTags(JObject systemJObject) {
            string year = "Faction3025";
            if (this.year == 1) {
                year = "Faction3040";
            }
            if (this.year == 2) {
                year = "Faction3063";
            }
            List<string> tagList = new List<string>();

            //Nametag
            tagList.Add("planet_name_" + ((string)systemJObject["PlanetName"]).Replace(" ", "").ToLower());

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
            }
            else {
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
            }
            else {
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
            string owner = getOwner(systemJObject);
            tagList.Add(owner);
            if (owner.Equals("NoFaction")) {
                tagList.Add("planet_other_empty");
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

        public List<string> getTargets(string faction) {
            List<string> targets = getAllFactions();
            if (faction.Equals("NoFaction")) {
                return targets;
            }
            targets.Remove(faction);
            return targets;
        }

        public static string GetFactionTag(Faction faction) {
            return "planet_faction_" + faction.ToString().ToLower();
        }
    }
}
