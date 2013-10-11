using System;

namespace AccidentalFish.Xamarin.DependencyInjection.Tests.Model
{
	public interface IMultipleParameterObject
	{
		IShallowDependent ShallowDependent { get; }
		ISimpleObject SimpleObject { get; }
	}
}

