using HamstarHelpers.Helpers.World;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.World.Generation;


namespace Orbs {
	partial class OrbsWorld : ModWorld {
		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			int shards;
			WorldSize wldSize = WorldHelpers.GetSize();

			switch( wldSize ) {
			default:
			case WorldSize.SubSmall:
				shards = OrbsConfig.Instance.OrbsPerTinyWorld;
				break;
			case WorldSize.Small:
				shards = OrbsConfig.Instance.OrbsPerSmallWorld;
				break;
			case WorldSize.Medium:
				shards = OrbsConfig.Instance.OrbsPerMediumWorld;
				break;
			case WorldSize.Large:
				shards = OrbsConfig.Instance.OrbsPerLargeWorld;
				break;
			case WorldSize.SuperLarge:
				shards = OrbsConfig.Instance.OrbsPerHugeWorld;
				break;
			}

			tasks.Add( new OrbsWorldGen(shards) );
		}
	}
}
