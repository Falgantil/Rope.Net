using System;

namespace Rope.Net
{
    /// <summary>
    /// The Binding interface.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IBinding : IDisposable
    {
        /// <summary>
        /// Add an Action to occur when the binding is unhooking (disposing).
        /// </summary>
        /// <param name="onDisposing">The action to occur on dispose.</param>
        /// <returns>Returns itself, for chaining purposes.</returns>
        IBinding With(Action onDisposing);
    }
}