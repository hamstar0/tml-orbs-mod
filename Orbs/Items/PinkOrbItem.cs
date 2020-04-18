using Terraria;
using Terraria.ModLoader;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class PinkOrbItem : OrbItemBase {
		public override OrbColorCode ColorCode => OrbColorCode.Pink;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Pink Orb" );
			this.Tooltip.SetDefault( "Resonates with a specific (pink) world terrain type"
				+ "\nUsing this item will destroy a nearby resonating area"
				+ "\nConsumed on use" );
		}
	}
}