using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AccidentalFish.Xamarin.DependencyInjection.Private
{
	internal class CsharpCodeGenerator : ICodeGenerator
	{
		public void WriteToStream (Dictionary<Type, DependencyDefinition> definitions, Stream stream, string namespaceName, string containerClassName)
		{
			StringBuilder stringBuilder = new StringBuilder ();
			stringBuilder.AppendLine ("using System;");
			stringBuilder.AppendLine ("using System.Collections.Generic;");
			stringBuilder.AppendFormat ("namespace {0}\r\n", namespaceName);
			stringBuilder.AppendLine ("{");
			stringBuilder.AppendFormat ("    internal class {0} : AccidentalFish.Xamarin.DependencyInjection.IContainer\r\n", containerClassName);
			stringBuilder.AppendLine ("    {");
			stringBuilder.AppendLine ("        private readonly Dictionary<Type, Func<object>> _resolvers = new Dictionary<Type,Func<object>> {");

			bool first = true;
			foreach (DependencyDefinition definition in definitions.Values) {
				if (first) {
					first = false;
				} else {
					stringBuilder.Append (",");
				}
				stringBuilder.AppendFormat ("{{ typeof({0}), () => Resolve{1}() }}\r\n", definition.Dependency.FullName, definition.Dependency.Name);
			}
			stringBuilder.AppendLine ("    };");
			 
			stringBuilder.AppendLine ("        public void Register<T1>() { throw new NotImplementedException(); }");
			stringBuilder.AppendLine ("        public void Register<T1, T2> () { throw new NotImplementedException(); }");
			stringBuilder.AppendLine ("        public void Register (Type t1, Type t2) { throw new NotImplementedException(); }");
			stringBuilder.AppendLine ("        public void Register(Type t1) { throw new NotImplementedException(); }");
			stringBuilder.AppendLine ("        public void RegisterInstance<T>(T instance) { throw new NotImplementedException(); }");
			stringBuilder.AppendLine ("        public T1 Resolve<T1> () { return (T1)Resolve (typeof(T1)); }");
			stringBuilder.AppendLine ("        public object Resolve (Type t1) { return _resolvers[t1](); }");

			foreach (DependencyDefinition definition in definitions.Values) {
				// TODO: Need a better way of doing this
				stringBuilder.AppendFormat ("        private static {0} Resolve{1}()\r\n", definition.Dependency.FullName, definition.Dependency.Name);
				stringBuilder.AppendLine ("        {");
				stringBuilder.Append ("            return ");
				RecursivelyGenerate (stringBuilder, definition.Implementation, definitions);
				stringBuilder.AppendLine (";");
				stringBuilder.AppendLine ("        }");
			}

			stringBuilder.AppendLine ("    }");
			stringBuilder.AppendLine ("}");

			string result = stringBuilder.ToString ();
			StreamWriter streamWriter = new StreamWriter (stream);
			streamWriter.Write (result);
			streamWriter.Flush ();
		}

		private void RecursivelyGenerate(StringBuilder stringBuilder, Type type, Dictionary<Type, DependencyDefinition> definitions)
		{
			DependencyDefinition dependencyDefinition = null;
			if (definitions.TryGetValue (type, out dependencyDefinition)) {
				if (dependencyDefinition.Instance != null) {
					throw new InvalidOperationException ("Code generator does not yet support instances. Come back soon.");
				}
			}

			Type typeToResolve = dependencyDefinition != null ? dependencyDefinition.Implementation : type;

			ConstructorInfo constructor = typeToResolve.GetShortestConstructor ();

			ParameterInfo[] parameters = constructor.GetParameters ();

			stringBuilder.Append ("new ");
			stringBuilder.Append (typeToResolve.FullName);
			stringBuilder.Append ("(");
			if (parameters.Length > 0) {

				for (int parameterIndex=0; parameterIndex < parameters.Length; parameterIndex++) {
					if (parameterIndex > 0) {
						stringBuilder.Append (", ");
					}
					ParameterInfo parameter = parameters [parameterIndex];
					Type parameterType = parameter.ParameterType;
					RecursivelyGenerate (stringBuilder, parameterType, definitions);
				}
			}
			stringBuilder.Append (")");
		}
	}
}

