using Terraria;
using Terraria.ModLoader;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class CyanOrbItem : OrbItemBase {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Cyan Orb" );
			this.Tooltip.SetDefault( "Resonates with certain (cyan) areas of the world"
				+"\nUsing this item will destroy nearby resonating areas" );
		}


		////////////////

		public override bool ConsumeItem( Player player ) {
			// Check if nearby resonating areas

			return false;
		}
	}
}