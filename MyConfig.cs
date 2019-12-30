using System;
using System.ComponentModel;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Services.Configs;
using HamstarHelpers.Classes.UI.ModConfig;
using Orbs.Items;


namespace Orbs {
	class MyFloatInputElement : FloatInputElement { }




	public partial class OrbsConfig : StackableModConfig {
		public static OrbsConfig Instance => ModConfigStack.GetMergedConfigs<OrbsConfig>();



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
		[DefaultValue( 2 )]
		public int PinkOrbDropsViaTrickster { get; set; } = 2;

		[Range( 0, 99 )]
		[DefaultValue( 2 )]
		public int RedOrbDropsViaBossWithoutSoG { get; set; } = 2;

		[Range( 0, 99 )]
		[DefaultValue( 2 )]
		public int GreenOrbDropsViaBossWithSoG { get; set; } = 2;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int BlueOrbCraftStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int PurpleOrbDropsViaShadowOrb { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int CyanOrbCraftStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int YellowOrbCraftStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int WhiteOrbCraftStack { get; set; } = 1;


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



		////////////////

		public IEnumerable<(float Weight, int OrbItemType)> GetOrbChestWeights( out float totalWeight ) {
			totalWeight = this.BlueOrbPercentChanceForOrbChest;
			totalWeight += this.CyanOrbPercentChanceForOrbChest;
			totalWeight += this.GreenOrbPercentChanceForOrbChest;
			totalWeight += this.PinkOrbPercentChanceForOrbChest;
			totalWeight += this.PurpleOrbPercentChanceForOrbChest;
			totalWeight += this.RedOrbPercentChanceForOrbChest;
			totalWeight += this.TealOrbPercentChanceForOrbChest;
			totalWeight += this.WhiteOrbPercentChanceForOrbChest;
			totalWeight += this.YellowOrbPercentChanceForOrbChest;

			IEnumerable<(float, int)> getOrbs() {
				yield return (this.BlueOrbPercentChanceForOrbChest, ModContent.ItemType<BlueOrbItem>());
				yield return (this.CyanOrbPercentChanceForOrbChest, ModContent.ItemType<CyanOrbItem>());
				yield return (this.GreenOrbPercentChanceForOrbChest, ModContent.ItemType<GreenOrbItem>());
				yield return (this.PinkOrbPercentChanceForOrbChest, ModContent.ItemType<PinkOrbItem>());
				yield return (this.PurpleOrbPercentChanceForOrbChest, ModContent.ItemType<PurpleOrbItem>());
				yield return (this.RedOrbPercentChanceForOrbChest, ModContent.ItemType<RedOrbItem>());
				yield return (this.TealOrbPercentChanceForOrbChest, ModContent.ItemType<TealOrbItem>());
				yield return (this.WhiteOrbPercentChanceForOrbChest, ModContent.ItemType<WhiteOrbItem>());
				yield return (this.YellowOrbPercentChanceForOrbChest, ModContent.ItemType<YellowOrbItem>());
			}
			return getOrbs();
		}
	}
}
