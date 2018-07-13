using BattleTech;
using BattleTech.Framework;
using System.Collections.Generic;

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

        public float MinFov; // this is the vertical FOV
        public float MaxFov; // this is the vertical FOV

        public float MapWidth;
        public float MapHeight;

        public float MapTopViewBuffer;
        public float MapLeftViewBuffer;
        public float MapRightViewBuffer;
        public float MapBottomViewBuffer;


    }

    public class Fields {
        public static float cbill = 0;
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