using Rope.Net.Sample.Core;

using UIKit;

namespace Rope.Net.iOS.Sample.Ui.ViewControllers
{
    public partial class SampleViewController : UIViewController
    {
        public SampleViewController() : base("SampleViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ViewModel = new SampleViewModel
            {
                Value = 50,
                Multiplier = 1
            };

            this.sliderValue.BindValue(this.ViewModel, vm => vm.Value);
            this.sliderMultiplier.BindValue(this.ViewModel, vm => vm.Multiplier);
            this.lblResult.Bind(
                this.ViewModel,
                vm => vm.Result,
                (lbl, val) => lbl.Text = $"Result: {val.ToString("F2")}");

            this.btnSubmit.BindEnable(this.ViewModel, vm => vm.CanSubmit);
        }

        public SampleViewModel ViewModel { get; private set; }
    }
}