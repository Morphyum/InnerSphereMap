using BattleTech;
using BattleTech.UI;
using BattleTech.UI.Tooltips;
using Harmony;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace InnerSphereMap {
    [HarmonyPatch(typeof(SimGameState), "InitializeDataFromDefs")]
    public static class SimGameState_InitializeDataFromDefs_Patch {

        static void Prefix(SimGameState __instance) {
            StarSystemDef starSystemDef = null;
            try {
                Dictionary<string, StarSystem> test = new Dictionary<string, StarSystem>();
                foreach (string id in __instance.DataManager.SystemDefs.Keys) {
                    starSystemDef = __instance.DataManager.SystemDefs.Get(id);
                    if (starSystemDef.StartingSystemModes.Contains(__instance.SimGameMode)) {
                        StarSystem starSystem = new StarSystem(starSystemDef, __instance);
                        test.Add(starSystemDef.CoreSystemID, starSystem);
                    }
                }
                //TODO: SOMESOMETHINGSOMETHING FACTION STORE
            }
            catch (Exception e) {
                Logger.LogLine("STARSYSTEM BROKEN: " + starSystemDef.CoreSystemID);
                Logger.LogError(e);
            }
        }
    }
    

    [HarmonyPatch(typeof(MainMenu), "Init")]
    public static class MainMenu_Init_Patch {

        static void Prefix(MainMenu __instance) {
            try {
                HBSRadioSet topLevelMenu = (HBSRadioSet)ReflectionHelper.GetPrivateField(__instance, "topLevelMenu");
                topLevelMenu.RadioButtons.Find((HBSButton x) => x.GetText() == "Campaign").gameObject.SetActive(false);
            }
            catch (Exception e) {
                Logger.LogError(e);

            }
        }
    }

    [HarmonyPatch(typeof(SGCaptainsQuartersReputationScreen), "RefreshWidgets")]
    public static class SGCaptainsQuartersReputationScreen_RefreshWidgets {

        static void Prefix(ref SGCaptainsQuartersReputationScreen __instance, List<SGFactionReputationWidget> ___FactionPanelWidgets, ref SimGameState ___simState) {
            try {
                Settings settings = InnerSphereMap.SETTINGS;
                if (___simState.displayedFactions.Contains(FactionEnumeration.GetFactionByName("Locals").Name)) {
                    ___simState.displayedFactions.Remove(FactionEnumeration.GetFactionByName("Locals").Name);
                }
                GameObject parent = GameObject.Find("factionsPanel_V2");
                if (parent != null) {
                    parent.transform.position = new Vector3(830, 670, parent.transform.position.z);
                    Transform factionHeader = parent.transform.FindRecursive("factionHeader");
                    factionHeader.localPosition = new Vector3(factionHeader.localPosition.x, 250, factionHeader.localPosition.z);
                    GameObject restPanel = GameObject.Find("RestorationRepPanel");
                    if (restPanel != null) {
                        restPanel.SetActive(false);
                    } 
                    GameObject superParent = GameObject.Find("uixPrfPanl_captainsQuarters_Reputation-Panel_V2(Clone)");
                    if (superParent != null) {
                        GameObject bgfill = superParent.transform.FindRecursive("bgFill").gameObject;
                        if(bgfill != null) {
                            bgfill.SetActive(false);
                        } 
                    }
                    GameObject MRBRep = GameObject.Find("uixPrfPanl_AA_MercBoardReputationPanel");
                    if (MRBRep != null) {
                        MRBRep.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                        MRBRep.transform.localPosition = new Vector3(0, 390, MRBRep.transform.localPosition.z);
                    }
                    GridLayoutGroup grid = parent.GetComponent<GridLayoutGroup>();
                    if (grid != null) {
                        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                        grid.constraintCount = 5;
                        grid.spacing = new Vector2(0, 0);
                        grid.cellSize = new Vector2(275, grid.cellSize.y);
                        grid.childAlignment = TextAnchor.UpperLeft;
                    }
                    GameObject primeWidget = ___FactionPanelWidgets[0].gameObject;
                    if (___FactionPanelWidgets.Count < ___simState.displayedFactions.Count + 1) {
                        ___FactionPanelWidgets.Clear();
                        for (int i = 0; i < ___simState.displayedFactions.Count + 1; i++) {
                            GameObject newwidget = GameObject.Instantiate(primeWidget);
                            newwidget.transform.parent = primeWidget.transform.parent;
                            newwidget.name = "NewWidget";
                            newwidget.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                            newwidget.transform.position = new Vector3(newwidget.transform.position.x, 200, newwidget.transform.position.z);
                            RectTransform repText = newwidget.transform.FindRecursive("classification-text").GetComponent<RectTransform>();
                            repText.localPosition = new Vector3(0, repText.localPosition.y, repText.localPosition.z);
                            RectTransform bar = newwidget.transform.FindRecursive("factionBar_Layout").GetComponent<RectTransform>();
                            bar.sizeDelta = new Vector2(125, bar.sizeDelta.y);
                            RectTransform score = newwidget.transform.FindRecursive("RepScore-text").GetComponent<RectTransform>();
                            score.localPosition = new Vector3(120, score.localPosition.y, score.localPosition.z);
                            RectTransform negative = newwidget.transform.FindRecursive("faction_Negativefill_moveThisNegative").GetComponent<RectTransform>();
                            negative.localPosition = new Vector3(0, 0, 0);
                            negative.sizeDelta = new Vector2(64, 0);
                            RectTransform allianceButton = newwidget.transform.FindRecursive("OBJ_allianceButtons").GetComponent<RectTransform>();
                            allianceButton.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                            allianceButton.transform.FindRecursive("connectorH").gameObject.SetActive(false);
                            RectTransform positive = newwidget.transform.FindRecursive("faction_Positivefill_moveThisPositive").GetComponent<RectTransform>();
                            positive.localPosition = new Vector3(0, 0, 0);
                            positive.sizeDelta = new Vector2(64, 0);
                            /*RectTransform square = newwidget.transform.FindRecursive("squaresPanel").GetComponent<RectTransform>();
                            square.localPosition = new Vector3(18, square.localPosition.y, square.localPosition.z);*/
                            SGFactionReputationWidget newSGWidget = newwidget.GetComponent<SGFactionReputationWidget>();
                            ___FactionPanelWidgets.Add(newSGWidget);
                        }
                    }
                    foreach (GameObject go in parent.FindAllContains("uixPrfWidget_factionReputationBidirectionalWidget")) {
                        go.SetActive(false);
                    }
                }
            }
            catch (Exception e) {
                Logger.LogError(e);

            }
        }

        static void Postfix(ref SGCaptainsQuartersReputationScreen __instance, List<SGFactionReputationWidget> ___FactionPanelWidgets, SimGameState ___simState) {
            try {
                FactionDef factionDef = FactionEnumeration.GetAuriganRestorationFactionValue().FactionDef;
                if (factionDef != null) {
                    ___FactionPanelWidgets[___FactionPanelWidgets.Count-1].gameObject.SetActive(true);
                    ___FactionPanelWidgets[___FactionPanelWidgets.Count-1].Init(___simState, FactionEnumeration.GetAuriganRestorationFactionValue(), new UnityAction(__instance.RefreshWidgets), false);

                }
            }
            catch (Exception e) {
                Logger.LogError(e);

            }
        }
    }

    [HarmonyPatch(typeof(StarmapBorders), "OnWillRenderObject")]
    public static class StarmapBorders_OnWillRenderObject {

        static bool Prefix(StarmapBorders __instance) {
            // Disables the dashed-line border
            GameObject.Find("Edges").SetActive(false);

            // Disables the gray box, and other borders -- also fully disables StarmapBorders itself (since its a MonoBehavior attached to RegionBorders)
            GameObject.Find("RegionBorders").SetActive(false);
            return false;
        }
    }

    [HarmonyPatch(typeof(StarmapRenderer), "RefreshSystems")]
    public static class StarmapRenderer_RefreshSystems {

        static bool Prefix(StarmapRenderer __instance) {

            __instance.starmapCamera.gameObject.SetActive(true);
            Dictionary<GameObject, StarmapSystemRenderer> systemDictionary = (Dictionary<GameObject, StarmapSystemRenderer>)ReflectionHelper.GetPrivateField(__instance, "systemDictionary");
            foreach (StarmapSystemRenderer starmapSystemRenderer in systemDictionary.Values) {
                ReflectionHelper.InvokePrivateMethode(__instance, "InitializeSysRenderer", new object[] { starmapSystemRenderer.system, starmapSystemRenderer });
                if (__instance.starmap.CurSelected != null && __instance.starmap.CurSelected.System.ID == starmapSystemRenderer.system.System.ID) {
                    starmapSystemRenderer.Selected();
                }
                else {
                    starmapSystemRenderer.Deselected();
                }
            }
            return false;

        }
        static void Postfix(StarmapRenderer __instance) {
            try {
                var davionLogo = GameObject.Find("davionLogo");
                var marikLogo = GameObject.Find("marikLogo");
                var directorateLogo = GameObject.Find("directorateLogo");
                directorateLogo?.SetActive(false);
                davionLogo?.SetActive(false);
                marikLogo?.SetActive(false);
                var liaoLogo = GameObject.Find("liaoLogo");
                liaoLogo?.SetActive(false);
                var taurianLogo = GameObject.Find("taurianLogo");
                taurianLogo?.SetActive(false);
                var magistracyLogo = GameObject.Find("magistracyLogo");
                magistracyLogo?.SetActive(false);
                var restorationLogo = GameObject.Find("restorationLogo");
                restorationLogo?.SetActive(false);

                GameObject go;
                if (Fields.originalTransform == null) {
                    Fields.originalTransform = UnityEngine.Object.Instantiate(__instance.restorationLogo).transform;
                }
                Texture2D texture2D2;
                byte[] data;
                if (GameObject.Find("davionLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/davionLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "davionLogoMap";
                }
                else
                {
                    go = GameObject.Find("davionLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Davion"), go });

                if (GameObject.Find("liaoLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/liaoLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "liaoLogoMap";
                }
                else
                {
                    go = GameObject.Find("liaoLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Liao"), go });

                if (GameObject.Find("magistracyLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/magistracyLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "magistracyLogoMap";
                }
                else
                {
                    go = GameObject.Find("magistracyLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("MagistracyOfCanopus"), go });

                if (GameObject.Find("marikLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/marikLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "marikLogoMap";
                }
                else
                {
                    go = GameObject.Find("marikLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Marik"), go });

                if (GameObject.Find("restorationLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/restorationLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "restorationLogoMap";
                }
                else
                {
                    go = GameObject.Find("restorationLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("AuriganRestoration"), go });

                if (GameObject.Find("taurianLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/taurianLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "taurianLogoMap";
                }
                else
                {
                    go = GameObject.Find("taurianLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("TaurianConcordat"), go });

                if (GameObject.Find("steinerLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/steinerLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "steinerLogoMap";
                }
                else
                {
                    go = GameObject.Find("steinerLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Steiner"), go });

                if (GameObject.Find("draconisLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/draconisLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "draconisLogoMap";
                }
                else
                {
                    go = GameObject.Find("draconisLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Kurita"), go });

                if (GameObject.Find("circinusLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/circinusLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "circinusLogoMap";
                }
                else
                {
                    go = GameObject.Find("circinusLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Circinus"), go });


                if (GameObject.Find("oberonLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/oberonLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "oberonLogoMap";
                }
                else
                {
                    go = GameObject.Find("oberonLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Oberon"), go });


                if (GameObject.Find("rasalhagueLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/rasalhagueLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "rasalhagueLogoMap";
                }
                else
                {
                    go = GameObject.Find("rasalhagueLogoMap");
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Rasalhague"), go });


                if (GameObject.Find("illyrianLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/illyrianLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "illyrianLogoMap";
                }
                else
                {
                    go = GameObject.Find("illyrianLogoMap");
                }

                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("llyrian"), go });

                if (GameObject.Find("stivesLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/stivesLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "stivesLogoMap";
                }
                else
                {
                    go = GameObject.Find("stivesLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Ives"), go });


                if (GameObject.Find("lothianLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/lothianLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "lothianLogoMap";
                }
                else
                {
                    go = GameObject.Find("lothianLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Lothian"), go });

                if (GameObject.Find("marianLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/marianLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "marianLogoMap";
                }
                else
                {
                    go = GameObject.Find("marianLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Marian"), go });

                if (GameObject.Find("outworldsLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/outworldsLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "outworldsLogoMap";
                }
                else
                {
                    go = GameObject.Find("outworldsLogoMap");
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Outworld"), go });

                if (GameObject.Find("directorateLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/directorateLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "directorateLogoMap";
                }
                else
                {
                    go = GameObject.Find("directorateLogoMap");
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("AuriganDirectorate"), go });

                if (GameObject.Find("AxumiteLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/AxumiteLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "AxumiteLogoMap";
                }
                else
                {
                    go = GameObject.Find("AxumiteLogoMap");
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Axumite"), go });

                if (GameObject.Find("CastileLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/CastileLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "CastileLogoMap";
                }
                else
                {
                    go = GameObject.Find("CastileLogoMap");
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Castile"), go });

                if (GameObject.Find("WordOfBlakeLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/WordOfBlakeLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "WordOfBlakeLogoMap";
                }
                else
                {
                    go = GameObject.Find("WordOfBlakeLogoMap");
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("WordOfBlake"), go });

                if (GameObject.Find("ChainelaneLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/ChainelaneLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "ChainelaneLogoMap";
                }
                else
                {
                    go = GameObject.Find("ChainelaneLogoMap");
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Chainelane"), go });


                FactionValue leaderclan = __instance.starmap.GetSystemByID("starsystemdef_StranaMechty").System.OwnerValue;
                if (GameObject.Find("ClansGenericLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/" + leaderclan.Name + "Logo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "ClansGenericLogoMap";
                }
                else
                {
                    go = GameObject.Find("ClansGenericLogoMap");
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/" + leaderclan.Name + "Logo.png");
                    texture2D2 = new Texture2D(2, 2);
                    texture2D2.LoadImage(data);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { leaderclan, go });


                SimGameState sim = (SimGameState)AccessTools.Field(typeof(Starmap), "sim").GetValue(__instance.starmap);
                List<FactionValue> contestingFactions = new List<FactionValue>();
                foreach (FactionValue faction in FactionEnumeration.FactionList)
                {
                    if (faction.IsClan && faction.CanAlly)
                    {
                        contestingFactions.Add(faction);
                    }
                }
                Dictionary<FactionValue, int> ranking = new Dictionary<FactionValue, int>();
                foreach (StarSystem system in sim.StarSystems)
                {
                    if (contestingFactions.Contains(system.OwnerValue))
                    {
                        if (!ranking.ContainsKey(system.OwnerValue))
                        {
                            ranking.Add(system.OwnerValue, 0);
                        }
                        ranking[system.OwnerValue]++;
                    }
                }
                FactionValue invaderclan = FactionEnumeration.GetInvalidUnsetFactionValue();
                if (ranking.Count > 0)
                {
                    invaderclan = ranking.OrderByDescending(x => x.Value).First().Key;
                }
                if (invaderclan != FactionEnumeration.GetInvalidUnsetFactionValue())
                {
                    if (GameObject.Find("ClansInvaderLogoMap") == null)
                    {
                        texture2D2 = new Texture2D(2, 2);
                        data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/" + invaderclan.Name + "Logo.png");
                        texture2D2.LoadImage(data);
                        go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                        go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                        go.name = "ClansInvaderLogoMap";
                    }
                    else
                    {
                        go = GameObject.Find("ClansInvaderLogoMap");
                        data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/" + invaderclan.Name + "Logo.png");
                        texture2D2 = new Texture2D(2, 2);
                        texture2D2.LoadImage(data);
                        go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    }
                    ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { invaderclan, go });
                }


                if (GameObject.Find("DelphiLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/DelphiLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "DelphiLogoMap";
                }
                else
                {
                    go = GameObject.Find("DelphiLogoMap");
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Delphi"), go });

                if (GameObject.Find("ElysiaLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/ElysiaLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "ElysiaLogoMap";
                }
                else
                {
                    go = GameObject.Find("ElysiaLogoMap");
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Elysia"), go });

                if (GameObject.Find("HanseLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/HanseLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "HanseLogoMap";
                }
                else
                {
                    go = GameObject.Find("HanseLogoMap");
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Hanse"), go });

                if (GameObject.Find("JarnFolkLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/JarnFolkLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "JarnFolkLogoMap";
                }
                else
                {
                    go = GameObject.Find("JarnFolkLogoMap");
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("JarnFolk"), go });

                if (GameObject.Find("TortugaLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/TortugaLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "TortugaLogoMap";
                }
                else
                {
                    go = GameObject.Find("TortugaLogoMap");
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Tortuga"), go });

                if (GameObject.Find("ValkyrateLogoMap") == null)
                {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/ValkyrateLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "ValkyrateLogoMap";
                }
                else
                {
                    go = GameObject.Find("ValkyrateLogoMap");
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { FactionEnumeration.GetFactionByName("Valkyrate"), go });
            }
            catch (Exception e) {
                Logger.LogError(e);
            }
        }
    }

    // The original method had a rectangular normalization here -- it did 56% of the y axis
    [HarmonyPatch(typeof(StarmapRenderer), "NormalizeToMapSpace")]
    public static class StarmapRenderer_NormalizeToMapSpace_Patch {

        static bool Prefix(StarmapRenderer __instance, Vector2 normalizedPos, ref Vector3 __result) {
            // Reminder -- normalizedPos is normalized between [0,1]
            // This normalizes it between [-100,100]
            Vector3 newResult = normalizedPos;
            newResult.x = (newResult.x * 2f - 1f) * InnerSphereMap.SETTINGS.MapWidth;
            newResult.y = (newResult.y * 2f - 1f) * InnerSphereMap.SETTINGS.MapHeight;
            newResult.z = 0f;

            __result = newResult;

            return false;
        }
    }


    [HarmonyPatch(typeof(StarmapRenderer), "Update")]
    public static class StarmapRenderer_Update_Patch {

        // This transpiler aims to remove two method calls at the end of the Update loop
        // The this.needsPan = false; in the if statement
        // and the final this.starmapCamera.transform.position = position3;
        // These are converted to NOOPs and then properly handled with a PostFix
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {

            List<CodeInstruction> instructionList = instructions.ToList();
            try {
                // Targetting the last instance of: this.needsPan = false; 
                FieldInfo panInfo = AccessTools.Field(typeof(StarmapRenderer), "needsPan");
                int setPanIndex = instructionList.FindLastIndex(instruction => {
                    return instruction.opcode == OpCodes.Stfld && panInfo.Equals(instruction.operand);
                });
                instructionList[setPanIndex - 2].opcode = OpCodes.Nop; // remove loading "this"
                instructionList[setPanIndex - 1].opcode = OpCodes.Nop; // remove loading "false"
                instructionList[setPanIndex].opcode = OpCodes.Nop; // remove the set

                // Targetting the last instance of: this.starmapCamera.transform.position = position3
                // We don't want to simply remove this code, since there is some branching jumps that land on it earlier
                // So replace these with NOPs too
                MethodInfo setPosInfo = AccessTools.Property(typeof(Transform), nameof(Transform.position)).GetSetMethod();
                int setPositionIndex = instructionList.FindLastIndex(instruction => {
                    return instruction.opcode == OpCodes.Callvirt && setPosInfo.Equals(instruction.operand);
                });
                instructionList[setPositionIndex - 4].opcode = OpCodes.Nop; // remove loading "this"
                instructionList[setPositionIndex - 3].opcode = OpCodes.Nop; // remove load starmapCamera
                instructionList[setPositionIndex - 2].opcode = OpCodes.Nop; // remove get_transform
                instructionList[setPositionIndex - 1].opcode = OpCodes.Nop; // remove loading position3
                instructionList[setPositionIndex].opcode = OpCodes.Nop; // remove the set_position

                return instructionList;
            }
            catch (Exception e) {
                Logger.LogError(e);
                return instructions;
            }
        }

        static void Postfix(StarmapRenderer __instance) {
            try {
                // Two private fields
                Traverse travInstance = Traverse.Create(__instance);
                float zoomLevel = travInstance.Field("zoomLevel").GetValue<float>();
                bool needsPan = travInstance.Field("needsPan").GetValue<bool>();
                Camera fakeCamera = travInstance.Field("fakeCamera").GetValue<Camera>();

                // starMapCamera is public
                Camera starMapCamera = __instance.starmapCamera;

                // We want to readjust the field of view given our own min and max
                float newFov = Mathf.Lerp(InnerSphereMap.SETTINGS.MinFov, InnerSphereMap.SETTINGS.MaxFov, zoomLevel);
                starMapCamera.fieldOfView = newFov;
                fakeCamera.fieldOfView = newFov;

                // Now we need to clamp the bounadries
                float verticalViewSize = CameraHelper.GetViewSize(Mathf.Abs(starMapCamera.transform.position.z), newFov);
                float horizontalViewSize = CameraHelper.GetViewSize(Mathf.Abs(starMapCamera.transform.position.z), CameraHelper.GetHorizontalFov(newFov));

                Vector3 currentPosition = starMapCamera.transform.position;
                Vector3 clampedPosition = currentPosition;

                // The clamping boundaries are the map width / height + the buffers + the viewing distance created with the FOVs
                float leftBoundary = -InnerSphereMap.SETTINGS.MapWidth - InnerSphereMap.SETTINGS.MapLeftViewBuffer + horizontalViewSize;
                float rightBoundary = InnerSphereMap.SETTINGS.MapWidth + InnerSphereMap.SETTINGS.MapRightViewBuffer - horizontalViewSize;
                float bottomBoundary = -InnerSphereMap.SETTINGS.MapHeight - InnerSphereMap.SETTINGS.MapBottomViewBuffer + verticalViewSize;
                float topBoundary = InnerSphereMap.SETTINGS.MapHeight + InnerSphereMap.SETTINGS.MapTopViewBuffer - verticalViewSize;

                float totalWidth = InnerSphereMap.SETTINGS.MapWidth * 2f + InnerSphereMap.SETTINGS.MapLeftViewBuffer + InnerSphereMap.SETTINGS.MapRightViewBuffer;
                float totalHeight = InnerSphereMap.SETTINGS.MapHeight * 2f + InnerSphereMap.SETTINGS.MapTopViewBuffer + InnerSphereMap.SETTINGS.MapBottomViewBuffer;

                // We have to check for the FOV being larger than the whole map -- or it'll bounce around
                if (horizontalViewSize * 2f >= totalWidth) {
                    clampedPosition.x = 0f;
                    needsPan = false;
                }
                else {
                    clampedPosition.x = Mathf.Clamp(currentPosition.x, leftBoundary, rightBoundary);
                }

                if (verticalViewSize * 2f >= totalHeight) {
                    clampedPosition.y = 0f;
                    needsPan = false;
                }
                else {
                    clampedPosition.y = Mathf.Clamp(currentPosition.y, bottomBoundary, topBoundary);
                }

                // Check for boundaries conditions -- continue the previous HBS behavior of not panning
                if (clampedPosition.x == leftBoundary || clampedPosition.x == rightBoundary || clampedPosition.y == topBoundary || clampedPosition.y == bottomBoundary) {
                    needsPan = false;
                }
                starMapCamera.transform.position = clampedPosition;
            }
            catch (Exception e) {
                Logger.LogError(e);
            }
        }

        //rezising all the map logos
        [HarmonyPatch(typeof(StarmapRenderer), "PlaceLogo")]
        public static class StarmapRenderer_PlaceLogo_Patch {

            static void Postfix(StarmapRenderer __instance, FactionValue faction, GameObject logo) {
                try {
                    if (logo.transform.localScale == Fields.originalTransform.localScale) {
                        logo.transform.localScale += new Vector3(4f, 4f, 4f);
                    }
                }
                catch (Exception e) {
                    Logger.LogError(e);
                }
            }
        }

        [HarmonyPatch(typeof(SGNavigationActiveFactionWidget), "ActivateFactions")]
        public static class SGSystemViewPopulator_UpdateRoutedSystem_Patch {
            static bool Prefix(SGNavigationActiveFactionWidget __instance, List<string> activeFactions, string OwnerFaction, List<HBSButton> ___FactionButtons, List<Image> ___FactionIcons, SimGameState ___simState) {
                try {
                    ___FactionButtons.ForEach(delegate (HBSButton btn) {
                        btn.gameObject.SetActive(false);
                    });
                    int index = 0;
                    foreach (string faction in activeFactions) {
                        FactionDef factionDef = FactionEnumeration.GetFactionByName(faction).FactionDef;
                            ___FactionIcons[index].sprite = factionDef.GetSprite();
                            HBSTooltip component = ___FactionIcons[index].GetComponent<HBSTooltip>();
                            if (component != null) {
                                component.SetDefaultStateData(TooltipUtilities.GetStateDataFromObject(factionDef));
                            }
                            ___FactionButtons[index].SetState(ButtonState.Enabled, false);
                            ___FactionButtons[index].gameObject.SetActive(true);
                            index++;
                    }
                    return false;
                }
                catch (Exception e) {
                    Logger.LogError(e);
                    return true;
                }
            }
        }

        [HarmonyPatch(typeof(StarmapSystemRenderer), "SetBlackMarket")]
        public static class StarmapSystemRenderer_SetBlackMarket_Patch {
            static void Prefix(StarmapSystemRenderer __instance, ref bool state) {
                try {
                    state = false;
                }
                catch (Exception e) {
                    Logger.LogError(e);
                }
            }
        }

        [HarmonyPatch(typeof(SGCharacterCreationCareerBackgroundSelectionPanel), "Done")]
        public class SGCharacterCreationCareerBackgroundSelectionPanel_Done_Patch {
            // Token: 0x06000004 RID: 4 RVA: 0x000020BC File Offset: 0x000002BC
            public static bool Prefix(SGCharacterCreationCareerBackgroundSelectionPanel __instance) {
                Settings settings = InnerSphereMap.SETTINGS;
                SimGameResultAction simGameResultAction = new SimGameResultAction();
                simGameResultAction.Type = SimGameResultAction.ActionType.System_ShowSummaryOverlay;
                simGameResultAction.value = settings.splashTitle;
                simGameResultAction.additionalValues = new string[1];
                simGameResultAction.additionalValues[0] = settings.splashText;
                SimGameState.ApplyEventAction(simGameResultAction, null);
                __instance.onComplete.Invoke();
                return false;
            }
        }
    }
}
