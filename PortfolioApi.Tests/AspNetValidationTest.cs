using PortfolioApi.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using System.ComponentModel.DataAnnotations;

namespace PortfolioApi.Tests
{
    public class AspNetValidationTest
    {

        [Theory]
        [InlineData("adam@frank.com", true)]
        [InlineData("adamfrank", false)]
        public void TestValidation(string email, bool shouldPass)
        {
            var message = new Message
            {
                SenderEmail = email,
                SenderName = "test",
                Content = "test"
            };

            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(message, new ValidationContext(message), results, true);
            Assert.Equal(shouldPass, isValid);            
        }
    }
}
