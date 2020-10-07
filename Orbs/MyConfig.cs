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

		[Range( 0, 1000 )]
		[DefaultValue( 80 )]
		public int OrbPseudoBiomeTileRadius { get; set; } = 80;


		////

		[DefaultValue( true )]
		public bool IsGeoResonantOrbSoldByDryad { get; set; } = true;

		////
		
		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int PinkOrbDropsViaTrickster { get; set; } = 1;

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
		public int TealOrbRecipeUniqueIngredientCount { get; set; } = 5;

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
		public int TealOrbRecipeStack { get; set; } = 1;

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

		[Range( 0f, 1f )]
		[DefaultValue( 0.1f )]
		public float BlueOrbPercentChanceForOrbChest { get; set; } = 0.1f;

		[Range( 0f, 1f )]
		[DefaultValue( 0.1f )]
		public float CyanOrbPercentChanceForOrbChest { get; set; } = 0.1f;

		[Range( 0f, 1f )]
		[DefaultValue( 0.1f )]
		public float GreenOrbPercentChanceForOrbChest { get; set; } = 0.1f;

		[Range( 0f, 1f )]
		[DefaultValue( 0.1f )]
		public float PinkOrbPercentChanceForOrbChest { get; set; } = 0.1f;

		[Range( 0f, 1f )]
		[DefaultValue( 0.1f )]
		public float PurpleOrbPercentChanceForOrbChest { get; set; } = 0.1f;

		[Range( 0f, 1f )]
		[DefaultValue( 0.1f )]
		public float RedOrbPercentChanceForOrbChest { get; set; } = 0.1f;

		[Range( 0f, 1f )]
		[DefaultValue( 0.1f )]
		public float TealOrbPercentChanceForOrbChest { get; set; } = 0.1f;

		[Range( 0f, 1f )]
		[DefaultValue( 0.1f )]
		public float WhiteOrbPercentChanceForOrbChest { get; set; } = 0.1f;

		[Range( 0f, 1f )]
		[DefaultValue( 0.1f )]
		public float YellowOrbPercentChanceForOrbChest { get; set; } = 0.1f;


		[DefaultValue(true)]
		public bool OnlyGenOrbsInUndergroundChests { get; set; } = true;


		[DefaultValue( true )]
		public bool CanDestroyActuatedTiles { get; set; } = true;



		////////////////

		public IEnumerable<(float Weight, int OrbItemType)> GetOrbChestWeights( out float totalWeight ) {
			float blueOrb = this.Get<float>( nameof(OrbsConfig.BlueOrbPercentChanceForOrbChest) );
			float cyanOrb = this.Get<float>( nameof(OrbsConfig.CyanOrbPercentChanceForOrbChest) );
			float greenOrb = this.Get<float>( nameof(OrbsConfig.GreenOrbPercentChanceForOrbChest) );
			float pinkOrb = this.Get<float>( nameof(OrbsConfig.PinkOrbPercentChanceForOrbChest) );
			float purpleOrb = this.Get<float>( nameof(OrbsConfig.PurpleOrbPercentChanceForOrbChest) );
			float redOrb = this.Get<float>( nameof(OrbsConfig.RedOrbPercentChanceForOrbChest) );
			float tealOrb = this.Get<float>( nameof(OrbsConfig.TealOrbPercentChanceForOrbChest) );
			float whiteOrb = this.Get<float>( nameof(OrbsConfig.WhiteOrbPercentChanceForOrbChest) );
			float yellowOrb = this.Get<float>( nameof(OrbsConfig.YellowOrbPercentChanceForOrbChest) );
			totalWeight = blueOrb + cyanOrb + greenOrb + pinkOrb + purpleOrb + redOrb + tealOrb + whiteOrb + yellowOrb;

			IEnumerable<(float, int)> getOrbs() {
				yield return (blueOrb, ModContent.ItemType<BlueOrbItem>());
				yield return (cyanOrb, ModContent.ItemType<CyanOrbItem>());
				yield return (greenOrb, ModContent.ItemType<GreenOrbItem>());
				yield return (pinkOrb, ModContent.ItemType<PinkOrbItem>());
				yield return (purpleOrb, ModContent.ItemType<PurpleOrbItem>());
				yield return (redOrb, ModContent.ItemType<RedOrbItem>());
				yield return (tealOrb, ModContent.ItemType<TealOrbItem>());
				yield return (whiteOrb, ModContent.ItemType<WhiteOrbItem>());
				yield return (yellowOrb, ModContent.ItemType<YellowOrbItem>());
			}
			return getOrbs();
		}
	}
}
