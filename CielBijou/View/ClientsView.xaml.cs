using CielBijou.Model; // Remplacer par le namespace de votre modèle
using CielBijou.ViewModel; // Remplacer par le namespace de votre ViewModel
using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CielBijou.View
{
    /// <summary>
    /// Logique d'interaction pour ClientsView.xaml
    /// </summary>
    public partial class ClientsView : UserControl
    {
        private string Mode;
        private ViewModelClient viewModelClients;
        private static Random _random = new Random();

        public ClientsView()
        {
            InitializeComponent();
            this.viewModelClients = new ViewModelClient();
            this.DataContext = viewModelClients;
            this.Mode = null;

            DataGridClients.ItemsSource = viewModelClients.getLesClients();
        }

        private client CreerObjetClientDepuisTextBox()
        {
            client client = new client();

            if (TextBoxId.Text != "")
                client.id = int.Parse(TextBoxId.Text);
            else
                client.id = 0;

            client.nom = TextBoxNom.Text;
            client.prenom = TextBoxPrenom.Text;
            client.email = TextBoxEmail.Text;
            client.password = TextBoxMdp.Text;
            client.roles = ComboBoxGroupe.SelectedValue?.ToString();

            /*if (ImagePhoto.Source != null)
            {
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(ImagePhoto.Source as BitmapSource));

                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    client.photo = ms.ToArray();
                }
            }
            else
            {*/
                client.photo = null;
            //}

            return client;
        }

        private bool Verif()
        {
            if (string.IsNullOrWhiteSpace(TextBoxNom.Text) ||
                string.IsNullOrWhiteSpace(TextBoxPrenom.Text) ||
                ComboBoxGroupe.SelectedItem == null ||
                string.IsNullOrWhiteSpace(TextBoxMdp.Text) ||
                string.IsNullOrWhiteSpace(TextBoxEmail.Text))
            {
                MessageBox.Show("Veuillez compléter tous les champs", "AVERTISSEMENT", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }

            return true;
        }

        private void Deverouiller()
        {
            GrilleClients.IsEnabled = true;
        }

        private void Verrouiller()
        {
            GrilleClients.IsEnabled = false;
        }

        private void ActualiserListView()
        {
            string filtreGroupe = "";
            var selectedItem = ComboBoxFiltreGroupe.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                // Afficher la valeur dans une MessageBox
                filtreGroupe = selectedItem.Content.ToString();
            }

            DataGridClients.ItemsSource = null;
            DataGridClients.ItemsSource = viewModelClients.getLesClients(TextBoxFiltreNom.Text, TextBoxFiltrePrenom.Text, TextBoxFiltreEmail.Text, filtreGroupe);
        }

        private void remiseAZero()
        {
            TextBoxNom.Clear();
            TextBoxPrenom.Clear();
            TextBoxEmail.Clear();
            TextBoxId.Clear();
            TextBoxMdp.Clear();
        }

        private void BasculerBoutons()
        {
            if (ButtonValider.Visibility == Visibility.Visible)
            {
                ButtonValider.Visibility = Visibility.Hidden;
                ButtonAnnuler.Visibility = Visibility.Hidden;
                ButtonSupprimer.Visibility = Visibility.Visible;
                ButtonAjouter.Visibility = Visibility.Visible;
                ButtonModifier.Visibility = Visibility.Visible;
            }
            else
            {
                ButtonValider.Visibility = Visibility.Visible;
                ButtonAnnuler.Visibility = Visibility.Visible;
                ButtonSupprimer.Visibility = Visibility.Hidden;
                ButtonAjouter.Visibility = Visibility.Hidden;
                ButtonModifier.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonAjouter_Click(object sender, RoutedEventArgs e)
        {
            remiseAZero();
            DataGridClients.SelectedIndex = -1;
            BasculerBoutons();
            Deverouiller();
            Mode = "Ajout";
            TextBoxId.IsEnabled = false;
            TextBoxNom.Focus();
        }

        private void ButtonModifier_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridClients.SelectedIndex != -1)
            {
                Mode = "Modification";
                TextBoxId.IsEnabled = false;
                BasculerBoutons();
                Deverouiller();
                TextBoxMdp.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Aucun client sélectionné", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridClients.SelectedIndex != -1)
            {
                if (MessageBox.Show($"Êtes-vous sûr de vouloir supprimer ce client ?", "CONFIRMATION", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        var client = CreerObjetClientDepuisTextBox();
                        viewModelClients.deleteClient(client.id);
                        ActualiserListView();
                        remiseAZero();
                        MessageBox.Show("Suppression effectuée", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Aucun client sélectionné", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            client Unclient = null;
            try
            {
                if (Verif())
                {
                    Unclient = CreerObjetClientDepuisTextBox();

                    if (Mode == "Ajout")
                    {
                        viewModelClients.insertClient(Unclient);
                        MessageBox.Show("Ajout du client effectué", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        viewModelClients.updateClient(Unclient);
                        MessageBox.Show("Modification du client effectuée", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    ActualiserListView();
                    BasculerBoutons();
                    Verrouiller();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            ActualiserListView();
            BasculerBoutons();
            remiseAZero();
            Verrouiller();
        }


        private void TextBoxFiltreNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActualiserListView();
        }

        private void TextBoxFiltrePrenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActualiserListView();
        }

        private void TextBoxFiltreEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActualiserListView();
        }


        private void ButtonPacourirPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    ImagePhoto.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Une erreur s'est produite lors du chargement de l'image : {ex.Message}");
                }
            }
        }

        private void rechargerDataGrid()
        {
            DataGridClients.ItemsSource = "";
            DataGridClients.ItemsSource = viewModelClients.getLesClients();
        }

        private void AnnulerFiltreGroupe_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxFiltreGroupe.SelectedItem = null;
            ActualiserListView();
        }

        private void ComboBoxFiltreGroupe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActualiserListView();
        }

        private void GénérerMdp_Click(object sender, RoutedEventArgs e)
        {
            int length = 12;
            if (length < 12)
                length = 12;

            string lowerCase = "abcdefghijklmnopqrstuvwxyz";
            string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string digits = "0123456789";
            string specialChars = "-#?";

            string allChars = lowerCase + upperCase + digits + specialChars;
            StringBuilder password = new StringBuilder(length);


            password.Append(lowerCase[_random.Next(lowerCase.Length)]); 
            password.Append(upperCase[_random.Next(upperCase.Length)]);  
            password.Append(digits[_random.Next(digits.Length)]);  
            password.Append(specialChars[_random.Next(specialChars.Length)]);  

        
            for (int i = password.Length; i < length; i++)
            {
                password.Append(allChars[_random.Next(allChars.Length)]);
            }

           
            string finalPassword = new string(password.ToString().OrderBy(c => _random.Next()).ToArray());

            Clipboard.SetText(finalPassword);
            MessageBox.Show($"Mot de passe généré et copié dans le presse-papier : {finalPassword}", "Mot de passe", MessageBoxButton.OK, MessageBoxImage.Information); ;
            
        
        }

        private void SaisirMdp_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Voulez-vous changer le mot de passe", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                String mdp = Microsoft.VisualBasic.Interaction.InputBox("Saisir le mot de passe", "Confirmation");
                // Vérifie la complexité du mot de passe
                string regexPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\d])(?=.*[-#?])[A-Za-z\d-#?]{10,}$";
                if (Regex.IsMatch(mdp, regexPattern))
                {
                    TextBoxMdp.Text = CryptSharp.Crypter.Blowfish.Crypt(mdp);
                }
                else
                {
                    MessageBox.Show("Le mot de passe doit contenir au moins une minuscule, une majuscule, un chiffre, un caractère spécial (-#?) et faire au moins 10 caractères.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Modification Annulée", "ANNULATION", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
