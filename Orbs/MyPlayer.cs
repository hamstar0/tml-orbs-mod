using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace Orbs {
	partial class OrbsPlayer : ModPlayer {
		public (int ChunkX, int ChunkY)? CurrentTargettedOrbableChunkGridPos { get; private set; } = null;



		////////////////

		public override void PreUpdate() {
			if( this.player.whoAmI == Main.myPlayer ) {
				this.CurrentTargettedOrbableChunkGridPos = this.FindNearbyOrbChunkTarget();
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
