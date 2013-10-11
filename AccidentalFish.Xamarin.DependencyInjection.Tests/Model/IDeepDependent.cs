using System;

namespace AccidentalFish.Xamarin.DependencyInjection.Tests.Model
{
	public interface IDeepDependent
	{
		IShallowDependent ShallowDependent { get; }
	}
}

