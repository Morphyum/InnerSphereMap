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

        public float MinFov; // this is the vertical FOV
        public float MaxFov; // this is the vertical FOV
        public float MapWidth;
        public float MapHeight;

        // Wide monitors are 16/9 -- some are 'more square` than this, but if we handle the widest one by default we should be safe
        static float MonitorAspectRatio = 16f / 9f;
        static float FixedCameraDistance = 100.0f; // This is from HBS's code
        static float ViewSizeScaling = 0.9f; // Pretend the view is slightly smaller so the very edges can get further into the screen

        public static float GetHorizontalFov(float verticalFovDegrees)
        {
            return 2.0f * Mathf.Atan(MonitorAspectRatio * Mathf.Tan(verticalFovDegrees * Mathf.Deg2Rad / 2.0f)) * Mathf.Rad2Deg;
        }

        public float GetXBoundaryForMinFov()
        {
            float viewSize = GetViewSize(FixedCameraDistance, GetHorizontalFov(MinFov));
            return MapWidth - (viewSize * ViewSizeScaling);
        }

        public float GetXBoundaryForMaxFov()
        {
            float viewSize = GetViewSize(FixedCameraDistance, GetHorizontalFov(MaxFov));
            return MapWidth - (viewSize * ViewSizeScaling);
        }

        public float GetYBoundaryForMinFov()
        {
            float viewSize = GetViewSize(FixedCameraDistance, MinFov);
            return MapHeight - (viewSize * ViewSizeScaling);
        }

        public float GetYBoundaryForMaxFov()
        {
            float viewSize = GetViewSize(FixedCameraDistance, MaxFov);
            return MapHeight - (viewSize * ViewSizeScaling);
        }

        // This assumes the camera is stuck at -100.0f for its z position
        public float GetViewSize(float cameraDistance, float cameraFovDegrees)
        {
            return cameraDistance * Mathf.Tan(cameraFovDegrees * Mathf.Deg2Rad / 2.0f);
        }
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