namespace DocsToSystemJSON {
    class Program {
        static string dataPath = "C:/Users/morph/Source/Repos/InnerSphereMap/DocsToSystemJSON/DocsToSystemJSON/InnerSphereData.json";
        static string arrayName = "BACKUP";
        static string outputPath = "C:/Users/morph/Source/Repos/InnerSphereMap/InnerSphereMap_data";
        static string BlueprintPath = "C:/Users/morph/Source/Repos/InnerSphereMap/DocsToSystemJSON/DocsToSystemJSON/Blueprint.json";
        static string originalData = "C:/Program Files (x86)/Steam/steamapps/common/BATTLETECH/BattleTech_Data/StreamingAssets/data/starsystem";

        static void Main(string[] args) {
            Converter converter = new Converter(dataPath, arrayName, outputPath, BlueprintPath, true, originalData, false);
            converter.newMap();

        }
    }
}