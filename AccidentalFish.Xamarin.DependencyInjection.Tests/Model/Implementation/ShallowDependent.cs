using System;
using AccidentalFish.Xamarin.DependencyInjection.Tests.Model;

namespace AccidentalFish.Xamarin.DependencyInjection.Tests.Model.Implementation
{
	public class ShallowDependent : IShallowDependent
	{
		public ShallowDependent (ISimpleObject simpleObject)
		{
			SimpleObject = simpleObject;
		}

		public ISimpleObject SimpleObject { get; private set; }
	}
}

