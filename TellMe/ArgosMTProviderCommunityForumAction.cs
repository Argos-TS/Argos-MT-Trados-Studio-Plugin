using System.Diagnostics;
using System.Drawing;
using Sdl.Community.ArgosTranslateProvider;
using Sdl.TellMe.ProviderApi;

namespace Sdl.Community.ArgosTranslateTradosPlugin.TellMe
{
	public class ArgosMTProviderCommunityForumAction : AbstractTellMeAction
	{
		public override bool IsAvailable => true;
		public override string Category => "Argos MT Provider results";
		public override Icon Icon => PluginResources.ForumIcon;

		public ArgosMTProviderCommunityForumAction()
		{
			Name = "SDL Community AppStore Forum";
		}

		public override void Execute()
		{
			Process.Start("http://community.sdl.com/appsupport");
		}
	}
}