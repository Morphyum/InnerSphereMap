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

    /*
       * HELPER HARMONY FUNCTION -- for looking for active game objects
       * and using a csv file to disable them while the game is running
       * 
      static bool Prefix(StarmapRenderer __instance) {
          var lines = File.ReadAllLines($"{ InnerSphereMap.ModDirectory}/activations.csv").Select(vals => vals.Split(','));
          foreach (var line in lines) {
              try {
                  if (line.Length == 2) {
                      string name = line[0];
                      bool value = bool.Parse(line[1]);
                      var gameObject = GameObject.Find(name);
                      if (gameObject != null) {
                          gameObject.SetActive(value);
                      }
                  }
              }
              catch (Exception e) {
                  Logger.LogError(e);
              }
          }

          List<LineRenderer> renderers = new List<LineRenderer>(UnityEngine.Object.FindObjectsOfType<LineRenderer>());
          List<LineRenderer> filteredList = renderers.Where(r => {
              return r.name != null && !r.name.Contains("StarInner") && !r.name.Contains("StarOuter") && !r.name.Contains("Logo");
          }).ToList<LineRenderer>();

          if (filteredList.Count > 90000) {
              Logger.LogLine(filteredList.Count.ToString());
          }

          return true;
      }*/
}
