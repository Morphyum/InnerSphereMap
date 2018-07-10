using BattleTech;
using BattleTech.StringInterpolation;
using BattleTech.UI;
using Harmony;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

namespace InnerSphereMap {

    [HarmonyPatch(typeof(MainMenu), "Init")]
    public static class MainMenu_Init_Patch {

        static void Prefix(MainMenu __instance) {
            try {
                HBSRadioSet topLevelMenu = (HBSRadioSet)ReflectionHelper.GetPrivateField(__instance, "topLevelMenu");
                topLevelMenu.RadioButtons.Find((HBSButton x) => x.GetText() == "Campaign").SetText("Sandbox");
            }
            catch (Exception e) {
                Logger.LogError(e);

            }
        }
    }

    
    [HarmonyPatch(typeof(SimGameState), "GetFactionDefIDFromEnum")]
    public static class SimGameState_GetFactionDefIDFromEnum_Patch {

        static void Postfix(ref string __result) {
            try {
                if (__result.Equals("faction_Betrayers")) {
                    __result = "faction_AuriganBetrayers";
                } 
            }
            catch (Exception e) {
                Logger.LogError(e);

            }
        }
    }

    [HarmonyPatch(typeof(SGCaptainsQuartersReputationScreen), "RefreshWidgets")]
    public static class SGCaptainsQuartersReputationScreen_RefreshWidgets {

        static void Prefix(ref SGCaptainsQuartersReputationScreen __instance) {
            try {
                SimGameState simState = (SimGameState)ReflectionHelper.GetPrivateField(__instance, "simState");
                simState.displayedFactions = simState.displayedFactions.OrderByDescending(o => simState.GetRawReputation(o)).ToList();
            }
            catch (Exception e) {
                Logger.LogError(e);

            }
        }
    }

    [HarmonyPatch(typeof(StarmapBorders), "OnWillRenderObject")]
    public static class StarmapBorders_OnWillRenderObject {

        static bool Prefix(StarmapBorders __instance) {
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
                Texture2D texture2D2;
                byte[] data;
                if (GameObject.Find("davionLogoMap") == null) {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/davionLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "davionLogoMap";
                } else {
                    go = GameObject.Find("davionLogoMap");
                    
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Davion, go });

