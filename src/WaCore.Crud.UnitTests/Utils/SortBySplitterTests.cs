using System;
using System.Collections.Generic;
using System.Text;
using WaCore.Crud.Utils;
using Xunit;

namespace WaCore.Crud.UnitTests.Utils
{
    public class SortBySplitterTests
    {
        [Fact]
        public void SplitSortByStringWhenNoLeadingPlusOrMinusReturnsOneCorrectOrderItem()
        {
            Validate("myField", 1, OrderItem.OrderBy.Ascending, "myField");
        }

        [Fact]
        public void SplitSortByStringWhenLeadingPlusReturnsOneCorrectOrderItem()
        {
            Validate("+myField", 1, OrderItem.OrderBy.Ascending, "myField");
        }

        [Fact]
        public void SplitSortByStringWhenLeadingMinusReturnsOneCorrectOrderItem()
        {
            Validate("-myField", 1, OrderItem.OrderBy.Descending, "myField");
        }

        [Fact]
        public void SplitSortByStringWhenTwoFieldsAndNoLeadingPlusOrMinusReturnsFirstCorrectOrderItem()
        {
            Validate("myField+Field2", 2, OrderItem.OrderBy.Ascending, "myField", 0);
        }

        [Fact]
        public void SplitSortByStringWhenTwoFieldsAndLeadingPlusReturnsFirstCorrectOrderItem()
        {
            Validate("+myField+Field2", 2, OrderItem.OrderBy.Ascending, "myField", 0);
        }

        [Fact]
        public void SplitSortByStringWhenTwoFieldsAndLeadingMinusReturnsFirstCorrectOrderItem()
        {
            Validate("-myField+Field2", 2, OrderItem.OrderBy.Descending, "myField", 0);
        }

        [Fact]
        public void SplitSortByStringWhenTwoFieldsAndNoLeadingPlusOrMinusReturnsSecondCorrectOrderItem()
        {
            Validate("myField+Field2", 2, OrderItem.OrderBy.Ascending, "Field2", 1);
        }

        [Fact]
        public void SplitSortByStringWhenTwoFieldsAndLeadingPlusReturnsSecondCorrectOrderItem()
        {
            Validate("+myField+Field2", 2, OrderItem.OrderBy.Ascending, "Field2", 1);
        }

        [Fact]
        public void SplitSortByStringWhenTwoFieldsAndLeadingMinusReturnsSecondtCorrectOrderItem()
        {
            Validate("-myField+Field2", 2, OrderItem.OrderBy.Ascending, "Field2", 1);
        }


        private void Validate(string inputString, int expectedFieldCount, OrderItem.OrderBy expectedOrderBy, string expectedFieldName, int itemNrToValidate = 0)
        {
            var list = SortBySplitter.SplitSortByString(inputString);

            Assert.Equal(expectedFieldCount, list.Count);
            Assert.Equal(expectedOrderBy, list[itemNrToValidate].OrderDirection);
            Assert.Equal(expectedFieldName, list[itemNrToValidate].FieldName);
        }
    }
}
