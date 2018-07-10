using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace InnerSphereMap
{
    class CameraHelper
    {
        // Wide monitors are 16/9 -- some are 'more square` than this, but if we handle the widest one by default we should be safe
        static float MonitorAspectRatio = 16f / 9f;

        public static float GetHorizontalFov(float verticalFovDegrees)
        {
            return 2.0f * Mathf.Atan(MonitorAspectRatio * Mathf.Tan(verticalFovDegrees * Mathf.Deg2Rad / 2.0f)) * Mathf.Rad2Deg;
        }

        // This assumes the camera is stuck at -100.0f for its z position
        public static float GetViewSize(float cameraDistance, float cameraFovDegrees)
        {
            return cameraDistance * Mathf.Tan(cameraFovDegrees * Mathf.Deg2Rad / 2.0f);
        }

    }
}
