using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Orbs.Items.Base;


namespace Orbs {
	public partial class OrbsMod : Mod {
		private static void Load_PKEMeter_WeakRef() {
			Color brown = Color.Lerp( OrbItemBase.ColorValues[OrbColorCode.Brown], new Color(255, 128, 0), 0.25f );
			Color purple = Color.Lerp( OrbItemBase.ColorValues[OrbColorCode.Purple], new Color(255, 0, 255), 0.5f );

			PKEMeter.PKEMeterAPI.SetMiscLights( ( player, position ) => {
				var myplayer = Main.LocalPlayer.GetModPlayer<OrbsPlayer>();
				var codes = myplayer.CurrentNearbyChunkTypes;
				if( codes == null ) {
					return null;
				}

//Color.Blue, Color.Cyan, Color.Lime, Color.HotPink, Color.DarkMagenta, Color.Red, Color.Peru, Color.White, Color.Yellow
				return new PKEMeter.Logic.PKEMiscLightsValues(
					codes.Contains( OrbColorCode.Brown ) ? brown : default,
					codes.Contains( OrbColorCode.Red ) ? OrbItemBase.ColorValues[OrbColorCode.Red] : default,
					codes.Contains( OrbColorCode.Yellow ) ? OrbItemBase.ColorValues[OrbColorCode.Yellow] : default,
					codes.Contains( OrbColorCode.White ) ? OrbItemBase.ColorValues[OrbColorCode.White] : default,
					codes.Contains( OrbColorCode.Green ) ? OrbItemBase.ColorValues[OrbColorCode.Green] : default,
					codes.Contains( OrbColorCode.Cyan ) ? OrbItemBase.ColorValues[OrbColorCode.Cyan] : default,
					codes.Contains( OrbColorCode.Blue ) ? OrbItemBase.ColorValues[OrbColorCode.Blue] : default,
					codes.Contains( OrbColorCode.Purple ) ? purple : default,
					codes.Contains( OrbColorCode.Pink ) ? OrbItemBase.ColorValues[OrbColorCode.Pink] : default
				);
			} );
		}
	}
}
