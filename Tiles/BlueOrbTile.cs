using System;
using Microsoft.Xna.Framework;
using Orbs.Items;
using Orbs.Tiles.Base;
using Terraria;
using Terraria.ModLoader;


namespace Orbs.Tiles {
	class BlueOrbTile : OrbTileBase {
		public override string MyName => "Blue Orb";

		public override Color PrimaryColor => new Color( 64, 64, 192 );



		////////////////

		public override void KillMultiTile( int i, int j, int frameX, int frameY ) {
			Item.NewItem(
				X: i * 16,
				Y: j * 16,
				Width: 24,
				Height: 24,
				Type: ModContent.ItemType<BlueOrbItem>(),
				Stack: 4
			);
		}


		////////////////

		public override void NearbyEffects( int i, int j, bool closer ) {
			base.NearbyEffects( i, j, closer );
		}


		////////////////

		public override void ApplyPseudoBiomeToNPC( NPC npc ) {
			var mynpc = npc.GetGlobalNPC<OrbsNPC>();
			mynpc.HealTimer = 1;
			mynpc.Tint = this.PrimaryColor;
		}
	}
}
