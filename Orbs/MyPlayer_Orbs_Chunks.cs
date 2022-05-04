using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Orbs.Items.Base;


namespace Orbs {
	partial class OrbsPlayer : ModPlayer {
		public static bool CanViewAllOrbChunks( Player player ) {
			if( OrbsConfig.Instance.Get<bool>( nameof(OrbsConfig.DebugModeTheColorsDuke) ) ) {
				return true;
			}

			Item heldItem = player.HeldItem;
			return heldItem?.active == true && heldItem.type == ItemID.Binoculars;
		}

		public static bool CanPlayerOrbTargetAnyChunk( Player player ) {
			Item heldItem = player.HeldItem;
			OrbItemBase myorb = heldItem?.modItem as OrbItemBase;
			if( myorb == null ) {
				return false;
			}

			OrbColorCode plrColorCode = myorb?.ColorCode ?? (OrbColorCode)0;
			return plrColorCode != 0;
		}


		////////////////

		public static bool IsPlayerOrbTargettingChunkForGivenTile( Player player, int tileX, int tileY ) {
			if( !OrbsTile.IsTileOrbable(tileX, tileY) ) {
				return false;
			}

			var orbWld = ModContent.GetInstance<OrbsWorld>();
			OrbColorCode tileColorCode = orbWld.GetColorCodeOfOrbChunkOfTile( tileX, tileY );
			if( tileColorCode == 0 ) {
				return false;
			}

			Item heldItem = player.HeldItem;
			OrbItemBase myorb = heldItem?.modItem as OrbItemBase;
			if( myorb == null ) {
				return false;
			}

			OrbColorCode plrColorCode = myorb?.ColorCode ?? (OrbColorCode)0;
			bool canSeeColor = tileColorCode == plrColorCode;// || canSeeAllColors;
			if( !canSeeColor ) {
				return false;
			}

/*var myplayer = player.GetModPlayer<OrbsPlayer>();
DebugLibraries.Print( "orb",
	"i:"+tileX+", j:"+tileY
	+", plr:"+(player.Center/16).ToShortString()
	+", dist:"+((player.Center/16)-(new Vector2(tileX, tileY))).Length().ToString("N2")
	+" - "+tileColorCode.ToString()
	+", match?"+(tileColorCode == plrColorCode)
	+", within?"+(OrbItemBase.IsTileWithinUseRange(player, tileX, tileY)) );*/

			return OrbItemBase.IsTileWithinOrbRange( player, tileX, tileY );
		}



		////////////////

		private void UpdateNearbyOrbChunkTarget_Local() {
			if( this.player.whoAmI != Main.myPlayer ) {
				return;
			}

			//

			this.CurrentTargettedOrbableChunkGridPosition = this.FindNearbyOrbChunkTarget_If_Local(
				out Vector2 preferredFindWorldPos
			);

			this.CurrentNearbyChunkTypes = this.FindNearbyOrbChunkTypes( preferredFindWorldPos );
//DebugLibraries.Print( "chunks", string.Join(", ", this.CurrentNearbyChunkTypes) );
		}


		////////////////

