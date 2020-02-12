namespace DocsToSystemJSON {
    class Program {
        static string dataPath = "C:/Users/morph/Source/Repos/InnerSphereMap/DocsToSystemJSON/DocsToSystemJSON/InnerSphereData.json";
        static string arrayName = "Kopie von BACKUP";
        static string outputPath = "C:/Users/morph/Source/Repos/InnerSphereMap/InnerSphereMap_data";
        static string BlueprintPath = "C:/Users/morph/Source/Repos/InnerSphereMap/DocsToSystemJSON/DocsToSystemJSON/Blueprint.json";
        static string originalData = "D:/SteamLibrary/steamapps/common/BATTLETECH/BattleTech_Data/StreamingAssets/data/starsystem";
        static string galaxyPath = "C:/Users/morph/Source/Repos/InnerSphereMap/DocsToSystemJSON/DocsToSystemJSON/mod.json";
        static void Main(string[] args) {
            Converter converter = new Converter(dataPath, arrayName, outputPath, BlueprintPath, 2, originalData, true, true, galaxyPath);
            converter.newMap();
        }
    }
}