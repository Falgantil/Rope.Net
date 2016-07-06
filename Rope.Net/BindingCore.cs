using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Rope.Net
{
    /// <summary>
    /// The root of all Bindings!
    /// </summary>
    public static class BindingCore
    {
        /// <summary>
        /// Creates the most basic binding of all. 
        /// </summary>
        /// <typeparam name="TView">The type of the view.</typeparam>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="view">The view.</param>
        /// <param name="model">The model.</param>
        /// <param name="getVal">An expression that required  value.</param>
        /// <param name="setVal">The set value.</param>
        /// <returns></returns>
        public static IBinding CreateBinding<TView, TModel, TValue>(
            TView view,
            TModel model,
            Expression<Func<TModel, TValue>> getVal,
            Action<TView, TValue> setVal)
            where TView : class
            where TModel : INotifyPropertyChanged
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (getVal == null)
                throw new ArgumentNullException(nameof(getVal));
            if (setVal == null)
                throw new ArgumentNullException(nameof(setVal));

            var propertyName = ((getVal.Body as MemberExpression)?.Member as PropertyInfo)?.Name;
            if (propertyName == null)
            {
                throw new ArgumentException(
                    "The getVal expression requires a Property, located on the Model!",
                    nameof(getVal));
            }

            var viewRef = new WeakReference<TView>(view);
            var func = getVal.Compile();

            setVal(view, func(model));

            var bindingEvent = new Binding();
            PropertyChangedEventHandler modelOnPropertyChanged = (sender, args) =>
                {
                    TView target;
                    if (!viewRef.TryGetTarget(out target) || target == null)
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
