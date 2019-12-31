using Terraria;
using Terraria.ModLoader;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class CyanOrbItem : OrbItemBase {
		public override OrbColorCode ColorCode => OrbColorCode.Cyan;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Cyan Orb" );
			this.Tooltip.SetDefault( "Resonates with certain (cyan) areas of the world"
				+ "\nUsing this item will destroy nearby resonating areas"
				+ "\nConsumed on use" );
		}
	}
}