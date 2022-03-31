using HMUI;
using IPA.Utilities;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace YeetDab
{
	class DabYeeter : IInitializable, IDisposable
	{
		private static FieldAccessor<MainMenuViewController, Button>.Accessor SoloButton = FieldAccessor<MainMenuViewController, Button>.GetAccessor("_soloButton");
		private static FieldAccessor<ButtonSpriteSwap, Sprite>.Accessor ActiveStateSprite = FieldAccessor<ButtonSpriteSwap, Sprite>.GetAccessor("_normalStateSprite");
		private static FieldAccessor<ButtonSpriteSwap, Sprite>.Accessor HighlightStateSprite = FieldAccessor<ButtonSpriteSwap, Sprite>.GetAccessor("_highlightStateSprite");

		private MainMenuViewController mainMenuViewController;

		public DabYeeter(MainMenuViewController mainMenuView)
		{
			this.mainMenuViewController = mainMenuView;
		}

		public void Initialize()
		{
			if (mainMenuViewController != null)
				mainMenuViewController.didActivateEvent += MainMenuView_didActivateEvent;
			else
				Plugin.Log.Error(GetLogString("DabYeeter failed to initialize. Reason: mainMenuView == null."));
			
		}

		public void Dispose()
		{
			if (mainMenuViewController != null)
				mainMenuViewController.didActivateEvent -= MainMenuView_didActivateEvent;
		}

		private void MainMenuView_didActivateEvent(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
		{
			if (firstActivation)
				YeetTheDab();
		}

		private void YeetTheDab()
		{
			Button soloButton = SoloButton(ref mainMenuViewController);
			ButtonSpriteSwap soloSpriteSwap = soloButton?.GetComponentInChildren<ButtonSpriteSwap>() ?? null;
			Image img = GetImageOverlayFromButton(soloButton);

			if (soloSpriteSwap == null || img == null)
			{
				Plugin.Log.Error(GetLogString("Dab can't be yeeted! Reason: Failed to get all required components from the soloButton."));
				return;
			}
			
			ReplaceMenuButtonSprites(ref img, ref soloSpriteSwap, LoadSoloButtonSprites());
			Plugin.Log.Debug("DabYeeter: Successfully yeeted dab");

		}

		private Image GetImageOverlayFromButton(Button button)
		{
			Transform imgTrans = button?.transform.Find("Image/ImageOverlay") ?? null;
			Image img = imgTrans?.GetComponent<Image>() ?? null;
			if (img == null)
				Plugin.Log.Error(GetLogString("Failed to get Image from button."));
			return img;
		}

		private MenuButtonSprites LoadSoloButtonSprites()
		{
			Sprite soloOutlineSprite = Utilities.Utils.LoadSpriteFromResources("YeetDab.Resources.SoloOutlineOverlay.png");
			Sprite soloDefaultBGSprite = Utilities.Utils.LoadSpriteFromResources("YeetDab.Resources.SoloNormalState.png");
			Sprite soloHighlightedBGSprite = Utilities.Utils.LoadSpriteFromResources("YeetDab.Resources.SoloHighlightState.png");

			return new MenuButtonSprites(soloOutlineSprite, soloDefaultBGSprite, soloHighlightedBGSprite);
		}

		private void ReplaceMenuButtonSprites(ref Image img, ref ButtonSpriteSwap soloSpriteSwap, MenuButtonSprites menuButtonSprites)
		{
			img.sprite = menuButtonSprites.OverlaySprite;
			ActiveStateSprite(ref soloSpriteSwap) = menuButtonSprites.NormalStateSprite;
			HighlightStateSprite(ref soloSpriteSwap) = menuButtonSprites.HighlightStateSprite;
		}

		

		private string GetLogString(string logMessage, [CallerMemberName] string callerMethodName = "")
		{
			return $"Class: DabYeeter, Method: {callerMethodName}, Message: {logMessage}";
		}
	}

	internal struct MenuButtonSprites
	{
		public readonly Sprite OverlaySprite;
		public readonly Sprite NormalStateSprite;
		public readonly Sprite HighlightStateSprite;

		public MenuButtonSprites(Sprite overlaySprite, Sprite normalStateSprite, Sprite highlightStateSprite)
		{
			OverlaySprite = overlaySprite;
			NormalStateSprite = normalStateSprite;
			HighlightStateSprite = highlightStateSprite;
		}
	}
}
