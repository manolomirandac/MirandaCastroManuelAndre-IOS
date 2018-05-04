// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PhotoPicker09
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIBarButtonItem btnEdit { get; set; }

		[Outlet]
		UIKit.UIView CoverView { get; set; }

		[Outlet]
		UIKit.UIImageView imgCover { get; set; }

		[Outlet]
		UIKit.UIImageView imgProfile { get; set; }

		[Outlet]
		UIKit.UILabel lblCover { get; set; }

		[Outlet]
		UIKit.UILabel lblProfile { get; set; }

		[Outlet]
		UIKit.UIView profileView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnEdit != null) {
				btnEdit.Dispose ();
				btnEdit = null;
			}

			if (profileView != null) {
				profileView.Dispose ();
				profileView = null;
			}

			if (lblProfile != null) {
				lblProfile.Dispose ();
				lblProfile = null;
			}

			if (imgProfile != null) {
				imgProfile.Dispose ();
				imgProfile = null;
			}

			if (CoverView != null) {
				CoverView.Dispose ();
				CoverView = null;
			}

			if (imgCover != null) {
				imgCover.Dispose ();
				imgCover = null;
			}

			if (lblCover != null) {
				lblCover.Dispose ();
				lblCover = null;
			}
		}
	}
}
