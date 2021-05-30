using Terraria;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class CyanOrbItem : OrbItemBase {
		public override OrbColorCode ColorCode => OrbColorCode.Cyan;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Cyan Orb" );
			this.Tooltip.SetDefault( "Resonates with a specific (cyan) world terrain type"
				+ "\nUsing this item will destroy a nearby resonating area"
				+ "\nConsumed on use" );
		}
	}
}