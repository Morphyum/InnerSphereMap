using Harmony;
using System.Reflection;

namespace InnerSphereMap
{
    public class InnerSphereMap
    {
        public const string HarmonyPackage = "de.morphyum.InnerSphereMap";
        public const string LogName = "inner_sphere_map";

        internal static string ModDirectory;

        public static Settings SETTINGS;
        public static Logger Logger;

        public static void Init(string directory, string settingsJSON) {
            ModDirectory = directory;
            SETTINGS = Helper.LoadSettings();

            Logger = new Logger(directory, LogName);

            var harmony = HarmonyInstance.Create(HarmonyPackage);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }


}
