using System;
using System.Collections.Generic;
using Sdl.Community.ArgosTranslateTradosPlugin.ArgosConnect;
using Sdl.Core.Globalization;
using Sdl.LanguagePlatform.Core;
using Sdl.LanguagePlatform.TranslationMemory;
using Sdl.LanguagePlatform.TranslationMemoryApi;

namespace Sdl.Community.ArgosTranslateTradosPlugin
{
    public class MtTranslationProviderLanguageDirection : ITranslationProviderLanguageDirection
    {

        private readonly MtTranslationProvider _provider;
        private readonly LanguagePair _languageDirection;
        private readonly MtTranslationOptions _options;
        private TranslationUnit _inputTu;
        private ApiConnector _connector;

        /// <summary>
        /// Instantiates the variables and fills the list file content into
        /// a Dictionary collection object.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="languages"></param>

        public MtTranslationProviderLanguageDirection(MtTranslationProvider provider, LanguagePair languages)
        {

            _provider = provider;
            _languageDirection = languages;
            _options = _provider.Options;

        }

        public System.Globalization.CultureInfo SourceLanguage => _languageDirection.SourceCulture;

        public System.Globalization.CultureInfo TargetLanguage => _languageDirection.TargetCulture;

        public ITranslationProvider TranslationProvider => _provider;

		public bool CanReverseLanguageDirection => false;

		private string Lookup(string sourcetext, MtTranslationOptions options)
        {
            var sourcelang = _languageDirection.SourceCulture.ToString();
            var targetlang = _languageDirection.TargetCulture.ToString();
            //instantiate ApiConnector if necessary
            if (_connector == null)
            {
                _connector = new ApiConnector(_options);
            }

            var translatedText = _connector.Translate(sourcelang, targetlang, sourcetext);
            return translatedText;
        }


        /// <summary>
        /// Performs the actual search by looping through the
        /// delimited segment pairs contained in the text file.
        /// Depening on the search mode, a segment lookup (with exact machting) or a source / target
        /// concordance search is done.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="segment"></param>
        /// <returns></returns>

        public SearchResults SearchSegment(SearchSettings settings, Segment segment)
        {
            var translation = new Segment(_languageDirection.TargetCulture);//this will be the target segment

            var results = new SearchResults();
            results.SourceSegment = segment.Duplicate();

			
            if (_inputTu.TargetSegment != null && !_inputTu.TargetSegment.IsEmpty)
            {
                results.Add(CreateSearchResult(segment, translation, segment.ToString()));
                return results;
            }
			
            // Look up the currently selected segment in the collection (normal segment lookup).

            var translatedText = "";
            //a new seg avoids modifying the current segment object
            var newseg = segment.Duplicate();

            //do preedit if checked
            var sendTextOnly = _options.SendPlainTextOnly || !newseg.HasTags;
            if (!sendTextOnly)
            {
                //return our tagged target segment
                var tagplacer = new MtTranslationProviderTagPlacer(newseg);
                ////tagplacer is constructed and gives us back a properly marked up source string 
                translatedText = Lookup(tagplacer.PreparedSourceText, _options);

                //now we send the output back to tagplacer for our properly tagged segment
                translation = tagplacer.GetTaggedSegment(translatedText).Duplicate();
            }
            else //only send plain text
            {
                var sourcetext = newseg.ToPlain();
                //Do lookup
                translatedText = Lookup(sourcetext, _options);
                translation.Add(translatedText);
            }

            results.Add(CreateSearchResult(newseg, translation, newseg.ToPlain()));
            return results;

        }

        /// <summary>
        /// Used to do batch find-replace on a segment with tags.
        /// </summary>
        /// <param name="inSegment"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private Segment GetEditedSegment(SegmentEditor editor, Segment inSegment)
        {

            var newSeg = new Segment(inSegment.Culture);

            foreach (var element in inSegment.Elements)
            {
                var elType = element.GetType();

                if (elType.ToString() != "Sdl.LanguagePlatform.Core.Tag") //if other than tag, make string and edit it
                {
                    var temp = editor.EditText(element.ToString());
                    newSeg.Add(temp); //add edited text to segment
                }
                else
                {
                    newSeg.Add(element); //if tag just add the tag
                }
            }
            return newSeg;
        }

        /// <summary>
        /// Used to do batch find-replace on a string of plain text.
        /// </summary>
        /// <param name="sourcetext"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetEditedString(SegmentEditor editor, string sourcetext)
        {
            var result = editor.EditText(sourcetext);
            return result;
        }
        /// <summary>
        /// Creates the translation unit as it is later shown in the Translation Results
        /// window of SDL Trados Studio. This member also determines the match score
        /// (in our implementation always 100%, as only exact matches are supported)
        /// as well as the confirmation level, i.e. Translated.
        /// </summary>
        /// <param name="searchSegment"></param>
        /// <param name="translation"></param>
        /// <param name="sourceSegment"></param>
        /// <returns></returns>

