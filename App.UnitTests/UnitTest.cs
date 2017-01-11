using System;
using App.ViewModels;
using Xunit;

namespace App.UnitTests
{
    public class SignInViewModelTest
    {
        [Fact]
        public void SignIn_EmailAccountPropertyIsNotNullWhenSignIn()
        {
            var signInViewModel = new SignInViewModel();
            signInViewModel.Password = "";
            Assert.ThrowsAny<ArgumentNullException>(() => signInViewModel.SignIn());
        }

        [Fact]
        public void SignIn_PasswordPropertyIsNotNullWhenSignIn()
        {
            var signInViewModel = new SignInViewModel();
            signInViewModel.EmailAccount = "";
            Assert.ThrowsAny<ArgumentNullException>(() => signInViewModel.SignIn());
        }
    }
}
