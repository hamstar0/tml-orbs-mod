using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Orbs.Items.Base;


namespace Orbs {
	public partial class OrbsMod : Mod {
		private static void Load_PKEMeter_WeakRef() {
			PKEMeter.PKEMeterAPI.SetMiscLights( ( player, position ) => {
				var myplayer = Main.LocalPlayer.GetModPlayer<OrbsPlayer>();
				var codes = myplayer.CurrentNearbyChunkTypes;
				if( codes == null ) {
					return null;
				}

//Color.Blue, Color.Cyan, Color.Lime, Color.HotPink, Color.DarkMagenta, Color.Red, Color.Peru, Color.White, Color.Yellow
				return new PKEMeter.Logic.PKEMiscLightsValues(
					codes.Contains( OrbColorCode.Brown ) ? OrbItemBase.ColorValues[OrbColorCode.Brown] : default,
					codes.Contains( OrbColorCode.Red ) ? OrbItemBase.ColorValues[OrbColorCode.Red] : default,
					codes.Contains( OrbColorCode.Yellow ) ? OrbItemBase.ColorValues[OrbColorCode.Yellow] : default,
					codes.Contains( OrbColorCode.White ) ? OrbItemBase.ColorValues[OrbColorCode.White] : default,
					codes.Contains( OrbColorCode.Green ) ? OrbItemBase.ColorValues[OrbColorCode.Green] : default,
					codes.Contains( OrbColorCode.Cyan ) ? OrbItemBase.ColorValues[OrbColorCode.Cyan] : default,
					codes.Contains( OrbColorCode.Blue ) ? OrbItemBase.ColorValues[OrbColorCode.Blue] : default,
					codes.Contains( OrbColorCode.Purple ) ? OrbItemBase.ColorValues[OrbColorCode.Purple] : default,
					codes.Contains( OrbColorCode.Pink ) ? OrbItemBase.ColorValues[OrbColorCode.Pink] : default
				);
			} );
		}
	}
}
