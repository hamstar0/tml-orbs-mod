using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Orbs.Items.Base;


namespace Orbs {
	partial class OrbsPlayer : ModPlayer {
		public (int ChunkX, int ChunkY)? CurrentTargettedOrbableChunkGridPosition { get; private set; } = null;

		public ISet<OrbColorCode> CurrentNearbyChunkTypes { get; private set; }



		////////////////

		public override void PreUpdate() {
			if( this.player.whoAmI == Main.myPlayer ) {
				this.UpdateNearbyOrbChunkTarget();
			}

			/*if( OrbsConfig.Instance.DebugModeCheatCreate ) {
				if( Main.mouseRight && Main.mouseRightRelease ) {
					int x = (int)( ( Main.screenPosition.X + Main.mouseX ) / 16 );
					int y = (int)( ( Main.screenPosition.Y + Main.mouseY ) / 16 );

					OrbTileBase.CreateTile( x, y );

					var myworld = ModContent.GetInstance<OrbsWorld>();
					myworld.GetOrbs().Set2D( x, y );
				}
			}*/
		}
	}
}
