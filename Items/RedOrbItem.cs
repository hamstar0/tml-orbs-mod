using Terraria;
using Terraria.ModLoader;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class RedOrbItem : OrbItemBase {
		public override OrbColorCode ColorCode => OrbColorCode.Red;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Red Orb" );
			this.Tooltip.SetDefault( "Resonates with certain (red) areas of the world"
				+ "\nUsing this item will destroy nearby resonating areas"
				+ "\nConsumed on use" );
		}
	}
}