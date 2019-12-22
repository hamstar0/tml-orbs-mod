using Terraria;
using Terraria.ModLoader;
using Orbs.Items.Base;


namespace Orbs.Items {
	public class PinkOrbItem : OrbItemBase {
		public override OrbColorCode ColorCode => OrbColorCode.Pink;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Pink Orb" );
			this.Tooltip.SetDefault( "Resonates with certain (pink) areas of the world"
				+"\nUsing this item will destroy nearby resonating areas" );
		}
	}
}