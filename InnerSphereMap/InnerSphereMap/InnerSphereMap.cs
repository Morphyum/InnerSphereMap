using Harmony;
using System.Reflection;

namespace InnerSphereMap
{
    public class InnerSphereMap
    {
        internal static string ModDirectory;

        public static Settings SETTINGS;

        public static void Init(string directory, string settingsJSON) {
            ModDirectory = directory;
            SETTINGS = Helper.LoadSettings();
            var harmony = HarmonyInstance.Create("de.morphyum.InnerSphereMap");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
