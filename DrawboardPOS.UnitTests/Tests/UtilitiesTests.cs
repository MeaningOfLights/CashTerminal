using DrawboardPOS.Helpers;
using System.Collections.Generic;
using Xunit;

namespace DrawboardPOS.UnitTests.Tests
{
    public class UtilitiesTests
    {
        [Fact]
        public void Utilities_ToCurrencyDollarsAndCents()
        {
            // Act
            var result =Utilities.ToCurrencyString(1.5m);

            // Assert
            Assert.Equal(result, "$1.50");
        }

        [Fact]
        public void Utilities_ToCurrencyCents()
        {
            // Arrange

            // Act
            var result = Utilities.ToCurrencyString(0.5m);

            // Assert
            Assert.Equal(result, "50cents");
        }

        [Fact]
        public void Utilities_CreateDiscounts()
        {
            // Arrange

            // Act
            var result = Utilities.CreateDiscounts();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Utilities_CreateProducts()
        {
            // Arrange

            // Act
            var result = Utilities.CreateProducts(new List<string> { "A" });

            // Assert
            Assert.NotNull(result);
        }

    }
}
