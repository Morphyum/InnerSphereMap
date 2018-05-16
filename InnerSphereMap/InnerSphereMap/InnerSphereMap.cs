using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
