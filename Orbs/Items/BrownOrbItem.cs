using Terraria;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class BrownOrbItem : OrbItemBase {
		public override OrbColorCode ColorCode => OrbColorCode.Brown;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Brown Orb" );
			this.Tooltip.SetDefault( "Resonates with a specific (brown) world terrain type"
				+ "\nUsing this item will destroy a nearby resonating area"
				+ "\nConsumed on use" );
		}
	}
}