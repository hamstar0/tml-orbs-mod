using Terraria;
using Terraria.ModLoader;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class PurpleOrbItem : OrbItemBase {
		public override OrbColorCode ColorCode => OrbColorCode.Purple;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Purple Orb" );
			this.Tooltip.SetDefault( "Resonates with a specific (purple) world terrain type"
				+ "\nUsing this item will destroy a nearby resonating area"
				+ "\nConsumed on use" );
		}
	}
}