using System;
using System.IO;

namespace AccidentalFish.Xamarin.DependencyInjection
{
	public enum LanguageEnum {
		Csharp
	}

	public interface ICodeGeneration
	{
		void WriteToStream(LanguageEnum language, Stream stream);
	}
}

