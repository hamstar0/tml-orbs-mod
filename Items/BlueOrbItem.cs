using Terraria;
using Terraria.ModLoader;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class BlueOrbItem : OrbItemBase {
		public override OrbColorCode ColorCode => OrbColorCode.Blue;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Blue Orb" );
			this.Tooltip.SetDefault( "Resonates with certain (blue) areas of the world"
				+"\nUsing this item will destroy nearby resonating areas"
				+"\nConsumed on use" );
		}
	}
}