        private SearchResult CreateSearchResult(Segment searchSegment, Segment translation,
            string sourceSegment)
        {

            var tu = new TranslationUnit();
            tu.SourceSegment = searchSegment.Duplicate();//this makes the original source segment, with tags, appear in the search window
            tu.TargetSegment = translation;

            tu.ResourceId = new PersistentObjectToken(tu.GetHashCode(), Guid.Empty);

            var score = 0; //score to 0...change if needed to support scoring
            tu.Origin = TranslationUnitOrigin.MachineTranslation;
            var searchResult = new SearchResult(tu);
            searchResult.ScoringResult = new ScoringResult();
            searchResult.ScoringResult.BaseScore = score;
            tu.ConfirmationLevel = ConfirmationLevel.Draft;

            return searchResult;
        }

        public SearchResults[] SearchSegments(SearchSettings settings, Segment[] segments)
        {
            var results = new SearchResults[segments.Length];
            for (var p = 0; p < segments.Length; ++p)
            {
                results[p] = SearchSegment(settings, segments[p]);
            }
            return results;
        }

        public SearchResults[] SearchSegmentsMasked(SearchSettings settings, Segment[] segments, bool[] mask)
        {
            if (segments == null)
            {
                throw new ArgumentNullException("segments in SearchSegmentsMasked");
            }
            if (mask == null || mask.Length != segments.Length)
            {
                throw new ArgumentException("mask in SearchSegmentsMasked");
            }

            var results = new SearchResults[segments.Length];
            for (var p = 0; p < segments.Length; ++p)
            {
                if (mask[p])
                {
                    results[p] = SearchSegment(settings, segments[p]);
                }
                else
                {
                    results[p] = null;
                }
            }
            return results;
        }

        public SearchResults SearchText(SearchSettings settings, string segment)
        {
            var currentSegment = new Segment(_languageDirection.SourceCulture);
            currentSegment.Add(segment);
            return SearchSegment(settings, currentSegment);
        }

        public SearchResults SearchTranslationUnit(SearchSettings settings, TranslationUnit translationUnit)
        {
            //need to use the tu confirmation level in searchsegment method
            _inputTu = translationUnit;
            return SearchSegment(settings, translationUnit.SourceSegment);
        }

        public SearchResults[] SearchTranslationUnits(SearchSettings settings, TranslationUnit[] translationUnits)
        {
            var results = new SearchResults[translationUnits.Length];
            for (var p = 0; p < translationUnits.Length; ++p)
            {
                //need to use the tu confirmation level in searchsegment method
                _inputTu = translationUnits[p];
                results[p] = SearchSegment(settings, translationUnits[p].SourceSegment); //changed this to send whole tu
            }
            return results;
        }

        public SearchResults[] SearchTranslationUnitsMasked(SearchSettings settings, TranslationUnit[] translationUnits, bool[] mask)
        {
			// bug LG-15128 where mask parameters are true for both CM and the actual TU to be updated which cause an unnecessary call for CM segment
	        var results = new List<SearchResults>();
	        var i = 0;
	        foreach (var tu in translationUnits)
	        {
		        if (mask == null || mask[i])
		        {
			        var result = SearchTranslationUnit(settings, tu);
			        results.Add(result);
		        }
		        else
		        {
			        results.Add(null);
		        }
		        i++;
	        }
	        return results.ToArray();
		}

		public bool IsCompatible(LanguagePair languageDirection, string engineId)
		{
			return languageDirection.SourceCulture.Equals(SourceLanguage)
				&& languageDirection.TargetCulture.Equals(TargetLanguage)
				&& _options.CurrentEngineId == engineId;
		}

        /// <summary>
        /// Not required for this implementation.
        /// </summary>
        /// <param name="translationUnits"></param>
        /// <param name="settings"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public ImportResult[] AddTranslationUnitsMasked(TranslationUnit[] translationUnits, ImportSettings settings, bool[] mask)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not required for this implementation.
        /// </summary>
        /// <param name="translationUnit"></param>
        /// <returns></returns>
        public ImportResult UpdateTranslationUnit(TranslationUnit translationUnit)
        {

            throw new NotImplementedException();

        }

        /// <summary>
        /// Not required for this implementation.
        /// </summary>
        /// <param name="translationUnits"></param>
        /// <returns></returns>
        public ImportResult[] UpdateTranslationUnits(TranslationUnit[] translationUnits)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Not required for this implementation.
        /// </summary>
        /// <param name="translationUnits"></param>
        /// <param name="previousTranslationHashes"></param>
        /// <param name="settings"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public ImportResult[] AddOrUpdateTranslationUnitsMasked(TranslationUnit[] translationUnits, int[] previousTranslationHashes, ImportSettings settings, bool[] mask)
        {
            ImportResult[] result = { AddTranslationUnit(translationUnits[translationUnits.GetLength(0) - 1], settings) };
            return result;
        }

        /// <summary>
        /// Not required for this implementation.
        /// </summary>
        /// <param name="translationUnit"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public ImportResult AddTranslationUnit(TranslationUnit translationUnit, ImportSettings settings)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not required for this implementation.
        /// </summary>
        /// <param name="translationUnits"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public ImportResult[] AddTranslationUnits(TranslationUnit[] translationUnits, ImportSettings settings)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not required for this implementation.
        /// </summary>
        /// <param name="translationUnits"></param>
        /// <param name="previousTranslationHashes"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public ImportResult[] AddOrUpdateTranslationUnits(TranslationUnit[] translationUnits, int[] previousTranslationHashes, ImportSettings settings)
        {
            throw new NotImplementedException();
        }

    }
}
