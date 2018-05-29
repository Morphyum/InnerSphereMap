using BattleTech;
using BattleTech.Data;
using BattleTech.Framework;
using BattleTech.UI;
using Harmony;
using HBS;
using HBS.Collections;
using System;
using System.Collections;
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

                Texture2D texture2D2 = new Texture2D(2, 2);
                byte[] data = File.ReadAllBytes("mods/InnerSphereMap/Logos/davionLogo.png");
                texture2D2.LoadImage(data);
                GameObject go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Davion, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes("mods/InnerSphereMap/Logos/liaoLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Liao, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes("mods/InnerSphereMap/Logos/magistracyLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.MagistracyOfCanopus, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes("mods/InnerSphereMap/Logos/marikLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Marik, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes("mods/InnerSphereMap/Logos/restorationLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.AuriganRestoration, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes("mods/InnerSphereMap/Logos/taurianLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.TaurianConcordat, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes("mods/InnerSphereMap/Logos/steinerLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Steiner, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes("mods/InnerSphereMap/Logos/draconisLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Kurita, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes("mods/InnerSphereMap/Logos/circinusLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Nautilus, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes("mods/InnerSphereMap/Logos/oberonLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.MagistracyCentrella, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes("mods/InnerSphereMap/Logos/illyrianLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.AuriganMercenaries, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes("mods/InnerSphereMap/Logos/lothianLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.MajestyMetals, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes("mods/InnerSphereMap/Logos/marianLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.AuriganDirectorate, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes("mods/InnerSphereMap/Logos/outworldsLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
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
                if (__result == __instance.nofactionColor || __result == __instance.directorateColor) {
                    switch (thisFaction) {
                        case Faction.Kurita:
                            __result = new Color(0.863f, 0.078f, 0.235f, 1f);
                            break;
                        case Faction.Steiner:
                            __result = new Color(0.255f, 0.412f, 0.882f, 1f);
                            break;
                        case Faction.Betrayers:
                            __result = new Color(0.627f, 0.322f, 0.176f, 1f);
                            break;
                        case Faction.MagistracyCentrella:
                            __result = new Color(1f, 1f, 0f, 1f);
                            break;
                        case Faction.MajestyMetals:
                            __result = new Color(0.196f, 0.804f, 0.196f, 1f);
                            break;
                        case Faction.AuriganDirectorate:
                            __result = new Color(1f, 0.549f, 0f, 1f);
                            break;
                        case Faction.Nautilus:
                            __result = new Color(0.545f, 0f, 0f, 1f);
                            break;
                        case Faction.AuriganMercenaries:
                            __result = new Color(0.741f, 0.718f, 0.420f, 1f);
                            break;
                        default:
                            __result = __instance.nofactionColor;
                            break;
                    }
                }
            }
            catch (Exception e) {
                Logger.LogError(e);
            }
        }
    }
}