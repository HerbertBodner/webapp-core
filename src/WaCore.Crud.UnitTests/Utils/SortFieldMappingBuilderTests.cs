using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using WaCore.Crud.Utils.Sorting;
using Xunit;

namespace WaCore.Crud.UnitTests.Utils
{
    public class SortFieldMappingBuilderTests
    {
        public class TestDto
        {
            public string StringProperty { get; set; }
            public string StringField;
            public int IntProperty { get; set; }
            public int IntField { get; set; }
            public TestDto RelatedDto { get; set; }
        }

        [Fact]
        public void ForDtoSortFieldWhenMappingSingleReferenceTypePropertyReturnsPropertyName()
        {
            AssertRetrunsExpectedSortField(x => x.StringProperty, "StringProperty");
        }

        [Fact]
        public void ForDtoSortFieldWhenMappingSingleValueTypeTypePropertyReturnsPropertyName()
        {
            AssertRetrunsExpectedSortField(x => x.IntProperty, "IntProperty");
        }

        [Fact]
        public void ForDtoSortFieldWhenMappingSingleValueTypeTypeFieldReturnsFieldName()
        {
            AssertRetrunsExpectedSortField(x => x.IntField, "IntField");
        }

        [Fact]
        public void ForDtoSortFieldWhenMappingRelatedObjectPropertyReturnsPropertyAccessPath()
        {
            AssertRetrunsExpectedSortField(x => x.RelatedDto.StringProperty, "RelatedDto.StringProperty");
        }

        [Fact]
        public void ForDtoSortFieldWhenMappingSelfThrowsArgumentException()
        {
            AssertThrowsArgumentException(x => x);
        }

        [Fact]
        public void ForDtoSortFieldWhenMappingLiteralThrowsArgumentException()
        {
            AssertThrowsArgumentException(x => 1);
        }

        [Fact]
        public void ForDtoSortFieldWhenMappingMethodCallThrowsArgumentException()
        {
            AssertThrowsArgumentException(x => x.ToString());
        }

        [Fact]
        public void ForDtoSortFieldWhenMappingOtherFieldsPropertyThrowsArgumentException()
        {
            var notAParameter = new TestDto();
            AssertThrowsArgumentException(x => notAParameter.StringProperty);
        }


        private void AssertRetrunsExpectedSortField(Expression<Func<TestDto, object>> expr, string expectedFieldName)
        {
            var builder = new SortFieldMappingBuilder<object>();
            var mapping = (SingleSortFieldMap<object>)builder.ForDtoSortField(expr);
            Assert.Equal(expectedFieldName, mapping.SortField);
        }

        private void AssertThrowsArgumentException(Expression<Func<TestDto, object>> expr)
        {
            var builder = new SortFieldMappingBuilder<object>();
            Assert.Throws<ArgumentException>(() => builder.ForDtoSortField(expr));
        }
    }
}
