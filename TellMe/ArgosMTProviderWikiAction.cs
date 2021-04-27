using System.Diagnostics;
using System.Drawing;
using Sdl.Community.ArgosTranslateProvider;
using Sdl.TellMe.ProviderApi;

namespace Sdl.Community.ArgosTranslateTradosPlugin.TellMe
{
	public class ArgosMTProviderWikiAction : AbstractTellMeAction
	{
		public override bool IsAvailable => true;
		public override string Category => "Argos Translate MT Provider results";
		public override Icon Icon => PluginResources.question;

		public ArgosMTProviderWikiAction()
		{
			Name = "SDL Community Argos Translate MT Provider Wiki";
		}

		public override void Execute()
		{
			Process.Start("https://community.sdl.com/product-groups/translationproductivity/w/customer-experience/3315/argos-translate-mt-provider");
		}
	}
}