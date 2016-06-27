using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Rope.Net
{
    public static class BindingCore
    {
        public static IBinding CreateBinding<TView, TModel, TValue>(
            TView view,
            TModel model,
            Expression<Func<TModel, TValue>> getVal,
            Action<TView, TValue> setVal)
            where TView : class
            where TModel : INotifyPropertyChanged
        {
            var viewRef = new WeakReference<TView>(view);
            var prop = (PropertyInfo)((MemberExpression)getVal.Body).Member;
            var propertyName = prop.Name;
            var func = getVal.Compile();

            setVal(view, func(model));

            var bindingEvent = new Binding();
            PropertyChangedEventHandler modelOnPropertyChanged = (sender, args) =>
                {
                    TView target;
                    if (!viewRef.TryGetTarget(out target))
                    {
                        // Ensures the PropertyChanged event unhooks.
                        bindingEvent.Dispose();
                        return;
                    }
                    if (args.PropertyName != propertyName)
                    {
                        return;
                    }
                    setVal(target, func(model));
                };
            model.PropertyChanged += modelOnPropertyChanged;
            return bindingEvent.With(() => model.PropertyChanged -= modelOnPropertyChanged);
        }
    }
}
