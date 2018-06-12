using BattleTech;
using BattleTech.Framework;
using System.Collections.Generic;

namespace InnerSphereMap {
    public class Settings {
        public float percentageOfMechCost = 0.0025f;

        public bool CostByTons = false;
        public int cbillsPerTon = 500;

        public List<float> KuritaRGB = new List<float>() { 0.863f, 0.078f, 0.235f };
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