using Zenject;

namespace YeetDab
{
	class MenuInstaller : Installer
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<DabYeeter>().AsSingle();
		}
	}
}