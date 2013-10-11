AccidentalFish.Xamarin.DependencyInjection
==========================================

This is a simple dependency injector for Xamarin.iOS with a twist - it can generate C# code and cut out all the nasty reflection business associated with most AoT compiler compatible injection containers.

It's a little scrappy at the moment and checked in quite early to allow a friend to use it - as such there are probably quite a few bugs.

To use it during development simple create a new instance of the Container class and it supports the usual set of Register<> and Resolve<> type methods.

When you're ready you can cast the Container into a ICodeGeneration interface and use the WriteToStream method to generate a C# class with your dependencies created by compilable code as opposed to reflection.

The tests in CsharpGeneratorTests.cs show how to do this. I'll make getting the code easier in a future version for now the easiest way is simply to grab the resulting string in the Xamarin IDE Locals inspector and paste it into a new file.

In the current version Instance registrations are not supported in the generated code though I am planning on adding support for them in a future release via a couple of mechanisms.

Additionally if you've used internals for your dependency implementations you will need to make them visibly to the assembly that contains the container (assuming it's in a different assembly). I've got some ideas about this for a future version (chained / nested containers).

TestContainer.cs is a good example of a code generated container.

The code is licensed under the MIT License so you can use it in commercial projects.
