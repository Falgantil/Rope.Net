using System;

namespace Rope.Net
{
    public class Binding : IBinding
    {
        public event EventHandler Disposing;
        
        public IBinding With(Action onDispose)
        {
            this.Disposing += (sender, args) => onDispose();
            return this;
        }

        public void Dispose()
        {
            this.Disposing?.Invoke(this, EventArgs.Empty);
            this.Disposing = null;
        }
    }
}