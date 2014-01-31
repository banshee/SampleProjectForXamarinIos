using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libSampleNativeProject.a", LinkTarget.Simulator, ForceLoad = true)]
