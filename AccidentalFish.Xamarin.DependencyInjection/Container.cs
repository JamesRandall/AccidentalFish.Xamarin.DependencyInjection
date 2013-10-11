using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace AccidentalFish.Xamarin.DependencyInjection
{
	public class Container : IContainer, ICodeGeneration
	{
		private Dictionary<Type, DependencyDefinition> _definitions = new Dictionary<Type, DependencyDefinition>();
		private Dictionary<Type, ConstructorInfo> _constructors = new Dictionary<Type, ConstructorInfo>();

		#region IContainer implementation

		public void Register<T1>()
		{
			Register<T1,T1> ();
		}

		public void Register<T1, T2> ()
		{
			_definitions [typeof(T1)] = new DependencyDefinition {
				Dependency = typeof(T1),
				Implementation = typeof(T2),
				Instance = null
			};
		}

		public void Register (Type t1, Type t2)
		{
			_definitions [t1] = new DependencyDefinition {
				Dependency = t1,
				Implementation = t2,
				Instance = null
			};
		}

		public void Register(Type t1)
		{
			Register (t1, t1);
		}

		public void RegisterInstance<T>(T instance)
		{
			_definitions [typeof(T)] = new DependencyDefinition {
				Dependency = typeof(T),
				Implementation = typeof(T),
				Instance = instance
			};
		}

		public T1 Resolve<T1> ()
		{
			return (T1)Resolve (typeof(T1));
		}

		public object Resolve (Type t1)
		{
			DependencyDefinition rootDependency;
			if (!_definitions.TryGetValue (t1, out rootDependency)) {
				throw new DependencyException (String.Format("Root type {0} not registered", t1.Name));
			}

			return RecursivelyResolve (t1);
		}

		#endregion

		private object RecursivelyResolve(Type type)
		{
			object resolvedObject = null;
			DependencyDefinition dependencyDefinition = null;
			if (_definitions.TryGetValue (type, out dependencyDefinition)) {
				if (dependencyDefinition.Instance != null) {
					return dependencyDefinition.Instance;
				}
			}

			Type typeToResolve = dependencyDefinition != null ? dependencyDefinition.Implementation : type;

			ConstructorInfo constructor;
			if (!_constructors.TryGetValue (typeToResolve, out constructor)) {
				constructor = GetShortestConstructor (typeToResolve);
				_constructors [typeToResolve] = constructor;
			}

			ParameterInfo[] parameters = constructor.GetParameters ();

			if (parameters.Length > 0) {
				object[] values = new object[parameters.Length];

				for (int parameterIndex=0; parameterIndex < parameters.Length; parameterIndex++) {
					ParameterInfo parameter = parameters [parameterIndex];
					Type parameterType = parameter.ParameterType;
					values [parameterIndex] = RecursivelyResolve (parameterType);
				}

				resolvedObject = Activator.CreateInstance (typeToResolve, values);
			} else {
				resolvedObject = Activator.CreateInstance (typeToResolve);
			}

			return resolvedObject;
		}

		private ConstructorInfo GetShortestConstructor(Type type)
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

		#region ICodeGeneration implementation

		void ICodeGeneration.WriteToStream(LanguageEnum language, Stream stream)
		{
			CsharpGenerator generator = new CsharpGenerator ();
			generator.WriteToStream (stream, _definitions);
		}

		#endregion
	}
}

