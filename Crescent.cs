using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Crescent.UI;
using Terraria.UI;

namespace Crescent
{
	public class Crescent : Mod
	{
		internal static Crescent mod;
		public const int NUMSTATS = 8;
		public const int NUMPERKS = 6;
		public const int NUMSKILLS = 2;
		private UserInterface LifeForceInterface;
		internal UserInterface VanillaThemeInterface;
		internal UserInterface SettingsInterface;
		internal LifeForceUI LifeForceUI;
		internal VanillaThemeUI VanillaThemeUI;
		internal SettingsUI SettingsUI;
		public static ModHotKey keySkill;
		public static ModHotKey keyConfig;
		public bool UIOpen;
		public bool thoriumLoaded;
		public bool tremorLoaded;

		public Crescent()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true,
				AutoloadBackgrounds = true
			};
		}

		public override void PreSaveAndQuit()
		{
			mod.LifeForceUI.HideButtonClicked(2);
			//mod.LifeForceUI.PerkDescClose();
			mod.VanillaThemeUI.HideButtonClicked(2);
			//mod.VanillaThemeUI.PerkDescClose();
			mod.SettingsUI.SettingsToggle(false);
			Config.Configuration.Clear();
			Config.Configuration.Put("HealthBarColor", Config.BarColor[0].R + "," + Config.BarColor[0].G + "," + Config.BarColor[0].B);
			Config.Configuration.Put("ManaBarColor", Config.BarColor[1].R + "," + Config.BarColor[1].G + "," + Config.BarColor[1].B);
			Config.Configuration.Put("ExpBarColor", Config.BarColor[2].R + "," + Config.BarColor[2].G + "," + Config.BarColor[2].B);
			Config.Configuration.Put("Theme", Config.Theme);
			Config.Configuration.Put("HalfsizeHealthbar", Config.HalfsizeHealthbar);
			Config.Configuration.Put("MonsterLeveling", Config.MonsterLeveling);
			Config.Configuration.Save(false);
		}

		public override void Load()
		{
			Config.Load();
			thoriumLoaded = ModLoader.GetMod("ThoriumMod") != null;
			tremorLoaded = ModLoader.GetMod("Tremor") != null;
			mod = this;
			if (!Main.dedServ)
			{
				keySkill = RegisterHotKey("Skill", "F");
				keyConfig = RegisterHotKey("Config", "OemPipe");

				LifeForceUI = new LifeForceUI();
				LifeForceUI.Activate();
				LifeForceInterface = new UserInterface();
				LifeForceInterface.SetState(LifeForceUI);

				VanillaThemeUI = new VanillaThemeUI();
				VanillaThemeUI.Activate();
				VanillaThemeInterface = new UserInterface();
				VanillaThemeInterface.SetState(VanillaThemeUI);

				SettingsUI = new SettingsUI();
				SettingsUI.Activate();
				SettingsInterface = new UserInterface();
				SettingsInterface.SetState(SettingsUI);
			}
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			if(Config.Theme != 0)
			{
				for (int i = 0; i < layers.Count; i++)
				{
					if (layers[i].Name.Contains("Resource Bars"))
					{
						layers.RemoveAt(i);
						layers.Insert(i, new LegacyGameInterfaceLayer(
								"Lifeforce UI",
								delegate
								{
									if (LifeForceUI.visible)
									{
										LifeForceInterface.Update(Main._drawInterfaceGameTime);
										LifeForceUI.Draw(Main.spriteBatch);
									}

									return true;
								},
								InterfaceScaleType.UI));
					}
				}
			}
			else
			{
				layers.Insert(0, new LegacyGameInterfaceLayer(
								"VanillaTheme UI",
								delegate
								{
									if (VanillaThemeUI.visible)
									{
										VanillaThemeInterface.Update(Main._drawInterfaceGameTime);
										VanillaThemeUI.Draw(Main.spriteBatch);
									}

									return true;
								},
								InterfaceScaleType.UI));
			}

			layers.Insert(0, new LegacyGameInterfaceLayer(
							"Settings UI",
							delegate
							{
								SettingsInterface.Update(Main._drawInterfaceGameTime);
								SettingsUI.Draw(Main.spriteBatch);
								return true;
							},
							InterfaceScaleType.UI));
		}
	}
}
