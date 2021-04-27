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
using Sdl.LanguagePlatform.TranslationMemoryApi;

namespace Sdl.Community.ArgosTranslateTradosPlugin
{
    /// <summary>
    /// This class is used to hold the provider plug-in settings. 
    /// All settings are automatically stored in a URI.
    /// </summary>
    public class MtTranslationOptions
    {

        private static string _apikey;

        //The translation method affects when/if the plugin gets called by Studio
        public static readonly TranslationMethod ProviderTranslationMethod = TranslationMethod.MachineTranslation;

        TranslationProviderUriBuilder _uriBuilder;

        public MtTranslationOptions()
        {
            _uriBuilder = new TranslationProviderUriBuilder(MtTranslationProvider.TranslationProviderScheme);
        }

        public MtTranslationOptions(Uri uri)
        {
            _uriBuilder = new TranslationProviderUriBuilder(uri);
        }

		[JsonIgnore]
		private string sendPlainTextOnly
		{
			get { return GetStringParameter("sendplaintextonly"); }
			set { SetStringParameter("sendplaintextonly", value); }
		}

		[JsonIgnore]
		public string CurrentEngineId
		{
			get { return GetStringParameter("currentengineid"); }
			set { SetStringParameter("currentengineid", value); }
		}

        [JsonIgnore]
        public string ApiKey
        {
            get { return _apikey; } 
            set { _apikey = value; }
        }

        [JsonIgnore]
        public bool PersistCreds
        {
            get;
            set;
        }

		[JsonIgnore]
		public bool SendPlainTextOnly
		{
			get { return Convert.ToBoolean(sendPlainTextOnly); }
			set { sendPlainTextOnly = value.ToString(); }
		}

		private void SetStringParameter(string p, string value)
        {
            _uriBuilder[p] = value;
        }

        private string GetStringParameter(string p)
        {
            string paramString = _uriBuilder[p];
            return paramString;
        }

        [JsonIgnore]
        public Uri Uri
        {
            get
            {
                return _uriBuilder.Uri;
            }
        }

    }


}
