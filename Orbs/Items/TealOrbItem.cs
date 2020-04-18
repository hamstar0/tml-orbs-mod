using Terraria;
using Terraria.ModLoader;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class TealOrbItem : OrbItemBase {
		public override OrbColorCode ColorCode => OrbColorCode.Teal;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Teal Orb" );
			this.Tooltip.SetDefault( "Resonates with a specific (teal) world terrain type"
				+ "\nUsing this item will destroy a nearby resonating area"
				+ "\nConsumed on use" );
		}
	}
}