using System;
using System.Reflection;

namespace AccidentalFish.Xamarin.DependencyInjection.Private
{
	internal static class TypeExtensions
	{
		public static ConstructorInfo GetShortestConstructor(this Type type)
		{
			ConstructorInfo[] constructors = type.GetConstructors (BindingFlags.Public | BindingFlags.Instance);
			int minParameters = int.MaxValue;
			ConstructorInfo constructor = null;

			foreach (ConstructorInfo candidate in constructors) {
				if (candidate.GetParameters ().Length < minParameters) {
					minParameters = candidate.GetParameters ().Length;
					constructor = candidate;
				}
			}

			return constructor;
		}
	}
}

