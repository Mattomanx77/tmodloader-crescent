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
	public class VanillaThemeUI : UIState
	{
		public static bool visible = true;
		private UIPanel LevelIndicator = new UIPanel();
		private UIText Level = new UIText("", 2f);
		private UIText Exp = new UIText("");
		private UIPanel StatsPane = new UIPanel();
		private UIText StatsPaneTitle = new UIText("Stats");
		private UIPanel[] StatsButtons = new UIPanel[Crescent.NUMSTATS];
		private UIText[] StatsTitle = new UIText[Crescent.NUMSTATS+1];
		private UIText[] StatsText = new UIText[Crescent.NUMSTATS+1];
		private Vector2 offset; private bool dragging = false;
		private UIPanel RestartConfirm = new UIPanel();
		private UIText[] RestartConfirmText = new UIText[2] { new UIText("Are you sure you want"), new UIText("to start all over?") };
		private UIText[] RestartConfirmButton = new UIText[2] { new UIText("[Yes]"), new UIText("[No]") };
		private UIPanel PerksPane = new UIPanel();
		UIPanel PerksPaneInner = new UIPanel() { BackgroundColor = Color.Black * 0.5f };
		private UIText PerksPaneTitle = new UIText("Perks");
		private UIPanel[] PerksButtons = new UIPanel[Crescent.NUMPERKS];
		private UIImage[] PerksButtonsIcons = new UIImage[Crescent.NUMPERKS];
		private UIText[] PerksTitle = new UIText[Crescent.NUMPERKS];
		private UIText[] PerksText = new UIText[Crescent.NUMPERKS];
		private UIPanel PerkDia = new UIPanel();
		private UIText PerkIncButton = new UIText("+", 2);
		private UIText PerkDiaTitleText = new UIText("");
		private UIText PerkDiaText = new UIText("");
		private UIText MouseText = new UIText("");

		public override void OnInitialize()
		{
			//LevelIndicator
			LevelIndicator.BackgroundColor = new Color(0, 0, 255, 127);
			LevelIndicator.Width.Set(52f, 0f); LevelIndicator.Height.Set(52f, 0f);
			LevelIndicator.Left.Set(-360f, 1f); LevelIndicator.Top.Set(17f, 0f);
			LevelIndicator.OnClick += new MouseEvent(TopRightClicked);
			LevelIndicator.OnRightClick += (a, b) => HideButtonClicked(2);
			Append(LevelIndicator);
			LevelIndicator.Append(Level);
			LevelIndicator.Append(Exp);

			#region Stats Pane
			StatsPane.Height.Set(Crescent.NUMSTATS * 48 + 81f, 0f); StatsPane.Width.Set(104f, 0f);
			StatsPane.Left.Set(-363f, 0.5f); StatsPane.Top.Set(80f, 0f);
			StatsPane.OnMouseDown += new MouseEvent(DragStart);
			StatsPane.OnMouseUp += new MouseEvent(DragEnd);
			StatsPaneTitle.Left.Set(-StatsPaneTitle.MinWidth.Pixels / 2, 0.5f);
			StatsPane.Append(StatsPaneTitle);
			UIText StatsHider = new UIText("-");
			StatsHider.Left.Set(-StatsHider.MinWidth.Pixels, 1f);
			StatsHider.OnClick += (a, b) => HideButtonClicked(0);
			StatsPane.Append(StatsHider);
			UIImage Respec = new UIImage(ModLoader.GetTexture("Terraria/UI/ButtonSeed"));
			Respec.Left.Set(-Respec.Width.Pixels*3/2, 1f); Respec.Top.Set(-Respec.Height.Pixels/2, 1f);
			Respec.OnClick += (UIMouseEvent, UIElement) => Crescent.mod.LifeForceUI.RespecButtonClicked(UIMouseEvent, UIElement);
			StatsPane.Append(Respec);
			UIImage Reset = new UIImage(ModLoader.GetTexture("Terraria/UI/ButtonDelete"));
			Reset.Left.Set(-Reset.Height.Pixels/2, 1f); Reset.Top.Set(-Reset.Height.Pixels/2, 1f);
			Reset.OnClick += (UIMouseEvent, UIElement) => RestartButtonClicked(UIMouseEvent, UIElement);
			StatsPane.Append(Reset);

			for (int i = 0; i < Crescent.NUMSTATS; i++){
				int n = i;
				StatsButtons[i] = new UIPanel();
				StatsButtons[i].Width.Set(48f, 0f); StatsButtons[i].Height.Set(48f, 0f);
				StatsButtons[i].Left.Set(-6f, 0f); StatsButtons[i].Top.Set(i * 48 + 24f, 0f);
				StatsButtons[i].OnClick += (a, b) => Crescent.mod.LifeForceUI.StatButtonClicked(n, true);
				StatsButtons[i].OnRightClick += (a, b) => Crescent.mod.LifeForceUI.StatButtonClicked(n, false);
				StatsButtons[i].OnMouseOver += (a, b) => StatButtonHover(n);
				StatsButtons[i].OnMouseOut += (a, b) => StatButtonHover(-1);
				StatsPane.Append(StatsButtons[i]);
				StatsText[i] = new UIText("X");
				StatsText[i].Left.Set(40f, 0f); StatsText[i].Top.Set(-StatsText[i].MinHeight.Pixels/2, 0.5f);
				StatsButtons[i].Append(StatsText[i]);
				StatsTitle[i] = new UIText("Stat", 0.7f);
				StatsTitle[i].Left.Set(10f, 1f); StatsTitle[i].Top.Set(-10f, 0f);
				StatsButtons[i].Append(StatsTitle[i]);
			}

			StatsTitle[0].SetText("Strength", 0.7f, false); StatsButtons[0].BackgroundColor = new Color(255, 0, 0, 191);
			StatsTitle[1].SetText("Agility", 0.7f, false); StatsButtons[1].BackgroundColor = new Color(255, 127, 0, 191);
			StatsTitle[2].SetText("Fortune", 0.7f, false); StatsButtons[2].BackgroundColor = new Color(255, 255, 0, 191);
			StatsTitle[3].SetText("Dexterity", 0.7f, false); StatsButtons[3].BackgroundColor = new Color(0, 255, 0, 191);
			StatsTitle[4].SetText("Fortitude", 0.7f, false); StatsButtons[4].BackgroundColor = new Color(0, 255, 255, 191);
			StatsTitle[5].SetText("Intelligence", 0.6f, false); StatsButtons[5].BackgroundColor = new Color(0, 0, 255, 191);
			StatsTitle[6].SetText("Vitality", 0.7f, false); StatsButtons[6].BackgroundColor = new Color(255, 0, 255, 191);
			StatsTitle[7].SetText("Radiance", 0.7f, false); StatsButtons[7].BackgroundColor = new Color(255, 255, 255, 191);
			StatsText[Crescent.NUMSTATS] = new UIText("Left: X");
			StatsText[Crescent.NUMSTATS].Left.Set(0f, 0f);
			StatsText[Crescent.NUMSTATS].Top.Set(-30f, 1f);
			StatsPane.Append(StatsText[Crescent.NUMSTATS]);

			//Restart Confirmation
			RestartConfirm.Width.Set(200f, 0f); RestartConfirm.Height.Set(75f, 0f);

			RestartConfirmText[0].Left.Set(-RestartConfirmText[0].MinWidth.Pixels / 2f, 0.5f);
			RestartConfirm.Append(RestartConfirmText[0]);
			RestartConfirmText[1].Left.Set(-RestartConfirmText[1].MinWidth.Pixels / 2f, 0.5f);
			RestartConfirmText[1].Top.Set(RestartConfirmText[0].MinHeight.Pixels, 0f);
			RestartConfirm.Append(RestartConfirmText[1]);
			RestartConfirmButton[0].Top.Set(-RestartConfirmButton[0].MinHeight.Pixels - 6, 1f);
			RestartConfirmButton[0].Left.Set(-RestartConfirmButton[0].MinWidth.Pixels / 2, 1 / 3f);
			RestartConfirmButton[0].OnMouseDown += (a, b) => RestartButtonChecked(true);
			RestartConfirm.Append(RestartConfirmButton[0]);
			RestartConfirmButton[1].Top.Set(-RestartConfirmButton[1].MinHeight.Pixels - 6, 1f);
			RestartConfirmButton[1].Left.Set(-RestartConfirmButton[1].MinWidth.Pixels / 2, 2 / 3f);
			RestartConfirmButton[1].OnMouseDown += (a, b) => RestartButtonChecked(false);
			RestartConfirm.Append(RestartConfirmButton[1]);
			#endregion
			#region Perks Pane
			PerksPane.Height.Set(386f, 0f); PerksPane.Width.Set(514f, 0f);
			PerksPane.Left.Set(-257f, 0.5f); PerksPane.Top.Set(80f, 0f);
			PerksPaneTitle.Left.Set(-PerksPaneTitle.MinWidth.Pixels / 2, 0.5f);
			PerksPane.Append(PerksPaneTitle);
			UIText PerksHider = new UIText("-");
			PerksHider.Left.Set(-PerksHider.MinWidth.Pixels, 1f);
			PerksHider.OnClick += (a, b) => HideButtonClicked(1);
			PerksPane.Append(PerksHider);
			PerksPaneInner.Width.Set(0f, 1f); PerksPaneInner.Height.Set(-PerksPaneTitle.MinHeight.Pixels - 12f, 1f);
			PerksPaneInner.Left.Set(0f, 0f); PerksPaneInner.Top.Set(PerksPaneTitle.MinHeight.Pixels + 12f, 0f);
			PerksPane.Append(PerksPaneInner);

			//Buttons
			PerksButtonsIcons[0] = new UIImage(ModLoader.GetTexture("Terraria/Item_493"));
			PerksButtonsIcons[1] = new UIImage(ModLoader.GetTexture("Terraria/Item_1158"));
			PerksButtonsIcons[2] = new UIImage(ModLoader.GetTexture("Terraria/Item_159"));
			PerksButtonsIcons[3] = new UIImage(ModLoader.GetTexture("Terraria/Item_184"));
			PerksButtonsIcons[4] = new UIImage(ModLoader.GetTexture("Terraria/Item_58"));
			PerksButtonsIcons[5] = new UIImage(ModLoader.GetTexture("Terraria/Item_3810"));
			PerksButtonsIcons[6] = new UIImage(ModLoader.GetTexture("Terraria/Item_320"));
			PerksButtonsIcons[7] = new UIImage(ModLoader.GetTexture("Terraria/Item_1569"));

			for (int i = 0; i < Crescent.NUMPERKS; i++){
				int n = i;
				PerksButtons[i] = new UIPanel();
				PerksButtons[i].Width.Set(23f, 0f); PerksButtons[i].Height.Set(23f, 0f);
				PerksButtons[i].Left.Set((2 * (i - (-0.5f + Crescent.NUMPERKS / 2F)) * 23) - 12f, 0.5f); PerksButtons[i].Top.Set(-12f, 0.5f);
				PerksButtons[i].OnClick += (a, b) => PerkButtonClicked(n);
				PerksButtons[i].OnRightClick += (a, b) => PerkDescClose();
				PerksPaneInner.Append(PerksButtons[i]);
				PerksButtonsIcons[i].Left.Set(-PerksButtonsIcons[i].Width.Pixels/2, 0.5f); PerksButtonsIcons[i].Top.Set(-PerksButtonsIcons[i].Height.Pixels / 2, 0.5f);
				PerksButtons[i].Append(PerksButtonsIcons[i]);
				PerksText[i] = new UIText("");
				PerksText[i].Left.Set(0f, 0.75f); PerksText[i].Top.Set(0f, 0.5f);
				PerksButtons[i].Append(PerksText[i]);
			}

			PerkIncButton.OnClick += (a, b) => PerkIncButtonClicked(true);
			PerkIncButton.OnRightClick += (a, b) => PerkIncButtonClicked(false);

			PerkDia.Width.Set(300f, 0f); PerkDia.Height.Set(150f, 0f);
			PerkDia.Top.Set(23f, 0f);
			#endregion
		}

		private void StatButtonHover(int n)
		{
			Player player = Main.player[Main.myPlayer];
			Crescent.mod.LifeForceUI.StatDesc[0] = new UIText("+" + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[0] * 0.1).ToString() + "% melee damage\n+" + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[0] * 0.1).ToString() + "% throwing damage");
			Crescent.mod.LifeForceUI.StatDesc[1] = new UIText("+" + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[1] * 0.05).ToString() + "% movement speed");
			Crescent.mod.LifeForceUI.StatDesc[2] = new UIText("Increased crit chance\n+" + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[2] * 0.1).ToString() + "% money gained");
			Crescent.mod.LifeForceUI.StatDesc[3] = new UIText("+" + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[3] * 0.1).ToString() + "% ranged damage" + (Crescent.mod.thoriumLoaded ? "/n+" + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[3] * 0.1).ToString() + "% symphonic damage" : ""));
			Crescent.mod.LifeForceUI.StatDesc[4] = new UIText("+" + (Math.Floor(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[4] * 0.1)).ToString() + " defense");
			Crescent.mod.LifeForceUI.StatDesc[5] = new UIText("+" + (Math.Floor(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[5] * 0.1)).ToString() + "% mana\n+" + (Math.Floor(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[5] * 0.1)).ToString() + "% magic damage	" + (Crescent.mod.tremorLoaded ? "/n+" + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[5] * 0.1).ToString() + "% alchemical damage" : ""));
			Crescent.mod.LifeForceUI.StatDesc[6] = new UIText("+" + (Math.Floor(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[6] * 0.1)).ToString() + "% life");
			Crescent.mod.LifeForceUI.StatDesc[7] = new UIText("+" + (Math.Floor(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[7] * 0.1)).ToString() + "% minion damage" + (Crescent.mod.thoriumLoaded ? "/n+" + (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[7] * 0.1).ToString() + "% radiant damage" : ""));

			if (n != -1)
			{
				Append(MouseText);
				MouseText.SetText(Crescent.mod.LifeForceUI.StatDesc[n].Text);
			}
			else
			{
				MouseText.Remove();
				MouseText.SetText("");
			}
		}

		public void PerkDescClose()
		{
			if (PerksPaneInner.HasChild(PerkDia))
			{
				PerkDia.Remove();
				PerkIncButton.Remove();
			}
			Crescent.mod.LifeForceUI.PerkSelected = -1;
			Main.PlaySound(SoundID.MenuTick);
		}

		public void PerkIncButtonClicked(bool v)
		{
			Crescent.mod.LifeForceUI.PerkIncButtonClicked(v);
			PerkDiaText.SetText(Crescent.mod.LifeForceUI.PerkRelevantDescString[Crescent.mod.LifeForceUI.PerkSelected]);
		}

		public void PerkButtonClicked(int value)
		{
			Player player = Main.player[Main.myPlayer];
			Crescent.mod.LifeForceUI.PerkCostUpdate(player);
			//Descriptions
			PerkDia.Remove();
			PerkIncButton.Remove();
			PerksPaneInner.Append(PerkDia);
			PerksPaneInner.Append(PerkIncButton);
			PerkDia.Left.Set(PerksButtons[value].Left.Pixels - PerkDia.Width.Pixels / 2 + 12f, 0.5f);
			PerkDia.Top.Set(PerksButtons[value].Top.Pixels + 23f, 0.5f);
			PerkIncButton.Left.Set(PerksButtons[value].Left.Pixels + 23f, 0.5f);
			PerkIncButton.Top.Set(PerksButtons[value].Top.Pixels + 3f, 0.5f);
			PerkDiaTitleText.SetText(Crescent.mod.LifeForceUI.PerkRelevantString[value]);
			PerkDiaTitleText.Left.Set(-PerkDiaTitleText.MinWidth.Pixels/2, 0.5f);
			PerkDiaText.Left.Set(25f, 0f); PerkDiaText.Top.Set(25f, 0f);
			PerkDiaText.SetText(Crescent.mod.LifeForceUI.PerkRelevantDescString[value]);
			PerkDia.Append(PerkDiaTitleText);
			PerkDia.Append(PerkDiaText);
			Crescent.mod.LifeForceUI.PerkSelected = value;
			Main.PlaySound(SoundID.MenuTick);
		}

		private void RestartButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			RestartConfirm.Top.Set(Main.MouseScreen.Y, 0f);
			RestartConfirm.Left.Set(Main.MouseScreen.X - RestartConfirm.Width.Pixels / 2, 0f);
			Append(RestartConfirm);
		}

		private void RestartButtonChecked(bool yes)
		{
			Main.PlaySound(SoundID.MenuTick);
			if (yes)
			{
				Player player = Main.player[Main.myPlayer];
				player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lexp = 0; player.GetModPlayer<CrescentPlayer>(Crescent.mod).Llvl = 0;
				for (int i = 0; i < Crescent.NUMSTATS; i++)
				{
					player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[i] = 0;
				}
				player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt = 0;
				player.GetModPlayer<CrescentPlayer>(Crescent.mod).Llxp = (int)(Math.Pow((player.GetModPlayer<CrescentPlayer>(Crescent.mod).Llvl + 1) * 333, 1.2));
				for (int i = 0; i < 8; i++)
				{
					player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[i] = 0;
				}
			}
			RestartConfirm.Remove();
		}

		private void TopRightClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Append(StatsPane);
			Append(PerksPane);
			Main.PlaySound(SoundID.MenuOpen);
		}

		public void HideButtonClicked(int v)
		{
			switch (v)
			{
				case 0:
					StatsPane.Remove();
					break;
				case 1:
					PerksPane.Remove();
					//PerkDescClose();
					break;
				case 2:
					StatsPane.Remove();
					PerksPane.Remove();
					//PerkDescClose();
					break;
				default:
					break;
			}
			Main.PlaySound(SoundID.MenuClose);
		}

		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			offset = new Vector2(evt.MousePosition.X - StatsPane.Left.Pixels, evt.MousePosition.Y - StatsPane.Top.Pixels);
			dragging = true;
		}

		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			Vector2 end = evt.MousePosition;
			dragging = false;

			StatsPane.Left.Set(end.X - offset.X, 0.5f);
			StatsPane.Top.Set(end.Y - offset.Y, 0f);

			Recalculate();
		}

		public override void Update(GameTime gameTime)
		{
			Player player = Main.player[Main.myPlayer];
			float xPercent = player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lexp / (float)player.GetModPlayer<CrescentPlayer>(Crescent.mod).Llxp;

			LevelIndicator.BackgroundColor = new Color((byte)(255 * xPercent), (byte)(255 * xPercent), 255, 127);
			Level.TextColor = player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt > 0 ? new Color(255, 191, 0) : Color.White;
			Level.SetText(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Llvl.ToString(), 2, false);
			Level.Left.Set(-Level.MinWidth.Pixels / 2, 0.5f); Level.Top.Set(-Level.MinHeight.Pixels / 2, 1/3f);
			Exp.SetText((Math.Floor(10000 * (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lexp / (float)player.GetModPlayer<CrescentPlayer>(Crescent.mod).Llxp)) / 100).ToString() + "%", 0.7f, false);
			Exp.Left.Set(-Exp.MinWidth.Pixels / 2, 0.5f); Exp.Top.Set(-4f, 1f);

			//Stats text
			for (int i = 0; i < Crescent.NUMSTATS; i++)
			{
				StatsText[i].SetText(player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[i].ToString());
			}
			StatsText[Crescent.NUMSTATS].SetText("Left: " + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lstt.ToString());

			//Stats window
			if (dragging)
			{
				StatsPane.Left.Set(Main.mouseX - offset.X, 0.5f);
				StatsPane.Top.Set(Main.mouseY - offset.Y, 0f);
			}

			//Perk window
			for (int i = 0; i < Crescent.NUMPERKS; i++)
			{
				if (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[i] > 0) { PerksText[i].SetText("(" + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Perk[i].ToString() + ")"); }
				else { PerksText[i].SetText(""); }
			}

			Recalculate();
		}
	}
}