		private (int ChunkGridX, int ChunkGridY)? FindNearbyOrbChunkTarget_If_Local(
					out Vector2 preferredFindWorldPos ) {
			if( this.player.whoAmI != Main.myPlayer ) {
				preferredFindWorldPos = default;
				return null;
			}
			if( !OrbsPlayer.CanPlayerOrbTargetAnyChunk(this.player) ) {
				preferredFindWorldPos = default;
				return null;
			}

			//

			Vector2 nearMouseOffset = Main.MouseWorld - this.player.MountedCenter;

			float maxChunkCheckDist = 12 * 16;

			if( nearMouseOffset.LengthSquared() > (maxChunkCheckDist * maxChunkCheckDist) ) {
				nearMouseOffset = Vector2.Normalize(nearMouseOffset) * maxChunkCheckDist;
			}

			preferredFindWorldPos = this.player.MountedCenter + nearMouseOffset;

			//

			//int tileX = (int)this.player.Center.X / 16;
			//int tileY = (int)this.player.Center.Y / 16;
			int tileX = (int)preferredFindWorldPos.X / 16;
			int tileY = (int)preferredFindWorldPos.Y / 16;
			int chunkTileSize = OrbItemBase.ChunkTileSize;

			(int, int)? chunk;

			for( int j = 0; j < chunkTileSize; j++ ) {
				for( int i = 0; i < chunkTileSize; i++ ) {
					chunk = this.GetTargetOrbChunk( tileX + i, tileY + j );
					if( chunk.HasValue ) {
						return chunk;
					}

					chunk = this.GetTargetOrbChunk( tileX - i, tileY - j );
					if( chunk.HasValue ) {
						return chunk;
					}
				}
			}

			return null;
		}


		////////////////

		private ISet<OrbColorCode> FindNearbyOrbChunkTypes( Vector2 preferredFindWorldPos ) {
			var chunks = new HashSet<OrbColorCode>();

			var orbWld = ModContent.GetInstance<OrbsWorld>();
			int chunkTileSize = OrbItemBase.ChunkTileSize;
			int scanChunkRadius = 1;

			int prefferedTileX = (int)preferredFindWorldPos.X / 16;
			int prefferedTileY = (int)preferredFindWorldPos.Y / 16;
			int scanRadius = scanChunkRadius * chunkTileSize;
			int scanRadiusSqr = scanRadius * scanRadius;

			int minX = (prefferedTileX / chunkTileSize) - scanChunkRadius;
			int minY = (prefferedTileY / chunkTileSize) - scanChunkRadius;
			int maxX = (prefferedTileX / chunkTileSize) + scanChunkRadius;
			int maxY = (prefferedTileY / chunkTileSize) + scanChunkRadius;
			minX *= chunkTileSize;
			minY *= chunkTileSize;
			maxX *= chunkTileSize;
			maxY *= chunkTileSize;

//int i=0;
			for( int tileY = minY; tileY <= maxY; tileY += chunkTileSize ) {
				if( tileY < 0 ) {
					continue;
				}
				if( tileY >= Main.maxTilesY ) {
					break;
				}

				//

				int diffY = tileY - prefferedTileY;

				for( int tileX = minX; tileX <= maxX; tileX += chunkTileSize ) {
					if( tileX < 0 ) {
						continue;
					}
					if( tileX >= Main.maxTilesX ) {
						break;
					}

					//

					int diffX = tileX - prefferedTileX;

					int distSqr = (diffX * diffX) + (diffY * diffY);
					if( distSqr > scanRadiusSqr ) {
						continue;
					}

					//

					OrbColorCode tileColorCode = orbWld.GetColorCodeOfOrbChunkOfTile( tileX, tileY );

					if( tileColorCode != 0 ) {
						chunks.Add( tileColorCode );
					}
//i++;
				}
			}
//DebugLibraries.Print( "chunks", "i: "+i+", c:"+chunks.Count+", chunks: "+string.Join(", ", chunks) );

			return chunks;
		}


		////////////////

		/// <summary>
		/// Targets a given orb chunk of a given tile, if the player can target it.
		/// </summary>
		/// <param name="tileX"></param>
		/// <param name="tileY"></param>
		public (int ChunkGridX, int ChunkGridY)? GetTargetOrbChunk( int tileX, int tileY ) {
			if( OrbsPlayer.IsPlayerOrbTargettingChunkForGivenTile(this.player, tileX, tileY) ) {
				return OrbItemBase.GetChunk( tileX, tileY );
			}
			return null;
		}

		////////////////

		internal void ClearTargetOrbChunk() {
			this.CurrentTargettedOrbableChunkGridPosition = null;
		}
	}
}
