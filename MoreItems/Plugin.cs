using System.Reflection;
using BepInEx;
using HarmonyLib;
using LethalLib;
using LethalLib.Modules;
using UnityEngine;
using System.IO;
using System;

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

            string assetDir = Path.Combine(Assembly.GetExecutingAssembly().Location, "../czechitems");
            AssetBundle bundle = AssetBundle.LoadFromFile(assetDir);

            InitializeNetworkBehaviours();

            string enemyDir = Path.Combine(Assembly.GetExecutingAssembly().Location, "../enemies");
            AssetBundle enemyBundle = AssetBundle.LoadFromFile(enemyDir);

            var teacher = enemyBundle.LoadAsset<EnemyType>("Assets/Enemies/TeachCharvat/Charvat.asset");
            var teacherTK = enemyBundle.LoadAsset<TerminalKeyword>("Assets/Enemies/TeachCharvat/CharvatTK.asset");
            var teacherTN = enemyBundle.LoadAsset<TerminalNode>("Assets/Enemies/TeachCharvat/CharvatTN.asset");


            
            Item cocaine = bundle.LoadAsset<Item>("Assets/Coke.asset");
            NetworkPrefabs.RegisterNetworkPrefab(cocaine.spawnPrefab);
            Utilities.FixMixerGroups(cocaine.spawnPrefab);
            Items.RegisterScrap(cocaine, 50, Levels.LevelTypes.All);

            Item bitcoin = bundle.LoadAsset<Item>("Assets/items/bitcoin/Bitcoin.asset");
            NetworkPrefabs.RegisterNetworkPrefab(bitcoin.spawnPrefab);
            Utilities.FixMixerGroups(bitcoin.spawnPrefab);
            Items.RegisterScrap(bitcoin, 50, Levels.LevelTypes.All);

            Item milk = bundle.LoadAsset<Item>("Assets/items/milk/milk.asset");
            NetworkPrefabs.RegisterNetworkPrefab(milk.spawnPrefab);
            Utilities.FixMixerGroups(milk.spawnPrefab);
            Items.RegisterScrap(milk, 50, Levels.LevelTypes.All);

            Item pizza = bundle.LoadAsset<Item>("Assets/items/pizza/Pizza.asset");
            NetworkPrefabs.RegisterNetworkPrefab(pizza.spawnPrefab);
            Utilities.FixMixerGroups(pizza.spawnPrefab);
            Items.RegisterScrap(pizza, 50, Levels.LevelTypes.All);
            

            NetworkPrefabs.RegisterNetworkPrefab(teacher.enemyPrefab);

            Enemies.RegisterEnemy(teacher, 250, Levels.LevelTypes.All, Enemies.SpawnType.Default, teacherTN, teacherTK);

            foreach (Enemies.SpawnableEnemy enemy in Enemies.spawnableEnemies)
            {
                Logger.LogInfo(enemy.enemy.enemyName);
            }

            Logger.LogInfo("Patched Items And Enemies");
        }


        private static void InitializeNetworkBehaviours()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes(typeof(RuntimeInitializeOnLoadMethodAttribute), false);
                    if (attributes.Length > 0)
                    {
                        method.Invoke(null, null);
                    }
                }
            }
        }



    }


}
