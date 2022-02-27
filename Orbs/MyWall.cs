using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace Orbs {
	partial class OrbsWall : GlobalWall {
		public override void KillWall( int i, int j, int type, ref bool fail ) {
			var config = OrbsConfig.Instance;

			fail = !config.Get<bool>( nameof(config.CanDestroyWalls) );
		}
	}
}
