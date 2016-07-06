using System.ComponentModel;
using System.Runtime.CompilerServices;

using Rope.Net.Tests.Properties;

namespace Rope.Net.Tests
{
    public class DummyView
    {
        public string TextProperty { get; set; }
    }

    public class DummyVm : INotifyPropertyChanged
    {
        public string DummyProperty { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
