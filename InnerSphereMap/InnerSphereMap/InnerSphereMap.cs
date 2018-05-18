using Harmony;
using System.Reflection;

namespace InnerSphereMap
{
    public class InnerSphereMap
    {
        public static void Init() {
            var harmony = HarmonyInstance.Create("de.morphyum.InnerSphereMap");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }


}
