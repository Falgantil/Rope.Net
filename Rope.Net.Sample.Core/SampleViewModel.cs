using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Rope.Net.Sample.Core.Properties;

namespace Rope.Net.Sample.Core
{
    public class SampleViewModel : INotifyPropertyChanged
    {
        public float Value { get; set; }

        public float Multiplier { get; set; }

        public float Result => this.Multiplier * this.Value;

        public bool CanSubmit => this.Result > 3000;

        public PclColor Color { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class PclColor
    {
        public PclColor(byte red, byte green, byte blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        public byte Red { get; private set; }
        public byte Green { get; private set; }
        public byte Blue { get; private set; }
    }
}
