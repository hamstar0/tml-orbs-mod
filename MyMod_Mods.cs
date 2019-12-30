using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using HamstarHelpers.Helpers.TModLoader;
using Orbs.Items;
using Orbs.Recipes;
using HamstarHelpers.Helpers.Debug;

namespace Orbs {
	public partial class OrbsMod : Mod {
		private void LoadForStaffOfGaia() {
			StaffOfGaia.StaffOfGaiaMod.UseHookDefinition useHook = StaffOfGaia.StaffOfGaiaAPI.GetUseHook();

			if( useHook != null ) {
				useHook = new StaffOfGaia.StaffOfGaiaMod.UseHookDefinition( ( plr, powerPercent, charges ) => {
					myUseHook( plr, powerPercent, charges );
					useHook( plr, powerPercent, charges );
				} );
			} else {
				useHook = new StaffOfGaia.StaffOfGaiaMod.UseHookDefinition( myUseHook );
			}

			StaffOfGaia.StaffOfGaiaAPI.SetUseHook( useHook );

			void myUseHook( Player plr, float powerPercent, int charges ) {
				if( Main.netMode == 1 ) { return; }

				if( powerPercent >= 0.5f ) {
					int itemWho = Item.NewItem(
						plr.getRect(),
						ModContent.ItemType<GreenOrbItem>(),
						OrbsConfig.Instance.GreenOrbDropsViaBossWithSoG
					);
					if( Main.netMode == 2 ) {
						NetMessage.SendData( MessageID.SyncItem, -1, -1, null, itemWho );
					}
				}
			}
		}


		private void LoadForChestImplants() {
			ChestImplants.ChestImplantsAPI.AddCustomImplanter( ( context, chest ) => {
				if( !ChestImplants.ChestImplanter.IsChestMatch( context, "Vanilla Underground World Chest" ) ) {
					return;
				}

				UnifiedRandom rand = TmlHelpers.SafelyGetRand();
				if( rand.NextFloat() >= OrbsConfig.Instance.AnyOrbPercentChancePerChest ) {
					return;
				}
				
				int orbItemType = -1;

				float totalWeight;
				IEnumerable<(float, int)> weights = OrbsConfig.Instance.GetOrbChestWeights( out totalWeight );
				float weightRand = rand.NextFloat() * totalWeight;

				float countedWeight = 0f;
				foreach( (float weight, int checkOrbItemType) in weights ) {
					if( (countedWeight + weight) < weightRand ) {
						countedWeight += weight;
						continue;
					}

					orbItemType = checkOrbItemType;
					break;
				}

				if( orbItemType == -1 ) {
					LogHelpers.Warn( "Could not pick random orb to implant in chest." );
					return;
				}

				for( int i=0; i<chest.item.Length; i++ ) {
					Item item = chest.item[i];
					if( item?.active == true || !item.IsAir ) {
						continue;
					}

					chest.item[i] = new Item();
					chest.item[i].SetDefaults( orbItemType );
					chest.item[i].stack = 1;

					LogHelpers.Log( "Implanted "+chest.item[i].Name+" in chest." );
					break;
				}
			} );
		}


		////

		private void LoadRecipesForFindableManaCrystals() {
			var blueOrbRecipe = new BlueOrbRecipe();
			blueOrbRecipe.AddRecipe();
		}
	}
}