using Orbs.Items;
using Orbs.Recipes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


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
	}
}