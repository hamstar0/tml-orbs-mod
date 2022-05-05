using System;
using Terraria;
using Terraria.World.Generation;


namespace Orbs {
	partial class OrbsWorldGen : GenPass {
		//private int OrbCount;



		////////////////

		public OrbsWorldGen() : base( "Orbs: Populating orbs", 1f ) {
			//this.OrbCount = orbCount;
		}


		////////////////

		public override void Apply( GenerationProgress progress ) {
			if( progress != null ) {
				progress.Message = "Pre-placing Orbs";	//"Pre-placing Orbs: %";
			}

			this.ApplyChestOrbs_If();
		}
	}
}
