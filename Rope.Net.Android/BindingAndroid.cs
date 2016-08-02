using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Android.App;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Rope.Net.Android
{
    public static class BindingAndroid
    {
        /// <summary>
        /// Invokes the specified action on the main thread.
        /// </summary>
        /// <param name="operation">The operation.</param>
        public static void Apply(Action operation)
        {
			if (Looper.MainLooper.Thread == Thread.CurrentThread())
            {
                operation();
            }
            else
            {
                new Handler(Looper.MainLooper).Post(operation);
            }
        }

        public static IBinding Bind<TView, TModel, TValue>(
            this TView view,
            TModel model,
            Expression<Func<TModel, TValue>> getVal,
            Action<TView, TValue> setVal)
            where TView : Java.Lang.Object
            where TModel : INotifyPropertyChanged
        {
            return BindingCore.CreateBinding(view, model, getVal, (v, val) => Apply(() => setVal(v, val)));
        }

        public static IBinding BindText<TModel>(this EditText editText,
            TModel model,
            Expression<Func<TModel, string>> getVal)
            where TModel : INotifyPropertyChanged
        {
            var binding = BindingCore.CreateBinding(
                editText,
                model,
                getVal,
                (v, value) => Apply(() => v.Text = value));
            var prop = (PropertyInfo)((MemberExpression)getVal.Body).Member;

            EventHandler<TextChangedEventArgs> viewOnValueChanged = (sender, args) =>
            {
                if (!Equals(editText.Text, prop.GetValue(model)))
                {
                    prop.SetValue(model, editText.Text);
                }
            };
            editText.TextChanged += viewOnValueChanged;
            return binding.With(() => editText.TextChanged -= viewOnValueChanged);
        }

        public static IBinding BindText<TModel>(this TextView textView,
            TModel model,
            Expression<Func<TModel, string>> getVal)
            where TModel : INotifyPropertyChanged
        {
            return BindingCore.CreateBinding(
                textView,
                model,
                getVal,
                (v, value) => Apply(() => v.Text = value));
        }

        public static IBinding BindValue<TModel>(
            this CheckBox view,
            TModel model,
            Expression<Func<TModel, bool>> getVal)
            where TModel : INotifyPropertyChanged
        {
            var binding = BindingCore.CreateBinding(
                view,
                model,
                getVal,
                (v, value) => Apply(
                    () =>
                    {
                        if (v.Checked != value)
                        {
                            v.Checked = value;
                        }
                    }));

            var prop = (PropertyInfo)((MemberExpression)getVal.Body).Member;

            EventHandler<CompoundButton.CheckedChangeEventArgs> viewOnValueChanged = (sender, args) =>
            {
                if (!Equals(view.Checked, prop.GetValue(model)))
                {
                    prop.SetValue(model, view.Checked);
                }
            };
            view.CheckedChange += viewOnValueChanged;

            return binding.With(() => view.CheckedChange -= viewOnValueChanged);
        }

        public static IBinding BindValue<TModel>(
            this SeekBar view,
            TModel model,
            Expression<Func<TModel, int>> getVal)
            where TModel : INotifyPropertyChanged
        {
            var binding = Bind(
                view,
                model,
                getVal,
                (slider, val) => slider.Progress = val);

            var prop = (PropertyInfo)((MemberExpression)getVal.Body).Member;

            EventHandler<SeekBar.ProgressChangedEventArgs> viewOnValueChanged = (sender, args) =>
            {
                if (!Equals(view.Progress, prop.GetValue(model)))
                {
                    prop.SetValue(model, view.Progress);
                }
            };
            view.ProgressChanged += viewOnValueChanged;
            return binding.With(() => view.ProgressChanged -= viewOnValueChanged);
        }

        public static IBinding BindValue<TModel>(
            this SeekBar view,
            TModel model,
            Expression<Func<TModel, float>> getVal)
            where TModel : INotifyPropertyChanged
        {
            var binding = Bind(
                view,
                model,
                getVal,
                (slider, val) => slider.Progress = (int)val);

            var prop = (PropertyInfo)((MemberExpression)getVal.Body).Member;

            EventHandler<SeekBar.ProgressChangedEventArgs> viewOnValueChanged = (sender, args) =>
            {
                var objB = prop.GetValue(model);
                if (!Equals(view.Progress, objB))
                {
                    prop.SetValue(model, view.Progress);
                }
            };
            view.ProgressChanged += viewOnValueChanged;
            return binding.With(() => view.ProgressChanged -= viewOnValueChanged);
        }

        public static IBinding BindEnable<TModel>(
            this View view,
            TModel model,
            Expression<Func<TModel, bool>> getVal)
            where TModel : INotifyPropertyChanged
        {
            return BindingCore.CreateBinding(
                view,
                model,
                getVal,
                (v, value) => Apply(() => v.Enabled = value));
        }
    }
}
