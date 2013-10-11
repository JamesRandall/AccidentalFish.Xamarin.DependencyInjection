using System;

namespace AccidentalFish.Xamarin.DependencyInjection
{
	public class DependencyException : Exception
	{
		public DependencyException (string message) : base(message)
		{
		}

		public DependencyException(string message, Exception innerException) : base(message, innerException)
		{
		}

	}
}

