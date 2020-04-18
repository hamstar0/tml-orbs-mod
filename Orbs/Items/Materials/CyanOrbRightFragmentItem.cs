using Terraria;
using Terraria.ModLoader;
using Orbs.Items.Base;


namespace Orbs.Items.Materials {
	public class CyanOrbRightFragmentItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Cyan Orb Fragment (Right)" );
			this.Tooltip.SetDefault( "Assembles into an orb that can resonate with certain (cyan) areas of the world" );
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