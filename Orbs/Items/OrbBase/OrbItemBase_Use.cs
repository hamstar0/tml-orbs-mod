using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using HamstarHelpers.Helpers.Fx;


namespace Orbs.Items.Base {
	public abstract partial class OrbItemBase : ModItem {
		public static bool IsTileWithinUseRange( Player plr, int i, int j ) {
			int diffX = (int)( plr.Center.X * 0.0625f ) - i;
			int diffY = (int)( plr.Center.Y * 0.0625f ) - j;
			int distSqr = ( diffX * diffX ) + ( diffY * diffY );
			return distSqr <= 256;	//16 tiles
		}


		public static bool IsOrbChunkWithinUseRange( Vector2 worldPos, (int i, int j) tileChunkTilePos ) {
			int maxRange = OrbItemBase.MaxTileChunkUseRange;
			var rect = new Rectangle(
				(tileChunkTilePos.i - maxRange) << 4,
				(tileChunkTilePos.j - maxRange) << 4,
				(maxRange * 3) << 4,
				(maxRange * 3) << 4
			);

			return rect.Contains( (int)worldPos.X, (int)worldPos.Y );
		}


		public static OrbColorCode GetNextRandomColorCode( UnifiedRandom rand ) {
			int maxColors = Enum.GetValues( typeof(OrbColorCode) ).Length;
			int tileColorCode = rand.Next( maxColors + 1 );

			if( tileColorCode >= maxColors ) {
				tileColorCode = (int)OrbColorCode.White;
			} else {
				tileColorCode += 1;
			}

			return (OrbColorCode)tileColorCode;
		}


		////////////////

		public static bool CanActivateOrbForTileChunk( int chunkTileX, int chunkTileY ) {
			int maxX = chunkTileX + 16;
			int maxY = chunkTileY + 16;

			for( int y = chunkTileY; y < maxY; y++ ) {
				for( int x = chunkTileX; x < maxX; x++ ) {
					Tile tile = Main.tile[x, y];
					if( tile?.active() != true || tile.inActive() || !OrbsTile.IsTileTypeOrbable(tile.type) ) {
						continue;
					}

					return true;
				}
			}

			return false;
		}


		public static void ActivateOrbUponTileChunk( int chunkTileX, int chunkTileY ) {
			for( int y = chunkTileY; y < chunkTileY + OrbItemBase.MaxTileChunkUseRange; y++ ) {
				for( int x = chunkTileX; x < chunkTileX + OrbItemBase.MaxTileChunkUseRange; x++ ) {
					Tile tile = Main.tile[x, y];
					if( tile?.active() != true || !OrbsTile.IsTileTypeOrbable(tile.type) ) {
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

			Main.PlaySound(
				SoundID.Item70,
				(chunkTileX << 4) + midwayRange,
				(chunkTileY << 4) + midwayRange
			);
		}
	}
}