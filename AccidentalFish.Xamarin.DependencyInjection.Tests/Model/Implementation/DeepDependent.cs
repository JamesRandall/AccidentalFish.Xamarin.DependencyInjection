using System;
using AccidentalFish.Xamarin.DependencyInjection.Tests.Model;

namespace AccidentalFish.Xamarin.DependencyInjection.Tests.Model.Implementation
{
	public class DeepDependent : IDeepDependent
	{
		public DeepDependent (IShallowDependent shallowDependent)
		{
			ShallowDependent = shallowDependent;
		}


		public IShallowDependent ShallowDependent { get; private set; }
	}
}

