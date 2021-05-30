using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;


namespace Orbs {
	partial class OrbsWorld : ModWorld {
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

		public override void PreUpdate() {
//LogLibraries.LogOnce("1");
			if( Main.LocalPlayer.HeldItem.IsAir || Main.LocalPlayer.HeldItem.type != ItemID.Binoculars ) {
				return;
			}

			int plrTileY = (int)Main.LocalPlayer.Center.Y / 16;
			if( plrTileY <= WorldLibraries.SurfaceLayerBottomTileY ) {
				return;
			}
			if( plrTileY > WorldLibraries.UnderworldLayerTopTileY ) {
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

			bool[,] darks = new bool[width, height];

			var edges = new HashSet<(int x, int y)>();
			
//LogLibraries.LogOnce("3");
			for( int x = left; x < right; x += inc ) {
				for( int y = top; y < bot; y += inc ) {
					Tile tile = Main.tile[x, y];
					if( tile?.active() == true && Main.tileSolid[tile.type] ) {
						darks[x-left, y-top] = true;

						if( OrbsWorld.IsEdgeTile( x, y ) ) {
							edges.Add( (x, y) );
						}
					}
				}
			}
			
			int radius = 6;
			int third = (2 * radius) / 3;
			
//LogLibraries.LogOnce("4");
			foreach( (int x, int y) in edges ) {
				int inLeft = x - radius;
				if( inLeft < left ) {
					inLeft = left;
				}
				int inRight = x + radius;
				if( inRight >= right ) {
					inRight = right;
				}
				int inTop = y - radius;
				if( inTop < top ) {
					inTop = top;
				}
				int inBot = y + radius;
				if( inBot >= bot ) {
					inBot = bot;
				}

				int inOneThirdsX = (x - radius) + third;
				if( inOneThirdsX < left ) {
					inOneThirdsX = left;
				}
				int inTwoThirdsX = (x + radius) - third;
				if( inTwoThirdsX >= right ) {
					inTwoThirdsX = right;
				}
				int inOneThirdsY = (y - radius) + third;
				if( inOneThirdsY < top ) {
					inOneThirdsY = top;
				}

				for( int x2=inOneThirdsX; x2<inTwoThirdsX; x2++ ) {
					for( int y2=inTop; y2<inOneThirdsY; y2++ ) {
//LogLibraries.LogOnce("4a "+(i-left)+" ("+width+") "+(j-top)+" ("+height+")");
						darks[x2-left, y2-top] = true;
					}
				}
//LogLibraries.LogOnce("4a");

				int inTwoThirdsY = (y - radius) + third + third;
				if( inTwoThirdsY >= bot ) {
					inTwoThirdsY = bot;
				}

				for( int x2=inLeft; x2<inRight; x2++ ) {
					for( int y2=inOneThirdsY; y2<inTwoThirdsY; y2++ ) {
//LogLibraries.LogOnce("4b "+(i-left)+" ("+width+") "+(j-top)+" ("+height+")");
						darks[x2-left, y2-top] = true;
					}
				}
//LogLibraries.LogOnce("4b");

				for( int x2=inOneThirdsX; x2<inTwoThirdsX; x2++ ) {
					for( int y2=inTwoThirdsY; y2<inBot; y2++ ) {
						darks[x2-left, y2-top] = true;
					}
				}
			}

			var config = OrbsConfig.Instance;
			float lit = config.Get<float>( nameof(config.BinocularsCaveDiscoveryIntensity) ); //0.075f;

//LogLibraries.LogOnce("5 "+darkCount+" ("+(width*height)+")");
			for( int x=left; x<right; x+=litInc ) {
				for( int y=top; y<bot; y+=litInc ) {
					if( !darks[x-left, y-top] ) {
//Dust.QuickDust( new Point(x, y), Color.Red );
						Lighting.AddLight( x, y, lit, lit, lit );
					}
				}
			}
		}
	}
}
