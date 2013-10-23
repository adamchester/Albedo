﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class PropertyInfoElementTest
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            var property = typeof(TypeWithProperty).GetProperties().First();
            // Exercise system
            var sut = new PropertyInfoElement(property);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void PropertyInfoIsCorrect()
        {
            // Fixture setup
            var property = typeof(TypeWithProperty).GetProperties().First();
            var sut = new PropertyInfoElement(property);
            // Exercise system
            var actual = sut.PropertyInfo;
            // Verify outcome
            Assert.Equal(property, actual);
            // Teardown
        }

        [Fact]
        public void ConstructWithNullPropertyInfoThrows()
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                new PropertyInfoElement(null));
            // Teardown
        }

        [Fact]
        public void AcceptNullVisitorThrows()
        {
            // Fixture setup
            var property = typeof(TypeWithProperty).GetProperties().First();
            var sut = new PropertyInfoElement(property);
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                sut.Accept((IReflectionVisitor<object>)null));
            // Teardown
        }

        [Fact]
        public void AcceptReturnsTheCorrectVisitorInstance()
        {
            // Fixture setup
            var sut = new PropertyInfoElement(
                typeof(TypeWithProperty).GetProperties()[0]);

            var expected = new DelegatingReflectionVisitor<int>();
            var visitor = new DelegatingReflectionVisitor<int>
            {
                OnVisitPropertyInfoElement = (e, v) => expected
            };
            // Exercise system
            var actual = sut.Accept(visitor);
            // Verify outcome
            Assert.Same(expected, actual);
            // Teardown
        }

        [Fact]
        public void AcceptCallsVisitOnceWithCorrectType()
        {
            // Fixture setup
            var observed = new List<PropertyInfoElement>();
            var dummyVisitor = new DelegatingReflectionVisitor<int> { OnPropertyInfoElementVisited = observed.Add };
            var sut = new PropertyInfoElement(typeof(TypeWithProperty).GetProperties().First());
            // Exercise system
            sut.Accept(dummyVisitor);
            // Verify outcome
            Assert.True(new[] { sut }.SequenceEqual(observed));
            // Teardown
        }

        public class TypeWithProperty
        {
            public int Property { get; set; }
        }

    }
}