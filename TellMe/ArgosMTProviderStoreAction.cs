using System.Diagnostics;
using System.Drawing;
using Sdl.Community.ArgosTranslateProvider;
using Sdl.TellMe.ProviderApi;

namespace Sdl.Community.ArgosTranslateTradosPlugin.TellMe
{
	public class ArgosMTProviderStoreAction : AbstractTellMeAction
	{
		public override bool IsAvailable => true;
		public override string Category => "Argos Translate MT Provider results";
		public override Icon Icon => PluginResources.Download;

		public ArgosMTProviderStoreAction()
		{
			Name = "Download the Argos Translate MT Provider from the AppStore";
		}

		public override void Execute()
		{
			Process.Start("https://appstore.sdl.com/language/app/argos-translate-mt-provider/925/");
		}
	}
}
