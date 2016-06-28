// Rope.Net
// - Rope.Net.Tests
// -- BindingTests.cs
// -------------------------------------------
// Author: Bjarke Søgaard <sogaardbjarke@gmail.com>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

using Rope.Net.Tests.Annotations;

using Shouldly;

using Xunit;
using Xunit.Abstractions;

namespace Rope.Net.Tests
{
    public class BindingCoreTests
    {
        private readonly ITestOutputHelper output;

        public BindingCoreTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void BenchmarkExpressionCompileDuration()
        {
            var vm = new DummyVm();

            const int Iterations = 10000;

            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < Iterations; i++)
            {
                Expression<Func<DummyVm, string>> getFuncy = dummyVm => dummyVm.DummyProperty;
                var compile = getFuncy.Compile();
                var test = compile(vm);
            }
            stopwatch.Stop();

            this.output.WriteLine($"Took {stopwatch.Elapsed.ToString("c")} for {Iterations} iterations.");

            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < Iterations; i++)
            {
                Func<DummyVm, string> getFuncy = dummyVm => dummyVm.DummyProperty;
                var test = getFuncy(vm);
            }
            stopwatch.Stop();

            this.output.WriteLine($"Took {stopwatch.Elapsed.ToString("c")} for {Iterations} iterations.");

            this.output.WriteLine("Should we still use Expression as default?");

            // There's a significant performance difference between Expression and default property getting.
            // But since it's only (most of the time) run once per view, is it really something to worry about?
            // Personally I don't believe it'll be a problem, but Mobile devices or similar,
            // of course doesn't have anywhere near the processing power of a desktop PC... Opinions?
        }

        [Fact]
        public void VerifyBaseMethodExecutesOnceAtCall()
        {
            var view = new DummyView();
            var vm = new DummyVm();

            int called = 0;
            called.ShouldBe(0);

            BindingCore.CreateBinding(view,
                vm,
                d => d.DummyProperty,
                (v, text) =>
                    {
                        v.TextProperty = text;
                        called++;
                    });

            called.ShouldBe(1);
        }

        [Fact]
        public void VerifyBaseMethodGetsInvokedWhenPropertyChanges()
        {
            var view = new DummyView();
            var vm = new DummyVm();

            int called = 0;
            BindingCore.CreateBinding(view,
                vm,
                d => d.DummyProperty,
                (dummyView, s) =>
                    {
                        dummyView.TextProperty = s;
                        called++;
                    });
            called.ShouldBe(1);

            vm.DummyProperty = "Random value";
            called.ShouldBe(2);

            vm.DummyProperty = "Random value number 2";
            called.ShouldBe(3);
        }

        [Fact]
        public void VerifyBaseMethodDisposeUnhooksBindings()
        {
            var view = new DummyView();
            var vm = new DummyVm();

            int called = 0;
            var binding = BindingCore.CreateBinding(view,
                vm,
                d => d.DummyProperty,
                (dummyView, s) =>
                {
                    dummyView.TextProperty = s;
                    called++;
                });
            called.ShouldBe(1);
            view.TextProperty.ShouldBe(vm.DummyProperty);

            vm.DummyProperty = "Random value";
            called.ShouldBe(2);
            view.TextProperty.ShouldBe(vm.DummyProperty);

            binding.Dispose();

            vm.DummyProperty = "Random value numer 2";
            called.ShouldBe(2);
            view.TextProperty.ShouldNotBe(vm.DummyProperty);

            vm.DummyProperty = "Random value numer 3";
            called.ShouldBe(2);
            view.TextProperty.ShouldNotBe(vm.DummyProperty);
        }

        [Fact]
        public void VerifyBaseMethodDisposeWhenReferenceIsLost()
        {
            var view = new DummyView();
            var vm = new DummyVm();

            int called = 0;
            BindingCore.CreateBinding(view,
                vm,
                d => d.DummyProperty,
                (dummyView, s) =>
                {
                    dummyView.TextProperty = s;
                    called++;
                });
            called.ShouldBe(1);

            vm.DummyProperty = "Random value";
            called.ShouldBe(2);

            view = null;
            GC.Collect();

            vm.DummyProperty = "Random value number 2";
            called.ShouldBe(2);
        }

        [Fact]
        public void VerifyBaseMethodThrowsIfInvokedWithNullParameters()
        {
            DummyView nullView = null;
            DummyView valueView = new DummyView();
            DummyVm nullVm = null;
            DummyVm valueVm = new DummyVm();

            Should.Throw<ArgumentNullException>(
                () => BindingCore.CreateBinding(nullView, valueVm, vm => vm.DummyProperty, (m, t) => m.TextProperty = t));
            Should.Throw<ArgumentNullException>(
                () => BindingCore.CreateBinding(valueView, nullVm, vm => vm.DummyProperty, (m, t) => m.TextProperty = t));
            Should.Throw<ArgumentNullException>(
                () => BindingCore.CreateBinding<DummyView, DummyVm, string>(valueView, valueVm, null, (m, t) => m.TextProperty = t));
            Should.Throw<ArgumentNullException>(
                () => BindingCore.CreateBinding(valueView, valueVm, vm => vm.DummyProperty, null));
            var binding = BindingCore.CreateBinding(valueView, valueVm, vm => vm.DummyProperty, (m, t) => m.TextProperty = t);
            binding.ShouldNotBeNull();
        }

        [Fact]
        public void VerifyBaseMethodThrowsIfGetValIsNotVmProperty()
        {
            DummyView view = new DummyView();
            DummyVm vm = new DummyVm();

            Should.Throw<ArgumentException>(
                () =>
                BindingCore.CreateBinding(vm, vm, model => "1", (model, val) => model.DummyProperty = val));

            var binding = BindingCore.CreateBinding(view, vm, model => model.DummyProperty, (model, val) => model.TextProperty = val);
            binding.ShouldNotBeNull();
        }
    }
}