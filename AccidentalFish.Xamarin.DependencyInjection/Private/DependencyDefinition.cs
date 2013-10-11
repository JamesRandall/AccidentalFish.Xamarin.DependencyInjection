using System;

namespace AccidentalFish.Xamarin.DependencyInjection.Private
{
	internal class DependencyDefinition
	{
		public Type Dependency { get; set; }

		public Type Implementation { get; set; }

		public object Instance { get; set; }
	}
}

