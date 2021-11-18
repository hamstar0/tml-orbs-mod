using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Recipes;
using Orbs.Items;
using Orbs.Items.Base;


namespace Orbs {
	public partial class OrbsMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-orbs-mod";


		////////////////

		public static OrbsMod Instance { get; private set; }



		////////////////

		public bool IsMapOverlayOn { get; private set; } = false;



		////////////////

		public OrbsMod() {
			OrbsMod.Instance = this;
		}

		////////////////

		public override void Load() {
			if( Main.netMode != 2 && !Main.dedServ ) {
				this.InitializeMapUI();
			}

			OrbItemBase.InitializeItemTypeColorCodes();
		}

		public override void Unload() {
			OrbsMod.Instance = null;
		}

		public override void PostSetupContent() {
			/*ModHelpersConfig.Instance.OverlayChanges(
				new ModHelpersConfig {
					GeoResonantOrbSoldByDryad = true
				}
			);*/
		}


		////////////////

		public override void AddRecipes() {
			//if( ModLoader.GetMod("FindableManaCrystals") != null ) {
			//	this.LoadRecipesForFindableManaCrystals();
			//}

			var blueOrbRecipe = new BlueOrbRecipe();
			if( blueOrbRecipe.RecipeAvailable() ) {
				blueOrbRecipe.AddRecipe();
			}
			var cyanOrbRecipe = new CyanOrbRecipe();
			if( cyanOrbRecipe.RecipeAvailable() ) {
				cyanOrbRecipe.AddRecipe();
			}
			var greenOrbRecipe = new GreenOrbRecipe();
			if( greenOrbRecipe.RecipeAvailable() ) {
				greenOrbRecipe.AddRecipe();
			}
			var pinkOrbRecipe = new PinkOrbRecipe();
			if( pinkOrbRecipe.RecipeAvailable() ) {
				pinkOrbRecipe.AddRecipe();
			}
			var purpleOrbRecipe = new PurpleOrbRecipe();
			if( purpleOrbRecipe.RecipeAvailable() ) {
				purpleOrbRecipe.AddRecipe();
			}
			var redOrbRecipe = new RedOrbRecipe();
			if( redOrbRecipe.RecipeAvailable() ) {
				redOrbRecipe.AddRecipe();
			}
			var brownOrbRecipe = new BrownOrbRecipe();
			if( brownOrbRecipe.RecipeAvailable() ) {
				brownOrbRecipe.AddRecipe();
			}
			var yellowOrbRecipe = new YellowOrbRecipe();
			if( yellowOrbRecipe.RecipeAvailable() ) {
				yellowOrbRecipe.AddRecipe();
			}
			var whiteOrbRecipe = new WhiteOrbRecipe();
			if( whiteOrbRecipe.RecipeAvailable() ) {
				whiteOrbRecipe.AddRecipe();
			}
		}

		public override void AddRecipeGroups() {
			var copOrTinBar = new RecipeGroup( () => "Copper or Tin Bar", ItemID.CopperBar, ItemID.TinBar );
			var ironOrLeadBar = new RecipeGroup( () => "Copper or Tin Bar", ItemID.IronBar, ItemID.LeadBar );
			var silvOrTungBar = new RecipeGroup( () => "Silver or Tungsten Bar", ItemID.SilverBar, ItemID.TungstenBar );
			var goldOrPlatBar = new RecipeGroup( () => "Gold or Platinum Bar", ItemID.GoldBar, ItemID.PlatinumBar );
			var cobOrPalBar = new RecipeGroup( () => "Cobalt or Palladium Bar", ItemID.CobaltBar, ItemID.PalladiumBar );
			var mythOrOricBar = new RecipeGroup( () => "Mythril or Orichalcum Bar", ItemID.MythrilBar, ItemID.OrichalcumBar );
			var adaOrTitBar = new RecipeGroup( () => "Adamantite or Titanium Bar", ItemID.AdamantiteBar, ItemID.TitaniumBar );
			var strangePlants = new RecipeGroup( () => "Strange Plants", ItemID.StrangePlant1, ItemID.StrangePlant2, ItemID.StrangePlant3, ItemID.StrangePlant4 );

			RecipeGroup.RegisterGroup( "Orbs:CopperOrTinBars", copOrTinBar );
			RecipeGroup.RegisterGroup( "Orbs:IronOrLeadBars", ironOrLeadBar );
			RecipeGroup.RegisterGroup( "Orbs:SilverOrTungstenBars", silvOrTungBar );
			RecipeGroup.RegisterGroup( "Orbs:GoldOrPlatinumBars", goldOrPlatBar );

			RecipeGroup.RegisterGroup( "Orbs:CobaltOrPalladiumBars", cobOrPalBar );
			RecipeGroup.RegisterGroup( "Orbs:MythrilOrOrichalcumBars", mythOrOricBar );
			RecipeGroup.RegisterGroup( "Orbs:AdamantiteOrTitaniumBars", adaOrTitBar );

			RecipeGroup.RegisterGroup( "Orbs:StrangePlants", strangePlants );

			RecipeGroup.RegisterGroup( "Orbs:ChromaticOrbs", new RecipeGroup(
				() => "Chromatic Orbs",
				ModContent.ItemType<BlueOrbItem>(),
				ModContent.ItemType<CyanOrbItem>(),
				ModContent.ItemType<GreenOrbItem>(),
				ModContent.ItemType<PinkOrbItem>(),
				ModContent.ItemType<PurpleOrbItem>(),
				ModContent.ItemType<RedOrbItem>(),
				ModContent.ItemType<BrownOrbItem>(),
				ModContent.ItemType<WhiteOrbItem>(),
				ModContent.ItemType<YellowOrbItem>()
			) );
		}


		////////////////

		public override void MidUpdateTimeWorld() {
			if( Main.netMode != NetmodeID.Server ) {
				if( !Main.LocalPlayer.HeldItem.IsAir && Main.LocalPlayer.HeldItem.type == ItemID.Binoculars ) {
					this.UpdateBinocsModificationsIf();
				}
			}
		}
	}
}