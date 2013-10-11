using System;
using System.IO;

namespace AccidentalFish.Xamarin.DependencyInjection
{
	public enum LanguageEnum {
		Csharp
	}

	public interface ICodeGeneration
	{
		void WriteToStream(LanguageEnum language, string namespaceName, string containerClassName, Stream stream);
	}
}

