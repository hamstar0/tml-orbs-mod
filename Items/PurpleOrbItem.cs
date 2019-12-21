using Terraria;
using Terraria.ModLoader;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class PurpleOrbItem : OrbItemBase {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Purple Orb" );
			this.Tooltip.SetDefault( "Resonates with certain (purple) areas of the world"
				+"\nUsing this item will destroy nearby resonating areas" );
		}


		////////////////

		public override bool ConsumeItem( Player player ) {
			// Check if nearby resonating areas

			return false;
		}
	}
}