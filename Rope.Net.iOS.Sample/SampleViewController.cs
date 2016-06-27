using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Rope.Net.iOS.Sample.Annotations;

using UIKit;

namespace Rope.Net.iOS.Sample
{
    public partial class SampleViewController : UIViewController
    {
        public SampleViewController() : base("SampleViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ViewNodel = new SampleViewModel
            {
                Value = 50,
                Multiplier = 1
            };

            this.sliderValue.BindValue(this.ViewNodel, vm => vm.Value);
            this.sliderMultiplier.BindValue(this.ViewNodel, vm => vm.Multiplier);
            this.lblResult.Bind(
                this.ViewNodel,
                vm => vm.Result,
                (lbl, val) => lbl.Text = $"Result: {val.ToString("F2")}");

            this.btnSubmit.BindEnable(this.ViewNodel, vm => vm.CanSubmit);
        }

        public SampleViewModel ViewNodel { get; private set; }
    }

    public class SampleViewModel : INotifyPropertyChanged
    {
        public float Value { get; set; }

        public float Multiplier { get; set; }

        public float Result => this.Multiplier * this.Value;

        public bool CanSubmit => this.Result > 3000;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}