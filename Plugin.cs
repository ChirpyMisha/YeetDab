using IPA;
using IPALogger = IPA.Logging.Logger;
using SiraUtil.Zenject;
using Zenject;

namespace YeetDab
{
	[Plugin(RuntimeOptions.SingleStartInit), NoEnableDisable]
	public class Plugin
	{
		internal static Plugin Instance { get; private set; }
		internal static IPALogger Log { get; private set; }

		[Init]
		/// <summary>
		/// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
		/// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
		/// Only use [Init] with one Constructor.
		/// </summary>
		public Plugin(IPALogger logger, Zenjector zenjector)
		{
			Instance = this;
			Log = logger;

			zenjector.Install(Location.Menu, (DiContainer Container) =>
			{
				Container.BindInterfacesAndSelfTo<DabYeeter>().AsSingle();
			});
		}

		//[OnStart]
		//public void OnApplicationStart() { }
		//[OnExit]
		//public void OnApplicationQuit() { }
	}
}
