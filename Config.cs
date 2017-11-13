using System;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Crescent
{
	class Config
	{
		public static Color[] BarColor = new Color[3];
		public static byte Theme = 1;
		public static bool HalfsizeHealthbar = false;
		public static bool MonsterLeveling = true;

		static string ConfigPath = Path.Combine(Main.SavePath, "Mod Configs", "Crescent.json");

		public static Preferences Configuration = new Preferences(ConfigPath);

		public static void Load()
		{
			bool success = ReadConfig();

			if (!success)
			{
				ErrorLogger.Log("Failed to read Crescent's config file! Recreating config...");
				CreateConfig();
			}
		}

		private static bool ReadConfig()
		{
			if (Configuration.Load())
			{
				BarColor[0] = GetColor("HealthBarColor");
				BarColor[1] = GetColor("ManaBarColor");
				BarColor[2] = GetColor("ExpBarColor");
				Theme = (byte)Configuration.Get("Theme", 1);
				HalfsizeHealthbar = Configuration.Get("HalfsizeHealthbar", false);
				MonsterLeveling = Configuration.Get("MonsterLeveling", true);
				return true;
			}
			return false;
		}

		private static Color GetColor(string v){
			List<int> n = Configuration.Get(v, "0,0,0").Split(',').Select(s => Int32.Parse(s)).ToList();
			return new Color(n[0], n[1], n[2]);
		}

		static void CreateConfig()
		{
			Configuration.Clear();
			Configuration.Put("HealthBarColor", "255,0,0");
			Configuration.Put("ManaBarColor", "0,0,255");
			Configuration.Put("ExpBarColor", "0,127,0");
			Configuration.Put("Theme", 1);
			Configuration.Put("HalfsizeHealthbar", false);
			Configuration.Put("MonsterLeveling", true);
			Configuration.Save();
			ReadConfig();
		}
	}
}
