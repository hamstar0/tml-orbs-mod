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