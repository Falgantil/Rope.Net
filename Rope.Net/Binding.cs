using System;

namespace Rope.Net
{
    /// <summary>
    /// A basic Binding, that will invoke <see cref="Disposing"/> when disposing.
    /// </summary>
    /// <seealso cref="Rope.Net.IBinding" />
    public class Binding : IBinding
    {
        /// <summary>
        /// Occurs when the binding is unhooking (disposing).
        /// </summary>
        public event EventHandler Disposing;

        /// <summary>
        /// Add an Action to occur when the binding is unhooking (disposing).
        /// </summary>
        /// <param name="onDisposing">The action to occur on dispose.</param>
        /// <returns> Returns itself, for chaining purposes. </returns>
        public IBinding With(Action onDisposing)
        {
            this.Disposing += (sender, args) => onDisposing();
            return this;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Disposing?.Invoke(this, EventArgs.Empty);
            this.Disposing = null;
        }
    }
}