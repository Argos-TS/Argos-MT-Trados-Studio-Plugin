/* 

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.*/

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sdl.Community.ArgosTranslateProvider;
using Sdl.LanguagePlatform.Core;
using Sdl.LanguagePlatform.TranslationMemoryApi;

namespace Sdl.Community.ArgosTranslateTradosPlugin
{
    public class MtTranslationProvider : ITranslationProvider
    {
        ///<summary>
        /// This string needs to be a unique value.
        /// It is the string that precedes the plug-in URI.
        ///</summary>
        public static readonly string TranslationProviderScheme = "argostranslateprovider";
		private List<MtTranslationProviderLanguageDirection> _languageDirections;

		public MtTranslationOptions Options
        {
            get;
            set;
        }

        public MtTranslationProvider(MtTranslationOptions options)
        {
            Options = options;
			_languageDirections = new List<MtTranslationProviderLanguageDirection>();
		}

        public ITranslationProviderLanguageDirection GetLanguageDirection(LanguagePair languageDirection)
        {
			foreach (MtTranslationProviderLanguageDirection ld in _languageDirections)
			{
				if (ld.IsCompatible(languageDirection, Options.CurrentEngineId))
				{
					return ld;
				}
			}
			var newLd = new MtTranslationProviderLanguageDirection(this, languageDirection);
			_languageDirections.Add(newLd);
			return newLd;
        }

        public bool IsReadOnly => true;

        public void LoadState(string translationProviderState)
        {
	        Options = JsonConvert.DeserializeObject<MtTranslationOptions>(translationProviderState);
        }

        public string Name
        {
            get
            {
                return PluginResources.Plugin_Name;
            }
        }

        public void RefreshStatusInfo()
        {

        }

        public string SerializeState()
        {
            // Save settings
            return JsonConvert.SerializeObject(Options);
        }

        public ProviderStatusInfo StatusInfo => new ProviderStatusInfo(true, PluginResources.Plugin_NiceName);

        public bool SupportsConcordanceSearch { get; } = false;

        public bool SupportsDocumentSearches { get; } = false;

        public bool SupportsFilters { get; } = false;

        public bool SupportsFuzzySearch
        {
            get { return false; }
        }

        /// <summary>
        /// Determines the language direction of the delimited list file by
        /// reading the first line. Based upon this information it is determined
        /// whether the plug-in supports the language pair that was selected by
        /// the user.
        /// </summary>

        public bool SupportsLanguageDirection(LanguagePair languageDirection)
        {
			var languages = new List<string>{ "en", "es", "it", "pt", "de", "fr" };
			var sourceLanguage = languageDirection.SourceCulture.TwoLetterISOLanguageName.ToLower();
			var targetLanguage = languageDirection.TargetCulture.TwoLetterISOLanguageName.ToLower();
			return languages.Contains(sourceLanguage) && languages.Contains(targetLanguage);
        }

        public bool SupportsMultipleResults => false;

        public bool SupportsPenalties => true;

        public bool SupportsPlaceables => false;

        public bool SupportsScoring => false;

        public bool SupportsSearchForTranslationUnits => true;

        public bool SupportsSourceConcordanceSearch => false;

        public bool SupportsTargetConcordanceSearch => false;

        public bool SupportsStructureContext { get; } = false;

        public bool SupportsTaggedInput => true;

        public bool SupportsTranslation => true;

        public bool SupportsUpdate => false;

        public bool SupportsWordCounts => false;

        public TranslationMethod TranslationMethod => MtTranslationOptions.ProviderTranslationMethod;

        public Uri Uri => Options.Uri;

    }
}

