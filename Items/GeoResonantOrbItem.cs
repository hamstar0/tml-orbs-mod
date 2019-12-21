using Terraria;
using Terraria.ModLoader;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class GeoResonantOrbItem : OrbItemBase {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Geo-Resonant Orb" );
			this.Tooltip.SetDefault( "A special magical conduit able to be tuned to world energies" );
		}


		public override void SetDefaults() {
			this.item.maxStack = 99;
			this.item.width = 16;
			this.item.height = 16;
			this.item.material = true;
			//this.item.UseSound = SoundID.Item108;
			this.item.value = Item.buyPrice( 0, 5, 0, 0 );
			this.item.rare = 2;
		}
	}
}