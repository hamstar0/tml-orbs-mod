using Terraria;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class GreenOrbItem : OrbItemBase {
		public override OrbColorCode ColorCode => OrbColorCode.Green;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Green Orb" );
			this.Tooltip.SetDefault( "Resonates with a specific (green) world terrain type"
				+ "\nUsing this item will destroy a nearby resonating area"
				+ "\nConsumed on use" );
		}
	}
}