using Terraria;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class YellowOrbItem : OrbItemBase {
		public override OrbColorCode ColorCode => OrbColorCode.Yellow;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Yellow Orb" );
			this.Tooltip.SetDefault( "Resonates with a specific (yellow) world terrain type"
				+ "\nUsing this item will destroy a nearby resonating area"
				+ "\nConsumed on use" );
		}
	}
}