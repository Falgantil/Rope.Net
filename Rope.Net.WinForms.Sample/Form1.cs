using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Rope.Net.Sample.Core;

namespace Rope.Net.WinForms.Sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.ViewModel = new SampleViewModel
            {
                Value = 50,
                Multiplier = 1,
                Color = new PclColor(0, 0, 0)
            };

            BindingCore.CreateBinding(this.sldValue, this.ViewModel, vm => vm.Value, (bar, val) => bar.Value = (int)val);
            this.sldValue.ValueChanged += (sender, args) => this.ViewModel.Value = this.sldValue.Value;

            BindingCore.CreateBinding(this.sldMultiplier, this.ViewModel, vm => vm.Multiplier, (bar, val) => bar.Value = (int)val);
            this.sldMultiplier.ValueChanged += (sender, args) => this.ViewModel.Multiplier = this.sldMultiplier.Value;

            BindingCore.CreateBinding(
                this.lblResult,
                this.ViewModel,
                vm => vm.Result,
                (lbl, val) => lbl.Text = $"Result: {val.ToString("F2")}");

            BindingCore.CreateBinding(this.btnSubmit, this.ViewModel, vm => vm.CanSubmit, (btn, canSubmit) => btn.Enabled = canSubmit);

            BindingCore.CreateBinding(
                this,
                this.ViewModel,
                vm => vm.Color,
                (me, color) => me.BackColor = Color.FromArgb(color.Red, color.Green, color.Blue));

            BindingCore.CreateBinding(this.sldRed, this.ViewModel, vm => vm.Color, (bar, color) => bar.Value = color.Red);
            this.sldRed.ValueChanged +=
                (sender, args) =>
                this.ViewModel.Color =
                new PclColor((byte)this.sldRed.Value, this.ViewModel.Color.Green, this.ViewModel.Color.Blue);

            BindingCore.CreateBinding(this.sldGreen, this.ViewModel, vm => vm.Color, (bar, color) => bar.Value = color.Green);
            this.sldGreen.ValueChanged +=
                (sender, args) =>
                this.ViewModel.Color =
                new PclColor(this.ViewModel.Color.Red, (byte)this.sldGreen.Value, this.ViewModel.Color.Blue);

            BindingCore.CreateBinding(this.sldBlue, this.ViewModel, vm => vm.Color, (bar, color) => bar.Value = color.Blue);
            this.sldBlue.ValueChanged +=
                (sender, args) =>
                this.ViewModel.Color =
                new PclColor(this.ViewModel.Color.Red, this.ViewModel.Color.Green, (byte)this.sldBlue.Value);
        }

        public SampleViewModel ViewModel { get; private set; }

    }
}
