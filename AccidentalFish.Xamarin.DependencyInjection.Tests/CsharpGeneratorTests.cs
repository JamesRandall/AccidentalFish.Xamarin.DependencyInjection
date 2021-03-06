using System;
using NUnit.Framework;
using AccidentalFish.Xamarin.DependencyInjection.Tests.Model;
using AccidentalFish.Xamarin.DependencyInjection.Tests.Model.Implementation;
using System.IO;
using AccidentalFish.Xamarin.DependencyInjection.Tests.Implementation;
using SomeNamespace;

namespace AccidentalFish.Xamarin.DependencyInjection.Tests
{
	[TestFixture]
	public class CsharpGeneratorTests
	{
		[Test]
		public void SimpleContainerGeneratesCode()
		{
			// Arrange
			IContainer container = new Container ();
			container.Register<ISimpleObject, SimpleObject> ();

			CodeGenerator codeGenerator = new CodeGenerator ();
			MemoryStream memoryStream = new MemoryStream (); 

			// Act
			codeGenerator.WriteToStream(LanguageEnum.Csharp, container, memoryStream, "SomeNamespace", "TestContainer");
			memoryStream.Position = 0;
			string charpString;
			using (StreamReader reader = new StreamReader(memoryStream)) {
				charpString = reader.ReadToEnd ();
			}

			// Assert
			Assert.IsNotNull (charpString);
		}

		[Test]
		public void DeepContainerGeneratesCode()
		{
			// Arrange
			IContainer container = new Container ();
			container.Register<ISimpleObject, SimpleObject> ();
			container.Register<IShallowDependent, ShallowDependent> ();
			container.Register<IDeepDependent, DeepDependent> ();
			CodeGenerator codeGenerator = new CodeGenerator ();
			MemoryStream memoryStream = new MemoryStream (); 

			// Act
			codeGenerator.WriteToStream(LanguageEnum.Csharp, container, memoryStream, "SomeNamespace", "TestContainer");
			memoryStream.Position = 0;
			string charpString;
			using (StreamReader reader = new StreamReader(memoryStream)) {
				charpString = reader.ReadToEnd ();
			}

			// Assert
			Assert.IsNotNull (charpString);
		}

		[Test]
		public void MultipleParameterGeneratesCode()
		{
			// Arrange
			IContainer container = new Container ();
			container.Register<ISimpleObject, SimpleObject> ();
			container.Register<IShallowDependent, ShallowDependent> ();
			container.Register<IDeepDependent, DeepDependent> ();
			container.Register<IMultipleParameterObject, MultipleParameterObject> ();
			CodeGenerator codeGenerator = new CodeGenerator ();
			MemoryStream memoryStream = new MemoryStream (); 

			// Act
			codeGenerator.WriteToStream(LanguageEnum.Csharp, container, memoryStream, "SomeNamespace", "TestContainer");
			memoryStream.Position = 0;
			string charpString;
			using (StreamReader reader = new StreamReader(memoryStream)) {
				charpString = reader.ReadToEnd ();
			}
			TestContainer testContainer = new TestContainer ();
			IMultipleParameterObject result = testContainer.Resolve<IMultipleParameterObject> ();

			// Assert
			Assert.IsNotNull (result);
			Assert.IsNotNull (result.ShallowDependent);
			Assert.IsNotNull (result.SimpleObject);
		}
	}
}

