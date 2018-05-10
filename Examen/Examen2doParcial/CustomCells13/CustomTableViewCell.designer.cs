// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CustomCells13
{
	[Register ("CustomTableViewCell")]
	partial class CustomTableViewCell
	{
		[Outlet]
		UIKit.UIImageView imgProfile { get; set; }

		[Outlet]
		UIKit.UILabel lbl { get; set; }

		[Outlet]
		UIKit.UILabel lbl2 { get; set; }

		[Action ("btnDogche_TouchUpInside:")]
		partial void btnDogche_TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (imgProfile != null) {
				imgProfile.Dispose ();
				imgProfile = null;
			}

			if (lbl != null) {
				lbl.Dispose ();
				lbl = null;
			}

			if (lbl2 != null) {
				lbl2.Dispose ();
				lbl2 = null;
			}
		}
	}
}
