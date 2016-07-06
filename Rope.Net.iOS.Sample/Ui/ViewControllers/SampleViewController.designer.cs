// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//

using System.CodeDom.Compiler;

using Foundation;

namespace Rope.Net.iOS.Sample.Ui.ViewControllers
{
    [Register ("SampleViewController")]
    partial class SampleViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSubmit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblResult { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider sliderMultiplier { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider sliderValue { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (this.btnSubmit != null) {
                this.btnSubmit.Dispose ();
                this.btnSubmit = null;
            }

            if (this.lblResult != null) {
                this.lblResult.Dispose ();
                this.lblResult = null;
            }

            if (this.sliderMultiplier != null) {
                this.sliderMultiplier.Dispose ();
                this.sliderMultiplier = null;
            }

            if (this.sliderValue != null) {
                this.sliderValue.Dispose ();
                this.sliderValue = null;
            }
        }
    }
}