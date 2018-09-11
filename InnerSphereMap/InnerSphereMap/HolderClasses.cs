using BattleTech;
using BattleTech.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace InnerSphereMap {
    public class Settings {
        public List<float> KuritaRGB;
        public List<float> SteinerRGB;
        public List<float> OutworldsRGB;
        public List<float> OberonRGB;
        public List<float> LothianRGB;
        public List<float> MarianRGB;
        public List<float> CircinusRGB;
        public List<float> IllyrianRGB;
        public List<float> DavionRGB;
        public List<float> LiaoRGB;
        public List<float> MarikRGB;
        public List<float> TaurianRGB;
        public List<float> MagistracyRGB;
        public List<float> RestorationRGB;
        public List<float> RasalhagueRGB;
        public List<float> StIvesRGB;
        public List<float> AbandonedRGB;
        public List<float> MRBRGB;
        public List<float> ComStarRGB;
        public List<float> CastileRGB;
        public List<float> ChainelaneRGB;
        public List<float> ClanBurrockRGB;
        public List<float> ClanCloudCobraRGB;
        public List<float> ClanCoyoteRGB;
        public List<float> ClanDiamondSharkRGB;
        public List<float> ClanFireMandrillRGB;
        public List<float> ClanGhostBearRGB;
        public List<float> ClanGoliathScorpionRGB;
        public List<float> ClanHellsHorsesRGB;
        public List<float> ClanIceHellionRGB;
        public List<float> ClanJadeFalconRGB;
        public List<float> ClanNovaCatRGB;
        public List<float> ClansGenericRGB;
        public List<float> ClanSmokeJaguarRGB;
        public List<float> ClanSnowRavenRGB;
        public List<float> ClanStarAdderRGB;
        public List<float> ClanSteelViperRGB;
        public List<float> ClanWolfRGB;
        public List<float> DelphiRGB;
        public List<float> ElysiaRGB;
        public List<float> HanseRGB;
        public List<float> JarnFolkRGB;
        public List<float> TortugaRGB;
        public List<float> ValkyrateRGB;
        public List<float> AxumiteRGB;

        public float MinFov; // this is the vertical FOV
        public float MaxFov; // this is the vertical FOV

        public float MapWidth;
        public float MapHeight;

        public float MapTopViewBuffer;
        public float MapLeftViewBuffer;
        public float MapRightViewBuffer;
        public float MapBottomViewBuffer;

        public bool use3040Map = false;


    }

    public class Fields {
        public static float cbill = 0;
        public static Transform originalTransform = null;
    }

    public struct PotentialContract {
        // Token: 0x040089A4 RID: 35236
        public ContractOverride contractOverride;

        // Token: 0x040089A5 RID: 35237
        public Faction employer;

        // Token: 0x040089A6 RID: 35238
        public Faction target;

        // Token: 0x040089A7 RID: 35239
        public int difficulty;
    }
 
}