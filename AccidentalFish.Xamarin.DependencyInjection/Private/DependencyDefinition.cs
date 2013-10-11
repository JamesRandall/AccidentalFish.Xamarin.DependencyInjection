using System;

namespace AccidentalFish.Xamarin.DependencyInjection
{
	internal class DependencyDefinition
	{
		public Type Dependency { get; set; }

		public Type Implementation { get; set; }

		public object Instance { get; set; }
	}
}

