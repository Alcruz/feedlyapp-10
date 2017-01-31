using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;

namespace App.Services
{
    public class Application
    {
        public void TerminateApp()
        {
            CoreApplication.Exit();
        }
    }
}
