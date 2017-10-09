using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using WaCore.Crud.Utils.Sorting;

namespace WaCore.Crud.UnitTests.Utils
{
    [TestClass]
    public class SortFieldMappingBuilderTests
    {
        public class TestDto
        {
            public string StringProperty { get; set; }
            public string StringField;
            public int IntProperty { get; set; }
            public int IntField;
            public TestDto RelatedDto { get; set; }
        }

        [TestMethod]
        public void ForDtoSortFieldWhenMappingSingleReferenceTypePropertyReturnsPropertyName()
        {
            AssertRetrunsExpectedSortField(x => x.StringProperty, "StringProperty");
        }

        [TestMethod]
        public void ForDtoSortFieldWhenMappingSingleValueTypeTypePropertyReturnsPropertyName()
        {
            AssertRetrunsExpectedSortField(x => x.IntProperty, "IntProperty");
        }

        [TestMethod]
        public void ForDtoSortFieldWhenMappingSingleValueTypeTypeFieldReturnsFieldName()
        {
            AssertRetrunsExpectedSortField(x => x.IntField, "IntField");
        }

        [TestMethod]
        public void ForDtoSortFieldWhenMappingRelatedObjectPropertyReturnsPropertyAccessPath()
        {
            AssertRetrunsExpectedSortField(x => x.RelatedDto.StringProperty, "RelatedDto.StringProperty");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ForDtoSortFieldWhenMappingSelfThrowsArgumentException()
        {
            new SortFieldMappingBuilder<object>().ForDtoSortField<TestDto>(x => x);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ForDtoSortFieldWhenMappingLiteralThrowsArgumentException()
        {
            new SortFieldMappingBuilder<object>().ForDtoSortField<TestDto>(x => 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ForDtoSortFieldWhenMappingMethodCallThrowsArgumentException()
        {
            new SortFieldMappingBuilder<object>().ForDtoSortField<TestDto>(x => x.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ForDtoSortFieldWhenMappingOtherFieldsPropertyThrowsArgumentException()
        {
            var notAParameter = new TestDto();
            new SortFieldMappingBuilder<object>().ForDtoSortField<TestDto>(x => notAParameter.StringProperty);
        }

        private void AssertRetrunsExpectedSortField(Expression<Func<TestDto, object>> expr, string expectedFieldName)
        {
            var builder = new SortFieldMappingBuilder<object>();
            var mapping = (SingleSortFieldMap<object>)builder.ForDtoSortField(expr);
            Assert.AreEqual(expectedFieldName, mapping.SortField);
        }
    }
}
