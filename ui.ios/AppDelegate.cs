using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using com.example;

namespace ui.ios
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	// XXX explain "Register"
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		ui_iosViewController viewController;
		TimerService timerService;

		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			// XXX explain ui_iosViewController
			viewController = new ui_iosViewController ();
			window.RootViewController = viewController;
			window.MakeKeyAndVisible ();

			timerService = new TimerService ();
			timerService.StartTimer ();

			return true;
		}

		public override UIWindow Window {
			get;
			set;
		}

		public TimerService GetTimerService ()
		{
			return timerService;
		}
	}
}

