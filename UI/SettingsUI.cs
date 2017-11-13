using ReLogic.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ID;
using System;
using Terraria.ModLoader;
using Terraria.GameInput;
using Terraria.Localization;

namespace Crescent.UI
{
	public class SettingsUI : UIState
	{
		public UIPanel Settings = new UIPanel();
		private UIPanel SelectedTheme = new UIPanel() { BackgroundColor = Color.Yellow * 0.5f };
		private UIPanel ThemeSelectVanilla = new UIPanel();
		private UIImage ThemeSelectCrescent = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/ThemeCrescent/ThemeIcon"));
		private UIText[] ThemeSelectText = new UIText[2];
		private UIText[] InfoText = new UIText[2];
		private UIPanel[] SettingsBarColorPanel = new UIPanel[3];
		private UIText[] SettingsBarLabel = new UIText[3];
		private UIText[,] SettingsBarColorText = new UIText[3, 3];
		private UIText[,,] SettingsBarColorMod = new UIText[3, 3, 2];
		private UIText[] SettingsBoolsText = new UIText[2];
		private UIPanel[] SettingsBoolsPanel = new UIPanel[2];

		public override void OnInitialize()
		{
			Settings.Width.Set(640f, 0f); Settings.Height.Set(480f, 0f);
			Settings.Left.Set(-Settings.Width.Pixels / 2, 0.5f); Settings.Top.Set(-Settings.Height.Pixels / 2, 0.5f);
			Settings.BackgroundColor = Color.DarkBlue * 0.5f;

			UIText SettingsTitle = new UIText("Crescent Settings"); SettingsTitle.Left.Set(-SettingsTitle.MinWidth.Pixels / 2, 0.5f);
			Settings.Append(SettingsTitle);

			UIText SettingsHide = new UIText("-"); SettingsHide.Left.Set(-SettingsHide.MinWidth.Pixels, 1f); SettingsHide.OnClick += (a, b) => SettingsToggle(false);
			Settings.Append(SettingsHide);

			UIPanel SettingsInnerPanel = new UIPanel();
			SettingsInnerPanel.Width.Set(620f, 0f); SettingsInnerPanel.Height.Set(440f, 0f);
			SettingsInnerPanel.Left.Set(0f, 0f); SettingsInnerPanel.Top.Set(20f, 0f);
			Settings.Append(SettingsInnerPanel);

			SettingsBarLabel[0] = new UIText("Health Bar Color:");
			SettingsBarLabel[1] = new UIText("Mana Bar Color:");
			SettingsBarLabel[2] = new UIText("Exp Bar Color:");

			for (byte i = 0; i < 3; i++){
				byte x = i;
				SettingsBarColorPanel[i] = new UIPanel() { BackgroundColor = Config.BarColor[i] };
				SettingsBarColorPanel[i].Width.Set(25f, 0f); SettingsBarColorPanel[i].Height.Set(25f, 0f);
				SettingsBarColorPanel[i].Left.Set(-25f, 0.5f); SettingsBarColorPanel[i].Top.Set((i * 30f) + 24, 0f);
				SettingsInnerPanel.Append(SettingsBarColorPanel[i]);
				SettingsBarLabel[i].Top.Set(SettingsBarColorPanel[i].Height.Pixels / 2 - SettingsBarLabel[i].MinHeight.Pixels / 2 + (i * 30f) + 24, 0f);
				SettingsBarLabel[i].Left.Set(0, 0.1f);
				SettingsInnerPanel.Append(SettingsBarLabel[i]);
				SettingsBarColorText[i, 0] = new UIText(Config.BarColor[i].R.ToString()) { TextColor = Color.Red };
				SettingsBarColorText[i, 1] = new UIText(Config.BarColor[i].G.ToString()) { TextColor = Color.LightGreen };
				SettingsBarColorText[i, 2] = new UIText(Config.BarColor[i].B.ToString()) { TextColor = Color.Blue };
				for (byte v = 0; v < 3; v++){
					byte y = v;
					SettingsBarColorText[i, v].Left.Set(-SettingsBarColorText[i, v].MinWidth.Pixels / 2, 0.75f + ((v - 1) * 1 / 8f));
					SettingsBarColorText[i, v].Top.Set(SettingsBarColorPanel[i].Height.Pixels / 2 - SettingsBarColorText[i, v].MinHeight.Pixels / 2f + (i * 30f) + 24, 0f);
					SettingsInnerPanel.Append(SettingsBarColorText[i, v]);
					SettingsBarColorMod[i, v, 0] = new UIText("-");
					SettingsBarColorMod[i, v, 0].Left.Set(-SettingsBarColorMod[i, v, 0].MinWidth.Pixels / 2, 0.71f + ((v - 1) * 1 / 8f));
					SettingsBarColorMod[i, v, 0].Top.Set(SettingsBarColorPanel[i].Height.Pixels / 2 - SettingsBarColorText[i, v].MinHeight.Pixels / 2f + (i * 30f) + 24, 0f);
					SettingsBarColorMod[i, v, 0].OnClick += (a, b) => ColorButtonPress(false, y, x, 1);
					SettingsBarColorMod[i, v, 0].OnRightClick += (a, b) => ColorButtonPress(false, y, x, 32);
					SettingsInnerPanel.Append(SettingsBarColorMod[i, v, 0]);
					SettingsBarColorMod[i, v, 1] = new UIText("+");
					SettingsBarColorMod[i, v, 1].Left.Set(-SettingsBarColorMod[i, v, 1].MinWidth.Pixels / 2, 0.79f + ((v - 1) * 1 / 8f));
					SettingsBarColorMod[i, v, 1].Top.Set(SettingsBarColorPanel[i].Height.Pixels / 2 - SettingsBarColorText[i, v].MinHeight.Pixels / 2f + (i * 30f) + 24, 0f);
					SettingsBarColorMod[i, v, 1].OnClick += (a, b) => ColorButtonPress(true, y, x, 1);
					SettingsBarColorMod[i, v, 1].OnRightClick += (a, b) => ColorButtonPress(true, y, x, 32);
					SettingsInnerPanel.Append(SettingsBarColorMod[i, v, 1]);
				}
			}

			SettingsBoolsText[0] = new UIText("Half-Sized Healthbar:");
			SettingsBoolsText[0].Top.Set(SettingsBarColorPanel[0].Height.Pixels / 2 - SettingsBoolsText[0].MinHeight.Pixels / 2 + (3 * 30f) + 24, 0f);
			SettingsBoolsText[0].Left.Set(-SettingsBoolsText[0].MinWidth.Pixels / 2, 1 / 3f);
			SettingsInnerPanel.Append(SettingsBoolsText[0]);
			SettingsBoolsPanel[0] = new UIPanel() { BackgroundColor = Config.HalfsizeHealthbar ? Color.White : Color.Black };
			SettingsBoolsPanel[0].Width.Set(25f, 0f); SettingsBoolsPanel[0].Height.Set(25f, 0f);
			SettingsBoolsPanel[0].Top.Set((3 * 30f) + 24, 0f);
			SettingsBoolsPanel[0].Left.Set(-SettingsBoolsPanel[0].Width.Pixels / 2, 2 / 3f);
			SettingsBoolsPanel[0].OnClick += (a, b) => BoolPress(0);
			SettingsInnerPanel.Append(SettingsBoolsPanel[0]);
			SettingsBoolsText[1] = new UIText("Expert Enemy Leveling:");
			SettingsBoolsText[1].Top.Set(SettingsBarColorPanel[1].Height.Pixels / 2 - SettingsBoolsText[1].MinHeight.Pixels / 2 + (4 * 30f) + 24, 0f);
			SettingsBoolsText[1].Left.Set(-SettingsBoolsText[1].MinWidth.Pixels / 2, 1 / 3f);
			SettingsInnerPanel.Append(SettingsBoolsText[1]);
			SettingsBoolsPanel[1] = new UIPanel() { BackgroundColor = Config.MonsterLeveling ? Color.White : Color.Black };
			SettingsBoolsPanel[1].Width.Set(25f, 0f); SettingsBoolsPanel[1].Height.Set(25f, 0f);
			SettingsBoolsPanel[1].Top.Set((4 * 30f) + 24, 0f);
			SettingsBoolsPanel[1].Left.Set(-SettingsBoolsPanel[1].Width.Pixels / 2, 2 / 3f);
			SettingsBoolsPanel[1].OnClick += (a, b) => BoolPress(1);
			SettingsInnerPanel.Append(SettingsBoolsPanel[1]);

			InfoText[0] = new UIText("Right click to increment/decrement by 32");
			InfoText[0].Left.Set(-InfoText[0].MinWidth.Pixels / 2, 0.5f);
			SettingsInnerPanel.Append(InfoText[0]);
			InfoText[1] = new UIText("Note: Changing to or from Vanilla MIGHT require a reload");
			InfoText[1].Left.Set(-InfoText[1].MinWidth.Pixels / 2, 0.5f); InfoText[1].Top.Set(-64f - InfoText[1].MinHeight.Pixels, 0.75f);
			SettingsInnerPanel.Append(InfoText[1]);

			ThemeSelectText[0] = new UIText("Vanilla [BETA]");
			ThemeSelectText[0].Left.Set(-ThemeSelectText[0].MinWidth.Pixels / 2f, 1 / 3f); ThemeSelectText[0].Top.Set(64, 0.75f);
			SettingsInnerPanel.Append(ThemeSelectText[0]);
			ThemeSelectText[1] = new UIText("Crescent");
			ThemeSelectText[1].Left.Set(-ThemeSelectText[1].MinWidth.Pixels / 2f, 2 / 3f); ThemeSelectText[1].Top.Set(64, 0.75f);
			SettingsInnerPanel.Append(ThemeSelectText[1]);

			SelectedTheme.Width.Set(128f, 0f); SelectedTheme.Height.Set(128f, 0f);
			SelectedTheme.Left.Set(-64f, (1/3f) + (Config.Theme*(1/3f))); SelectedTheme.Top.Set(-64f, 0.75f);
			SettingsInnerPanel.Append(SelectedTheme);

			ThemeSelectCrescent.Left.Set(-ThemeSelectCrescent.Width.Pixels / 2, (2 / 3f)); ThemeSelectCrescent.Top.Set(-ThemeSelectCrescent.Height.Pixels / 2, 0.75f);
			ThemeSelectCrescent.OnClick += (a, b) => ThemeSelect(1);
			SettingsInnerPanel.Append(ThemeSelectCrescent);

			ThemeSelectVanilla.Width.Set(100f, 0f); ThemeSelectVanilla.Height.Set(100f, 0f);
			ThemeSelectVanilla.Left.Set(-50f, 1 / 3f); ThemeSelectVanilla.Top.Set(-50f, 0.75f);
			ThemeSelectVanilla.OnClick += (a, b) => ThemeSelect(0);
			SettingsInnerPanel.Append(ThemeSelectVanilla);
		}

