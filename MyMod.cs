using Terraria.ModLoader;


namespace Orbs {
	public class OrbsMod : Mod {
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
		}

		public override void Unload() {
			OrbsMod.Instance = null;
		}
	}
}