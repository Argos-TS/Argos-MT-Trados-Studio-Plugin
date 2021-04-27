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
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Sdl.Community.ArgosTranslateTradosPlugin;
using Sdl.Community.ArgosTranslateProvider;
using Sdl.Community.ArgosTranslateTradosPlugin.ArgosConnect;
using Sdl.Community.ArgosTranslateTradosPlugin.Helpers;
using Sdl.LanguagePlatform.Core;
using Sdl.LanguagePlatform.TranslationMemoryApi;

namespace Sdl.Community.ArgosTranslateTradosPlugin
{

    [TranslationProviderWinFormsUi(
        Id = "MtTranslationProviderWinFormsUI",
        Name = "MtTranslationProviderWinFormsUI",
        Description = "MtTranslationProviderWinFormsUI")]

    public class MtTranslationProviderWinFormsUI : ITranslationProviderWinFormsUI
    {

        /// <summary>
        /// Show the plug-in settings form when the user is adding the translation provider plug-in
        /// through the GUI of SDL Trados Studio
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="languagePairs"></param>
        /// <param name="credentialStore"></param>
        /// <returns></returns>

        private TranslationProviderCredential GetMyCredentials(ITranslationProviderCredentialStore credentialStore, string uri)
        {
            var myUri = new Uri(uri);
            TranslationProviderCredential cred = null;

            if (credentialStore.GetCredential(myUri) != null)
            {

                //get the credential to return
                cred = new TranslationProviderCredential(credentialStore.GetCredential(myUri).Credential, credentialStore.GetCredential(myUri).Persist);
            }

            return cred;

        }

        private void SetCredentials(ITranslationProviderCredentialStore credentialStore, string creds, bool persistCred)
        { //used to set credentials
            // we are only setting and getting credentials for the uri with no parameters...kind of like a master credential
            var myUri = new Uri("argostranslateprovider:///");

            var cred = new TranslationProviderCredential(creds, persistCred);
            credentialStore.RemoveCredential(myUri);
            credentialStore.AddCredential(myUri, cred);
        }

		public ITranslationProvider[] Browse(IWin32Window owner, LanguagePair[] languagePairs, ITranslationProviderCredentialStore credentialStore)
        {

			// Implement error display
			if (languagePairs.Length > 1)
			{
				return null;
			}

			//construct options to send to form
			var loadOptions = new MtTranslationOptions();
            //get saved key if there is one and put it into options

            //get credentials
            var getCred = GetMyCredentials(credentialStore, "argostranslateprovider:///");
            if (getCred != null)
            {
                loadOptions.ApiKey = getCred.Credential;
                loadOptions.PersistCreds = getCred.Persist;
            }

            //construct form
            var dialog = new MtProviderConfDialog(loadOptions, credentialStore, languagePairs[0]);
            //we are letting user delete creds but after testing it seems that it's ok if the individual credentials are null, b/c our method will re-add them to the credstore based on the uri
            if (dialog.ShowDialog(owner) == DialogResult.OK)
            {
                var testProvider = new MtTranslationProvider(dialog.Options);
                SetCredentials(credentialStore, dialog.Options.ApiKey, dialog.Options.PersistCreds);

				return new ITranslationProvider[] { testProvider };
            }
            return null;
        }

        /// <summary>
        /// Determines whether the plug-in settings can be changed
        /// by displaying the Settings button in SDL Trados Studio.
        /// </summary>

        public bool SupportsEditing
        {
            get { return true; }
        }

        /// <summary>
        /// If the plug-in settings can be changed by the user,
        /// SDL Trados Studio will display a Settings button.
        /// By clicking this button, users raise the plug-in user interface,
        /// in which they can modify any applicable settings, in our implementation
        /// the delimiter character and the list file name.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="translationProvider"></param>
        /// <param name="languagePairs"></param>
        /// <param name="credentialStore"></param>
        /// <returns></returns>

        public bool Edit(IWin32Window owner, ITranslationProvider translationProvider, LanguagePair[] languagePairs, ITranslationProviderCredentialStore credentialStore)
        {
            var editProvider = translationProvider as MtTranslationProvider;
            if (editProvider == null)
            {
                return false;
            }

			// Implement error display
			if(languagePairs.Length > 1)
			{
				return false;
			}

            //get credentials
            var getCred = GetMyCredentials(credentialStore, "argostranslateprovider:///");
            if (getCred != null)
            {
				editProvider.Options.ApiKey = getCred.Credential;
				editProvider.Options.PersistCreds = getCred.Persist;
			}

			var dialog = new MtProviderConfDialog(editProvider.Options, credentialStore, languagePairs[0]);
            //we are letting user delete creds but after testing it seems that it's ok if the individual credentials are null, b/c our method will re-add them to the credstore based on the uri
            if (dialog.ShowDialog(owner) == DialogResult.OK)
            {
                SetCredentials(credentialStore, dialog.Options.ApiKey, dialog.Options.PersistCreds);
                return true;
            }

            return false;
        }

        /// <summary>
        /// This gets called when a TranslationProviderAuthenticationException is thrown
        /// Since SDL Studio doesn't pass the provider instance here and even if we do a workaround...
        /// any new options set in the form that comes up are never saved to the project XML...
        /// so there is no way to change any options, only to provide the credentials
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="translationProviderUri"></param>
        /// <param name="translationProviderState"></param>
        /// <param name="credentialStore"></param>
        /// <returns></returns>

        public bool GetCredentialsFromUser(IWin32Window owner, Uri translationProviderUri, string translationProviderState, ITranslationProviderCredentialStore credentialStore)
        {

            var options = new MtTranslationOptions(translationProviderUri);
            var caption = "Credentials"; //default in case any problem retrieving localized resource below
            caption = PluginResources.PromptForCredentialsCaption;

            var dialog = new MtProviderConfDialog(options, caption, credentialStore);

            if (dialog.ShowDialog(owner) == DialogResult.OK)
            {
                SetCredentials(credentialStore, dialog.Options.ApiKey, dialog.Options.PersistCreds);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Used for displaying the plug-in info such as the plug-in name,
        /// tooltip, and icon.
        /// </summary>
        /// <param name="translationProviderUri"></param>
        /// <param name="translationProviderState"></param>
        /// <returns></returns>

        public TranslationProviderDisplayInfo GetDisplayInfo(Uri translationProviderUri, string translationProviderState)
        {

            var info = new TranslationProviderDisplayInfo();
            var options = new MtTranslationOptions(translationProviderUri);
            info.TranslationProviderIcon = PluginResources.ArgosMT;

            info.Name = PluginResources.Plugin_NiceName;
            info.TooltipText = PluginResources.Plugin_Tooltip;
            info.SearchResultImage = PluginResources.argos_small;
            //TODO: update icon
            return info;
        }

        public bool SupportsTranslationProviderUri(Uri translationProviderUri)
        {
            if (translationProviderUri == null)
            {
                throw new ArgumentNullException(PluginResources.UriNotSupportedMessage);
            }
            return String.Equals(translationProviderUri.Scheme, MtTranslationProvider.TranslationProviderScheme, StringComparison.CurrentCultureIgnoreCase);
        }

        public string TypeDescription => PluginResources.Plugin_Description;

        public string TypeName => PluginResources.Plugin_NiceName;

	}
}
