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
using System.Diagnostics;
using System.Windows.Forms;
using Sdl.Community.ArgosTranslateProvider;
using Sdl.Community.ArgosTranslateTradosPlugin.ArgosConnect;
using Sdl.LanguagePlatform.Core;
using Sdl.LanguagePlatform.TranslationMemoryApi;

namespace Sdl.Community.ArgosTranslateTradosPlugin
{
    public partial class MtProviderConfDialog : Form
    {

		private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

		ITranslationProviderCredentialStore credstore;
        Uri uri;
		ApiConnector connector;
		LanguagePair langPair;

		public MtProviderConfDialog(MtTranslationOptions options, ITranslationProviderCredentialStore credentialStore, LanguagePair langPair)
        {
            this.credstore = credentialStore;
            uri = new Uri("argostranslateprovider:///");
            Options = options;
			connector = new ApiConnector(Options);
			this.langPair = langPair;
			InitializeComponent();
            UpdateDialog();
        }

        public MtProviderConfDialog(MtTranslationOptions options, string caption, ITranslationProviderCredentialStore credentialStore)
        {
            this.credstore = credentialStore;
            uri = new Uri("argostranslateprovider:///");
            Options = options;
			connector = new ApiConnector(Options);
			this.langPair = new LanguagePair();
			InitializeComponent();
            UpdateDialog();
            this.Text = caption;
        }

        public MtTranslationOptions Options
        {
            get;
            set;
        }

        private void UpdateDialog()
        {
            apiKeyTextBox.Text = Options.ApiKey;
            chkPlainTextOnly.Checked = Options.SendPlainTextOnly;

            this.Icon = MtProviderConfDialogResources.form_Icon;

            try
            {
                LoadResources();
            }
            catch { }
        }

        /// <summary>
        /// Loads strings to the form from our resources file
        /// </summary>
        private void LoadResources()
        {
            this.Text = MtProviderConfDialogResources.form_Text;
            this.btn_OK.Text = MtProviderConfDialogResources.btn_OK_Text;
            this.btn_Cancel.Text = MtProviderConfDialogResources.btn_Cancel_Text;
            this.chkPlainTextOnly.Text = MtProviderConfDialogResources.chkPlainTextOnly_Text;
            this.groupBox3.Text = MtProviderConfDialogResources.groupBox3_Text;
            this.tabPage1.Text = MtProviderConfDialogResources.tabPage1_Text;

            //create multiline tooltip text from strings in form resource file
            string ttip = MtProviderConfDialogResources.KeyForm_SaveKeyTooltip1;
            ttip += System.Environment.NewLine + MtProviderConfDialogResources.KeyForm_SaveKeyTooltip2;
            ttip += System.Environment.NewLine + MtProviderConfDialogResources.KeyForm_SaveKeyTooltip3;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            Options.ApiKey = apiKeyTextBox.Text;
            Options.SendPlainTextOnly = chkPlainTextOnly.Checked;
			Options.PersistCreds = true; // Delete creds via options button

			this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Enables or disables a set of controls based on whether the given checkbox is checked or unchecked. 
        /// </summary>
        /// <param name="enableSet">An array of Controls whose Enabled property is to be modified.</param>
        /// <param name="boxToConsult">The checkbox whose Checked value will be consulted.</param>
        private void editControlsSetEnabled(Control[] enableSet, CheckBox boxToConsult)
        {

            for (int i = 0; i < enableSet.Length; i++)
            {
                if (boxToConsult.Checked) enableSet[i].Enabled = true;
                else enableSet[i].Enabled = false;
            }

        }

        private bool ValidateForm()
        {
            string newLine = System.Environment.NewLine;
            string prompt = MtProviderConfDialogResources.validationMessageHeader + newLine;
            bool result = true;
			if (apiKeyTextBox.Text == string.Empty)
			{
				prompt += newLine + MtProviderConfDialogResources.validationMessageNoKey;
				result = false;
			}
			if (engineListBox.CheckedItems.Count == 0)
			{
				prompt += newLine + MtProviderConfDialogResources.validationMessageNoEngine;
				result = false;
			}
			if (!result)
            {
                MessageBox.Show(prompt);
            }
            return result;
        }

        private void btnDeleteSavedAPIKey_Click(object sender, EventArgs e)
        {
			credstore.RemoveCredential(uri);
        }

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://docs.argos-ts.com/");
		}

		private void apiKeyTextBox_TextChanged(object sender, EventArgs e)
		{
			Options.ApiKey = apiKeyTextBox.Text;
			connector = new ApiConnector(Options);
			var engines = connector.GetAvailableEngines().FindAll(engine => engine.MatchesLangPair(langPair));
			for (int i = 0; i < engineListBox.Items.Count; i++)
			{
				engineListBox.Items.RemoveAt(0);
			}
			engines.ForEach(engine => engineListBox.Items.Add(engine, engine.id == Options.CurrentEngineId));
			engineListBox.Refresh();
		}

		private void engineListBox_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			var checklist = sender as CheckedListBox;
			var selectedEngine = checklist.SelectedItem as Engine;
			if(selectedEngine != null)
			{
				for (int i = 0; i < engineListBox.Items.Count; i++)
				{
					if (i == e.Index) continue;
					engineListBox.SetItemCheckState(i, CheckState.Unchecked);
				}
				Options.CurrentEngineId = selectedEngine.id;
			}
		}
	}
}
