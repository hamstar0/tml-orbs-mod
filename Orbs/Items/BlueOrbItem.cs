using Terraria;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class BlueOrbItem : OrbItemBase {
		public override OrbColorCode ColorCode => OrbColorCode.Blue;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Blue Orb" );
			this.Tooltip.SetDefault( "Resonates with a specific (blue) world terrain type"
				+"\nUsing this item will destroy a nearby resonating area"
				+"\nConsumed on use" );
		}
	}
}