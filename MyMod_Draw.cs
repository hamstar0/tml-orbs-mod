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
		private Color BaseBgColor;



		////////////////

		public UIMapButton() : base() {
			this.BaseBgColor = this.BackgroundColor;
		}


		////

		public CalculatedStyle GetMyDimensions() {
			return new CalculatedStyle(
				16f,//this.Left.Pixels,
				16f,//this.Top.Pixels,
				168f,//this.Width.Pixels,
				32f//this.Height.Pixels
			);
		}


		////

		public override void Draw( SpriteBatch sb ) {
			var mydim = this.GetMyDimensions();

			ReflectionHelpers.Set( typeof( UIElement ), this, "_dimensions", mydim );
			this.DrawSelf( sb );

			var textPos = new Vector2( mydim.X + 12, mydim.Y + 4 );
			sb.DrawString( Main.fontMouseText, "Toggle Orb Colors", textPos, Color.White );

			bool isHovering = false;

			if( Main.mouseX >= mydim.X && Main.mouseX < ( mydim.X + mydim.Width ) ) {
				if( Main.mouseY >= mydim.Y && Main.mouseY < ( mydim.Y + mydim.Height ) ) {
					isHovering = true;

					if( Main.mouseLeft && Main.mouseLeftRelease ) {
						this.Click( null );
					}
				}
			}

			if( isHovering ) {
				this.BackgroundColor = this.BaseBgColor * 1.2f;
			} else {
				this.BackgroundColor = this.BaseBgColor;
			}
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

			(int x, int y, bool isOnScreen) topLeftTile = HUDMapHelpers.FindTopLeftTileOfFullscreenMap();
//Main.spriteBatch.DrawString( Main.fontMouseText, "top left: "+topLeft.x+","+topLeft.y, new Vector2(16,400), Color.White );
			int minX = Math.Max( topLeftTile.x, 0 );
			int minY = Math.Max( topLeftTile.y, 0 );
			minX = (minX >> 4) << 4;
			minY = (minY >> 4) << 4;

			for( int tileY = minY; tileY < Main.maxTilesY; tileY += 16 ) {
				bool rowIsInBounds = false;
				bool colIsInBounds = false;

				for( int tileX = minX; tileX < Main.maxTilesX; tileX += 16 ) {
					var mapPos = HUDMapHelpers.GetFullMapPositionAsScreenPosition( new Vector2( tileX << 4, tileY << 4 ) );
					if( !mapPos.IsOnScreen ) {
						if( !rowIsInBounds ) {
							continue;
						} else {
							break;
						}
					}

					rowIsInBounds = true;

					this.DrawMapChunk( tileX, tileY, mapPos.ScreenPosition );
				}

				if( rowIsInBounds ) {
					colIsInBounds = true;
				} else if( colIsInBounds ) {
					break;
				}
			}
		}
		/*public override void PostDrawFullscreenMap( ref string mouseText ) {
			this.MapOverlayButton.Draw( Main.spriteBatch );

			if( !this.IsMapOverlayOn ) {
				return;
			}

			(int x, int y, bool isOnScreen) topLeftTile = HUDMapHelpers.FindTopLeftTileOfFullscreenMap();
//Main.spriteBatch.DrawString( Main.fontMouseText, "top left: "+topLeft.x+","+topLeft.y, new Vector2(16,400), Color.White );
			topLeftTile.x = Math.Max( (topLeftTile.x / 16) * 16, 0 );
			topLeftTile.y = Math.Max( (topLeftTile.y / 16) * 16, 0 );
			int maxX = Main.maxTilesX;
			int maxY = Main.maxTilesY;

			bool foundInBoundsY = false;

			for( int tileY = topLeftTile.y; tileY < maxY; tileY += 16 ) {
				bool foundInBoundsX = false;

				for( int tileX = topLeftTile.x; tileX < maxX; tileX += 16 ) {
					var mapPos = HUDMapHelpers.GetFullMapPositionAsScreenPosition( new Vector2(tileX<<4, tileY<<4) );

					if( foundInBoundsX ) {
						if( !mapPos.IsOnScreen ) {
							maxX = tileX;
							break;
						}
					}

					if( mapPos.IsOnScreen ) {
						foundInBoundsX = true;
						foundInBoundsY = true;
						this.DrawMapChunk( tileX, tileY, mapPos.ScreenPosition );
					}
				}

				if( foundInBoundsY && !foundInBoundsX ) {
					break;
				}
			}
		}*/


		////

		private bool DrawMapChunk( int tileX, int tileY, Vector2 screenPos ) {
			if( tileX < 0 || tileY < 0 || tileX >= Main.maxTilesX || tileY >= Main.maxTilesY ) {
				return false;
			}

			int chunkTileX = ( (tileX>>4) << 4 );
			int chunkTileY = ( (tileY>>4) << 4 );

			if( !OrbItemBase.CanActivateOrb( chunkTileX, chunkTileY ) ) {
				return false;
			}

			if( !OrbsConfig.Instance.DebugModeTheColorsDuke && !Main.Map.IsRevealed(chunkTileX + 8, chunkTileY + 8) ) {
				return false;
			}

			var orbWld = ModContent.GetInstance<OrbsWorld>();
			OrbColorCode colorCode = orbWld.GetTileColorCode( tileX, tileY );
			if( colorCode == 0 ) {
				return false;
			}

			float scale = HUDMapHelpers.GetFullMapScale();
			Color color = OrbItemBase.ColorValues[colorCode];

			Main.spriteBatch.Draw(
				texture: Main.magicPixel,
				destinationRectangle: new Rectangle(
					x: (int)screenPos.X,
					y: (int)screenPos.Y,
					width: (int)(16f * scale),
					height: (int)(16f * scale)
				),
				color: color * 0.5f
			);
			return true;
		}
	}
}