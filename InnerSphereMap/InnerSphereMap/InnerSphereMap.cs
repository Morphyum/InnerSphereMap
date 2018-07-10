using Harmony;
using System.Reflection;

namespace InnerSphereMap
{
    public class InnerSphereMap
    {
        internal static string ModDirectory;
         
        public static void Init(string directory, string settingsJSON) {
            
            var harmony = HarmonyInstance.Create("de.morphyum.InnerSphereMap");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            ModDirectory = directory;
        }
    }


}
