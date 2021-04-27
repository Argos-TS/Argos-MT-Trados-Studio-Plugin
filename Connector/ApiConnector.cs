using System.Collections.Generic;
using System.Globalization;
using System;
using System.Net.Http;
using Sdl.LanguagePlatform.Core;
using System.Text;
using Newtonsoft.Json;

namespace Sdl.Community.ArgosTranslateTradosPlugin.ArgosConnect
{
    internal class ApiConnector
    {
		private MtTranslationOptions _options;
		private string _apiKey = string.Empty;
		private string _engineId = string.Empty;
		private HttpClient Client = new HttpClient();

		/// <summary>
		/// This class allows connection to the ArgosTranslate API
		/// </summary>
		/// <param name="options"></param>
		internal ApiConnector(MtTranslationOptions options)
        {
            _options = options;
			_apiKey = _options.ApiKey;
			_engineId = _options.CurrentEngineId;

			Client.Timeout = TimeSpan.FromMinutes(5);
			Client.DefaultRequestHeaders.Add("User-Agent", "SDL Trados plugin");
		}

		private string _QueryServer(string endpoint, Dictionary<string, string> args = null)
		{
			var result = "";
			var request = new HttpRequestMessage()
			{
				RequestUri = new Uri($"https://api.argos-ts.com/{endpoint}"),
				Method = HttpMethod.Get,
			};

			request.Headers.Add("key", _apiKey);
			if(args != null)
			{
				foreach (KeyValuePair<string, string> entry in args)
				{
					request.Headers.Add(entry.Key, entry.Value);
				}
			}
			var rawResponse = Client.SendAsync(request).Result;
			rawResponse.EnsureSuccessStatusCode();
			result = rawResponse.Content?.ReadAsStringAsync().Result;
			return result;
		}

		/// <summary>
		/// Replaces unallowed charaters with their equivalents
		/// </summary>
		/// <param name="text"></param>
		/// <returns>
		/// Normalized text
		/// </returns>
		private string NormalizeText(string text)
		{
			text = text
				.Replace((char)0x2019, (char)0x27)
				.Replace((char)0x2018, (char)0x27);
			return text;
		}

		/// <summary>
		/// translates the text input
		/// </summary>
		/// <param name="sourceLang"></param>
		/// <param name="targetLang"></param>
		/// <param name="textToTranslate"></param>
		/// <param name="categoryId"></param>
		/// <param name="format"></param>
		/// <returns></returns>
		internal string Translate(string sourceLang, string targetLang, string textToTranslate)
        {
            var translatedText = string.Empty;
            try
            {
				var response = this._QueryServer($"translate/{_engineId}", new Dictionary<string, string>(){
					{"q", NormalizeText(textToTranslate)},
				});
				var result = JsonConvert.DeserializeObject<TranslationResponse>(response);
				System.Diagnostics.Debug.WriteLine(result.responseData.translatedText);
                translatedText = result.responseData.translatedText;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw e;
            }
            return translatedText;
        }

		public List<Engine> GetAvailableEngines()
		{
			try
			{
				var response = this._QueryServer("listPairs");
				var parsedResponse = JsonConvert.DeserializeObject<ListPairsResponse>(response);
				parsedResponse.responseData.Sort(delegate (Engine a, Engine b)
				{
					var sourceComparison = a.sourceLanguage.CompareTo(b.sourceLanguage);
					if (sourceComparison != 0) return sourceComparison;
					var targetComparison = a.targetLanguage.CompareTo(b.targetLanguage);
					if (targetComparison != 0) return targetComparison;
					return a.name.CompareTo(b.name);
				});
				return parsedResponse.responseData;
			}
			catch (Exception e)
			{
				return new List<Engine>();
			}
		}

		private string convertLangCode(string languageCode)
        {
            var ci = new CultureInfo(languageCode); //construct a CultureInfo object with the language code
            return ci.TwoLetterISOLanguageName;
        }

    }
}
