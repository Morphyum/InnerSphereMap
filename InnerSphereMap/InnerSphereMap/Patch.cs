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