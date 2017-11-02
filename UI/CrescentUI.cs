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
		public static bool visible = true;
		public Texture2D LifePoint1 = ModLoader.GetTexture("Crescent/Assets/UI/LifeBarFilled1");
		public Texture2D LifePoint2 = ModLoader.GetTexture("Crescent/Assets/UI/LifeBarFilled2");
		public Texture2D ManaPoint1 = ModLoader.GetTexture("Crescent/Assets/UI/ManaBarFilled1");
		public Texture2D ManaPoint2 = ModLoader.GetTexture("Crescent/Assets/UI/ManaBarFilled2");
		public Texture2D FloralImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeFloral");
		public Texture2D buttonHideImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/HideButton");
		public Texture2D buttonRespecImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/RespecButton");
		public Texture2D buttonRestartImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/RestartButton");
		public Texture2D buttonStrengthImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/StrengthButton");
		public Texture2D buttonAgilityImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/AgilityButton");
		public Texture2D buttonDexterityImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/DexterityButton");
		public Texture2D buttonFortitudeImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/FortitudeButton");
		public Texture2D buttonIntelligenceImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/IntelligenceButton");
		public Texture2D buttonVitalityImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/VitalityButton");
		public Texture2D buttonRadianceImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/RadianceButton");
		public UIImageButton PerkIncButton;
		public string[] PerkRelevantString = new string[32];
		public string[] PerkRelevantDescString = new string[32];
		public Texture2D PerkIncButtonImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/PlusButton");
		public int PerkSelected;
		public UIImageButton[] PerkButton = new UIImageButton[32];
		public UIText[] PerkText = new UIText[32];
		private LifeBar Life = new LifeBar();
		private ManaBar Mana = new ManaBar();
		private UIImage Exp = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/ExpBar"));
		private PerkDesc PerkDsc = new PerkDesc();
		private ExpBarFilled ExpFill = new ExpBarFilled();
		private UIImage[] LifePoint = new UIImage[25];
		private UIImage[] ManaPoint = new UIImage[25];
		private UIImage Floral;
		private FloralHider FlorLeft = new FloralHider();
		private FloralHider FlorRight = new FloralHider();
		private UIText Level = new UIText("0");
		private Color Gold = new Color(255, 191, 0);
		private UIText LifeText = new UIText("0");
		private UIText ManaText = new UIText("0");
		private LevelUIBox LevelUI = new LevelUIBox();
		public bool SkillMenuVisible;
		public UIText StrText = new UIText("");
		public UIText AgiText = new UIText("");
		public UIText DexText = new UIText("");
		public UIText ForText = new UIText("");
		public UIText IntText = new UIText("");
		public UIText VitText = new UIText("");
		public UIText RadText = new UIText("");
		public UIText SttText = new UIText("");
		public UIText PerkDscTitleText = new UIText("");
		public UIText PerkDscText = new UIText("");

		public override void OnInitialize()
		{
			Player player = Main.player[Main.myPlayer];
			#region TopRight
			TopRight parent = new TopRight();
			parent.Height.Set(93f, 0f);
			parent.Width.Set(84f, 0f);
			parent.Left.Set(-84f, 1f);
			parent.Top.Set(0f, 0f);
			parent.OnClick += new MouseEvent(TopRightClicked);
			parent.backgroundColor = new Color(255, 255, 255, 255);

			Level.Height.Set(0f, 0f);
			Level.Width.Set(0f, 0f);
			parent.Append(Level);

			LifeText.Height.Set(0f, 0f);
			LifeText.Width.Set(0f, 0f);
			LifeText.TextColor = new Color(255, 0, 0);
			parent.Append(LifeText);

			ManaText.Height.Set(0f, 0f);
			ManaText.Width.Set(0f, 0f);
			ManaText.TextColor = new Color(0, 0, 255);
			parent.Append(ManaText);

			//Life
			Life.Top.Set(0f, 0f);
			Life.backgroundColor = new Color(255, 255, 255, 255);

			//Mana
			Mana.Top.Set(93f, 0f);
			Mana.Left.Set(-40f, 1f);
			Mana.backgroundColor = new Color(255, 255, 255, 255);

			//Exp
			Exp.Top.Set(70f, 0f);
			Exp.Left.Set(-198f, 1f);

			//ExpFill
			ExpFill.Width.Set(110f, 0f);
			ExpFill.Height.Set(10f, 0f);
			ExpFill.Left.Set(-191f, 1f);
			ExpFill.Top.Set(71f, 0f);
			ExpFill.backgroundColor = new Color(255, 255, 255, 255);

			for (int i = 0; i < 25; i++)
			{

				if (i == 0)
				{
					LifePoint[i] = new UIImage(LifePoint1);
					LifePoint[i].Width.Set(40f, 0f);
					ManaPoint[i] = new UIImage(ManaPoint1);
					ManaPoint[i].Height.Set(20f, 0f);
					ManaPoint[i].Top.Set(0f, 0f);
				}
				else
				{
					LifePoint[i] = new UIImage(LifePoint2);
					LifePoint[i].Width.Set(71f, 0f);
					ManaPoint[i] = new UIImage(ManaPoint2);
					ManaPoint[i].Height.Set(39f, 0f);
					ManaPoint[i].Top.Set(-19 + i * 20, 0f);
				}
				LifePoint[i].Height.Set(40f, 0f);
				LifePoint[i].Left.Set(-(i + 1) * 40, 1f);
				LifePoint[i].Top.Set(4f, 0f);
				Life.Append(LifePoint[i]);
				ManaPoint[i].Width.Set(20f, 0f);
				ManaPoint[i].Left.Set(16, 0f);
				Mana.Append(ManaPoint[i]);
			}

			//Floral
			Floral = new UIImage(FloralImage);
			Floral.Width.Set(750f, 0f);
			Floral.Height.Set(23f, 0f);
			Floral.Left.Set(76f, 0f);
			Floral.Top.Set(46f, 0f);
			Floral.ImageScale = 1f;
			Life.Append(Floral);

			FlorLeft.Width.Set(350f, 0f);
			FlorLeft.Height.Set(23f, 0f);
			FlorLeft.Left.Set(73f, 0f);
			FlorLeft.Top.Set(46f, 0f);
			FlorLeft.backgroundColor = new Color(255, 255, 255, 255);

			FlorRight.Width.Set(320f, 0f);
			FlorRight.Height.Set(23f, 0f);
			FlorRight.Left.Set(479f, 0f);
			FlorRight.Top.Set(46f, 0f);
			FlorRight.backgroundColor = new Color(255, 255, 255, 255);

			base.Append(parent);
			base.Append(Life);
			base.Append(Mana);
			base.Append(Exp);
			base.Append(ExpFill);
			base.Append(FlorLeft);
			base.Append(FlorRight);
			#endregion
			#region LevelUI
			LevelUI.Height.Set(425f, 0f);
			LevelUI.Width.Set(611f, 0f);
			LevelUI.Left.Set(-305.5f, 0.5f);
			LevelUI.Top.Set(80f, 0f);
			LevelUI.backgroundColor = new Color(255, 255, 255, 255);
			//Append(LevelUI);

			UIImageButton hideButton = new UIImageButton(buttonHideImage);
			hideButton.Width.Set(15f, 0f); hideButton.Height.Set(15f, 0f);
			hideButton.Left.Set(595f, 0f); hideButton.Top.Set(1f, 0f);
			hideButton.OnClick += new MouseEvent(HideButtonClicked);
			LevelUI.Append(hideButton);

			UIImageButton respecButton = new UIImageButton(buttonRespecImage);
			respecButton.Width.Set(15f, 0f);respecButton.Height.Set(15f, 0f);
			respecButton.Left.Set(53f, 0f);respecButton.Top.Set(361f, 0f);
			respecButton.OnClick += new MouseEvent(RespecButtonClicked);
			LevelUI.Append(respecButton);

			UIImageButton restartButton = new UIImageButton(buttonRestartImage);
			restartButton.Width.Set(15f, 0f);restartButton.Height.Set(15f, 0f);
			restartButton.Left.Set(81f, 0f);restartButton.Top.Set(361f, 0f);
			restartButton.OnClick += new MouseEvent(RestartButtonClicked);
			LevelUI.Append(restartButton);

			UIImageButton StrengthButton = new UIImageButton(buttonStrengthImage);
			StrengthButton.Width.Set(48f, 0f);StrengthButton.Height.Set(48f, 0f);
			StrengthButton.Left.Set(1f, 0f);StrengthButton.Top.Set(32f, 0f);
			StrengthButton.OnClick += (a, b) => StatButtonClicked(0, true);
			StrengthButton.OnRightClick += (a, b) => StatButtonClicked(0, false);
			LevelUI.Append(StrengthButton);
			StrText.Left.Set(52f, 0f);
			StrText.Top.Set(16f, 0f);
			StrengthButton.Append(StrText);

			UIImageButton AgilityButton = new UIImageButton(buttonAgilityImage);
			AgilityButton.Width.Set(48f, 0f);AgilityButton.Height.Set(48f, 0f);
			AgilityButton.Left.Set(1f, 0f);AgilityButton.Top.Set(80f, 0f);
			AgilityButton.OnClick += (a, b) => StatButtonClicked(1, true);
			AgilityButton.OnRightClick += (a, b) => StatButtonClicked(1, false);
			LevelUI.Append(AgilityButton);
			AgiText.Left.Set(52f, 0f);
			AgiText.Top.Set(16f, 0f);
			AgilityButton.Append(AgiText);

			UIImageButton DexterityButton = new UIImageButton(buttonDexterityImage);
			DexterityButton.Width.Set(48f, 0f);DexterityButton.Height.Set(48f, 0f);
			DexterityButton.Left.Set(1f, 0f);DexterityButton.Top.Set(128f, 0f);
			DexterityButton.OnClick += (a, b) => StatButtonClicked(2, true);
			DexterityButton.OnRightClick += (a, b) => StatButtonClicked(2, false);
			LevelUI.Append(DexterityButton);
			DexText.Left.Set(52f, 0f);
			DexText.Top.Set(16f, 0f);
			DexterityButton.Append(DexText);

			UIImageButton FortitudeButton = new UIImageButton(buttonFortitudeImage);
			FortitudeButton.Width.Set(48f, 0f);FortitudeButton.Height.Set(48f, 0f);
			FortitudeButton.Left.Set(1f, 0f);FortitudeButton.Top.Set(176f, 0f);
			FortitudeButton.OnClick += (a, b) => StatButtonClicked(3, true);
			FortitudeButton.OnRightClick += (a, b) => StatButtonClicked(3, false);
			LevelUI.Append(FortitudeButton);
			ForText.Left.Set(52f, 0f);
			ForText.Top.Set(16f, 0f);
			FortitudeButton.Append(ForText);

			UIImageButton IntelligenceButton = new UIImageButton(buttonIntelligenceImage);
			IntelligenceButton.Width.Set(48f, 0f);IntelligenceButton.Height.Set(48f, 0f);
			IntelligenceButton.Left.Set(1f, 0f);IntelligenceButton.Top.Set(224f, 0f);
			IntelligenceButton.OnClick += (a, b) => StatButtonClicked(4, true);
			IntelligenceButton.OnRightClick += (a, b) => StatButtonClicked(4, false);
			LevelUI.Append(IntelligenceButton);
			IntText.Left.Set(52f, 0f);
			IntText.Top.Set(16f, 0f);
			IntelligenceButton.Append(IntText);

			UIImageButton VitalityButton = new UIImageButton(buttonVitalityImage);
			VitalityButton.Width.Set(48f, 0f);VitalityButton.Height.Set(48f, 0f);
			VitalityButton.Left.Set(1f, 0f);VitalityButton.Top.Set(272f, 0f);
			VitalityButton.OnClick += (a, b) => StatButtonClicked(5, true);
			VitalityButton.OnRightClick += (a, b) => StatButtonClicked(5, false);
			LevelUI.Append(VitalityButton);
			VitText.Left.Set(52f, 0f);
			VitText.Top.Set(16f, 0f);
			VitalityButton.Append(VitText);

			UIImageButton RadianceButton = new UIImageButton(buttonRadianceImage);
			RadianceButton.Width.Set(48f, 0f);RadianceButton.Height.Set(48f, 0f);
			RadianceButton.Left.Set(1f, 0f);RadianceButton.Top.Set(320f, 0f);
			RadianceButton.OnClick += (a, b) => StatButtonClicked(6, true);
			RadianceButton.OnRightClick += (a, b) => StatButtonClicked(6, false);
			LevelUI.Append(RadianceButton);
			RadText.Left.Set(52f, 0f);
			RadText.Top.Set(16f, 0f);
			RadianceButton.Append(RadText);

			SttText.Left.Set(15f, 0f);
			SttText.Top.Set(402f, 0f);
			LevelUI.Append(SttText);

			//Perks

			PerkDsc.backgroundColor = new Color(255, 255, 255, 255);
			PerkDsc.Width.Set(250f, 0f);PerkDsc.Height.Set(100f, 0f);
			PerkDsc.Top.Set(23f, 0f);
			LevelUI.Append(PerkDsc);

			PerkIncButton = new UIImageButton(PerkIncButtonImage);
			PerkIncButton.Width.Set(15f, 0f); PerkIncButton.Height.Set(15f, 0f);
			PerkIncButton.OnClick += (a, b) => PerkIncButtonClicked(true);
			PerkIncButton.OnRightClick += (a, b) => PerkIncButtonClicked(false);
			LevelUI.Append(PerkIncButton);
			
			//Names
			PerkRelevantString[1] = "Wing Muscle";
			PerkRelevantString[2] = "Overlord";
			//Buttons
			PerkButton[1] = new UIImageButton(ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/Perk_WingMuscle"));
			PerkButton[1].Width.Set(23f, 0f); PerkButton[1].Height.Set(23f, 0f);
			PerkButton[1].Left.Set(89f + 244f - 12.5f, 0f); PerkButton[1].Top.Set(14f + 180f - 12f, 0f);
			PerkButton[1].OnClick += (a, b) => PerkButtonClicked(1);
			PerkButton[1].OnRightClick += (a, b) => PerkDescClose();
			LevelUI.Append(PerkButton[1]);

			PerkButton[2] = new UIImageButton(ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/Perk_Overlord"));
			PerkButton[2].Width.Set(23f, 0f); PerkButton[1].Height.Set(23f, 0f);
			PerkButton[2].Left.Set(129f + 244f - 12.5f, 0f); PerkButton[2].Top.Set(14f + 180f - 12f, 0f);
			PerkButton[2].OnClick += (a, b) => PerkButtonClicked(2);
			PerkButton[2].OnRightClick += (a, b) => PerkDescClose();
			LevelUI.Append(PerkButton[2]);

			for (int i = 1; i <= 2; i++)
			{
				PerkText[i] = new UIText("");
				PerkText[i].Left.Set(0f, 0.75f); PerkText[i].Top.Set(0f, 0.5f);
				PerkButton[i].Append(PerkText[i]);
			}

			PerkDsc.Remove();
			PerkIncButton.Remove();
			#endregion
		}

		private void PerkIncButtonClicked(bool LeftClick)
		{
			Player player = Main.player[Main.myPlayer];
			if (LeftClick)
			{
				switch (PerkSelected)
				{
					case 1:
						if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt >= (int)Math.Pow(2 + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[1]) / 2F, 3))
						{
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt -= (int)Math.Pow(2 + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[1]) / 2F, 3);
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[1]++;
						}
						Main.PlaySound(SoundID.MenuTick);
						break;
					case 2:
						if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt >= (int)Math.Pow(10, 2 + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[2])))
						{
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt -= (int)Math.Pow(10, 2 + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[2]));
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[2]++;
						}
						Main.PlaySound(SoundID.MenuTick);
						break;
				}
			}
			else
			{
				switch (PerkSelected)
				{
					case 1:
						if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[1] >= 1)
						{
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt += (int)Math.Pow(2 + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[1] - 1) / 2F, 3);
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[1]--;
						}
						Main.PlaySound(SoundID.MenuTick);
						break;
					case 2:
						if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[2] >= 1)
						{
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt += (int)Math.Pow(10, 2 + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[2] - 1));
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[2]--;
						}
						Main.PlaySound(SoundID.MenuTick);
						break;
				}
			}
			//Descriptions
			PerkRelevantDescString[1] = "Each point into this perk\ngives +20% flight\nCosts " + (int)Math.Pow(2 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[1] / 2F, 3) + " points to upgrade";
			PerkRelevantDescString[2] = "Each point into this perk\ngives +1 maximum minions\nCosts " + (int)Math.Pow(10, 2 + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[2])) + " points to upgrade";
			PerkDscText.SetText(PerkRelevantDescString[PerkSelected]);
		}

		private void PerkButtonClicked(int value)
		{
			Player player = Main.player[Main.myPlayer];
			//Descriptions
			PerkRelevantDescString[1] = "Each point into this perk\ngives +20% flight\nCosts " + (int)Math.Pow(2 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[1] / 2F, 3) + " points to upgrade";
			PerkRelevantDescString[2] = "Each point into this perk\ngives +1 maximum minions\nCosts " + (int)Math.Pow(10, 2 + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[2])) + " points to upgrade";
			PerkDsc.Remove();
			PerkIncButton.Remove();
			LevelUI.Append(PerkDsc);
			LevelUI.Append(PerkIncButton);
			PerkDsc.Left.Set(PerkButton[value].Left.Pixels - PerkDsc.Width.Pixels / 2 + 12f, 0f);
			PerkDsc.Top.Set(PerkButton[value].Top.Pixels + 23f, 0f);
			PerkIncButton.Left.Set(PerkButton[value].Left.Pixels + 23f, 0f);
			PerkIncButton.Top.Set(PerkButton[value].Top.Pixels + 4f, 0f);
			PerkDscTitleText.Left.Set(73.5f, 0f);
			PerkDscText.Left.Set(25f, 0f); PerkDscText.Top.Set(25f, 0f);
			PerkDscTitleText.SetText(PerkRelevantString[value]);
			PerkDscText.SetText(PerkRelevantDescString[value]);
			PerkDsc.Append(PerkDscTitleText);
			PerkDsc.Append(PerkDscText);
			PerkSelected = value;
			Main.PlaySound(SoundID.MenuTick);
		}

		private void PerkDescClose()
		{
			PerkDsc.Remove();
			PerkIncButton.Remove();
			PerkSelected = -1;
			Main.PlaySound(SoundID.MenuTick);
		}

		private void HideButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			LevelUI.Remove();
			Main.PlaySound(SoundID.MenuClose);
		}

		private void StatButtonClicked(int num, bool LeftClick)
		{
			Player player = Main.player[Main.myPlayer];
			int i = 1;
			if (!LeftClick)i = 50;

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
			player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[0] = 0;player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[1] = 0;player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[2] = 0;player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[3] = 0;player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[4] = 0;player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[5] = 0;player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[6] = 0;
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
			player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[0] = 0;player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[1] = 0;player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[2] = 0;player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[3] = 0;player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[4] = 0;player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[5] = 0;player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[6] = 0;
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
			Append(LevelUI);
			Main.PlaySound(SoundID.MenuOpen);
		}

		public override void Update(GameTime gameTime)
		{
			Player player = Main.player[Main.myPlayer];
			float LifeBarWidth = player.statLifeMax * 2 + 24f;
			float ManaBarHeight = player.statManaMax * 2 + 20f;
			int ExpBarFillWidth = (int)(((float)player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lexp / player.GetModPlayer<CrescentPlayer>(Crescent.mod).Llxp) * 100) + 10;
			float MaxLife;
			float MaxLife2;
			if (player.statLifeMax <= 400)
			{
				MaxLife = player.statLifeMax;
				MaxLife2 = player.statLifeMax2;
			}
			else
			{
				MaxLife = 400;
				MaxLife2 = player.statLifeMax2;
			}

			//Status bars
			if (SkillMenuVisible)
			{

			}
			StrText.SetText(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[0].ToString());
			AgiText.SetText(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[1].ToString());
			DexText.SetText(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[2].ToString());
			ForText.SetText(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[3].ToString());
			IntText.SetText(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[4].ToString());
			VitText.SetText(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[5].ToString());
			RadText.SetText(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[6].ToString());
			SttText.SetText(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt.ToString());
			for (int i = 1; i <= 2; i++)
			{
				if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[i] > 0) { PerkText[i].SetText("(" + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[i].ToString() + ")"); }
				else { PerkText[i].SetText(""); }
			}

			if (LifeBarWidth > 824) { LifeBarWidth = 824; }

			Life.Width.Set(LifeBarWidth, 0f);
			Life.Left.Set(-(84 + LifeBarWidth), 1f);
			FlorLeft.Left.Set(Life.Left.Pixels + 73f, 1f);

			Mana.Height.Set(ManaBarHeight, 0f);

			ExpFill.Width.Set(ExpBarFillWidth, 0f);
			ExpFill.Left.Set(-81 - ExpBarFillWidth, 1f);

			for (int i = 0; i < 25; i++)
			{
				float Alpha = (player.statLife - ((MaxLife2 / (MaxLife / 20f)) * i)) / (MaxLife2 / (MaxLife / 20f));
				if (Alpha > 1) { Alpha = 1; }
				if (Alpha < 0) { Alpha = 0; }
				LifePoint[i].ImageScale = Alpha;

				Alpha = (player.statMana - ((player.statManaMax2 / (player.statManaMax / 10f)) * i)) / (player.statManaMax2 / (player.statManaMax / 10f));
				if (Alpha > 1) { Alpha = 1; }
				if (Alpha < 0) { Alpha = 0; }
				ManaPoint[i].ImageScale = Alpha;
			}
			if (player.statLifeMax >= 400)
			{
				Floral.ImageScale = 1f;
				FlorLeft.Width.Set(350f - (((player.statLifeMax - 400) / 5) * 16), 0f);
				FlorRight.Width.Set(320f - (((player.statLifeMax - 400) / 5) * 16), 0f);
				FlorRight.Left.Set(Life.Left.Pixels + 479f + (((player.statLifeMax - 400) / 5) * 16), 1f);
			}
			else
			{
				Floral.ImageScale = 0f;
				FlorLeft.Width.Set(0f, 0f);
				FlorRight.Width.Set(0f, 0f);
			}

			Level.SetText(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Llvl.ToString());
			LifeText.SetText(player.statLife.ToString());
			ManaText.SetText(player.statMana.ToString());

			Level.Left.Set(-Level.Text.Length * 3.7f, 0.5f);
			Level.Top.Set(-18f, 0.33f);
			LifeText.Left.Set(-LifeText.Text.Length * 3.7f, 0.5f);
			LifeText.Top.Set(-2f, 0.33f);
			ManaText.Left.Set(-ManaText.Text.Length * 3.7f, 0.5f);
			ManaText.Top.Set(14f, 0.33f);

			if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt > 0)
			{
				Level.TextColor = Gold;
			}
			else
			{
				Level.TextColor = Color.White;
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
		class TopRight : UIElement
		{
			public Color backgroundColor = Color.Gray;
			private static Texture2D _backgroundTexture;

			public TopRight()
			{
				if (_backgroundTexture == null)
					_backgroundTexture = ModLoader.GetTexture("Crescent/Assets/UI/TopRight");
			}

			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dimensions = GetDimensions();
				Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
				int width = (int)Math.Ceiling(dimensions.Width);
				int height = (int)Math.Ceiling(dimensions.Height);
				spriteBatch.Draw(_backgroundTexture, new Rectangle(point1.X, point1.Y, width, height), backgroundColor);
			}
		}

		class LifeBar : UIElement
		{
			public Color backgroundColor = Color.Gray;
			private static Texture2D Bar;
			private static Texture2D End;

			public LifeBar()
			{
				if (Bar == null)
					Bar = ModLoader.GetTexture("Crescent/Assets/UI/LifeBarMid");
				if (End == null)
					End = ModLoader.GetTexture("Crescent/Assets/UI/LifeBarEnd");
			}

			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dimensions = GetDimensions();
				Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
				int width = (int)Math.Ceiling(dimensions.Width);
				int height = (int)Math.Ceiling(dimensions.Height);
				spriteBatch.Draw(Bar, new Rectangle(point1.X + 125, point1.Y, width - 125, 70), backgroundColor);
				spriteBatch.Draw(End, new Rectangle(point1.X, point1.Y, 125, 70), backgroundColor);
			}
		}

		class ManaBar : UIElement
		{
			public Color backgroundColor = Color.Gray;
			private static Texture2D Bar;
			private static Texture2D End;
			private static Texture2D Spc;

			public ManaBar()
			{
				if (Bar == null)
					Bar = ModLoader.GetTexture("Crescent/Assets/UI/ManaBarMid");
				if (End == null)
					End = ModLoader.GetTexture("Crescent/Assets/UI/ManaBarEnd");
				if (Spc == null)
					Spc = ModLoader.GetTexture("Crescent/Assets/UI/ManaBarSpecialCase");
			}

			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dimensions = GetDimensions();
				Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
				int height = (int)Math.Ceiling(dimensions.Height);

				if (height > 40)
				{
					spriteBatch.Draw(Bar, new Rectangle(point1.X, point1.Y, 40, height - 43), backgroundColor);
					spriteBatch.Draw(End, new Rectangle(point1.X, point1.Y + height - 43, 40, 43), backgroundColor);
				}
				else
				{
					spriteBatch.Draw(Spc, new Rectangle(point1.X, point1.Y + height - 43, 40, 45), backgroundColor);
				}
			}
		}

		class ExpBarFilled : UIElement
		{
			public Color backgroundColor = Color.Gray;
			private static Texture2D Stt;
			private static Texture2D Bar;
			private static Texture2D End;

			public ExpBarFilled()
			{
				if (Stt == null)
					Stt = ModLoader.GetTexture("Crescent/Assets/UI/ExpBarFilledStart");
				if (Bar == null)
					Bar = ModLoader.GetTexture("Crescent/Assets/UI/ExpBarFilledMid");
				if (End == null)
					End = ModLoader.GetTexture("Crescent/Assets/UI/ExpBarFilledEnd");
			}

			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dimensions = GetDimensions();
				Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
				int width = (int)Math.Ceiling(dimensions.Width);
				int height = (int)Math.Ceiling(dimensions.Height);
				spriteBatch.Draw(Stt, new Rectangle(point1.X + width - 1, point1.Y, 1, 10), backgroundColor);
				spriteBatch.Draw(Bar, new Rectangle(point1.X + 9, point1.Y, width - 10, 10), backgroundColor);
				spriteBatch.Draw(End, new Rectangle(point1.X, point1.Y, 9, 9), backgroundColor);
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

		class FloralHider : UIElement
		{
			public Color backgroundColor = Color.Gray;
			private static Texture2D FloralBar;

			public FloralHider()
			{
				if (FloralBar == null)
					FloralBar = ModLoader.GetTexture("Crescent/Assets/UI/LifeFloralHider");
			}

			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dimensions = GetDimensions();
				Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
				int width = (int)Math.Ceiling(dimensions.Width);
				int height = (int)Math.Ceiling(dimensions.Height);
				spriteBatch.Draw(FloralBar, new Rectangle(point1.X, point1.Y, width, height), backgroundColor);
			}
		}

		class LevelUIBox : UIElement
		{
			public Color backgroundColor = Color.Gray;
			private static Texture2D LifeforceBox;
			private static Texture2D LifeforceBoxBG;
			private static Texture2D Twinkle;

			public LevelUIBox()
			{
				if (LifeforceBox == null)
					LifeforceBox = ModLoader.GetTexture("Crescent/Assets/UI/LifeforceBox");
				if (LifeforceBoxBG == null)
					LifeforceBoxBG = ModLoader.GetTexture("Crescent/Assets/UI/LifeforceBoxBackground");
				if (Twinkle == null)
					Twinkle = ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/Twinkle");
			}

			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dimensions = GetDimensions();
				Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
				int width = (int)Math.Ceiling(dimensions.Width);
				int height = (int)Math.Ceiling(dimensions.Height);
				spriteBatch.Draw(LifeforceBoxBG, new Rectangle(point1.X, point1.Y, width, height), backgroundColor);
				spriteBatch.Draw(Twinkle, new Rectangle(
					(int)(point1.X + 347 + Math.Sin((System.DateTime.Now.Ticks / Math.PI) / 10000000F) * 244),
					(int)(point1.Y + 188 + Math.Cos((System.DateTime.Now.Ticks / Math.PI) / 10000000F) * 180),
					12, 12), backgroundColor);
				spriteBatch.Draw(LifeforceBox, new Rectangle(point1.X, point1.Y, width, height), backgroundColor);
			}
		}
		#endregion
	}
}
