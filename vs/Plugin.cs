using BepInEx;
using HarmonyLib;
using UnityEngine;
using System.Reflection;
using ModelReplacement;
using BepInEx.Configuration;
using System;
using System.Xml.Linq;

namespace EbusSuits
{
	[BepInPlugin("dev.lucario.ebus_suits", "ebus_suits", "0.1.0")]
	[BepInDependency("meow.ModelReplacementAPI", BepInDependency.DependencyFlags.HardDependency)]
	public class Plugin : BaseUnityPlugin
	{
		public static ConfigFile config;

		// Example Config for single model mod
		public static ConfigEntry<bool> enableModelForAllSuits { get; private set; }
		public static ConfigEntry<bool> enableModelAsDefault { get; private set; }
		public static ConfigEntry<string> suitNamesToEnableModel { get; private set; }

		private static void InitConfig()
		{
			// enableModelForAllSuits = config.Bind<bool>("Suits to Replace Settings", "Enable Model for all Suits", false, "Enable to model replace every suit. Set to false to specify suits");
			// enableModelAsDefault = config.Bind<bool>("Suits to Replace Settings", "Enable Model as default", false, "Enable to model replace every suit that hasn't been otherwise registered.");
			// suitNamesToEnableModel = config.Bind<string>("Suits to Replace Settings", "Suits to enable Model for", "Default,Orange suit", "Enter a comma separated list of suit names.(Additionally, [Green suit,Pajama suit,Hazard suit])");
			ModelReplacementAPI.RegisterSuitModelReplacement("Ebu", typeof(MREBU));
		}
		private void Awake()
		{
			config = base.Config;
			InitConfig();
			Assets.PopulateAssets();

			// Plugin startup logic
			if (enableModelForAllSuits.Value)
			{
				ModelReplacementAPI.RegisterModelReplacementOverride(typeof(MREBU));

			}
			if (enableModelAsDefault.Value)
			{
				ModelReplacementAPI.RegisterModelReplacementDefault(typeof(MREBU));

			}
			var commaSepList = suitNamesToEnableModel.Value.Split(',');
			foreach (var item in commaSepList)
			{
				ModelReplacementAPI.RegisterSuitModelReplacement(item, typeof(MREBU));
			}


			Harmony harmony = new Harmony("dev.lucario.ebus_suits");
			harmony.PatchAll();
			Logger.LogInfo($"Plugin {"dev.lucario.ebus_suits"} is loaded!");
		}
	}
	public static class Assets
	{
		// Replace mbundle with the Asset Bundle Name from your unity project 
		public static string mainAssetBundleName = "ebus-suits";
		public static AssetBundle MainAssetBundle = null;

		private static string GetAssemblyName() => Assembly.GetExecutingAssembly().GetName().Name.Replace(" ", "_");
		public static void PopulateAssets()
		{
			if (MainAssetBundle == null)
			{
				Console.WriteLine(GetAssemblyName() + "." + mainAssetBundleName);
				using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(GetAssemblyName() + "." + mainAssetBundleName))
				{
					MainAssetBundle = AssetBundle.LoadFromStream(assetStream);
				}

			}
		}
	}

}