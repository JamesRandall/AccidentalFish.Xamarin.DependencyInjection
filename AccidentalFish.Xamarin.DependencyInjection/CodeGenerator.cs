using System;
using System.IO;
using AccidentalFish.Xamarin.DependencyInjection.Private;

namespace AccidentalFish.Xamarin.DependencyInjection
{
	public class CodeGenerator
	{
		public void WriteToStream (LanguageEnum language, IContainer container, Stream stream, string namespaceName, string containerClassName)
		{
			Container containerImpl = (Container)container;
			ICodeGenerator generator = null;
			if (language == LanguageEnum.Csharp) {
				generator = new CsharpCodeGenerator ();
			}

			if (generator == null)
			{
				throw new InvalidOperationException ("No generator for language");
			}

			generator.WriteToStream (containerImpl.Definitions, stream, namespaceName, containerClassName);
		}
	}
}

