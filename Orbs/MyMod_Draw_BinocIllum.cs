using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;


namespace Orbs {
	public partial class OrbsMod : Mod {
		private static bool IsEdgeTile( int tileX, int tileY ) {
			if( tileY > 1 ) {
				Tile tile = Main.tile[ tileX, tileY - 1 ];
				if( tile?.active() != true || !Main.tileSolid[tile.type] ) {
					return true;
				}
			}
			if( tileY < (Main.maxTilesY - 1) ) {
				Tile tile = Main.tile[ tileX, tileY + 1 ];
				if( tile?.active() != true || !Main.tileSolid[tile.type] ) {
					return true;
				}
			}
			if( tileX > 1 ) {
				Tile tile = Main.tile[ tileX - 1, tileY ];
				if( tile?.active() != true || !Main.tileSolid[tile.type] ) {
					return true;
				}
			}
			if( tileX < (Main.maxTilesX - 1) ) {
				Tile tile = Main.tile[ tileX + 1, tileY ];
				if( tile?.active() != true || !Main.tileSolid[tile.type] ) {
					return true;
				}
			}
			return false;
		}



		////////////////

		public override void MidUpdateTimeWorld() {
			if( Main.netMode != NetmodeID.Server ) {
				if( !Main.LocalPlayer.HeldItem.IsAir && Main.LocalPlayer.HeldItem.type == ItemID.Binoculars ) {
					this.UpdateBinocsModificationsIf();
				}
			}
		}


		////

		private void UpdateBinocsModificationsIf() {
			int plrTileY = (int)Main.LocalPlayer.Center.Y / 16;
			//if( plrTileY <= WorldLocationLibraries.SurfaceLayerBottomTileY ) {
			//	return;
			//}
			if( plrTileY > WorldLocationLibraries.UnderworldLayerTopTileY ) {
				return;
			}
			
//LogLibraries.LogOnce("2");
			int inc = 1;
			int litInc = 1;

			int width = Main.screenWidth / 16;
			int height = Main.screenHeight / 16;
			int left = (int)Main.screenPosition.X / 16;
			int right = left + width;
			int top = (int)Main.screenPosition.Y / 16;
			int bot = top + height;

			bool[,] isDark = new bool[width, height];

			var edges = new HashSet<(int x, int y)>();
			
//LogLibraries.LogOnce("3");
			// If solid, ignore (also find edge tiles)
			for( int x = left; x < right; x += inc ) {
				for( int y = top; y < bot; y += inc ) {
					Tile tile = Main.tile[x, y];
					if( tile?.active() == true && Main.tileSolid[tile.type] ) {
						isDark[ x-left, y-top ] = true;

						if( OrbsMod.IsEdgeTile( x, y ) ) {
							edges.Add( (x, y) );
						}
					}
				}
			}
			
			int radius = 5;
			int third = radius / 3;
			
//LogLibraries.LogOnce("4");
			// For each edge tile...
			foreach( (int tileX, int tileY) in edges ) {
				int tileAreaLeft = tileX - radius;
				if( tileAreaLeft < left ) {
					tileAreaLeft = left;
				}
				int tileAreaRight = tileX + radius;
				if( tileAreaRight > right ) {
					tileAreaRight = right;
				}
				int tileAreaTop = tileY - radius;
				if( tileAreaTop < top ) {
					tileAreaTop = top;
				}
				int tileAreaBot = tileY + radius;
				if( tileAreaBot > bot ) {
					tileAreaBot = bot;
				}

				int tileAreaLeftThirdX = (tileX - radius) + third;
				if( tileAreaLeftThirdX < left ) {
					tileAreaLeftThirdX = left;
				}
				int tileAreaRightThirdX = (tileX + radius) - third;
				if( tileAreaRightThirdX >= right ) {
					tileAreaRightThirdX = right;
				}
				int tileAreaTopThirdY = (tileY - radius) + third;
				if( tileAreaTopThirdY < top ) {
					tileAreaTopThirdY = top;
				}
				int tileAreaBotThirdY = (tileY + radius) - third;
				if( tileAreaBotThirdY >= bot ) {
					tileAreaBotThirdY = bot;
				}

				// Exclude top, middle tiles
				for( int tileX2=tileAreaLeftThirdX; tileX2<tileAreaRightThirdX; tileX2++ ) {
					for( int tileY2=tileAreaTop; tileY2<tileAreaTopThirdY; tileY2++ ) {
//LogLibraries.LogOnce("4a "+(x2-left)+" ("+width+") "+(y2-top)+" ("+height+")");
						isDark[ tileX2-left, tileY2-top ] = true;
					}
				}
//LogLibraries.LogOnce("4a");

				// Exclude center band of tiles
				for( int tileX2=tileAreaLeft; tileX2<tileAreaRight; tileX2++ ) {
					for( int tileY2=tileAreaTopThirdY; tileY2<tileAreaBotThirdY; tileY2++ ) {
//LogLibraries.LogOnce("4b "+(x2-left)+" ("+width+") "+(y2-top)+" ("+height+")");
						isDark[ tileX2-left, tileY2-top ] = true;
					}
				}
//LogLibraries.LogOnce("4b");
				
				// Exclude bottom, middle tiles
				for( int tileX2=tileAreaLeftThirdX; tileX2<tileAreaRightThirdX; tileX2++ ) {
					for( int tileY2=tileAreaBotThirdY; tileY2<tileAreaBot; tileY2++ ) {
						isDark[ tileX2-left, tileY2-top ] = true;
					}
				}
			}

			var config = OrbsConfig.Instance;
			float lit = config.Get<float>( nameof(config.BinocularsCaveDiscoveryIntensity) ); //0.075f;

//LogLibraries.LogOnce("5 "+lit+" ("+(width*height)+")");
			for( int offsetX=0; offsetX<width; offsetX+=litInc ) {
				for( int y=0; y<height; y+=litInc ) {
					if( !isDark[offsetX, y] ) {
						int litTileX = offsetX + left;
						int litTileY = y + top;

						if( litTileY < WorldLocationLibraries.SurfaceLayerBottomTileY ) {
							if( /*Main.tile[tileX, tileY].wall != 0 &&*/ Lighting.GetBlackness(litTileX, litTileY) == Color.Black ) {
								Lighting.AddLight( litTileX, litTileY, lit, lit, lit );
							}
						} else {
							//Dust.QuickDust( new Point(x, y), Color.Red );
							Lighting.AddLight( litTileX, litTileY, lit, lit, lit );
						}
					}
				}
			}
		}
	}
}
