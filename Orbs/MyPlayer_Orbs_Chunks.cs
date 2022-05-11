using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Orbs.Items.Base;
using ModLibsCore.Services.Timers;

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

		private void UpdateNearbyOrbChunkTarget_Local() {
			if( this.player.whoAmI != Main.myPlayer ) {
				return;
			}

			//

			Item chosenOrbItem;

			this.CurrentTargettedOrbableChunkGridPosition = this.FindNearbyOrbChunkTarget_If_Local( out chosenOrbItem );

			this.CurrentNearbyChunkTypes = this.FindNearbyOrbChunkTypes( this.player.MountedCenter );

			//

			this.SwapHeldOrbForTargettedChunk_If( chosenOrbItem );
		}


		////////////////

		private void SwapHeldOrbForTargettedChunk_If( Item chosenOrbItem ) {
			if( !this.CurrentTargettedOrbableChunkGridPosition.HasValue && chosenOrbItem != null ) {
				return;
			}

			int idx = Array.FindIndex( this.player.inventory, i => i == chosenOrbItem );
			if( idx == -1 || idx == this.player.selectedItem ) {
				return;
			}

			//

			Utils.Swap( ref this.player.inventory[this.player.selectedItem], ref this.player.inventory[idx] );

			if( Main.mouseItem?.active == true && Main.mouseItem.modItem is OrbItemBase ) {
				Main.mouseItem = this.player.HeldItem.Clone();
			}

			//

			// Failsafe?
			Timers.SetTimer( OrbsPlayer.OrbSwapLockoutTimerName, 2, false, () => false );
		}


		////////////////

		internal void ClearCurrentTargetOrbChunk() {
			this.CurrentTargettedOrbableChunkGridPosition = null;
		}
	}
}
