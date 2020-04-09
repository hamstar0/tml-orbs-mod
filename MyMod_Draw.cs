using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Reflection;
using HamstarHelpers.Helpers.HUD;
using Orbs.Items.Base;


namespace Orbs {
	class UIMapButton : UIPanel {
		public CalculatedStyle GetMyDimensions() {
			return new CalculatedStyle(
				16f,//this.Left.Pixels,
				16f,//this.Top.Pixels,
				128f,//this.Width.Pixels,
				32f//this.Height.Pixels
			);
		}


		public override void Draw( SpriteBatch sb ) {
			var mydim = this.GetMyDimensions();

			ReflectionHelpers.Set( typeof( UIElement ), this, "_dimensions", mydim );
			this.DrawSelf( sb );

			var textPos = new Vector2( mydim.X, mydim.Y );
			sb.DrawString( Main.fontMouseText, "Toggle Orb Colors", textPos, Color.White );
		}
	}




	public partial class OrbsMod : Mod {
		private UIMapButton MapOverlayButton;



		////////////////

		private void InitializeMapUI() {
			this.MapOverlayButton = new UIMapButton();
			this.MapOverlayButton.OnClick += ( _, __ ) => {
				this.IsMapOverlayOn = !this.IsMapOverlayOn;
			};
		}


		////////////////

		public override void PostDrawFullscreenMap( ref string mouseText ) {
			this.MapOverlayButton.Draw( Main.spriteBatch );

			if( !this.IsMapOverlayOn ) {
				return;
			}

			(int x, int y) topLeft = HUDMapHelpers.FindTopLeftTileOfFullscreenMap();
			topLeft.x = (topLeft.x >> 4) << 4;
			topLeft.y = (topLeft.y >> 4) << 4;
			int maxX = Main.maxTilesX;
			int maxY = Main.maxTilesY;

			for( int y = topLeft.y; y < maxY; y += 16 ) {
				for( int x = topLeft.x; x < maxX; x += 16 ) {
					var mapPos = HUDMapHelpers.GetFullMapPositionAsScreenPosition( new Vector2( x << 4, y << 4 ) );

					if( maxX == Main.maxTilesX && !mapPos.IsOnScreen ) {
						maxX = (int)mapPos.ScreenPosition.X;
						break;
					}

					this.DrawMapChunk( x, y, mapPos.ScreenPosition );
				}
			}
		}


		////

		private void DrawMapChunk( int tileX, int tileY, Vector2 screenPos ) {
			OrbColorCode colorCode = OrbsTile.GetTileColorCode( tileX, tileY );
			if( colorCode == 0 ) {
				return;
			}

			float scale = HUDMapHelpers.GetFullMapScale();
			Color color = OrbItemBase.ColorValues[colorCode];

			Main.spriteBatch.Draw(
				texture: Main.magicPixel,
				position: screenPos,
				sourceRectangle: null,
				color: color * 0.5f,
				rotation: 0f,
				origin: new Vector2( 16f * scale ),
				scale: scale,
				effects: SpriteEffects.None,
				layerDepth: 1f
			);
		}
	}
}