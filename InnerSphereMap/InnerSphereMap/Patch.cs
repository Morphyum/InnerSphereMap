using BattleTech;
using BattleTech.UI;
using Harmony;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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
                if (GameObject.Find("davionLogoMap") == null) {
                    texture2D2 = new Texture2D(2, 2);
                    data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/davionLogo.png");
                    texture2D2.LoadImage(data);
                    go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                    go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                    go.name = "davionLogoMap";
                }
                else {
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

 
                if (InnerSphereMap.SETTINGS.use3040Map) {
                    if (GameObject.Find("rasalhagueLogoMap") == null) {
                        texture2D2 = new Texture2D(2, 2);
                        data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/rasalhagueLogo.png");
                        texture2D2.LoadImage(data);
                        go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                        go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                        go.name = "rasalhagueLogoMap";
                    }
                    else {
                        go = GameObject.Find("rasalhagueLogoMap");
                    }
                }
                else {
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
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.AuriganMercenaries, go });

                if (InnerSphereMap.SETTINGS.use3040Map) {
                    if (GameObject.Find("stivesLogoMap") == null) {
                        texture2D2 = new Texture2D(2, 2);
                        data = File.ReadAllBytes($"{InnerSphereMap.ModDirectory}/Logos/stivesLogo.png");
                        texture2D2.LoadImage(data);
                        go = UnityEngine.Object.Instantiate(__instance.restorationLogo);
                        go.GetComponent<Renderer>().material.mainTexture = texture2D2;
                        go.name = "stivesLogoMap";
                    }
                    else {
                        go = GameObject.Find("stivesLogoMap");

                    }
                    ReflectionHelper.InvokePrivateMethode(__instance, "PlaceLogo", new object[] { Faction.ComStar, go });
                }

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
                        if (!InnerSphereMap.SETTINGS.use3040Map) {
                            __result = new Color(settings.IllyrianRGB[0], settings.IllyrianRGB[1], settings.IllyrianRGB[2], 1f);
                        }
                        else {
                            __result = new Color(settings.RasalhagueRGB[0], settings.RasalhagueRGB[1], settings.RasalhagueRGB[2], 1f);
                        }
                        break;
                    case Faction.ComStar:
                        __result = new Color(settings.StIvesRGB[0], settings.StIvesRGB[1], settings.StIvesRGB[2], 1f);
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
                    case Faction.NoFaction:
                        __result = new Color(settings.AbandonedRGB[0], settings.AbandonedRGB[1], settings.AbandonedRGB[2], 0.7f);
                        break;
                    case Faction.MercenaryReviewBoard:
                        __result = new Color(settings.MRBRGB[0], settings.MRBRGB[1], settings.MRBRGB[2], 1f);
                        break;
                    case Faction.MorphyumTestFaction:
                        __result = new Color(1, 0, 1, 1f);
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

            static void Postfix(StarmapRenderer __instance, Faction faction, GameObject logo) {
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
    }
}