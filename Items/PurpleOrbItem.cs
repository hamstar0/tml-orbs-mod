using Terraria;
using Terraria.ModLoader;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class PurpleOrbItem : OrbItemBase {
		public override OrbColorCode ColorCode => OrbColorCode.Purple;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Purple Orb" );
			this.Tooltip.SetDefault( "Resonates with certain (purple) areas of the world"
				+ "\nUsing this item will destroy nearby resonating areas"
				+ "\nConsumed on use" );
		}
	}
}