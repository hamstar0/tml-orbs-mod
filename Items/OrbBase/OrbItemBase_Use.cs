using HamstarHelpers.Helpers.Fx;
using HamstarHelpers.Helpers.Tiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Orbs.Items.Base {
	public abstract partial class OrbItemBase : ModItem {
		public static bool IsTileWithinUseRange( int i, int j ) {
			Player plr = Main.LocalPlayer;

			int diffX = (int)( plr.Center.X * 0.0625 ) - i;
			int diffY = (int)( plr.Center.Y * 0.0625 ) - j;
			int distSqr = ( diffX * diffX ) + ( diffY * diffY );
			return distSqr <= 256;
		}


		public static bool IsTileChunkWithinUseRange( Vector2 worldPos, (int i, int j) tileChunkPos ) {
			int maxRange = OrbItemBase.MaxTileChunkUseRange;
			var rect = new Rectangle(
				( tileChunkPos.i - maxRange ) << 4,
				( tileChunkPos.j - maxRange ) << 4,
				( maxRange << 4 ) * 3,
				( maxRange << 4 ) * 3
			);

			return rect.Contains( (int)worldPos.X, (int)worldPos.Y );
		}


		public static OrbColorCode GetRandomColorCode( int randSeed ) {
			var rand = new Random( randSeed );
			int maxColors = Enum.GetValues( typeof( OrbColorCode ) ).Length;

			int tileColorCode = rand.Next( maxColors + 1 );

			if( tileColorCode >= maxColors ) {
				tileColorCode = (int)OrbColorCode.White;
			} else {
				tileColorCode += 1;
			}

			return (OrbColorCode)tileColorCode;
		}


		////////////////

		public static bool CanActivateOrb() {
			int minX = OrbsTile.CurrentTargetTileChunk.Value.X;
			int minY = OrbsTile.CurrentTargetTileChunk.Value.Y;

			for( int y = minY; y < minY + OrbItemBase.MaxTileChunkUseRange; y++ ) {
				for( int x = minX; x < minX + OrbItemBase.MaxTileChunkUseRange; x++ ) {
					Tile tile = Main.tile[x, y];
					if( tile?.active() != true || !OrbsTile.IsOrbTileType(tile.type) ) {
						continue;
					}

					return true;
				}
			}

			return false;
		}


		public static void ActivateOrb( int chunkTileX, int chunkTileY ) {
			for( int y = chunkTileY; y < chunkTileY + OrbItemBase.MaxTileChunkUseRange; y++ ) {
				for( int x = chunkTileX; x < chunkTileX + OrbItemBase.MaxTileChunkUseRange; x++ ) {
					Tile tile = Main.tile[x, y];
					if( tile?.active() != true || !OrbsTile.IsOrbTileType(tile.type) ) {
						continue;
					}

					tile.inActive( true );
					WorldGen.SquareTileFrame( x, y );

					ParticleFxHelpers.MakeDustCloud(
						position: new Vector2( ( x * 16 ) + 8, ( y * 16 ) + 8 ),
						quantity: 1,
						sprayAmount: 0.3f,
						scale: 1.2f
					);
				}
			}

			int midwayRange = (OrbItemBase.MaxTileChunkUseRange * 16) / 2;

			Main.PlaySound( SoundID.Item70, (chunkTileX << 4) + midwayRange, (chunkTileY << 4) + midwayRange );
		}
	}
}