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
	public class LifeForceUI : UIState
	{
		public const int NUMSTATS = 8;
		public const int NUMPERKS = 5;

		public static bool visible = true;
		private UIImage LevelIndicator = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/LevelIndicatorCarved"));
		private UIText Level = new UIText("0");
		private UITransparantImage LevelIndicatorGlass1 = new UITransparantImage(ModLoader.GetTexture("Crescent/Assets/UI/LevelIndicatorCarvedGlass1"), Color.White * 0.75f);
		private UITransparantImage LevelIndicatorGlass2 = new UITransparantImage(ModLoader.GetTexture("Crescent/Assets/UI/LevelIndicatorCarvedGlass2"), Color.White * 0.5f);
		private UIImage Life = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/LifeBar"));
		private UIText LifeText = new UIText("0");
		private UITransparantImage LifeFill = new UITransparantImage(ModLoader.GetTexture("Crescent/Assets/UI/LifeBarFill"), Color.Red);
		private UITransparantImage LifeGlass1 = new UITransparantImage(ModLoader.GetTexture("Crescent/Assets/UI/LifeBarGlass1"), Color.White * 0.75f);
		private UITransparantImage LifeGlass2 = new UITransparantImage(ModLoader.GetTexture("Crescent/Assets/UI/LifeBarGlass2"), Color.White * 0.5f);
		private UITransparantImage LifeGlassBroken = new UITransparantImage(ModLoader.GetTexture("Crescent/Assets/UI/LifeBarGlassBroken"), Color.White * 0.25f);
		private UIImage Mana = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/ManaBar"));
		private UIText ManaText = new UIText("0");
		private UITransparantImage ManaFill = new UITransparantImage(ModLoader.GetTexture("Crescent/Assets/UI/ManaBarFill"), Color.Blue);
		private UITransparantImage ManaGlass1 = new UITransparantImage(ModLoader.GetTexture("Crescent/Assets/UI/ManaBarGlass1"), Color.White * 0.75f);
		private UITransparantImage ManaGlass2 = new UITransparantImage(ModLoader.GetTexture("Crescent/Assets/UI/ManaBarGlass2"), Color.White * 0.5f);
		private UIImage Exp = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/ExpBar"));
		private UIText ExpText = new UIText("0");
		private UITransparantImage ExpFill = new UITransparantImage(ModLoader.GetTexture("Crescent/Assets/UI/ExpBarFill"), Color.Green);
		private UITransparantImage ExpGlass1 = new UITransparantImage(ModLoader.GetTexture("Crescent/Assets/UI/ExpBarGlass1"), Color.White * 0.75f);
		private UITransparantImage ExpGlass2 = new UITransparantImage(ModLoader.GetTexture("Crescent/Assets/UI/ExpBarGlass2"), Color.White * 0.5f);
		private UIImage SettingsHide = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/PlusButton"));
		private Texture2D buttonHideImage = ModLoader.GetTexture("Crescent/Assets/UI/HideButton");
		private Texture2D buttonRespecImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/RespecButton");
		private Texture2D buttonRestartImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/RestartButton");
		private UIImage PerkIncButton;
		private int[] PerkCost = new int[NUMPERKS + 1];
		private int[] PerkValue = new int[NUMPERKS + 1];
		private bool[] PerkOne = new bool[NUMPERKS + 1];
		private string[] PerkRelevantString = new string[NUMPERKS + 1];
		private string[] PerkRelevantDescString = new string[NUMPERKS + 1];
		private string[] SkillRelevantString = new string[NUMPERKS + 1];
		private string[] SkillRelevantDescString = new string[NUMPERKS + 1];
		private Texture2D PerkIncButtonImage = ModLoader.GetTexture("Crescent/Assets/UI/PlusButton");
		private int PerkSelected;
		private UIImage[] PerkButton = new UIImage[NUMPERKS + 1];
		private UIImage[] SkillButton = new UIImage[NUMPERKS + 1];
		private UIText[] PerkText = new UIText[NUMPERKS + 1];
		private UIText[] SkillText = new UIText[NUMPERKS + 1];
		private PerkDesc PerkDsc = new PerkDesc();
		private Color Gold = new Color(255, 191, 0);
		private StatUIBox StatUI = new StatUIBox();
		private PerkUIBox PerkUI = new PerkUIBox();
		private bool SkillMenuVisible;
		private UIText SttText = new UIText("");
		private UIText[] StatText = new UIText[NUMSTATS];
		private UIImage[] StatButton = new UIImage[NUMSTATS];
		private UIText PerkDscTitleText = new UIText("");
		private UIText PerkDscText = new UIText("");
		private Vector2 offset;public bool dragging = false;

		public override void OnInitialize()
		{
			Player player = Main.player[Main.myPlayer];
			#region TopRight
			//Behind inventory to prevent life bar being dumb
			UITransparantImage Button = new UITransparantImage(ModLoader.GetTexture("Crescent/Assets/UI/LevelIndicatorCarved"), Color.White * 0f);
			Button.Top.Set(0f, 0f); Button.Left.Set(0f, 0f);
			Button.Width.Set(500f, 0f); Button.Height.Set(100f, 0f);
			//Button.OnClick += new MouseEvent(TopRightClicked);
			//Button.OnRightClick += (a, b) => HideButtonClicked(2);
			Append(Button);

			//Life
			Life.Top.Set(-2f, 0f);
			Life.OnClick += new MouseEvent(TopRightClicked);
			Life.OnRightClick += (a, b) => HideButtonClicked(2);
			Append(Life);
			LifeFill.Top.Set(8f, 0f);
			Life.Append(LifeFill);
			LifeGlass1.Left.Set(46f, 0f); LifeGlass1.Top.Set(8f, 0f);
			Life.Append(LifeGlass1);
			LifeGlass2.Left.Set(46f, 0f); LifeGlass2.Top.Set(8f, 0f);
			Life.Append(LifeGlass2);
			LifeGlassBroken.Left.Set(46f, 0f); LifeGlassBroken.Top.Set(8f, 0f);
			Life.Append(LifeGlassBroken);
			LifeText.Top.Set(-10f, 0.5f);
			Life.Append(LifeText);	

			//Exp
			Exp.Top.Set(52f, 0f);
			Exp.OnClick += new MouseEvent(TopRightClicked);
			Exp.OnRightClick += (a, b) => HideButtonClicked(2);
			Append(Exp);
			ExpFill.Top.Set(6f, 0f);
			Exp.Append(ExpFill);
			ExpGlass1.Left.Set(34f, 0f); ExpGlass1.Top.Set(6f, 0f);
			Exp.Append(ExpGlass1);
			ExpGlass2.Left.Set(34f, 0f); ExpGlass2.Top.Set(6f, 0f);
			Exp.Append(ExpGlass2);
			ExpText.Top.Set(-10f, 0.5f);
			Exp.Append(ExpText);

			//Mana
			Mana.Left.Set(-Mana.Width.Pixels + 2f, 1f);
			Mana.OnClick += new MouseEvent(TopRightClicked);
			Mana.OnRightClick += (a, b) => HideButtonClicked(2);
			Append(Mana);
			ManaFill.Left.Set(12f, 0f);
			Mana.Append(ManaFill);
			ManaGlass1.Left.Set(12f, 0f);
			Mana.Append(ManaGlass1);
			ManaGlass2.Left.Set(12f, 0f);
			Mana.Append(ManaGlass2);
			ManaText.Left.Set(3f, 0f);
			Mana.Append(ManaText);

			//Indicator has to go last to be on top
			LevelIndicator.Left.Set(-LevelIndicator.Width.Pixels+2f, 1f);
			LevelIndicator.Top.Set(-30f, 0f);
			LevelIndicator.OnClick += new MouseEvent(TopRightClicked);
			LevelIndicator.OnRightClick += (a, b) => HideButtonClicked(2);
			Append(LevelIndicator);
			LevelIndicator.Append(Level);
			LevelIndicator.Append(LevelIndicatorGlass1);
			LevelIndicator.Append(LevelIndicatorGlass2);
			#endregion
			#region StatUI
			StatUI.Height.Set(NUMSTATS*48 + 81f, 0f);
			StatUI.Width.Set(104f, 0f);
			StatUI.Left.Set(-363f, 0.5f);
			StatUI.Top.Set(80f, 0f);
			StatUI.OnMouseDown += new MouseEvent(DragStart);
			StatUI.OnMouseUp += new MouseEvent(DragEnd);
			//Append(StatUI);

			UIImage respecButton = new UIImage(buttonRespecImage);
			respecButton.Width.Set(15f, 0f);respecButton.Height.Set(15f, 0f);
			respecButton.Left.Set(StatUI.Width.Pixels - 50f, 0f); respecButton.Top.Set(StatUI.Height.Pixels - 63f, 0f);
			respecButton.OnClick += new MouseEvent(RespecButtonClicked);
			StatUI.Append(respecButton);

			UIImage restartButton = new UIImage(buttonRestartImage);
			restartButton.Width.Set(17f, 0f);restartButton.Height.Set(17f, 0f);
			restartButton.Left.Set(StatUI.Width.Pixels - 24f, 0f);restartButton.Top.Set(StatUI.Height.Pixels - 63f, 0f);
			restartButton.OnClick += new MouseEvent(RestartButtonClicked);
			StatUI.Append(restartButton);

			UIImage hideStatUIButton = new UIImage(buttonHideImage);
			hideStatUIButton.Width.Set(17f, 0f); hideStatUIButton.Height.Set(17f, 0f);
			hideStatUIButton.Left.Set(StatUI.Width.Pixels - 22f, 0f); hideStatUIButton.Top.Set(6f, 0f);
			hideStatUIButton.OnClick += (a, b) => HideButtonClicked(0);
			StatUI.Append(hideStatUIButton);

			StatButton[0] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/StrengthButton"));
			StatButton[1] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/AgilityButton"));
			StatButton[2] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/FortuneButton"));
			StatButton[3] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/DexterityButton"));
			StatButton[4] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/FortitudeButton"));
			StatButton[5] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/IntelligenceButton"));
			StatButton[6] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/VitalityButton"));
			StatButton[7] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/RadianceButton"));

			for (int i = 0; i < NUMSTATS; i++)
			{
				int n = i;
				StatButton[i].Left.Set(1f, 0f); StatButton[i].Top.Set(i*48 + 24f, 0f);
				StatButton[i].OnClick += (a, b) => StatButtonClicked(n, true);
				StatButton[i].OnRightClick += (a, b) => StatButtonClicked(n, false);
				StatUI.Append(StatButton[i]);
				StatText[i] = new UIText("");
				StatText[i].Left.Set(52f, 0f);
				StatText[i].Top.Set(16f, 0f);
				StatButton[i].Append(StatText[i]);
			}

			SttText.Left.Set(17f, 0f);
			SttText.Top.Set(NUMSTATS * 48 + 54f, 0f);
			StatUI.Append(SttText);
			#endregion
			#region PerkUI
			PerkUI.Height.Set(386f, 0f);
			PerkUI.Width.Set(514f, 0f);
			PerkUI.Left.Set(-257f, 0.5f);
			PerkUI.Top.Set(80f, 0f);

			UIImage hidePerkUIButton = new UIImage(buttonHideImage);
			hidePerkUIButton.Width.Set(17f, 0f); hidePerkUIButton.Height.Set(17f, 0f);
			hidePerkUIButton.Left.Set(PerkUI.Width.Pixels - 17f, 0f); hidePerkUIButton.Top.Set(0f, 0f);
			hidePerkUIButton.OnClick += (a, b) => HideButtonClicked(1);
			PerkUI.Append(hidePerkUIButton);

			PerkDsc.backgroundColor = new Color(255, 255, 255, 255);
			PerkDsc.Width.Set(250f, 0f);PerkDsc.Height.Set(100f, 0f);
			PerkDsc.Top.Set(23f, 0f);
			PerkUI.Append(PerkDsc);

			PerkIncButton = new UIImage(PerkIncButtonImage);
			PerkIncButton.OnClick += (a, b) => PerkIncButtonClicked(true);
			PerkIncButton.OnRightClick += (a, b) => PerkIncButtonClicked(false);
			PerkUI.Append(PerkIncButton);
			
			//Names
			PerkRelevantString[1] = "Wing Muscle";
			PerkRelevantString[2] = "Overlord";
			PerkRelevantString[3] = "Jumpman";
			PerkRelevantString[4] = "Reaper of Stars";PerkOne[4] = false;
			PerkRelevantString[5] = "Second Wind";PerkOne[5] = true;
			//Buttons
			PerkButton[1] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/Perk_WingMuscle"));
			PerkButton[2] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/Perk_Overlord"));
			PerkButton[3] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/Perk_Jumpman"));
			PerkButton[4] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/Perk_ReaperofStars"));
			PerkButton[5] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/Perk_SecondWind"));

			for (int i = 1; i <= NUMPERKS; i++)
			{
				int n = i;
				PerkButton[i].Width.Set(23f, 0f); PerkButton[i].Height.Set(23f, 0f);
				PerkButton[i].Left.Set((2*(i-(0.5f+NUMPERKS/2F))*23)+257-12f, 0f);PerkButton[i].Top.Set(193-12f, 0f);
				PerkButton[i].OnClick += (a, b) => PerkButtonClicked(n);
				PerkButton[i].OnRightClick += (a, b) => PerkDescClose();
				PerkUI.Append(PerkButton[i]);
				PerkText[i] = new UIText("");
				PerkText[i].Left.Set(0f, 0.75f); PerkText[i].Top.Set(0f, 0.5f);
				PerkButton[i].Append(PerkText[i]);
			}

			PerkDsc.Remove();
			PerkIncButton.Remove();
			#endregion
		}

		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			offset = new Vector2(evt.MousePosition.X - StatUI.Left.Pixels, evt.MousePosition.Y - StatUI.Top.Pixels);
			dragging = true;
		}

		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			Vector2 end = evt.MousePosition;
			dragging = false;

			StatUI.Left.Set(end.X - offset.X, 0.5f);
			StatUI.Top.Set(end.Y - offset.Y, 0f);

			Recalculate();
		}

		private void PerkIncButtonClicked(bool LeftClick)
		{
			Player player = Main.player[Main.myPlayer];

			if (LeftClick)
			{
				if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt >= PerkCost[PerkSelected] && (PerkOne[PerkSelected] ? player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[PerkSelected] == 0 : true))
				{
					player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt -= PerkCost[PerkSelected];
					player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[PerkSelected]++;
				}
			}
			else
			{
				if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[PerkSelected] > 0)
				{
					player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt += PerkValue[PerkSelected];
					player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[PerkSelected]--;
				}
			}

			Main.PlaySound(SoundID.MenuTick);
			PerkCostUpdate(player);
			//PerkDescClose();
			PerkDscText.SetText(PerkRelevantDescString[PerkSelected]);
		}

		private void PerkCostUpdate(Player player)
		{
			PerkCost[1] = 50 * (1 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[1]);
			PerkCost[2] = (int)Math.Pow(10, 2 + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[2]));
			PerkCost[3] = 25 * (1 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[3]);
			PerkCost[4] = 50 * (1 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[4]);
			PerkCost[5] = 100;

			PerkValue[1] = 50 * (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[1]);
			PerkValue[2] = (int)Math.Pow(10, 1 + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[2]));
			PerkValue[3] = 25 * (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[3]);
			PerkValue[4] = 50 * (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[4]);
			PerkValue[5] = 100;

			PerkRelevantDescString[1] = "Each point into this perk\ngives +20% flight\nCosts " + PerkCost[1] + " points to upgrade";
			PerkRelevantDescString[2] = "Each point into this perk\ngives +1 maximum minions\nCosts " + PerkCost[2] + " points to upgrade";
			PerkRelevantDescString[3] = "Each point into this perk\nboosts your jump 1 block\nCosts " + PerkCost[3] + " points to upgrade";
			PerkRelevantDescString[4] = "Each point into this perk\ngrants 1 mana per hit\nCosts " + PerkCost[4] + " points to upgrade";
			PerkRelevantDescString[5] = "Survive a fatal blow with\n10% of your life\nCosts 100 points to obtain";
		}

		private void PerkButtonClicked(int value)
		{
			Player player = Main.player[Main.myPlayer];
			PerkCostUpdate(player);
			//Descriptions
			PerkDsc.Remove();
			PerkIncButton.Remove();
			PerkUI.Append(PerkDsc);
			PerkUI.Append(PerkIncButton);
			PerkDsc.Left.Set(PerkButton[value].Left.Pixels - PerkDsc.Width.Pixels / 2 + 12f, 0f);
			PerkDsc.Top.Set(PerkButton[value].Top.Pixels + 23f, 0f);
			PerkIncButton.Left.Set(PerkButton[value].Left.Pixels + 23f, 0f);
			PerkIncButton.Top.Set(PerkButton[value].Top.Pixels + 3f, 0f);
			PerkDscTitleText.Left.Set(73.5f, 0f);
			PerkDscText.Left.Set(25f, 0f); PerkDscText.Top.Set(25f, 0f);
			PerkDscTitleText.SetText(PerkRelevantString[value]);
			PerkDscText.SetText(PerkRelevantDescString[value]);
			PerkDsc.Append(PerkDscTitleText);
			PerkDsc.Append(PerkDscText);
			PerkSelected = value;
			Main.PlaySound(SoundID.MenuTick);
		}

		public void PerkDescClose()
		{
			if (PerkUI.HasChild(PerkDsc))
			{
				PerkDsc.Remove();
				PerkIncButton.Remove();
			}
			PerkSelected = -1;
			Main.PlaySound(SoundID.MenuTick);
		}

		public void HideButtonClicked(int num)
		{
			switch (num)
			{
				case 0:
					StatUI.Remove();
					break;
				case 1:
					PerkUI.Remove();
					break;
				case 2:
					StatUI.Remove();
					PerkUI.Remove();
					break;
				default:
					break;
			}
			Main.PlaySound(SoundID.MenuClose);
		}

		private void StatButtonClicked(int num, bool LeftClick)
		{
			Player player = Main.player[Main.myPlayer];
			int i = LeftClick ? 1 : 50;

			if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt >= i)
			{
				player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[num] = player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[num] + i;
				player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt = player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt - i;
			}
			Main.PlaySound(SoundID.MenuTick);
		}

		private void RespecButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Player player = Main.player[Main.myPlayer];
			for (int i = 0; i < NUMSTATS; i++)
			{
				player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[i] = 0;
			}
			player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt = player.GetModPlayer<CrescentPlayer>(Crescent.mod).Llvl * 10;
			for (int i = 0; i < 8; i++)
			{
				player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[i] = 0;
			}
			Main.PlaySound(2, -1, -1, 4);
		}

		private void RestartButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Player player = Main.player[Main.myPlayer];
			player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lexp = 0;player.GetModPlayer<CrescentPlayer>(Crescent.mod).Llvl = 0;
			for (int i = 0; i < NUMSTATS; i++)
			{
				player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[i] = 0;
			}
			player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt = 0;
			player.GetModPlayer<CrescentPlayer>(Crescent.mod).Llxp = (int)(Math.Pow((player.GetModPlayer<CrescentPlayer>(Crescent.mod).Llvl + 1) * 333, 1.2));
			for (int i = 0; i < 8; i++)
			{
				player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[i] = 0;
			}
			Main.PlaySound(SoundID.MenuTick);
		}

		private void TopRightClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Append(StatUI);
			Append(PerkUI);
			Main.PlaySound(SoundID.MenuOpen);
		}

		public override void Update(GameTime gameTime)
		{
			Player player = Main.player[Main.myPlayer];

			//Topright
			Level.SetText(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Llvl.ToString());
			Level.Left.Set(-Level.Text.Length * 3.7f, 0.5f); Level.Top.Set(-10f, 0.5f);
			Level.TextColor = player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt > 0 ? Gold : Color.White;

			//Resource bars
			Life.Left.Set(-84-46-player.statLifeMax * (Main.screenWidth >= 1626 ? 3 : (Main.screenWidth >= 1376 ? 2.5f : (Main.screenWidth >= 1126 ? 2 : 1.5f))), 1f);
			LifeFill.Left.Set(46+(player.statLifeMax - (player.statLife / (float)player.statLifeMax2) * player.statLifeMax) * 3, 0f);
			LifeText.SetText(player.statLife.ToString() + "/" + player.statLifeMax2.ToString());
			LifeText.Left.Set((-LifeText.Text.Length * 3.7f), (player.statLifeMax/1000f));
			if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).secondWindTimer > 0)
			{
				LifeGlassBroken.ImageScale = 1f;
				LifeGlass2.ImageScale = 0f;
			}
			else
			{
				LifeGlassBroken.ImageScale = 0f;
				LifeGlass2.ImageScale = 1f;
			}

			Exp.Left.Set(Life.Left.Pixels + 150f < -1080 ? -1080 : Life.Left.Pixels + 150f, 1f);
			ExpFill.Left.Set(34f - (Exp.Left.Pixels + 34f) + (Exp.Left.Pixels + 34f) * (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lexp / (float)player.GetModPlayer<CrescentPlayer>(Crescent.mod).Llxp), 0f);
			ExpText.SetText((Math.Floor(10000*(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lexp/(float)player.GetModPlayer<CrescentPlayer>(Crescent.mod).Llxp))/100).ToString() + "%");
			ExpText.Left.Set((-LifeText.Text.Length * 3.7f), (player.statLifeMax / 1000f));

			Mana.Top.Set(50-800+player.statManaMax * (Main.screenHeight >= 888 ? 4 : (Main.screenHeight >= 688 ? 3 : 2)), 0f);
			ManaFill.Top.Set((-player.statManaMax+(player.statMana / (float)player.statManaMax2)*player.statManaMax)*4, 0f);
			ManaText.SetText(player.statMana.ToString() + "\n/" + player.statManaMax2.ToString());
			ManaText.Top.Set((Mana.Height.Pixels*(1 + Mana.Top.Pixels/-850))/2f, 0f);

			//Status bars
			for (int i = 0; i < NUMSTATS; i++){
				StatText[i].SetText(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[i].ToString());
			}
			SttText.SetText(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt.ToString());

			//Stats window
			if (dragging){
				StatUI.Left.Set(Main.mouseX - offset.X, 0.5f);
				StatUI.Top.Set(Main.mouseY - offset.Y, 0f);
			}

			//Perk window
			for (int i = 1; i <= NUMPERKS; i++){
				if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[i] > 0) { PerkText[i].SetText("(" + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[i].ToString() + ")"); }
				else { PerkText[i].SetText(""); }
			}

			DrawInterface_Resources_Buffs();
			DrawInterface_Resources_Breath();
			Recalculate();
		}
		
		#region More buffs n shit

		private void DrawInterface_Resources_Buffs()
		{
			Main.bannerMouseOver = false;
			//Main.recBigList = false;
			Main.buffString = "";
			int drawBuffText = -1;
			int num1 = 11;
			for (int i = 0; i < 22; ++i)
			{
				if (Main.player[Main.myPlayer].buffType[i] > 0)
				{
					int b = Main.player[Main.myPlayer].buffType[i];
					int x = 32 + i * 38;
					int y = 76;
					if (i >= num1)
					{
						x = 32 + (i - num1) * 38;
						y += 50;
					}
					drawBuffText = DrawBuffIcon(drawBuffText, i, b, x, y);
				}
				else
					Main.buffAlpha[i] = 0.4f;
			}
			if (drawBuffText < 0) return;
			int id = Main.player[Main.myPlayer].buffType[drawBuffText];
			if (id <= 0)return;
			Main.buffString = Lang.GetBuffDescription(id);
			if (id == 26 && Main.expertMode)
				Main.buffString = Language.GetTextValue("BuffDescription.WellFed_Expert");
			if (id == 147)
				Main.bannerMouseOver = true;
			if (id == 94)
			{
				int num2 = (int)((double)Main.player[Main.myPlayer].manaSickReduction * 100.0) + 1;
				Main.buffString = Main.buffString + (object)num2 + "%";
			}
			if (Main.meleeBuff[id])				
				Main.instance.MouseTextHackZoom(Lang.GetBuffName(id), -10, 0);
			else
				Main.instance.MouseTextHackZoom(Lang.GetBuffName(id));
		}

		private int DrawBuffIcon(int drawBuffText, int i, int b, int x, int y)
		{
			if (b == 0)
				return drawBuffText;
			Color color;
			color = new Color(Main.buffAlpha[i], Main.buffAlpha[i], Main.buffAlpha[i], Main.buffAlpha[i]);
			Main.spriteBatch.Draw(Main.buffTexture[b], new Vector2((float)x, (float)y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Main.buffTexture[b].Width, Main.buffTexture[b].Height)), color, 0.0f, new Vector2(0, 0), 1f, (SpriteEffects)0, 0.0f);
			if (!Main.vanityPet[b] && !Main.lightPet[b] && !Main.buffNoTimeDisplay[b] && (!Main.player[Main.myPlayer].honeyWet || b != 48) && ((!Main.player[Main.myPlayer].wet || !Main.expertMode || b != 46) && Main.player[Main.myPlayer].buffTime[i] > 2))
			{
				string str = Lang.LocalizedDuration(new TimeSpan(0, 0, Main.player[Main.myPlayer].buffTime[i] / 60), true, false);
				DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, Main.fontItemStack, str, new Vector2(x, (y + Main.buffTexture[b].Height)), color, 0.0f, new Vector2(0, 0), 0.8f, (SpriteEffects)0, 0.0f);
			}
			if (Main.mouseX < x + Main.buffTexture[b].Width && Main.mouseY < y + Main.buffTexture[b].Height && Main.mouseX > x && Main.mouseY > y)
			{
				drawBuffText = i;
				Main.buffAlpha[i] += 0.1f;
				bool flag = Main.mouseRight && Main.mouseRightRelease;
				if (PlayerInput.UsingGamepad)
				{
					flag = Main.mouseLeft && Main.mouseLeftRelease && Main.playerInventory;
					if (Main.playerInventory)
						Main.player[Main.myPlayer].mouseInterface = true;
				}
				else
					Main.player[Main.myPlayer].mouseInterface = true;
				if (flag)
					TryRemovingBuff(i, b);
			}
			else
			{
				Main.buffAlpha[i] -= 0.05f;
			}
			if (Main.buffAlpha[i] > 1.0)
				Main.buffAlpha[i] = 1f;
			else if (Main.buffAlpha[i] < 0.4)
				Main.buffAlpha[i] = 0.4f;
			if (PlayerInput.UsingGamepad && !Main.playerInventory)
				drawBuffText = -1;
			return drawBuffText;
		}

		private static void TryRemovingBuff(int i, int b)
		{
			bool flag = false;
			if (Main.debuff[b] || b == 60 || b == 151)
				return;
			if (Main.player[Main.myPlayer].mount.Active && Main.player[Main.myPlayer].mount.CheckBuff(b))
			{
				Main.player[Main.myPlayer].mount.Dismount(Main.player[Main.myPlayer]);
				flag = true;
			}
			if (Main.player[Main.myPlayer].miscEquips[0].buffType == b && !Main.player[Main.myPlayer].hideMisc[0])
				Main.player[Main.myPlayer].hideMisc[0] = true;
			if (Main.player[Main.myPlayer].miscEquips[1].buffType == b && !Main.player[Main.myPlayer].hideMisc[1])
				Main.player[Main.myPlayer].hideMisc[1] = true;
			Main.PlaySound(12, -1, -1, 1, 1f, 0.0f);
			if (flag)
				return;
			Main.player[Main.myPlayer].DelBuff(i);
		}

		private static void DrawInterface_Resources_Breath()
		{
			bool flag = false;
			if (Main.player[Main.myPlayer].dead)
				return;
			if (Main.player[Main.myPlayer].lavaTime < Main.player[Main.myPlayer].lavaMax && Main.player[Main.myPlayer].lavaWet)
				flag = true;
			else if (Main.player[Main.myPlayer].lavaTime < Main.player[Main.myPlayer].lavaMax && Main.player[Main.myPlayer].breath == Main.player[Main.myPlayer].breathMax)
				flag = true;
			Vector2 vector2_1 = Vector2.Add(Main.player[Main.myPlayer].Top, new Vector2(0.0f, Main.player[Main.myPlayer].gfxOffY));
			if (Main.playerInventory && Main.screenHeight < 1000)
			{
				float local = vector2_1.Y;
				double num =  local + (double)(Main.player[Main.myPlayer].height - 20);
				local = (float)num;
			}
			Vector2 vector2_2 = Vector2.Transform(Vector2.Subtract(vector2_1, Main.screenPosition), Main.GameViewMatrix.ZoomMatrix);
			if (!Main.playerInventory || Main.screenHeight >= 1000)
			{
				float local = vector2_2.Y;
				double num = local - 100.0;
				local = (float)num;
			}
			Vector2 vector2_3 = Vector2.Divide(vector2_2, Main.UIScale);
			if (Main.ingameOptionsWindow || Main.InGameUI.IsVisible)
			{
				vector2_3 = new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 + 236));
				if (Main.InGameUI.IsVisible)
					vector2_3.Y = Main.screenHeight - 64;
			}
			if (Main.player[Main.myPlayer].breath < Main.player[Main.myPlayer].breathMax && !Main.player[Main.myPlayer].ghost && !flag)
			{
				int num1 = Main.player[Main.myPlayer].breathMax / 20;
				int num2 = 20;
				for (int index = 1; index < Main.player[Main.myPlayer].breathMax / num2 + 1; ++index)
				{
					float num3 = 1f;
					int num4;
					if (Main.player[Main.myPlayer].breath >= index * num2)
					{
						num4 = (int)byte.MaxValue;
					}
					else
					{
						float num5 = (float)(Main.player[Main.myPlayer].breath - (index - 1) * num2) / (float)num2;
						num4 = (int)(30.0 + 225.0 * (double)num5);
						if (num4 < 30)
							num4 = 30;
						num3 = (float)((double)num5 / 4.0 + 0.75);
						if ((double)num3 < 0.75)
							num3 = 0.75f;
					}
					int num6 = 0;
					int num7 = 0;
					if (index > 10)
					{
						num6 -= 260;
						num7 += 26;
					}
					Main.spriteBatch.Draw(Main.bubbleTexture, Vector2.Add(vector2_3, new Vector2((float)(26 * (index - 1) + num6) - 125f, (float)(32.0 + ((double)Main.bubbleTexture.Height - (double)Main.bubbleTexture.Height * (double)num3) / 2.0) + (float)num7)), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Main.bubbleTexture.Width, Main.bubbleTexture.Width)), new Microsoft.Xna.Framework.Color(num4, num4, num4, num4), 0.0f, new Vector2(0, 0), num3, (SpriteEffects)0, 0.0f);
				}
			}
			if (((Main.player[Main.myPlayer].lavaTime >= Main.player[Main.myPlayer].lavaMax ? 0 : (!Main.player[Main.myPlayer].ghost ? 1 : 0)) & (flag ? 1 : 0)) == 0)
				return;
			int num8 = Main.player[Main.myPlayer].lavaMax / 10;
			int num9 = Main.player[Main.myPlayer].breathMax / num8;
			for (int index = 1; index < Main.player[Main.myPlayer].lavaMax / num8 + 1; ++index)
			{
				float num1 = 1f;
				int num2;
				if (Main.player[Main.myPlayer].lavaTime >= index * num8)
				{
					num2 = (int)byte.MaxValue;
				}
				else
				{
					float num3 = (float)(Main.player[Main.myPlayer].lavaTime - (index - 1) * num8) / (float)num8;
					num2 = (int)(30.0 + 225.0 * (double)num3);
					if (num2 < 30)
						num2 = 30;
					num1 = (float)((double)num3 / 4.0 + 0.75);
					if ((double)num1 < 0.75)
						num1 = 0.75f;
				}
				int num4 = 0;
				int num5 = 0;
				if (index > 10)
				{
					num4 -= 260;
					num5 += 26;
				}
				Main.spriteBatch.Draw(Main.flameTexture, Vector2.Add(vector2_3, new Vector2((26 * (index - 1) + num4) - 125f, (float)(32.0 + (Main.flameTexture.Height - Main.flameTexture.Height * (double)num1) / 2.0) + (float)num5)), new Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Main.bubbleTexture.Width, Main.bubbleTexture.Height)), new Microsoft.Xna.Framework.Color(num2, num2, num2, num2), 0.0f, new Vector2(0, 0), num1, (SpriteEffects)0, 0.0f);
			}
		}
		#endregion
		#region CustomUIClasses

		public class UITransparantImage : UIElement
		{
			private Texture2D _texture;
			public float ImageScale = 1f;
			private Color color;

			public UITransparantImage(Texture2D texture, Color color)
			{
				_texture = texture;
				Width.Set(_texture.Width, 0f);
				Height.Set(_texture.Height, 0f);
				this.color = color;
			}

			public void SetImage(Texture2D texture)
			{
				_texture = texture;
				Width.Set(_texture.Width, 0f);
				Height.Set(_texture.Height, 0f);
			}

			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dimensions = GetDimensions();
				spriteBatch.Draw(_texture, dimensions.Position() + _texture.Size() * (1f - ImageScale) / 2f, null, color, 0f, Vector2.Zero, ImageScale, SpriteEffects.None, 0f);
			}
		}
		
		class PerkDesc : UIElement
		{
			public Color backgroundColor = Color.Gray;
			private static Texture2D White;
			private static Texture2D BG;
			private static Texture2D Dark;

			public PerkDesc()
			{
				if (White == null)
					White = ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/PerkExt_White");
				if (BG == null)
					BG = ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/PerkExt_BG");
				if (Dark == null)
					Dark = ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/PerkExt_Dark");
			}

			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dimensions = GetDimensions();
				Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
				int height = (int)Math.Ceiling(dimensions.Height);
				int width = (int)Math.Ceiling(dimensions.Width);

				spriteBatch.Draw(BG, new Rectangle(point1.X + 1, point1.Y + 1, width - 2, height - 2), backgroundColor);
				spriteBatch.Draw(White, new Rectangle(point1.X + 1, point1.Y, width - 2, 2), backgroundColor);
				spriteBatch.Draw(White, new Rectangle(point1.X, point1.Y + 1, 2, height - 2), backgroundColor);
				spriteBatch.Draw(Dark, new Rectangle(point1.X + 1, point1.Y + height - 2, width - 2, 2), backgroundColor);
				spriteBatch.Draw(Dark, new Rectangle(point1.X + width - 2, point1.Y + 1, 2, height - 2), backgroundColor);
			}
		}

		class StatUIBox : UIElement
		{
			public Color backgroundColor = new Color(223, 223, 223);
			private static Texture2D StaticCorner;
			private static Texture2D StatsText;
			private static Texture2D StatsBottom;
			private static Texture2D PointsPane;

			public StatUIBox()
			{
				if (StaticCorner == null)
					StaticCorner = ModLoader.GetTexture("Crescent/Assets/UI/Static_Corner");
				if (StatsText == null)
					StatsText = ModLoader.GetTexture("Crescent/Assets/UI/StatsText");
				if (StatsBottom == null)
					StatsBottom = ModLoader.GetTexture("Crescent/Assets/UI/StatsBottom");
				if (PointsPane == null)
					PointsPane = ModLoader.GetTexture("Crescent/Assets/UI/PointsPane");
			}

			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dimensions = GetDimensions();
				Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
				int width = (int)Math.Ceiling(dimensions.Width);	
				int height = (int)Math.Ceiling(dimensions.Height);
				
				spriteBatch.Draw(Main.magicPixel, new Rectangle(point1.X, point1.Y, 104, height - 40), new Color(0, 0, 0));
				spriteBatch.Draw(Main.magicPixel, new Rectangle(point1.X + 1, point1.Y + 1, 102, height - 42), backgroundColor);
				spriteBatch.Draw(Main.magicPixel, new Rectangle(point1.X + 1, point1.Y + 1, 99, 3), new Color(255, 255, 255, 127));
				spriteBatch.Draw(Main.magicPixel, new Rectangle(point1.X + 1, point1.Y + 1, 3, height - 45), new Color(255, 255, 255, 127));
				spriteBatch.Draw(Main.magicPixel, new Rectangle(point1.X + 4, point1.Y + height - 44, 99, 3), new Color(0, 0, 0, 63));
				spriteBatch.Draw(Main.magicPixel, new Rectangle(point1.X + 100, point1.Y + 4, 3, height - 45), new Color(0, 0, 0, 63));
				spriteBatch.Draw(StaticCorner, new Rectangle(point1.X + 100, point1.Y + 1, 3, 3), backgroundColor);
				spriteBatch.Draw(StaticCorner, new Rectangle(point1.X + 1, point1.Y + height - 44, 3, 3), backgroundColor);
				spriteBatch.Draw(StatsText, new Rectangle(point1.X + 3, point1.Y + 5, 59, 19), backgroundColor);
				spriteBatch.Draw(StatsBottom, new Rectangle(point1.X + 3, point1.Y + height - 57, 44, 3), backgroundColor);

				spriteBatch.Draw(Main.magicPixel, new Rectangle(point1.X + 11, point1.Y + height - 38, 82, 38), backgroundColor);
				spriteBatch.Draw(PointsPane, new Rectangle(point1.X + 11, point1.Y + height - 38, 82, 38), backgroundColor);
			}
		}

		class PerkUIBox : UIElement
		{
			public Color backgroundColor = new Color(255, 255, 255);
			private static Texture2D Twinkle;
			private static Texture2D Tex;

			public PerkUIBox()
			{
				if (Twinkle == null)
					Twinkle = ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/Twinkle");
				if (Tex == null)
					Tex = ModLoader.GetTexture("Crescent/Assets/UI/PerkBox");
			}

			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dimensions = GetDimensions();
				Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
				int width = (int)Math.Ceiling(dimensions.Width);
				int height = (int)Math.Ceiling(dimensions.Height);

				spriteBatch.Draw(Main.magicPixel, new Rectangle(point1.X, point1.Y, width, height), new Color(0, 0, 0));
				spriteBatch.Draw(Twinkle, new Rectangle(
					(int)(point1.X + 13 + Math.Sin((System.DateTime.Now.Ticks / Math.PI) / 10000000F) * 244 + 244),
					(int)(point1.Y + 13 + Math.Cos((System.DateTime.Now.Ticks / Math.PI) / 10000000F) * 180 + 180),
					12, 12), backgroundColor);
				spriteBatch.Draw(Tex, new Rectangle(point1.X, point1.Y, width, height), backgroundColor);
			}
		}
		#endregion
	}
}
