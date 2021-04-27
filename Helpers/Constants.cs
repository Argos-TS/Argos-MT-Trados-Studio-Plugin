using System;
using System.IO;

namespace Sdl.Community.ArgosTranslateTradosPlugin.Helpers
{
	public static class Constants
	{
		public readonly static string JsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"SDL Community\ArgosTranslationProvider");
		public readonly static string JsonFileName = "ArgosProviderSettings.json";
	}
}