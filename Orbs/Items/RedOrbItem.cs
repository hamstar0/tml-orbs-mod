using Terraria;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class RedOrbItem : OrbItemBase {
		public override OrbColorCode ColorCode => OrbColorCode.Red;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Red Orb" );
			this.Tooltip.SetDefault( "Resonates with a specific (red) world terrain type"
				+ "\nUsing this item will destroy a nearby resonating area"
				+ "\nConsumed on use" );
		}
	}
}