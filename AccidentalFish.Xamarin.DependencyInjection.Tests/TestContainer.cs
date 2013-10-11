using System;
using System.Collections.Generic;
namespace SomeNamespace
{
	internal class TestContainer : AccidentalFish.Xamarin.DependencyInjection.IContainer
	{
		private readonly Dictionary<Type, Func<object>> _resolvers = new Dictionary<Type,Func<object>> {
			{ typeof(AccidentalFish.Xamarin.DependencyInjection.Tests.Model.ISimpleObject), () => ResolveISimpleObject() }
			,{ typeof(AccidentalFish.Xamarin.DependencyInjection.Tests.Model.IShallowDependent), () => ResolveIShallowDependent() }
			,{ typeof(AccidentalFish.Xamarin.DependencyInjection.Tests.Model.IDeepDependent), () => ResolveIDeepDependent() }
			,{ typeof(AccidentalFish.Xamarin.DependencyInjection.Tests.Model.IMultipleParameterObject), () => ResolveIMultipleParameterObject() }
		};
		public void Register<T1>() { throw new NotImplementedException(); }
		public void Register<T1, T2> () { throw new NotImplementedException(); }
		public void Register (Type t1, Type t2) { throw new NotImplementedException(); }
		public void Register(Type t1) { throw new NotImplementedException(); }
		public void RegisterInstance<T>(T instance) { throw new NotImplementedException(); }
		public T1 Resolve<T1> () { return (T1)Resolve (typeof(T1)); }
		public object Resolve (Type t1) { return _resolvers[t1](); }
		private static AccidentalFish.Xamarin.DependencyInjection.Tests.Model.ISimpleObject ResolveISimpleObject()
		{
			return new AccidentalFish.Xamarin.DependencyInjection.Tests.Model.Implementation.SimpleObject();
		}
		private static AccidentalFish.Xamarin.DependencyInjection.Tests.Model.IShallowDependent ResolveIShallowDependent()
		{
			return new AccidentalFish.Xamarin.DependencyInjection.Tests.Model.Implementation.ShallowDependent(new AccidentalFish.Xamarin.DependencyInjection.Tests.Model.Implementation.SimpleObject());
		}
		private static AccidentalFish.Xamarin.DependencyInjection.Tests.Model.IDeepDependent ResolveIDeepDependent()
		{
			return new AccidentalFish.Xamarin.DependencyInjection.Tests.Model.Implementation.DeepDependent(new AccidentalFish.Xamarin.DependencyInjection.Tests.Model.Implementation.ShallowDependent(new AccidentalFish.Xamarin.DependencyInjection.Tests.Model.Implementation.SimpleObject()));
		}
		private static AccidentalFish.Xamarin.DependencyInjection.Tests.Model.IMultipleParameterObject ResolveIMultipleParameterObject()
		{
			return new AccidentalFish.Xamarin.DependencyInjection.Tests.Implementation.MultipleParameterObject(new AccidentalFish.Xamarin.DependencyInjection.Tests.Model.Implementation.ShallowDependent(new AccidentalFish.Xamarin.DependencyInjection.Tests.Model.Implementation.SimpleObject()), new AccidentalFish.Xamarin.DependencyInjection.Tests.Model.Implementation.SimpleObject());
		}
	}
}
