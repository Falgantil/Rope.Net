using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

using Foundation;

using MonoTouch.Dialog;

using UIKit;

namespace Rope.Net.iOS
{
    public static class BindingIos
    {
        /// <summary>
        /// Invokes the specified action on the main thread.
        /// </summary>
        /// <param name="operation">The operation.</param>
        public static void Apply(Action operation)
        {
            if (NSThread.IsMain)
            {
                operation();
            }
            else
            {
                NSThread.MainThread.InvokeOnMainThread(operation);
            }
        }

        public static IBinding BindText<TModel>(
            this UILabel view,
            TModel model,
            Expression<Func<TModel, string>> getVal,
            string format = "{0}")
            where TModel : INotifyPropertyChanged
        {
            return BindingCore.CreateBinding(
                view,
                model,
                getVal,
                (v, value) => Apply(() => v.Text = string.Format(format, value)));
        }

        public static IBinding BindText<TModel>(
            this UITextField view,
            TModel model,
            Expression<Func<TModel, string>> getVal)
            where TModel : INotifyPropertyChanged
        {
            var binding = BindingCore.CreateBinding(
                view,
                model,
                getVal,
                (v, value) => Apply(() => v.Text = value));
            var prop = (PropertyInfo)((MemberExpression)getVal.Body).Member;

            var observer = NSNotificationCenter.DefaultCenter.AddObserver(
                UITextField.TextFieldTextDidChangeNotification,
                notification =>
                {
                    if (notification.Object != view)
                    {
                        return;
                    }
                    if (!Equals(view.Text, prop.GetValue(model)))
                    {
                        prop.SetValue(model, view.Text);
                    }
                });

            return binding.With(() => NSNotificationCenter.DefaultCenter.RemoveObserver(observer));
        }

        public static IBinding BindText<TModel>(
            this UITextView view,
            TModel model,
            Expression<Func<TModel, string>> getVal)
            where TModel : INotifyPropertyChanged
        {
            var binding = BindingCore.CreateBinding(
                view,
                model,
                getVal,
                (v, value) => Apply(() => v.Text = value));
            var prop = (PropertyInfo)((MemberExpression)getVal.Body).Member;

            EventHandler handler = (sender, args) =>
            {
                if (!Equals(view.Text, prop.GetValue(model)))
                {
                    prop.SetValue(model, view.Text);
                }
            };
            view.Changed += handler;

            return binding.With(() => view.Changed -= handler);
        }

        public static IBinding BindEnable<TModel>(
            this UIControl view,
            TModel model,
            Expression<Func<TModel, bool>> getVal)
            where TModel : INotifyPropertyChanged
        {
            var binding = BindingCore.CreateBinding(
                view,
                model,
                getVal,
                (v, value) => Apply(() => v.Enabled = value));
            var prop = (PropertyInfo)((MemberExpression)getVal.Body).Member;

            EventHandler viewOnValueChanged = (sender, args) =>
            {
                if (!Equals(view.Enabled, prop.GetValue(model)))
                {
                    prop.SetValue(model, view.Enabled);
                }
            };
            view.ValueChanged += viewOnValueChanged;
            return binding.With(() => view.ValueChanged -= viewOnValueChanged);
        }

        public static IBinding Bind<TView, TModel, TValue>(
            this TView view,
            TModel model,
            Expression<Func<TModel, TValue>> getVal,
            Action<TView, TValue> setVal)
            where TView : UIView
            where TModel : INotifyPropertyChanged
        {
            return BindingCore.CreateBinding(view, model, getVal, (v, val) => Apply(() => setVal(v, val)));
        }

        public static IBinding BindValue<TModel>(
            this UISlider view,
            TModel model,
            Expression<Func<TModel, float>> getVal)
            where TModel : INotifyPropertyChanged
        {
            var binding = Bind(
                view,
                model,
                getVal,
                (slider, val) => slider.Value = val);

            var prop = (PropertyInfo)((MemberExpression)getVal.Body).Member;

            EventHandler viewOnValueChanged = (sender, args) =>
            {
                if (!Equals(view.Value, prop.GetValue(model)))
                {
                    prop.SetValue(model, view.Value);
                }
            };
            view.ValueChanged += viewOnValueChanged;
            return binding.With(() => view.ValueChanged -= viewOnValueChanged);
        }

    }

