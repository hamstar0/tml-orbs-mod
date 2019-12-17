using HamstarHelpers.Classes.Tiles.TilePattern;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.World.Generation;


namespace Orbs {
	class OrbsWorldGen : GenPass {
		private int OrbCount;



		////////////////

		public OrbsWorldGen( int orbCount ) : base( "Orbs: Populating orbs", 1f ) {
			this.OrbCount = orbCount;
		}


		////////////////

		public override void Apply( GenerationProgress progress ) {
			float stepWeight = 1f / (float)this.OrbCount;

			TilePattern pattern = new TilePattern( new TilePatternBuilder {
				IsActive = false
			} );
			
			var within = new Rectangle(
				64,
				(int)Main.worldSurface,
				Main.maxTilesX - 128,
				Main.maxTilesY - ( 220 + 64 )
			);

			if( progress != null ) {
				progress.Message = "Pre-placing Orbs: %";
			}

			for( int i = 0; i < this.OrbCount; i++ ) {
				progress?.Set( stepWeight * (float)i );

				
			}
		}
	}
}
