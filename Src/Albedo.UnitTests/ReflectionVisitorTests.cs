using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class ReflectionVisitorTests
    {
        [Fact]
        public void SutIsIReflectionVisitorOfT()
        {
            // Fixture setup
            // Exercise system
            var sut = new TestReflectionVisitor<int>(0);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionVisitor<int>>(sut);
            // Teardown
        }

        [Fact]
        public void AllVisitOverloadsReturnThisByDefault()
        {
            // Fixture setup
            var sut = new TestReflectionVisitor<int>(0);
            // Exercise system
            var visitMethods = sut.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.Name == "Visit");
            // Verify outcome
            foreach (var visitOverload in visitMethods)
            {
                
            }
            // Teardown
        }

        class TestReflectionVisitor<T> : ReflectionVisitor<T>
        {
            private readonly T value;

            public TestReflectionVisitor(T value)
            {
                this.value = value;
            }

            public override T Value { get { return this.value; } }
        }
    }
}