    public static class BindingDialog
    {
        private static T HasParentElement<T>(Element parent) where T : Element
        {
            while (true)
            {
                if (parent.Parent == null)
                {
                    return null;
                }
                var root = parent.Parent as T;
                if (root != null)
                {
                    return root;
                }
                parent = parent.Parent;
            }
        }

        public static void Apply(this Element parent, Func<bool> operation)
        {
            BindingIos.Apply(
                () =>
                    {
                        if (!operation())
                        {
                            return;
                        }
                        var rootElement = HasParentElement<RootElement>(parent);
                        if (rootElement != null)
                        {
                            try
                            {
                                rootElement.Reload(parent, UITableViewRowAnimation.Fade);
                            }
                            catch (Exception ex)
                            {
                                // For unexplainable reasons, this tends to in rare occasions throw an exception, despite RootElement being present... :/
                                // If someone would look into this, that would be great!
                            }
                        }
                    });
        }

        public static IBinding BindText<TModel>(
            this StringElement view,
            TModel model,
            Expression<Func<TModel, string>> getVal)
            where TModel : INotifyPropertyChanged
        {
            return BindingCore.CreateBinding(
                view,
                model,
                getVal,
                (v, value) => Apply(
                    v,
                    () =>
                        {
                            if (v.Value != value)
                            {
                                v.Value = value;
                                return true;
                            }
                            return false;
                        }));
        }

        public static IBinding BindText<TModel>(
            this EntryElement view,
            TModel model,
            Expression<Func<TModel, string>> getVal)
            where TModel : INotifyPropertyChanged
        {
            var binding = BindingCore.CreateBinding(
                view,
                model,
                getVal,
                (v, value) => Apply(v,
                    () =>
                    {
                        if (v.Value != value)
                        {
                            v.Value = value;
                            return true;
                        }
                        return false;
                    }));

            var prop = (PropertyInfo)((MemberExpression)getVal.Body).Member;

            EventHandler viewOnValueChanged = (sender, args) =>
            {
                if (!Equals(view.Value, prop.GetValue(model)))
                {
                    prop.SetValue(model, view.Value);
                }
            };
            view.Changed += viewOnValueChanged;
            view.NotifyChangedOnKeyStroke = true;

            return binding.With(() => view.Changed -= viewOnValueChanged);
        }

        public static IBinding BindValue<TModel>(
            this BooleanElement view,
            TModel model,
            Expression<Func<TModel, bool>> getVal)
            where TModel : INotifyPropertyChanged
        {
            var binding = BindingCore.CreateBinding(
                view,
                model,
                getVal,
                (v, value) => BindingIos.Apply(
                    () =>
                    {
                        if (v.Value != value)
                        {
                            v.Value = value;
                        }
                    }));

            var prop = (PropertyInfo)((MemberExpression)getVal.Body).Member;

            EventHandler viewOnValueChanged = (sender, args) =>
                {
                    if (!Equals(view.Value, prop.GetValue(model)))
                    {
                        prop.SetValue(model, view.Value);
                    }
                };
            view.ValueChanged += viewOnValueChanged;

            return binding.With(() => view.ValueChanged -= viewOnValueChanged);
        }

        public static IBinding BindDate<TModel>(
            this DateTimeElement view,
            TModel model,
            Expression<Func<TModel, DateTime>> getVal)
            where TModel : INotifyPropertyChanged
        {
            var binding = BindingCore.CreateBinding(
                view,
                model,
                getVal,
                (v, value) => Apply(v,
                    () =>
                    {
                        if (v.DateValue != value)
                        {
                            v.DateValue = value;
                            return true;
                        }
                        return false;
                    }));

            var prop = (PropertyInfo)((MemberExpression)getVal.Body).Member;

            Action<DateTimeElement> viewOnValueChanged = element =>
            {
                if (!Equals(element.DateValue, prop.GetValue(model)))
                {
                    prop.SetValue(model, element.DateValue);
                }
            };
            view.DateSelected += viewOnValueChanged;

            return binding.With(() => view.DateSelected -= viewOnValueChanged);
        }
    }
}
