using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Helpers.Draw;
using HamstarHelpers.Helpers.HUD;
using Orbs.Items.Base;
using Orbs.UI;


namespace Orbs {
	public partial class OrbsMod : Mod {
		public static bool IsNearer( Vector2 source, Vector2 nearerThan, Vector2 lastKnownNearest ) {
			return (source - nearerThan).LengthSquared() < (source - lastKnownNearest).LengthSquared();
		}



		////////////////

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

			if( this.IsMapOverlayOn ) {
				this.DrawAllMapChunks();
			}
		}


		////////////////

		private void DrawAllMapChunks() {
			int chunkSize = OrbItemBase.ChunkTileSize;
			(int x, int y, bool isOnScreen) topLeftTile = HUDMapHelpers.FindTopLeftTileOfFullscreenMap();
//Main.spriteBatch.DrawString( Main.fontMouseText, "top left: "+topLeft.x+","+topLeft.y, new Vector2(16,400), Color.White );
			int minTileX = Math.Max( topLeftTile.x, 0 );
			int minTileY = Math.Max( topLeftTile.y, 0 );
			minTileX = (minTileX / chunkSize) * chunkSize;
			minTileY = (minTileY / chunkSize) * chunkSize;

			this.DrawAllMapChunksWithin( chunkSize, minTileX, minTileY );
		}

		private void DrawAllMapChunksWithin( int chunkSize, int minTileX, int minTileY) {
			int drawnChunks = 0, skippedChunks = 0;
			var avgSkippedScrPos = default( Vector2 );
			var minSkippedScrPos = new Vector2( float.MaxValue, float.MaxValue );
			var maxSkippedScrPos = new Vector2( float.MinValue, float.MinValue );

			((int x, int y) tilePos, Vector2 scrPos) closestChunkToCursor = default;

			bool rowIsInBounds = false;
			bool colIsInBounds = false;

			for( int tileY = minTileY; tileY < Main.maxTilesY; tileY += chunkSize ) {
				rowIsInBounds = false;

				for( int tileX = minTileX; tileX < Main.maxTilesX; tileX += chunkSize ) {
					var wldPos = new Vector2( tileX * 16, tileY * 16 );
					(Vector2 scrPos, bool isOnScreen) mapScrPos = HUDMapHelpers.GetFullMapPositionAsScreenPosition( wldPos );

					if( !mapScrPos.isOnScreen ) {
						if( OrbsConfig.Instance.DebugModeInfo ) {
							avgSkippedScrPos += mapScrPos.scrPos;
							if( mapScrPos.scrPos.X <= minSkippedScrPos.X && mapScrPos.scrPos.Y <= minSkippedScrPos.Y ) {
								minSkippedScrPos = mapScrPos.scrPos;
							} else if( mapScrPos.scrPos.X >= maxSkippedScrPos.X && mapScrPos.scrPos.Y >= maxSkippedScrPos.Y ) {
								maxSkippedScrPos = mapScrPos.scrPos;
							}

							skippedChunks++;
						}

						if( !rowIsInBounds ) {
							continue;	// Skip until map is on screen
						} else {
							break;	// We're now offscreen
						}
					}

					rowIsInBounds = true;

					if( OrbsMod.IsNearer(Main.MouseScreen, mapScrPos.scrPos, closestChunkToCursor.scrPos) ) {
						closestChunkToCursor = (
							tilePos: (tileX, tileY),
							scrPos: mapScrPos.scrPos
						);
					}

					if( this.DrawMapChunk(tileX, tileY, mapScrPos.scrPos, false) ) {
						drawnChunks++;
					}
				}

				if( rowIsInBounds ) {
					colIsInBounds = true;
				} else if( colIsInBounds ) {
					break;
				}
			}

			this.DrawMapChunk(
				tileX: closestChunkToCursor.tilePos.x,
				tileY: closestChunkToCursor.tilePos.y,
				screenPos: closestChunkToCursor.scrPos,
				isHighlighted: true
			);
			
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


		////////////////

		private bool DrawMapChunk( int tileX, int tileY, Vector2 screenPos, bool isHighlighted ) {
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
			var rect = new Rectangle(
				x: (int)screenPos.X,
				y: (int)screenPos.Y,
				width: (int)( (float)chunkSize * scale ),
				height: (int)( (float)chunkSize * scale )
			);
			Color color = OrbItemBase.ColorValues[ colorCode ];

			Main.spriteBatch.Draw(
				texture: Main.magicPixel,
				destinationRectangle: rect,
				color: color * 0.2f
			);

			if( isHighlighted ) {
				float pulse = (float)Main.mouseTextColor / 255f;

				DrawHelpers.DrawBorderedRect(
					sb: Main.spriteBatch,
					bgColor: color * 0.2f,
					borderColor: Color.White * pulse * 0.75f,
					rect: rect,
					borderWidth: 2
				);

				Utils.DrawBorderStringFourWay(
					sb: Main.spriteBatch,
					font: Main.fontMouseText,
					text: colorCode.ToString(),
					x: Main.MouseScreen.X + 12,
					y: Main.MouseScreen.Y + 16,
					textColor: color * pulse,
					borderColor: Color.Black * pulse,
					origin: default,
					scale: 1f
				);
			}

			return true;
		}
	}
}