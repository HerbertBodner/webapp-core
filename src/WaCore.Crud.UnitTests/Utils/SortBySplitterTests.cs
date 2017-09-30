using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaCore.Crud.Utils.Sorting;

namespace WaCore.Crud.UnitTests.Utils
{
    [TestClass]
    public class SortBySplitterTests
    {
        [TestMethod]
        public void SplitSortByStringWhenNoLeadingPlusOrMinusReturnsOneCorrectOrderItem()
        {
            var expected = new SortingValidation
            {
                ExpectedFieldCount = 1,
                ExpectedFieldName = "myField",
                ExpectedOrderBy = OrderItem.OrderBy.Ascending
            };
            Validate("myField", expected);
        }

        [TestMethod]
        public void SplitSortByStringWhenLeadingPlusReturnsOneCorrectOrderItem()
        {
            var expected = new SortingValidation
            {
                ExpectedFieldCount = 1,
                ExpectedFieldName = "myField",
                ExpectedOrderBy = OrderItem.OrderBy.Ascending
            };
            Validate("+myField", expected);
        }

        [TestMethod]
        public void SplitSortByStringWhenLeadingMinusReturnsOneCorrectOrderItem()
        {
            var expected = new SortingValidation
            {
                ExpectedFieldCount = 1,
                ExpectedFieldName = "myField",
                ExpectedOrderBy = OrderItem.OrderBy.Descending
            };
            Validate("-myField", expected);
        }

        [TestMethod]
        public void SplitSortByStringWhenTwoFieldsAndNoLeadingPlusOrMinusReturnsFirstCorrectOrderItem()
        {
            var expected = new SortingValidation
            {
                ExpectedFieldCount = 2,
                ExpectedFieldName = "myField",
                ExpectedOrderBy = OrderItem.OrderBy.Ascending
            };
            Validate("myField,+Field2", expected, 0);
        }

        [TestMethod]
        public void SplitSortByStringWhenTwoFieldsAndLeadingPlusReturnsFirstCorrectOrderItem()
        {
            var expected = new SortingValidation
            {
                ExpectedFieldCount = 2,
                ExpectedFieldName = "myField",
                ExpectedOrderBy = OrderItem.OrderBy.Ascending
            };
            Validate("+myField,+Field2", expected, 0);
        }

        [TestMethod]
        public void SplitSortByStringWhenTwoFieldsAndLeadingMinusReturnsFirstCorrectOrderItem()
        {
            var expected = new SortingValidation
            {
                ExpectedFieldCount = 2,
                ExpectedFieldName = "myField",
                ExpectedOrderBy = OrderItem.OrderBy.Descending
            };
            Validate("-myField,+Field2", expected, 0);
        }

        [TestMethod]
        public void SplitSortByStringWhenTwoFieldsAndNoLeadingPlusOrMinusReturnsSecondCorrectOrderItem()
        {
            var expected = new SortingValidation
            {
                ExpectedFieldCount = 2,
                ExpectedFieldName = "Field2",
                ExpectedOrderBy = OrderItem.OrderBy.Ascending
            };
            Validate("myField,+Field2", expected, 1);
        }

        [TestMethod]
        public void SplitSortByStringWhenTwoFieldsAndLeadingPlusReturnsSecondCorrectOrderItem()
        {
            var expected = new SortingValidation
            {
                ExpectedFieldCount = 2,
                ExpectedFieldName = "Field2",
                ExpectedOrderBy = OrderItem.OrderBy.Ascending
            };
            Validate("+myField,+Field2", expected, 1);
        }

        [TestMethod]
        public void SplitSortByStringWhenTwoFieldsAndLeadingMinusReturnsSecondtCorrectOrderItem()
        {
            var expected = new SortingValidation {
                ExpectedFieldCount = 2,
                ExpectedFieldName = "Field2",
                ExpectedOrderBy = OrderItem.OrderBy.Ascending
            };
            Validate("-myField,+Field2", expected, 1);
        }

        [TestMethod]
        public void SplitSortByStringWhenSecondFieldWithoutPlusOrMinusReturnsSecondCorrectOrderItem()
        {
            var expected = new SortingValidation
            {
                ExpectedFieldCount = 2,
                ExpectedFieldName = "Field2",
                ExpectedOrderBy = OrderItem.OrderBy.Ascending
            };
            Validate("myField,Field2", expected, 1);
        }

        private void Validate(string inputString, SortingValidation sortingVal, int itemNrToValidate = 0)
        {
            var list = SortBySplitter.SplitSortByString(inputString);

            Assert.AreEqual(sortingVal.ExpectedFieldCount, list.Count);
            Assert.AreEqual(sortingVal.ExpectedFieldName, list[itemNrToValidate].FieldName);
            Assert.AreEqual(sortingVal.ExpectedOrderBy, list[itemNrToValidate].OrderDirection);
        }
    }

    public class SortingValidation
    {
        public int ExpectedFieldCount { get; set; }
        public string ExpectedFieldName { get; set; }
        public OrderItem.OrderBy ExpectedOrderBy { get; set; }
    }
}
