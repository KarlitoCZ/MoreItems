using System.Reflection;
using BepInEx;
using HarmonyLib;
using LethalLib;
using LethalLib.Modules;
using UnityEngine;
using System.IO;

namespace MoreItems
{

    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    [BepInDependency(LethalLib.Plugin.ModGUID)]
    public class Plugin : BaseUnityPlugin
    {

        const string PLUGIN_GUID = "Karlito.CzechItems";
        const string PLUGIN_NAME = " Czech Items";
        const string PLUGIN_VERSION = "1.0.0";

        void Awake()
        {
            string assetDir = Path.Combine(Assembly.GetExecutingAssembly().Location, "../itemmod");
            AssetBundle bundle = AssetBundle.LoadFromFile(assetDir);


            Item cocaine = bundle.LoadAsset<Item>("Assets/Coke.asset");
            NetworkPrefabs.RegisterNetworkPrefab(cocaine.spawnPrefab);
            Utilities.FixMixerGroups(cocaine.spawnPrefab);
            Items.RegisterScrap(cocaine, 10, Levels.LevelTypes.All);

            Item bitcoin = bundle.LoadAsset<Item>("Assets/items/bitcoin/Bitcoin.asset");
            NetworkPrefabs.RegisterNetworkPrefab(bitcoin.spawnPrefab);
            Utilities.FixMixerGroups(bitcoin.spawnPrefab);
            Items.RegisterScrap(bitcoin, 10, Levels.LevelTypes.All);

            Item milk = bundle.LoadAsset<Item>("Assets/items/milk/milk.asset");
            NetworkPrefabs.RegisterNetworkPrefab(milk.spawnPrefab);
            Utilities.FixMixerGroups(milk.spawnPrefab);
            Items.RegisterScrap(milk, 10, Levels.LevelTypes.All);

            Logger.LogInfo("Patched Items");
        }

    }
}
