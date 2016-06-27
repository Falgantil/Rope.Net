using System;

namespace Rope.Net
{
    public interface IBinding : IDisposable
    {
        IBinding With(Action onDispose);
    }
}