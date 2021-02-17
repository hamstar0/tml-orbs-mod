using System;
using System.ComponentModel;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.ModConfig;
using Orbs.Items;


namespace Orbs {
	class MyFloatInputElement : FloatInputElement { }
	//[CustomModConfigItem( typeof( MyFloatInputElement ) )]




	public partial class OrbsConfig : ModConfig {
		public static OrbsConfig Instance => ModContent.GetInstance<OrbsConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		public IEnumerable<(float Weight, int OrbItemType)> GetOrbChestWeights( out float totalWeight ) {
			float blueOrb = this.Get<float>( nameof(this.BlueOrbWeightPerOrbChest) );
			float cyanOrb = this.Get<float>( nameof(this.CyanOrbWeightPerOrbChest) );
			float greenOrb = this.Get<float>( nameof(this.GreenOrbWeightPerOrbChest) );
			float pinkOrb = this.Get<float>( nameof(this.PinkOrbWeightPerOrbChest) );
			float purpleOrb = this.Get<float>( nameof(this.PurpleOrbWeightPerOrbChest) );
			float redOrb = this.Get<float>( nameof(this.RedOrbWeightPerOrbChest) );
			float brownOrb = this.Get<float>( nameof(this.BrownOrbWeightPerOrbChest) );
			float whiteOrb = this.Get<float>( nameof(this.WhiteOrbWeightPerOrbChest) );
			float yellowOrb = this.Get<float>( nameof(this.YellowOrbWeightPerOrbChest) );
			totalWeight = blueOrb + cyanOrb + greenOrb + pinkOrb + purpleOrb + redOrb + brownOrb + whiteOrb + yellowOrb;

			IEnumerable<(float, int)> getOrbs() {
				yield return (blueOrb, ModContent.ItemType<BlueOrbItem>());
				yield return (cyanOrb, ModContent.ItemType<CyanOrbItem>());
				yield return (greenOrb, ModContent.ItemType<GreenOrbItem>());
				yield return (pinkOrb, ModContent.ItemType<PinkOrbItem>());
				yield return (purpleOrb, ModContent.ItemType<PurpleOrbItem>());
				yield return (redOrb, ModContent.ItemType<RedOrbItem>());
				yield return (brownOrb, ModContent.ItemType<BrownOrbItem>());
				yield return (whiteOrb, ModContent.ItemType<WhiteOrbItem>());
				yield return (yellowOrb, ModContent.ItemType<YellowOrbItem>());
			}
			return getOrbs();
		}
	}
}
