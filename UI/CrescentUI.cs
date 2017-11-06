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
		public Texture2D LifePointSW1 = ModLoader.GetTexture("Crescent/Assets/UI/SecondWindWarning1");
		public Texture2D LifePointSW2 = ModLoader.GetTexture("Crescent/Assets/UI/SecondWindWarning2");
		public Texture2D ManaPoint1 = ModLoader.GetTexture("Crescent/Assets/UI/ManaBarFilled1");
		public Texture2D ManaPoint2 = ModLoader.GetTexture("Crescent/Assets/UI/ManaBarFilled2");
		public Texture2D FloralImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeFloral");
		public Texture2D buttonHideImage = ModLoader.GetTexture("Crescent/Assets/UI/HideButton");
		public Texture2D buttonRespecImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/RespecButton");
		public Texture2D buttonRestartImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/RestartButton");
		public UIImage PerkIncButton;
		public string[] PerkRelevantString = new string[32];
		public string[] PerkRelevantDescString = new string[32];
		public string[] SkillRelevantString = new string[32];
		public string[] SkillRelevantDescString = new string[32];
		public Texture2D PerkIncButtonImage = ModLoader.GetTexture("Crescent/Assets/UI/LifeForceBoxSubAssets/PlusButton");
		public int PerkSelected;
		public UIImage[] PerkButton = new UIImage[32];
		public UIImage[] SkillButton = new UIImage[32];
		public UIText[] PerkText = new UIText[32];
		public UIText[] SkillText = new UIText[32];
		private LifeBar Life = new LifeBar();
		private ManaBar Mana = new ManaBar();
		private UIImage Exp = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/ExpBar"));
		private PerkDesc PerkDsc = new PerkDesc();
		private ExpBarFilled ExpFill = new ExpBarFilled();
		private UIImage[] LifePoint = new UIImage[25];
		private UIImage[] LifePointSW = new UIImage[25];
		private UIImage[] ManaPoint = new UIImage[25];
		private UIImage Floral;
		private FloralHider FlorLeft = new FloralHider();
		private FloralHider FlorRight = new FloralHider();
		private UIText Level = new UIText("0");
		private Color Gold = new Color(255, 191, 0);
		private UIText LifeText = new UIText("0");
		private UIText ManaText = new UIText("0");
		private StatUIBox StatUI = new StatUIBox();
		private PerkUIBox PerkUI = new PerkUIBox();
		public bool SkillMenuVisible;
		public UIText SttText = new UIText("");
		public UIText[] StatText = new UIText[10];
		public UIImage[] StatButton = new UIImage[10];
		public UIText PerkDscTitleText = new UIText("");
		public UIText PerkDscText = new UIText("");
		public int NumStats = 8;
		public int NumPerks = 5;
		private Vector2 offset;public bool dragging = false;

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
			parent.OnRightClick += (a, b) => HideButtonClicked(2);
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
					LifePointSW[i] = new UIImage(LifePointSW1);
					LifePointSW[i].Width.Set(40f, 0f);
					ManaPoint[i] = new UIImage(ManaPoint1);
					ManaPoint[i].Height.Set(20f, 0f);
					ManaPoint[i].Top.Set(0f, 0f);
				}
				else
				{
					LifePoint[i] = new UIImage(LifePoint2);
					LifePoint[i].Width.Set(71f, 0f);
					LifePointSW[i] = new UIImage(LifePointSW2);
					LifePointSW[i].Width.Set(71f, 0f);
					ManaPoint[i] = new UIImage(ManaPoint2);
					ManaPoint[i].Height.Set(39f, 0f);
					ManaPoint[i].Top.Set(-19 + i * 20, 0f);
				}
				LifePoint[i].Height.Set(40f, 0f);
				LifePoint[i].Left.Set(-(i + 1) * 40, 1f);
				LifePoint[i].Top.Set(4f, 0f);
				Life.Append(LifePoint[i]);
				LifePointSW[i].Height.Set(40f, 0f);
				LifePointSW[i].Left.Set(-(i + 1) * 40, 1f);
				LifePointSW[i].Top.Set(4f, 0f);
				Life.Append(LifePointSW[i]);
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
			#region StatUI
			StatUI.Height.Set(NumStats*48 + 81f, 0f);
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

			for (int i = 0; i < NumStats; i++)
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
			SttText.Top.Set(NumStats * 48 + 54f, 0f);
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
			PerkIncButton.Width.Set(15f, 0f); PerkIncButton.Height.Set(15f, 0f);
			PerkIncButton.OnClick += (a, b) => PerkIncButtonClicked(true);
			PerkIncButton.OnRightClick += (a, b) => PerkIncButtonClicked(false);
			PerkUI.Append(PerkIncButton);
			
			//Names
			PerkRelevantString[1] = "Wing Muscle";
			PerkRelevantString[2] = "Overlord";
			PerkRelevantString[3] = "Jumpman";
			PerkRelevantString[4] = "Reaper of Stars";
			PerkRelevantString[5] = "Second Wind";
			//Buttons
			PerkButton[1] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/Perk_WingMuscle"));
			PerkButton[2] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/Perk_Overlord"));
			PerkButton[3] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/Perk_Jumpman"));
			PerkButton[4] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/Perk_ReaperofStars"));
			PerkButton[5] = new UIImage(ModLoader.GetTexture("Crescent/Assets/UI/PerkAssets/Perk_SecondWind"));

			for (int i = 1; i <= NumPerks; i++)
			{
				int n = i;
				PerkButton[i].Width.Set(23f, 0f); PerkButton[i].Height.Set(23f, 0f);
				PerkButton[i].Left.Set((2*(i-(0.5f+NumPerks/2F))*23)+257-12f, 0f);PerkButton[i].Top.Set(193-12f, 0f);
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
				switch (PerkSelected)
				{
					case 1:
						if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt >= 50 * (1 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[1]))
						{
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt -= 50 * (1 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[1]);
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
					case 3:
						if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt >= 25 * (1 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[3]))
						{
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt -= 25 * (1 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[3]);
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[3]++;
						}
						Main.PlaySound(SoundID.MenuTick);
						break;
					case 4:
						if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt >= 50 * (1 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[4]))
						{
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt -= 50 * (1 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[4]);
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[4]++;
						}
						Main.PlaySound(SoundID.MenuTick);
						break;
					case 5:
						if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt >= 100 && player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[5] == 0)
						{
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt -= 100;
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[5]++;
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
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt += 50 * player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[1];
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
					case 3:
						if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[3] >= 1)
						{
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt += 25 * player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[3];
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[3]--;
						}
						Main.PlaySound(SoundID.MenuTick);
						break;
					case 4:
						if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[4] >= 1)
						{
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt += 50 * player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[4];
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[4]--;
						}
						Main.PlaySound(SoundID.MenuTick);
						break;
					case 5:
						if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[5] >= 1)
						{
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt += 100;
							player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[5]--;
						}
						Main.PlaySound(SoundID.MenuTick);
						break;
				}
			}
			//Descriptions
			PerkRelevantDescString[1] = "Each point into this perk\ngives +20% flight\nCosts " + 50 * (1 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[1]) + " points to upgrade";
			PerkRelevantDescString[2] = "Each point into this perk\ngives +1 maximum minions\nCosts " + (int)Math.Pow(10, 2 + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[2])) + " points to upgrade";
			PerkRelevantDescString[3] = "Each point into this perk\nboosts your jump 1 block\nCosts " + 25 * (1 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[3]) + " points to upgrade";
			PerkRelevantDescString[4] = "Each point into this perk\ngrants 1 mana per hit\nCosts " + 50 * (1 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[4]) + " points to upgrade";
			PerkRelevantDescString[5] = "Survive a fatal blow with\n10% of your life\nCosts 100 points to obtain";
			PerkDscText.SetText(PerkRelevantDescString[PerkSelected]);
		}

		private void PerkButtonClicked(int value)
		{
			Player player = Main.player[Main.myPlayer];
			//Descriptions
			PerkRelevantDescString[1] = "Each point into this perk\ngives +20% flight\nCosts " + 50 * (1 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[1]) + " points to upgrade";
			PerkRelevantDescString[2] = "Each point into this perk\ngives +1 maximum minions\nCosts " + (int)Math.Pow(10, 2 + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[2])) + " points to upgrade";
			PerkRelevantDescString[3] = "Each point into this perk\nboosts your jump 1 block\nCosts " + 25 * (1 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[3]) + " points to upgrade";
			PerkRelevantDescString[4] = "Each point into this perk\ngrants 1 mana per hit\nCosts " + 50 * (1 + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[4]) + " points to upgrade";
			PerkRelevantDescString[5] = "Survive a fatal blow with\n10% of your life\nCosts 100 points to obtain";
			PerkDsc.Remove();
			PerkIncButton.Remove();
			PerkUI.Append(PerkDsc);
			PerkUI.Append(PerkIncButton);
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
			if (PerkUI.HasChild(PerkDsc))
			{
				PerkDsc.Remove();
				PerkIncButton.Remove();
			}
			PerkSelected = -1;
			Main.PlaySound(SoundID.MenuTick);
		}

		private void HideButtonClicked(int num)
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
			for (int i = 0; i < NumStats; i++)
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
			for (int i = 0; i < NumStats; i++)
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
			for (int i = 0; i < NumStats; i++)
			{
				StatText[i].SetText(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[i].ToString());
			}
			SttText.SetText(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt.ToString());

			//Stats window
			if (dragging){
				StatUI.Left.Set(Main.mouseX - offset.X, 0.5f);
				StatUI.Top.Set(Main.mouseY - offset.Y, 0f);
			}
			for (int i = 1; i <= NumPerks; i++){
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
				if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).secondWindTimer <= 0)
				{
					LifePoint[i].ImageScale = Alpha;
					LifePointSW[i].ImageScale = 0;
				}
				else
				{
					LifePoint[i].ImageScale = 0;
					LifePointSW[i].ImageScale = Alpha;
				}

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
			}

			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dimensions = GetDimensions();
				Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
				int height = (int)Math.Ceiling(dimensions.Height);
				
				spriteBatch.Draw(Bar, new Rectangle(point1.X, point1.Y, 40, height - 43), backgroundColor);
				spriteBatch.Draw(End, new Rectangle(point1.X, point1.Y + height - 43, 40, 43), backgroundColor);
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
