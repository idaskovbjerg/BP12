using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OpenTeleNet.API;

namespace BpNFCApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //First - let us create a facade for the OpenTeleNetWrapperFacade API (SW)   
            //var openTeleFacade = new OpenTeleNetWrapperFacade("http://opentele-citizen.4s-online.dk/");
            var openTeleFacade = new OpenTeleNetWrapperFacade("http://opentele.aliviate.dk:4288/opentele-citizen-server/");

            //TROUBLESHOOT: Use the below if 4s is down 
            //var openTeleFacade = new OpenTeleNetWrapperFacade("http://openteletest1.medicaconnect.dk:8088/opentele-citizen-server/");

            var list = openTeleFacade.Login(UsernameTextbox.Text, PasswordBox.Password);
            Guide guideWindow = new Guide();
            guideWindow.PassWord = PasswordBox.Password;
            guideWindow.UserName = UsernameTextbox.Text;
            this.Close();
            guideWindow.Show();
        }
    }
}
