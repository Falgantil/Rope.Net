using System;

using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;

using Rope.Net.Sample.Core;

namespace Rope.Net.Android.Sample.Ui.Activities
{
    [Activity(Label = "Rope.Net.Android.Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class SampleActivity : Activity, IActivityWithLayout
    {
        private SeekBar sldValue;

        private SeekBar sldMultiplier;

        private TextView lblResult;

        private Button btnSubmit;

        private FrameLayout content;

        private SeekBar sldRed;

        private SeekBar sldGreen;

        private SeekBar sldBlue;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.InitializeComponents();

            this.ViewModel = new SampleViewModel
            {
                Value = 50,
                Multiplier = 1,
                Color = new PclColor(0, 0, 0)
            };

            this.sldValue.BindValue(this.ViewModel, vm => vm.Value);
            this.sldMultiplier.BindValue(this.ViewModel, vm => vm.Multiplier);
            this.lblResult.Bind(
                this.ViewModel,
                vm => vm.Result,
                (lbl, val) => lbl.Text = $"Result: {val.ToString("F2")}");

            this.btnSubmit.BindEnable(this.ViewModel, vm => vm.CanSubmit);

            this.content.Bind(
                this.ViewModel,
                vm => vm.Color,
                (l, color) => l.SetBackgroundColor(Color.Rgb(color.Red, color.Green, color.Blue)));

            this.sldRed.Bind(this.ViewModel, vm => vm.Color, (sld, color) => sld.Progress = color.Red);
            this.sldGreen.Bind(this.ViewModel, vm => vm.Color, (sld, color) => sld.Progress = color.Green);
            this.sldBlue.Bind(this.ViewModel, vm => vm.Color, (sld, color) => sld.Progress = color.Blue);

            this.sldRed.ProgressChanged += this.SldRedOnProgressChanged;
            this.sldGreen.ProgressChanged += this.SldGreenOnProgressChanged;
            this.sldBlue.ProgressChanged += this.SldBlueOnProgressChanged;
        }

        private void SldRedOnProgressChanged(object sender, SeekBar.ProgressChangedEventArgs progressChangedEventArgs)
        {
            this.ViewModel.Color = new PclColor((byte)progressChangedEventArgs.Progress, this.ViewModel.Color.Green, this.ViewModel.Color.Blue);
        }

        private void SldGreenOnProgressChanged(object sender, SeekBar.ProgressChangedEventArgs progressChangedEventArgs)
        {
            this.ViewModel.Color = new PclColor(this.ViewModel.Color.Red, (byte)progressChangedEventArgs.Progress, this.ViewModel.Color.Blue);
        }

        private void SldBlueOnProgressChanged(object sender, SeekBar.ProgressChangedEventArgs progressChangedEventArgs)
        {
            this.ViewModel.Color = new PclColor(this.ViewModel.Color.Red, this.ViewModel.Color.Green, (byte)progressChangedEventArgs.Progress);
        }

        protected override void OnDestroy()
        {
            this.sldRed.ProgressChanged -= this.SldRedOnProgressChanged;
            this.sldGreen.ProgressChanged -= this.SldGreenOnProgressChanged;
            this.sldBlue.ProgressChanged -= this.SldBlueOnProgressChanged;

            base.OnDestroy();
        }

        public SampleViewModel ViewModel { get; private set; }

        private void InitializeComponents()
        {
            this.SetContentView(this.LayoutResourceId);

            this.sldValue = this.FindViewById<SeekBar>(Resource.Id.sldValue);
            this.sldMultiplier = this.FindViewById<SeekBar>(Resource.Id.sldMultiplier);
            this.lblResult = this.FindViewById<TextView>(Resource.Id.lblResult);
            this.btnSubmit = this.FindViewById<Button>(Resource.Id.btnSubmit);
            this.content = this.FindViewById<FrameLayout>(global::Android.Resource.Id.Content);
            this.sldRed = this.FindViewById<SeekBar>(Resource.Id.sldRed);
            this.sldGreen = this.FindViewById<SeekBar>(Resource.Id.sldGreen);
            this.sldBlue = this.FindViewById<SeekBar>(Resource.Id.sldBlue);
        }

        public int LayoutResourceId { get; } = Resource.Layout.Main;
    }

    public interface IActivityWithLayout
    {
        int LayoutResourceId { get; }
    }
}

