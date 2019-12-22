using Terraria;
using Terraria.ModLoader;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class YellowOrbItem : OrbItemBase {
		public override OrbColorCode ColorCode => OrbColorCode.Yellow;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Yellow Orb" );
			this.Tooltip.SetDefault( "Resonates with certain (yellow) areas of the world"
				+"\nUsing this item will destroy nearby resonating areas" );
		}
	}
}