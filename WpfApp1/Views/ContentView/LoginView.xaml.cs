using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Base;
using WpfApp1.ViewModels.ContentViewModel;

namespace WpfApp1.Views.ContentView
{
    /// <summary>
    /// LoginView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginView : BaseUserControl, IHavePassword
    {


        public LoginView()
        {
            InitializeComponent();
        }
        // 호출될 때마다 PasswordBox의 현재 값을 꺼내 줌
        public SecureString SecurePassword => PasswordText.SecurePassword;




    }
}
