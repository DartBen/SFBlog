using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Views
{
    public class MainViewModel
    {
        public LoginViewModel LoginView { get; set; }


        public MainViewModel()
        {
            LoginView = new LoginViewModel();
        }
    }
}
