using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Orbs.Items;
using Orbs.Tiles.Base;


namespace Orbs.Tiles {
	class PinkOrbTile : OrbTileBase {
		public override string MyName => "Pink Orb";

		public override Color PrimaryColor => new Color( 192, 128, 128 );



		////////////////

		public override void KillMultiTile( int i, int j, int frameX, int frameY ) {
			Item.NewItem(
				X: i * 16,
				Y: j * 16,
				Width: 24,
				Height: 24,
				Type: ModContent.ItemType<PinkOrbItem>(),
				Stack: 4
			);
		}


		////////////////

		public override void NearbyEffects( int i, int j, bool closer ) {
			base.NearbyEffects( i, j, closer );
		}


		////////////////

		public override void ApplyPseudoBiomeToNPC( NPC npc ) {
			//TODO
		}
	}
}
