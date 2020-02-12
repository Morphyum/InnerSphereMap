using BattleTech;
using BattleTech.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace InnerSphereMap {
    public class Settings {

        public float MinFov; // this is the vertical FOV
        public float MaxFov; // this is the vertical FOV

        public float MapWidth;
        public float MapHeight;

        public float MapTopViewBuffer;
        public float MapLeftViewBuffer;
        public float MapRightViewBuffer;
        public float MapBottomViewBuffer;

        public string splashTitle = "";
        public string splashText = "";

        public List<LogoItem> logos = new List<LogoItem>();
        public bool reducedClanLogos = true;    

    }

    public class LogoItem
    {
        public string factionName = "";
        public string logoImage = "";
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