using System;
using GalaSoft.MvvmLight.Messaging;

namespace App.Services.OAuth
{
    public class OAuth2SignInMessage : MessageBase
    {
        public Uri Uri { get; set; }
    }
}