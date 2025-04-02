using CielBijou.Model;
using CielBijou.View;
using CielBijou.ViewModel;
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
            try
            {
                string mail = txtEmail.Text;
                string pwd = txtPassword.Password;
                unCli = viewModelClient.getUnClientByMail(mail); //pwd admin :  5Hdj34#ZSAyb
                if (unCli == null)
                {
                    MessageBox.Show("Erreur,  inconnu", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    bool res = CryptSharp.Crypter.CheckPassword(pwd, unCli.password);
                    if (res)
                    {
                        /*if (unCli.roles != "[\"ROLE_ADMIN\"]")
                        {
                            MessageBox.Show("Vous devez etre administrateur pour accéder a cette interface");
                        }
                        else
                        {*/
                        utilConnecte = unCli;
                        accueil.LabelStatutBarre.Content = "Bienvenue " + utilConnecte.nom + " " + utilConnecte.prenom;
                        MessageBox.Show("Bonjour " + utilConnecte.nom);
                        //}
                    }
                    else
                    {
                        MessageBox.Show("Erreur, utilisateur ", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur, utilisateur inconnu", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Warning);
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
