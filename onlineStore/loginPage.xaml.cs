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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace onlineStore
{
    /// <summary>
    /// Логика взаимодействия для loginPage.xaml
    /// </summary>
    public partial class loginPage : Page
    {
        public loginPage()
        {
            InitializeComponent();
            SessionManager.ClearSession();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool access = false;
            foreach (var row in onlineStoreEntities.GetContext().users.ToList() )
            {
                if (row.login.ToString()==loginTextBox.Text && row.password.ToString() == passwordBox.Password)
                {
                    SessionManager.CurrentUserId = row.ID; // Сохраняем ID
                    SessionManager.CurrentUserLogin = row.login;
                    access =true;
                    if (row.access_level.ToString() == "admin")
                    {
                        Manager.allFrame.Navigate(new adminPage());
                    }
                    else
                    {
                        Manager.allFrame.Navigate(new userPage());
                    }
                }
            }
            if (!access)
            {
                MessageBox.Show("Неверный логин/пароль");
                loginTextBox.Text = "";
                passwordBox.Password = "";
            }
        }
    }
}
