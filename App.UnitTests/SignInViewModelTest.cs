using System;
using App.ViewModels;
using Xunit;

namespace App.UnitTests
{
    public class SignInViewModelTest
    {
        [Fact]
        public void SignInCommand_IsActivateWhenEmailAccountAndPasswordAreNotNullOrEmpty()
        {
            var signInViewModel = new SignInViewModel();
            signInViewModel.EmailAccount = "johndow@domain.com";
            signInViewModel.Password = "123abc";
            
            Assert.True(signInViewModel.SignInCommand.CanExecute(null));
        }

        [Fact]
        public void SignInCommand_RaisCanExecuteChangeEventWhenEmailAccountAndPasswordChange()
        {
            var signInViewModel = new SignInViewModel();
            bool commandHasNotified = false;

            signInViewModel.SignInCommand.CanExecuteChanged += (sender, args) => commandHasNotified = true;
            signInViewModel.EmailAccount = "johndow@domain.com";
            signInViewModel.Password = "123abc";
            
            Assert.True(commandHasNotified);
        }


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
