using System;
using App.Services.OAuth;
using App.UnitTests.Fakes;
using App.ViewModels;
using Xunit;

namespace App.UnitTests
{
    public class SignInViewModelTest
    {
        [Fact]
        public void SignInCommand_IsActivateWhenEmailAccountAndPasswordAreNotNullOrEmpty()
        {
            var signInViewModel = new SignInViewModel(new FeedlyOAuth2AuthenticatorStub())
            {
                EmailAccount = "johndow@domain.com"
            };

            Assert.True(signInViewModel.SignInCommand.CanExecute(null));
        }

        [Fact]
        public void SignInCommand_RaisCanExecuteChangeEventWhenEmailAccountAndPasswordChange()
        {
            var signInViewModel = new SignInViewModel(new FeedlyOAuth2Authenticator());
            bool commandHasNotified = false;

            signInViewModel.SignInCommand.CanExecuteChanged += (sender, args) => commandHasNotified = true;
            signInViewModel.EmailAccount = "johndow@domain.com";
            
            Assert.True(commandHasNotified);
        }


        [Fact]
        public async void SignIn_EmailAccountPropertyIsNullWhenSignIn_ThrowsNullReferenceException()
        {
            var signInViewModel = new SignInViewModel(new FeedlyOAuth2AuthenticatorStub());
            await Assert.ThrowsAsync<ArgumentNullException>(() => signInViewModel.SignIn());
        }
    }
}
