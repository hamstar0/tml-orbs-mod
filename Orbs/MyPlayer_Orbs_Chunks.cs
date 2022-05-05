using System;
using System.Collections.Generic;
using System.Linq;
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

		public static bool CanPlayerOrbTargetAnyChunk_Local( Player player ) {
			Item heldItem = player.HeldItem;
			OrbItemBase myorb = heldItem?.modItem as OrbItemBase;
			if( myorb == null ) {
				return false;
			}

			//
			
			if( player.selectedItem == 58 ) {
				if( Main.mouseItem?.active != true || !(Main.mouseItem.modItem is OrbItemBase) ) {
					return false;
				}
			}

			//

			OrbColorCode plrColorCode = myorb?.ColorCode ?? (OrbColorCode)0;
			return plrColorCode != 0;
		}


		////////////////

		public static Item ChoseOrbItemToTargetChunkForGivenTile_If(
					Player player,
					int tileX,
					int tileY,
					IEnumerable<Item> availableOrbs ) {
			if( !OrbsTile.IsTileOrbable(tileX, tileY) ) {
				return null;
			}

			var orbWld = ModContent.GetInstance<OrbsWorld>();
			OrbColorCode tileColorCode = orbWld.GetColorCodeOfOrbChunkOfTile( tileX, tileY );
			if( tileColorCode == 0 ) {
				return null;
			}

			//

			if( !OrbItemBase.IsTileWithinOrbRange(player.MountedCenter, tileX, tileY) ) {
				return null;
			}

			//

			Item chosenOrb = null;

			foreach( Item orb in availableOrbs ) {
				var myorb = orb.modItem as OrbItemBase;

				if( tileColorCode == myorb.ColorCode ) {
					chosenOrb = orb;

					break;
				}
			}

			return chosenOrb;
/*var myplayer = player.GetModPlayer<OrbsPlayer>();
DebugLibraries.Print( "orb",
	"i:"+tileX+", j:"+tileY
	+", plr:"+(player.Center/16).ToShortString()
	+", dist:"+((player.Center/16)-(new Vector2(tileX, tileY))).Length().ToString("N2")
	+" - "+tileColorCode.ToString()
	+", match?"+(tileColorCode == plrColorCode)
	+", within?"+(OrbItemBase.IsTileWithinUseRange(player, tileX, tileY)) );*/
		}



		////////////////

		private void UpdateNearbyOrbChunkTarget_Local() {
			if( this.player.whoAmI != Main.myPlayer ) {
				return;
			}

			//

			Item chosenOrbItem;

			this.CurrentTargettedOrbableChunkGridPosition = this.FindNearbyOrbChunkTarget_If_Local( out chosenOrbItem );

			this.CurrentNearbyChunkTypes = this.FindNearbyOrbChunkTypes( this.player.MountedCenter );

			//

			if( this.CurrentTargettedOrbableChunkGridPosition.HasValue && chosenOrbItem != null ) {
				int idx = Array.FindIndex( this.player.inventory, i => i == chosenOrbItem );

				if( idx != -1 && idx != this.player.selectedItem ) {
					Utils.Swap( ref this.player.inventory[this.player.selectedItem], ref this.player.inventory[idx] );
					Main.mouseItem = this.player.HeldItem.Clone();
				}
			}
		}


		////////////////

		private (int ChunkGridX, int ChunkGridY)? FindNearbyOrbChunkTarget_If_Local( out Item chosenOrbItem ) {
			if( this.player.whoAmI != Main.myPlayer ) {
				chosenOrbItem = null;
				return null;
			}

			if( !OrbsPlayer.CanPlayerOrbTargetAnyChunk_Local(this.player) ) {
				chosenOrbItem = null;
				return null;
			}

			//

			Vector2 nearMouseOffset = Main.MouseWorld - this.player.MountedCenter;

			float maxChunkCheckDist = 12 * 16;

			if( nearMouseOffset.LengthSquared() > (maxChunkCheckDist * maxChunkCheckDist) ) {
				nearMouseOffset = Vector2.Normalize(nearMouseOffset) * maxChunkCheckDist;
			}

			Vector2 preferredFindWorldPos = this.player.MountedCenter + nearMouseOffset;

			//

			IEnumerable<Item> availableOrbs = this.player.inventory
				.Where( i => i?.active == true && i.modItem is OrbItemBase );

			//int tileX = (int)this.player.Center.X / 16;
			//int tileY = (int)this.player.Center.Y / 16;
			int tileX = (int)preferredFindWorldPos.X / 16;
			int tileY = (int)preferredFindWorldPos.Y / 16;
			int chunkTileSize = OrbItemBase.ChunkTileSize;

			(int, int)? chunk;

			for( int j = 0; j < chunkTileSize; j++ ) {
				for( int i = 0; i < chunkTileSize; i++ ) {
					chunk = this.GetTargetOrbChunk( tileX + i, tileY + j, availableOrbs, out chosenOrbItem );
					if( chunk.HasValue ) {
						return chunk;
					}

					chunk = this.GetTargetOrbChunk( tileX - i, tileY - j, availableOrbs, out chosenOrbItem );
					if( chunk.HasValue ) {
						return chunk;
					}
				}
			}

			chosenOrbItem = null;
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
		/// <param name="availableOrbs"></param>
		/// <param name="chosenOrbItem"></param>
		public (int ChunkGridX, int ChunkGridY)? GetTargetOrbChunk(
					int tileX,
					int tileY,
					IEnumerable<Item> availableOrbs,
					out Item chosenOrbItem ) {
			chosenOrbItem = OrbsPlayer.ChoseOrbItemToTargetChunkForGivenTile_If(
				this.player,
				tileX,
				tileY,
				availableOrbs
			);

			if( chosenOrbItem != null ) {
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
