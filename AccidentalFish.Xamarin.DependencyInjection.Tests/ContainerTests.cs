using System;
using NUnit.Framework;
using AccidentalFish.Xamarin.DependencyInjection.Tests.Model;
using AccidentalFish.Xamarin.DependencyInjection.Tests.Model.Implementation;

namespace AccidentalFish.Xamarin.DependencyInjection.Tests
{
	[TestFixture]
	public class ContainerTests
	{
		[Test]
		public void BasicRegistrationResolves()
		{
			// Arrange
			IContainer container = new Container ();
			container.Register<ISimpleObject, SimpleObject> ();

			// Act
			ISimpleObject result = container.Resolve<ISimpleObject> ();

			// Assert
			Assert.IsNotNull (result);
		}

		[Test]
		public void SimpleDependencyResolves()
		{
			// Arrange
			IContainer container = new Container ();
			container.Register<ISimpleObject, SimpleObject> ();
			container.Register<IShallowDependent, ShallowDependent> ();

			// Act
			IShallowDependent result = container.Resolve<IShallowDependent> ();

			// Assert
			Assert.IsNotNull (result);
			Assert.IsNotNull (result.SimpleObject);
		}
	}
}

