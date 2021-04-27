using Sdl.TellMe.ProviderApi;

namespace Sdl.Community.ArgosTranslateTradosPlugin.TellMe
{
	[TellMeProvider]
	public class ArgosMTTellMeProvider : ITellMeProvider
	{
		public string Name => "Argos Translate MT Provider tell me provider";

		public AbstractTellMeAction[] ProviderActions => new AbstractTellMeAction[]
		{
			new ArgosMTProviderWikiAction
			{
				Keywords = new[] { "argos translate mt provider", "argos mt provider", "argos", "argos community", "argos support", "argos wiki" }
			},
			new ArgosMTProviderCommunityForumAction
			{
				Keywords = new[] { "argos translate mt provider", "argos mt provider", "argos", "argos community", "argos support", "argos forum" }
			},
			new ArgosMTProviderStoreAction
			{
				Keywords = new[] { "argos translate mt provider", "argos mt provider", "argos", "argos store", "argos download", "argos appstore" }
			}
		};
	}
}