		private void BoolPress(int v1)
		{
			if(v1 == 0)
			{
				Config.HalfsizeHealthbar = !Config.HalfsizeHealthbar;
			}
			else
			{
				Config.MonsterLeveling = !Config.MonsterLeveling;
			}
			SettingsBoolsPanel[v1].BackgroundColor = SettingsBoolsPanel[v1].BackgroundColor == Color.White ? Color.Black : Color.White;
		}

		private void ThemeSelect(byte v)
		{
			Config.Theme = v;
			SelectedTheme.Left.Set(-64f, (1 / 3f) + (Config.Theme * (1 / 3f))); SelectedTheme.Top.Set(-64f, 0.75f);
			Recalculate();
		}

		public void ColorButtonPress(bool inc, byte color, byte bar, byte amt)
		{
			switch (color)
			{
				case 0:
					Config.BarColor[bar].R = (byte)(Config.BarColor[bar].R + (inc ? amt : -amt));
					SettingsBarColorText[bar, 0].SetText(Config.BarColor[bar].R.ToString());
					break;
				case 1:
					Config.BarColor[bar].G = (byte)(Config.BarColor[bar].G + (inc ? amt : -amt));
					SettingsBarColorText[bar, 1].SetText(Config.BarColor[bar].G.ToString());
					break;
				case 2:
					Config.BarColor[bar].B = (byte)(Config.BarColor[bar].B + (inc ? amt : -amt));
					SettingsBarColorText[bar, 2].SetText(Config.BarColor[bar].B.ToString());
					break;
			}
			SettingsBarColorPanel[bar].BackgroundColor = Config.BarColor[bar];
			Crescent.mod.LifeForceUI.LifeFill.color = Config.BarColor[0];
			Crescent.mod.LifeForceUI.ManaFill.color = Config.BarColor[1];
			Crescent.mod.LifeForceUI.ExpFill.color = Config.BarColor[2];
		}

		public void SettingsToggle(bool on)
		{
			if (on)
				Append(Settings);
			else Settings.Remove();
		}
	}
}