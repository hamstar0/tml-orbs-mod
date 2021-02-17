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

		public bool DebugModeInfo { get; set; } = false;

		public bool DebugModeTheColorsDuke { get; set; } = false;

		/*public bool DebugModeCheatCreate { get; set; } = false;


		[Range( 0, 1024 )]
		[DefaultValue( 16 )]
		public int OrbsPerTinyWorld { get; set; } = 16;

		[Range( 0, 1024 )]
		[DefaultValue( 24 )]
		public int OrbsPerSmallWorld { get; set; } = 24;

		[Range( 0, 1024 )]
		[DefaultValue( 48 )]
		public int OrbsPerMediumWorld { get; set; } = 48;

		[Range( 0, 1024 )]
		[DefaultValue( 96 )]
		public int OrbsPerLargeWorld { get; set; } = 96;

		[Range( 0, 1024 )]
		[DefaultValue( 128 )]
		public int OrbsPerHugeWorld { get; set; } = 128;*/

		////

		[DefaultValue( true )]
		public bool EnableOrbUseUponTiles { get; set; } = true;

		[DefaultValue( false )]
		public bool EnableOrbPseudoBiomeForTiles { get; set; } = false;

		////

		public string OrbDisabledMessage { get; set; } = "";

		////

		[Range( 0, 1000 )]
		[DefaultValue( 80 )]
		public int OrbPseudoBiomeTileRadius { get; set; } = 80;


		////

		[DefaultValue( true )]
		public bool IsGeoResonantOrbSoldByDryad { get; set; } = true;

		////

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int PurpleOrbDropsViaShadowOrb { get; set; } = 1;

		////

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int BlueOrbRecipeUniqueIngredientCount { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 5 )]
		public int CyanOrbRecipeUniqueIngredientCount { get; set; } = 5;

		[Range( 0, 99 )]
		[DefaultValue( 3 )]
		public int GreenOrbRecipeUniqueIngredientCount { get; set; } = 3;

		[Range( 0, 99 )]
		[DefaultValue( 5 )]
		public int PinkOrbRecipeUniqueIngredientCount { get; set; } = 5;

		[Range( 0, 99 )]
		[DefaultValue( 5 )]
		public int PurpleOrbRecipeUniqueIngredientCount { get; set; } = 5;

		[Range( 0, 99 )]
		[DefaultValue( 8 )]
		public int RedOrbRecipeUniqueIngredientCount { get; set; } = 8;

		[Range( 0, 99 )]
		[DefaultValue( 5 )]
		public int BrownOrbRecipeUniqueIngredientCount { get; set; } = 5;

		[Range( 0, 99 )]
		[DefaultValue( 5 )]
		public int YellowOrbRecipeUniqueIngredientCount { get; set; } = 5;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int WhiteOrbRecipeUniqueIngredientCount { get; set; } = 1;

		////

		[Range( 0, 99 )]
		[DefaultValue( 2 )]
		public int BlueOrbRecipeStack { get; set; } = 2;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int CyanOrbRecipeStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int GreenOrbRecipeStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int PinkOrbRecipeStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int PurpleOrbRecipeStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int RedOrbRecipeStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int BrownOrbRecipeStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int YellowOrbRecipeStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int WhiteOrbRecipeStack { get; set; } = 1;


		////

		[Range( 0f, 1f )]
		[DefaultValue( 0.5f )]
		public float AnyOrbPercentChancePerChest { get; set; } = 0.5f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BlueOrbWeightPerOrbChest { get; set; } = 1f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float CyanOrbWeightPerOrbChest { get; set; } = 1f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float GreenOrbWeightPerOrbChest { get; set; } = 1f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PinkOrbWeightPerOrbChest { get; set; } = 1f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PurpleOrbWeightPerOrbChest { get; set; } = 1f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RedOrbWeightPerOrbChest { get; set; } = 1f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BrownOrbWeightPerOrbChest { get; set; } = 1f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float WhiteOrbWeightPerOrbChest { get; set; } = 1f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float YellowOrbWeightPerOrbChest { get; set; } = 1f;


		[DefaultValue(true)]
		public bool OnlyGenOrbsInUndergroundChests { get; set; } = true;


		[DefaultValue( true )]
		public bool CanDestroyActuatedTiles { get; set; } = true;


		[Range( 0f, 1f )]
		[DefaultValue( 0.075f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BinocularsCaveDiscoveryIntensity { get; set; } = 0.075f;



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
