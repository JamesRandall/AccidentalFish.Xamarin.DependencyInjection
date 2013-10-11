using System;
using AccidentalFish.Xamarin.DependencyInjection.Tests.Model;

namespace AccidentalFish.Xamarin.DependencyInjection.Tests.Implementation
{
	public class MultipleParameterObject : IMultipleParameterObject
	{
		public MultipleParameterObject (IShallowDependent shallowDependent, ISimpleObject simpleObject)
		{
			ShallowDependent = shallowDependent;
			SimpleObject = simpleObject;
		}

		public IShallowDependent ShallowDependent { get; private set; }

		public ISimpleObject SimpleObject { get; private set; }
	}
}

