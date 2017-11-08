using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Crescent.UI;
using Terraria.UI;

namespace Crescent
{
	public class Crescent : Mod
	{
		internal static Crescent mod;
		private UserInterface LifeForceInterface;
		internal LifeForceUI LifeForceUI;
		public static ModHotKey keySkill;
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

		public override void Load()
		{
			thoriumLoaded = ModLoader.GetMod("ThoriumMod") != null;
			tremorLoaded = ModLoader.GetMod("Tremor") != null;
			mod = this;
			if (!Main.dedServ)
			{
				keySkill = RegisterHotKey("Skill", "F");
				LifeForceUI = new LifeForceUI();
				LifeForceUI.Activate();
				LifeForceInterface = new UserInterface();
				LifeForceInterface.SetState(LifeForceUI);
			}
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
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
							InterfaceScaleType.UI)
						);
				}
			}
		}
	}
}
