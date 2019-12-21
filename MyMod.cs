using HamstarHelpers.Services.Hooks.ExtendedHooks;
using Microsoft.Xna.Framework;
using Orbs.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Orbs {
	public partial class OrbsMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-orbs-mod";


		////////////////

		public static OrbsMod Instance { get; private set; }



		////////////////

		public OrbsMod() {
			OrbsMod.Instance = this;
		}

		////////////////

		public override void Load() {
			if( ModLoader.GetMod("StaffOfGaia") != null ) {
				this.LoadForStaffOfGaia();
			}

			if( OrbsConfig.Instance.PurpleOrbDropsViaShadowOrb > 0 ) {
				ExtendedTileHooks.AddKillMultiTileHook( ( i, j, type ) => {
					if( type != TileID.ShadowOrbs ) { return; }
					if( OrbsConfig.Instance.PurpleOrbDropsViaShadowOrb == 0 ) { return; }
					
					int itemWho = Item.NewItem(
						new Rectangle( i << 4, j << 4, 32, 32 ),
						ModContent.ItemType<PurpleOrbItem>()
					);
				} );
			}
		}

		public override void Unload() {
			OrbsMod.Instance = null;
		}


		public override void AddRecipes() {
			if( ModLoader.GetMod("FindableManaCrystals") != null ) {
				this.AddRecipesForFindableManaCrystals();
			}
		}
	}
}