using BattleTech;
using BattleTech.UI;
using Harmony;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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
                byte[] data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/davionLogo.png");
                texture2D2.LoadImage(data);
                GameObject go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Davion, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/liaoLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Liao, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/magistracyLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.MagistracyOfCanopus, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/marikLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Marik, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/restorationLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.AuriganRestoration, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/taurianLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.TaurianConcordat, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/steinerLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Steiner, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/draconisLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Kurita, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/circinusLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.Nautilus, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/oberonLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.MagistracyCentrella, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/illyrianLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.AuriganMercenaries, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/lothianLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.MajestyMetals, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/marianLogo.png");
                texture2D2.LoadImage(data);
                go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.AuriganDirectorate, go });

                texture2D2 = new Texture2D(2, 2);
                data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/outworldsLogo.png");
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

                Settings settings = InnerSphereMap.SETTINGS;
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

    // The original method had a rectangular normalization here -- it did 56% of the y axis
    [HarmonyPatch(typeof(StarmapRenderer), "NormalizeToMapSpace")]
    public static class StarmapRenderer_NormalizeToMapSpace_Patch
    {
        static bool Prefix(StarmapRenderer __instance, Vector2 normalizedPos, ref Vector3 __result)
        {
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
    public static class StarmapRenderer_Update_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionList = instructions.ToList();

            try
            {
                // Targetting the line: float num4 = Mathf.Lerp(150f, 70f, this.zoomLevel);
                int index0 = instructionList.FindIndex(instruction => ((object)150f).Equals(instruction.operand));
                int index1 = instructionList.FindIndex(instruction => ((object)70f).Equals(instruction.operand));
                if (index1 - index0 == 1) // Make sure these two calls are adjacent -- since it should be loading the stack
                {
                    instructionList[index0].operand = InnerSphereMap.SETTINGS.GetXBoundaryForMinFov();
                    instructionList[index1].operand = InnerSphereMap.SETTINGS.GetXBoundaryForMaxFov();
                }

                // Targetting the line: float num5 = Mathf.Lerp(99f, 50f, this.zoomLevel);
                int index2 = instructionList.FindIndex(instruction => ((object)99f).Equals(instruction.operand));
                int index3 = instructionList.FindIndex(instruction => ((object)50f).Equals(instruction.operand));
                if (index3 - index2 == 1)  // Make sure these two calls are adjacent -- since it should be loading the stack
                {
                    instructionList[index2].operand = InnerSphereMap.SETTINGS.GetYBoundaryForMinFov();
                    instructionList[index3].operand = InnerSphereMap.SETTINGS.GetYBoundaryForMaxFov();
                }

                // Targetting the line: this.starmapCamera.fieldOfView = Mathf.Lerp(this.fovMin, this.fovMax, this.zoomLevel);
                FieldInfo fovMinInfo = AccessTools.Field(typeof(StarmapRenderer), nameof(StarmapRenderer.fovMin));
                int fovMinIndex = instructionList.FindIndex(instruction => fovMinInfo.Equals(instruction.operand));
               
                // First remove the previous ldarg.0 (StarmapRenderer.this)
                instructionList[fovMinIndex - 1].opcode = OpCodes.Nop;

                // Now change the load from this.fovMin to InnerSphereMap.SETTINGS.MinFov
                instructionList[fovMinIndex].opcode = OpCodes.Ldc_R4;
                instructionList[fovMinIndex].operand = InnerSphereMap.SETTINGS.MinFov;

                FieldInfo fovMaxInfo = AccessTools.Field(typeof(StarmapRenderer), nameof(StarmapRenderer.fovMax));
                int fovMaxIndex = instructionList.FindIndex(instruction => fovMaxInfo.Equals(instruction.operand));

                // First remove the previous ldarg.0 (StarmapRenderer.this)
                instructionList[fovMaxIndex - 1].opcode = OpCodes.Nop;

                // Now change the load from this.fovMax to InnerSphereMap.SETTINGS.MaxFov
                instructionList[fovMaxIndex].opcode = OpCodes.Ldc_R4;
                instructionList[fovMaxIndex].operand = InnerSphereMap.SETTINGS.MaxFov;

                return instructionList;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return instructions;
            }
        }
    }
}