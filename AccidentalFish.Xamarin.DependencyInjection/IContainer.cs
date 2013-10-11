using System;

namespace AccidentalFish.Xamarin.DependencyInjection
{
	public interface IContainer
	{
		void Register<T1>();
		void Register<T1, T2>();
		void Register (Type t1, Type t2);
		void Register (Type t1);
	    void RegisterInstance<T1>(T1 instance);
		T1 Resolve<T1>();
		object Resolve(Type t1);
	}
}

