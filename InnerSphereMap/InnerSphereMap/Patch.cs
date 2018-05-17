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

    [HarmonyPatch(typeof(SimGameState), "DoesFactionGainReputation")]
    public static class SimGameState_DoesFactionGainReputation {
        
        static void Postfix(SimGameState __instance, ref bool __result, Faction fac) {
            try {
                __result = fac < Faction.Player1sMercUnit && fac != Faction.MercenaryReviewBoard && fac != Faction.NoFaction && fac != Faction.Locals;
            }
            catch (Exception e) {
                Logger.LogError(e);
            }
        }
    }

    [HarmonyPatch(typeof(SimGameState), "OnTargetSystemFound")]
    public static class SimGameState_OnTargetSystemFound {

        static bool Prefix(SimGameState __instance) {
            try {
                Logger.LogLine("Start");
                __instance.CurSystem.RefreshSystem();
                if (__instance.UXAttached) {
                    __instance.RoomManager.ShipRoom.RefreshData();
                }
                Logger.LogLine("BeforeInvoke");
                ReflectionHelper.InvokePrivateMethode(__instance, "SetReputation", new object[]{ Faction.Owner, __instance.CurSystem.OwnerReputation, StatCollection.StatOperation.Set, null });
                Logger.LogLine("AfterInvoke");
                List<StarSystem> travels = __instance.StarSystems;
                travels.Shuffle<StarSystem>();
                __instance.GeneratePotentialContracts(true, null, travels[0], false);
                Logger.LogLine("End");
                return false;
            }
            catch (Exception e) {
                Logger.LogError(e);
                return false;
            }
        }
    }

    [HarmonyPatch(typeof(StarSystem), "RefreshBreadcrumbs")]
    public static class StarSystem_RefreshBreadcrumbs {

        static bool Prefix(StarSystem __instance) {
            try {
                if (__instance.CurBreadcrumbOverride > 0) {
                    ReflectionHelper.InvokePrivateMethode(__instance, "set_CurMaxBreadcrumbs", new object[] { __instance.CurBreadcrumbOverride });
                }
                else {
                    int num = __instance.MissionsCompleted;
                    if (num < __instance.Sim.Constants.Story.MissionsForFirstBreadcrumb) {
                        return false;
                    }
                    ReflectionHelper.InvokePrivateMethode(__instance, "set_CurMaxBreadcrumbs", new object[] { __instance.Sim.Constants.Story.MaxBreadcrumbsPerSystem });
                }

                return false;
            }
            catch (Exception e) {
                Logger.LogError(e);
                return false;
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