                if (GameObject.Find("liaoLogoMap") == null) {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/liaoLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "liaoLogoMap";
                }
                else {
                    go = GameObject.Find("liaoLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Liao, go });

                if (GameObject.Find("magistracyLogoMap") == null) {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/magistracyLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "magistracyLogoMap";
                }
                else {
                    go = GameObject.Find("magistracyLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.MagistracyOfCanopus, go });

                if (GameObject.Find("marikLogoMap") == null) {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/marikLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "marikLogoMap";
                }
                else {
                    go = GameObject.Find("marikLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Marik, go });

                if (GameObject.Find("restorationLogoMap") == null) {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/restorationLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "restorationLogoMap";
                }
                else {
                    go = GameObject.Find("restorationLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.AuriganRestoration, go });

                if (GameObject.Find("taurianLogoMap") == null) {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/taurianLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "taurianLogoMap";
                }
                else {
                    go = GameObject.Find("taurianLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.TaurianConcordat, go });

                if (GameObject.Find("steinerLogoMap") == null) {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/steinerLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "steinerLogoMap";
                }
                else {
                    go = GameObject.Find("steinerLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Steiner, go });

                if (GameObject.Find("draconisLogoMap") == null) {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/draconisLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "draconisLogoMap";
                }
                else {
                    go = GameObject.Find("draconisLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Kurita, go });

                if (GameObject.Find("circinusLogoMap") == null) {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/circinusLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "circinusLogoMap";
                }
                else {
                    go = GameObject.Find("circinusLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Nautilus, go });


                if (GameObject.Find("oberonLogoMap") == null) {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/oberonLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "oberonLogoMap";
                }
                else {
                    go = GameObject.Find("oberonLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.MagistracyCentrella, go });

                if (GameObject.Find("illyrianLogoMap") == null) {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/illyrianLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "illyrianLogoMap";
                }
                else {
                    go = GameObject.Find("illyrianLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.AuriganMercenaries, go });

                if (GameObject.Find("lothianLogoMap") == null) {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/lothianLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "lothianLogoMap";
                }
                else {
                    go = GameObject.Find("lothianLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.MajestyMetals, go });

                if (GameObject.Find("marianLogoMap") == null) {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/marianLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "marianLogoMap";
                }
                else {
                    go = GameObject.Find("marianLogoMap");

                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.AuriganDirectorate, go });

                if (GameObject.Find("outworldsLogoMap") == null) {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/outworldsLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "outworldsLogoMap";
                }
                else {
                    go = GameObject.Find("outworldsLogoMap");
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Betrayers, go });
            }
            catch (Exception e) {
                Logger.LogError(e);
            }
        }
    }


    [HarmonyPatch(typeof(SimGameState), "DoesFactionGainReputation")]
    public static class SimGameState_DoesFactionGainReputation {

        static void Postfix(SimGameState __instance, ref bool __result, Faction fac) {
            try {
                __result = fac != Faction.MercenaryReviewBoard && fac != Faction.NoFaction && fac != Faction.Locals;
            }
            catch (Exception e) {
                Logger.LogError(e);
            }
        }
    }

    [HarmonyPatch(typeof(StarmapRenderer), "FactionColor")]
    public static class StarmapRenderer_FactionColor {

        static void Postfix(StarmapRenderer __instance, ref Color __result, Faction thisFaction) {
            try {

                Settings settings = Helper.LoadSettings();
                switch (thisFaction) {
                    case Faction.Kurita:
                        __result = new Color(settings.KuritaRGB[0], settings.KuritaRGB[1], settings.KuritaRGB[2], 1f);
                        break;
                    case Faction.Steiner:
                        __result = new Color(settings.SteinerRGB[0], settings.SteinerRGB[1], settings.SteinerRGB[2], 1f);
                        break;
                    case Faction.Betrayers:
                        __result = new Color(settings.OutworldsRGB[0], settings.OutworldsRGB[1], settings.OutworldsRGB[2], 1f);
                        break;
                    case Faction.MagistracyCentrella:
                        __result = new Color(settings.OberonRGB[0], settings.OberonRGB[1], settings.OberonRGB[2], 1f);
                        break;
                    case Faction.MajestyMetals:
                        __result = new Color(settings.LothianRGB[0], settings.LothianRGB[1], settings.LothianRGB[2], 1f);
                        break;
                    case Faction.AuriganDirectorate:
                        __result = new Color(settings.MarianRGB[0], settings.MarianRGB[1], settings.MarianRGB[2], 1f);
                        break;
                    case Faction.Nautilus:
                        __result = new Color(settings.CircinusRGB[0], settings.CircinusRGB[1], settings.CircinusRGB[2], 1f);
                        break;
                    case Faction.AuriganMercenaries:
                        __result = new Color(settings.IllyrianRGB[0], settings.IllyrianRGB[1], settings.IllyrianRGB[2], 1f);
                        break;
                    case Faction.Davion:
                        __result = new Color(settings.DavionRGB[0], settings.DavionRGB[1], settings.DavionRGB[2], 1f);
                        break;
                    case Faction.Liao:
                        __result = new Color(settings.LiaoRGB[0], settings.LiaoRGB[1], settings.LiaoRGB[2], 1f);
                        break;
                    case Faction.Marik:
                        __result = new Color(settings.MarikRGB[0], settings.MarikRGB[1], settings.MarikRGB[2], 1f);
                        break;
                    case Faction.TaurianConcordat:
                        __result = new Color(settings.TaurianRGB[0], settings.TaurianRGB[1], settings.TaurianRGB[2], 1f);
                        break;
                    case Faction.MagistracyOfCanopus:
                        __result = new Color(settings.MagistracyRGB[0], settings.MagistracyRGB[1], settings.MagistracyRGB[2], 1f);
                        break;
                    case Faction.AuriganRestoration:
                        __result = new Color(settings.RestorationRGB[0], settings.RestorationRGB[1], settings.RestorationRGB[2], 1f);
                        break;
                    default:
                        __result = __instance.nofactionColor;
                        break;

                }
            }
            catch (Exception e) {
                Logger.LogError(e);
            }
        }
    }
}