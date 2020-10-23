using Terraria;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class WhiteOrbItem : OrbItemBase {
		public override OrbColorCode ColorCode => OrbColorCode.White;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "White Orb" );
			this.Tooltip.SetDefault( "Resonates with a specific (white) world terrain type"
				+ "\nUsing this item will destroy a nearby resonating area"
				+ "\nConsumed on use" );
		}
	}
}