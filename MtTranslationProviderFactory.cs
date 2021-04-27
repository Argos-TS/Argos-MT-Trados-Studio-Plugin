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
using Sdl.Community.ArgosTranslateProvider;
using Sdl.LanguagePlatform.TranslationMemoryApi;

namespace Sdl.Community.ArgosTranslateTradosPlugin
{

    [TranslationProviderFactory(
        Id = "MtTranslationProviderFactory",
        Name = "MtTranslationProviderFactory",
        Description = "Argos Translate Trados Plugin")]

    public class MtTranslationProviderFactory : ITranslationProviderFactory
    {

        public ITranslationProvider CreateTranslationProvider(Uri translationProviderUri, string translationProviderState, ITranslationProviderCredentialStore credentialStore)
        {

            if (!SupportsTranslationProviderUri(translationProviderUri))
            {
                throw new Exception(PluginResources.UriNotSupportedMessage);
            }

            //create options class based on URI passed to the method
            var loadOptions = new MtTranslationOptions(translationProviderUri);

            var myUri = new Uri("argostranslateprovider:///");
			var creds = credentialStore.GetCredential(myUri);
			if (creds != null && creds.Credential != String.Empty)
            {
                loadOptions.ApiKey = creds.Credential;
            }
            var tp = new MtTranslationProvider(loadOptions);

            return tp;
        }

        public bool SupportsTranslationProviderUri(Uri translationProviderUri)
        {

            if (translationProviderUri == null)
            {
                throw new ArgumentNullException(PluginResources.UriNotSupportedMessage);
            }
            return String.Equals(translationProviderUri.Scheme, MtTranslationProvider.TranslationProviderScheme, StringComparison.OrdinalIgnoreCase);
        }

        public TranslationProviderInfo GetTranslationProviderInfo(Uri translationProviderUri, string translationProviderState)
        {
            var info = new TranslationProviderInfo();

            info.TranslationMethod = MtTranslationOptions.ProviderTranslationMethod;

            info.Name = PluginResources.Plugin_NiceName;

            return info;
        }

    }
}
