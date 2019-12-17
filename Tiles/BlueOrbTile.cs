using System;
using Microsoft.Xna.Framework;
using Orbs.Items;
using Orbs.Tiles.Base;
using Terraria;
using Terraria.ModLoader;


namespace Orbs.Tiles {
	class BlueOrbTile : OrbTile {
		public override string MyName => "Blue Orb";

		public override Color PrimaryColor => new Color( 64, 64, 192 );



		////////////////

		public override void KillMultiTile( int i, int j, int frameX, int frameY ) {
			Item.NewItem( i * 16, j * 16, 24, 24, ModContent.ItemType<BlueOrbItem>() );
		}


		////////////////

		public override void NearbyEffects( int i, int j, bool closer ) {
			base.NearbyEffects( i, j, closer );
		}
	}
}
