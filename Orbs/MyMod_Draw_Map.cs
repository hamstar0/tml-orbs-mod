using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Helpers.HUD;
using Orbs.Items.Base;
using Orbs.UI;


namespace Orbs {
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

			int chunkSize = OrbItemBase.ChunkTileSize;
			(int x, int y, bool isOnScreen) topLeftTile = HUDMapHelpers.FindTopLeftTileOfFullscreenMap();
//Main.spriteBatch.DrawString( Main.fontMouseText, "top left: "+topLeft.x+","+topLeft.y, new Vector2(16,400), Color.White );
			int minTileX = Math.Max( topLeftTile.x, 0 );
			int minTileY = Math.Max( topLeftTile.y, 0 );
			minTileX = (minTileX / chunkSize) * chunkSize;
			minTileY = (minTileY / chunkSize) * chunkSize;

			int drawnChunks = 0, skippedChunks = 0;
			var avgSkippedScrPos = default( Vector2 );
			var minSkippedScrPos = new Vector2( float.MaxValue, float.MaxValue );
			var maxSkippedScrPos = new Vector2( float.MinValue, float.MinValue );

			for( int tileY = minTileY; tileY < Main.maxTilesY; tileY += chunkSize ) {
				bool rowIsInBounds = false;
				bool colIsInBounds = false;

				for( int tileX = minTileX; tileX < Main.maxTilesX; tileX += chunkSize ) {
					var wldPos = new Vector2( tileX * 16, tileY * 16 );
					(Vector2 scrPos, bool isOnScreen) mapPos = HUDMapHelpers.GetFullMapPositionAsScreenPosition( wldPos );

					if( !mapPos.isOnScreen ) {
						if( OrbsConfig.Instance.DebugModeInfo ) {
							avgSkippedScrPos += mapPos.scrPos;
							if( mapPos.scrPos.X <= minSkippedScrPos.X && mapPos.scrPos.Y <= minSkippedScrPos.Y ) {
								minSkippedScrPos = mapPos.scrPos;
							} else if( mapPos.scrPos.X >= maxSkippedScrPos.X && mapPos.scrPos.Y >= maxSkippedScrPos.Y ) {
								maxSkippedScrPos = mapPos.scrPos;
							}

							skippedChunks++;
						}

						if( !rowIsInBounds ) {
							continue;
						} else {
							break;
						}
					}

					rowIsInBounds = true;

					if( this.DrawMapChunk(tileX, tileY, mapPos.scrPos) ) {
						drawnChunks++;
					}
				}

				if( rowIsInBounds ) {
					colIsInBounds = true;
				} else if( colIsInBounds ) {
					break;
				}
			}

			if( OrbsConfig.Instance.DebugModeInfo ) {
				DebugHelpers.Print(
					"FullscreenMapChunksDrawn", "Drawn: "+drawnChunks
					+", skipped: "+skippedChunks
					+", avg skip pos: "+(avgSkippedScrPos / skippedChunks).ToShortString()
					+", min skip pos: "+ minSkippedScrPos.ToShortString()
					+", max skip pos: "+ maxSkippedScrPos.ToShortString()
				);
			}
		}


		////

		private bool DrawMapChunk( int tileX, int tileY, Vector2 screenPos ) {
			if( tileX < 0 || tileY < 0 || tileX >= Main.maxTilesX || tileY >= Main.maxTilesY ) {
				return false;
			}

			int chunkSize = OrbItemBase.ChunkTileSize;
			int halfChunkSize = OrbItemBase.ChunkTileSize / 2;
			int chunkTileX = (tileX/chunkSize) * chunkSize;
			int chunkTileY = (tileY/chunkSize) * chunkSize;

			if( !OrbItemBase.CanActivateOrbForChunk(chunkTileX/chunkSize, chunkTileY/chunkSize) ) {
				return false;
			}
			
			if( !OrbsConfig.Instance.DebugModeTheColorsDuke ) {
				if( !Main.Map.IsRevealed(chunkTileX + halfChunkSize, chunkTileY + halfChunkSize) ) {
					return false;
				}
			}

			var orbWld = ModContent.GetInstance<OrbsWorld>();
			OrbColorCode colorCode = orbWld.GetColorCodeOfOrbChunkOfTile( tileX, tileY );
			if( colorCode == 0 ) {
				return false;
			}

			float scale = HUDMapHelpers.GetFullMapScale();
			Color color = OrbItemBase.ColorValues[ colorCode ];

			Main.spriteBatch.Draw(
				texture: Main.magicPixel,
				destinationRectangle: new Rectangle(
					x: (int)screenPos.X,
					y: (int)screenPos.Y,
					width: (int)((float)chunkSize * scale),
					height: (int)((float)chunkSize * scale)
				),
				color: color * 0.15f
			);
			return true;
		}
	}
}