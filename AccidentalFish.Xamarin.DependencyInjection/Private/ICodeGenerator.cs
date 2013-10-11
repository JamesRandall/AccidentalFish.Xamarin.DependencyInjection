using System;
using System.IO;
using System.Collections.Generic;
using AccidentalFish.Xamarin.DependencyInjection.Private;

namespace AccidentalFish.Xamarin.DependencyInjection
{
	public enum LanguageEnum {
		Csharp
	}

	internal interface ICodeGenerator
	{
		void WriteToStream (Dictionary<Type, DependencyDefinition> definitions, Stream stream, string namespaceName, string containerClassName);
	}
}

