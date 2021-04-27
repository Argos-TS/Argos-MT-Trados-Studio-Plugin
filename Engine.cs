using System.Collections.Generic;
using Sdl.LanguagePlatform.Core;

namespace Sdl.Community.ArgosTranslateTradosPlugin
{
	public class Engine
	{
		public string id { get; set; }
		public string name { get; set; }
		public string sourceLanguage { get; set; }
		public string targetLanguage { get; set; }

		public bool MatchesLangPair(LanguagePair languagePair)
		{
			return languagePair.SourceCulture.TwoLetterISOLanguageName.ToLower() == sourceLanguage
			&& languagePair.TargetCulture.TwoLetterISOLanguageName.ToLower() == targetLanguage;
		}

		override
		public string ToString()
		{
			return $"{this.name}";
		}
	}
}
