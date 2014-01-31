using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ui.ios
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			CSharpBindingProject.SampleNativeObjCObject w = new CSharpBindingProject.SampleNativeObjCObject ();
			Console.WriteLine(w.returnThisString("test"));

			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
