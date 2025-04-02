using CielBijou;
using CielBijou.Model;
using CielBijou.View;
using CielBijou.ViewModel;
using Microsoft.VisualBasic.ApplicationServices;
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
namespace PPE.Wpf.Windows
{
    /// <summary>
    /// Logique d'interaction pour ConnexionView.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private ViewModelLogin viewModelClient;
        public static client utilConnecte;
        private client unCli;
        private AcceuilView accueil;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="LoginWindow"/>.
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  le focus de l'application est déplacé vers le champ de texte
        /// </summary>
        private void TextEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        /// <summary>
        /// Texte de vérifiaction si le champ est null
        /// </summary>
        private void TxtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        ///  le focus de l'application est déplacé vers le champ de texte
        /// </summary>
        private void TextPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        /// <summary>
        /// Texte de vérifiaction si le champ est null
        /// </summary>
        private void TxtPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }


        /// <summary>
        /// Action du bouton de connexion
        /// </summary>
        private void BtnConnexion_Click(object sender, RoutedEventArgs e)
        {
            string useremail = txtEmail.Text;
            string password = txtPassword.Password;

            bool isValidUser = false;

            // Utilisation du bon contexte (Database First)
            using (cielbijouEntities2 context = new cielbijouEntities2())
            {
                client user = context.client.FirstOrDefault(u => u.email == useremail);

                if (user != null && user.roles.Contains("ROLE_ADMIN"))
                {
                    ((App)Application.Current).client = user;
                    user.password = user.password.Replace("$Password", "$2a$"); // ⚠️ Vérifie que ce remplacement est nécessaire
                    isValidUser = BCrypt.Net.BCrypt.Verify(password, user.password);
                }
            }

            if (isValidUser)
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Email ou mot de passe incorrect.");
            }
        }


        /// <summary>
        /// action avec le bouton gauche de la souris la page de connexion
        /// </summary>
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// action avec le bouton close de la page de connexion
        /// </summary>
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
