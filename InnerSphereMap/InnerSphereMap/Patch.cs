using BattleTech;
using BattleTech.Framework;
using BattleTech.UI;
using Harmony;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace InnerSphereMap {

    [HarmonyPatch(typeof(StarmapBorders), "OnWillRenderObject")]
    public static class StarmapBorders_OnWillRenderObject {

        static bool Prefix(StarmapBorders __instance) {
            return false;
        }
    }

    [HarmonyPatch(typeof(StarmapRenderer), "FactionColor")]
    public static class StarmapRenderer_FactionColor {

        static void Postfix(StarmapRenderer __instance, ref Color __result, Faction thisFaction) {
            try {
                if (__result == __instance.nofactionColor) {
                    switch (thisFaction) {
                        case Faction.Kurita:
                            __result = new Color(0.7f, 0f, 0f, 1f);
                            break;
                        case Faction.Steiner:
                            __result = new Color(0f, 0f, 0.7f, 1f);
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