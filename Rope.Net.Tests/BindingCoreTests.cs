// Rope.Net
// - Rope.Net.Tests
// -- BindingTests.cs
// -------------------------------------------
// Author: Bjarke Søgaard <sogaardbjarke@gmail.com>

using System;
using System.Diagnostics;
using System.Linq.Expressions;

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

            this.output.WriteLine("And this is why we don't use Expressions by default!");

            // Yes, I know most bindings will only be run once per view.
            // But consider the performance difference in these 2 results.
            // And this is on a PC! Imagine if it was used on Mobile,
            // or similar less-powerful devices. Maybe there will be a variation with 
            // Expressions at SOME point, who knows.
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

            vm.DummyProperty = "Random value";
            called.ShouldBe(2);

            binding.Dispose();

            vm.DummyProperty = "Random value numer 2";
            called.ShouldBe(2);

            vm.DummyProperty = "Random value numer 3";
            called.ShouldBe(2);
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
    }
}