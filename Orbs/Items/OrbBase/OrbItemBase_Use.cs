using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Fx;


namespace Orbs.Items.Base {
	public abstract partial class OrbItemBase : ModItem {
		public static bool IsTileWithinOrbRange( Player plr, int tileX, int tileY ) {
			return OrbItemBase.IsTileWithinOrbRange( plr.Center, tileX, tileY );
		}
		
		public static bool IsTileWithinOrbRange( Vector2 useSrcWorldPos, int tileX, int tileY ) {
			int diffX = ((int)useSrcWorldPos.X / 16) - tileX;
			int diffY = ((int)useSrcWorldPos.Y / 16) - tileY;
			int distSqr = ( diffX * diffX ) + ( diffY * diffY );

			int chunkTileSize = OrbItemBase.ChunkTileSize;
			return distSqr <= (chunkTileSize * chunkTileSize);
		}


		////

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

		public static bool CanActivateOrbForChunk( int chunkGridX, int chunkGridY ) {
			int chunkTileSize = OrbItemBase.ChunkTileSize;
			int minTileX = chunkGridX * chunkTileSize;
			int minTileY = chunkGridY * chunkTileSize;
			int maxTileX = Math.Min( (chunkGridX + 1) * chunkTileSize, Main.maxTilesX );
			int maxTileY = Math.Min( (chunkGridY + 1) * chunkTileSize, Main.maxTilesY );

			for( int y = minTileY; y < maxTileY; y++ ) {
				for( int x = minTileX; x < maxTileX; x++ ) {
					if( !OrbsTile.IsTileOrbable(x, y) ) {
						continue;
					}

					return true;
				}
			}

			return false;
		}


		public static void ActivateOrbUponTileChunk( int chunkGridX, int chunkGridY ) {
			int chunkTileSize = OrbItemBase.ChunkTileSize;
			int minTileX = chunkGridX * chunkTileSize;
			int minTileY = chunkGridY * chunkTileSize;
			int maxTileX = (chunkGridX + 1) * chunkTileSize;
			int maxTileY = (chunkGridY + 1) * chunkTileSize;

			for( int y = minTileY; y < maxTileY; y++ ) {
				for( int x = minTileX; x < maxTileX; x++ ) {
					Tile tile = Main.tile[x, y];
					if( tile?.active() != true || !OrbsTile.IsTileTypeOrbable(tile.type) ) {
						continue;
					}

					tile.inActive( true );
					WorldGen.SquareTileFrame( x, y );

					ParticleFxHelpers.MakeDustCloud(
						position: new Vector2( (x * 16) + 8, (y * 16) + 8 ),
						quantity: 1,
						sprayAmount: 0.3f,
						scale: 1.2f
					);
				}
			}

			int chunkWorldSize = chunkTileSize * 16;
			int chunkDistWorldMid = chunkWorldSize / 2;

			Main.PlaySound(
				SoundID.Item70,
				(chunkGridX * chunkWorldSize) + chunkDistWorldMid,
				(chunkGridY * chunkWorldSize) + chunkDistWorldMid
			);
		}
	}
}