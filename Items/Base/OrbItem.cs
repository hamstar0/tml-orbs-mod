using Terraria;
using Terraria.ModLoader;


namespace Orbs.Items.Base {
	public abstract class OrbItem : ModItem {
		public override void SetDefaults() {
			//this.item.maxStack = 99;
			//this.item.width = 16;
			//this.item.height = 16;
			this.item.consumable = true;
			this.item.useStyle = 4;
			this.item.useTime = 30;
			this.item.useAnimation = 30;
			//this.item.UseSound = SoundID.Item108;
			this.item.maxStack = 1;
			this.item.value = Item.buyPrice( 0, 5, 0, 0 );
			this.item.rare = 3;
		}


		////////////////

		public override bool UseItem( Player player ) {
			if( player.itemAnimation > 0 && player.itemTime == 0 ) {
				player.itemTime = item.useTime;
				return true;
			}
			return base.UseItem( player );
		}

		//public override bool ConsumeItem( Player player ) {
		//	return false;
		//}
	}
}