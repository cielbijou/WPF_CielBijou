using CielBijou.Model;
using CielBijou.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace CielBijou.View
{
    /// <summary>
    /// Logique d'interaction pour CommandeView.xaml
    /// </summary>
    public partial class CommandeView : UserControl
    {

        private string Mode;
        private ViewModelClient viewModelClient;
        private ViewModelLigneCommande viewModelLigneCommande;
        private ViewModelCommande viewModelCommande;
        private ViewModelProduit viewModelProduit;

        public CommandeView()
        {
            InitializeComponent();
            this.viewModelProduit = new ViewModelProduit();
            this.viewModelClient = new ViewModelClient();
            this.viewModelCommande = new ViewModelCommande();
            this.viewModelLigneCommande = new ViewModelLigneCommande();
            this.Mode = null;
            TextBoxNomClient.IsEnabled = false;
            TextBoxIdClient.IsEnabled = false;
            TextBoxPrenomClient.IsEnabled = false;
            TextBoxRoleClient.IsEnabled = false;
            DataGridCommandes.ItemsSource = viewModelCommande.getLesCommandes();
        }
        private void TextBoxFiltreIdCommande_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActualiserListView();
        }

        private void DatePickerFiltreDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ActualiserListView();
        }

        private void ActualiserListView()
        {
            // Déconnecter l'événement de sélection
            DataGridCommandes.SelectionChanged -= DataGridCommandes_SelectionChanged;

         
            string selectedDate = DatePickerFiltreDate.Text;
            DataGridCommandes.ItemsSource = null;
            DataGridCommandes.ItemsSource = viewModelCommande.getLesCommandes(TextBoxFiltreIdCommande.Text, selectedDate);

            // Reconnecter l'événement de sélection
            DataGridCommandes.SelectionChanged += DataGridCommandes_SelectionChanged;
        }
        private void DataGridProduit()
        {
            commande commandeSelectionnee = (commande)DataGridCommandes.SelectedItem;
            if (commandeSelectionnee != null)
            {
                int idCommande = commandeSelectionnee.id;
                DataGridProduits.ItemsSource = null;
                List<int> lesId = viewModelLigneCommande.getIdProduit(idCommande);
                List<produit> tousLesProduits = lesId.Select(id => viewModelProduit.getUnProduit(id)).ToList();
                DataGridProduits.ItemsSource = tousLesProduits;
            }
        }


        private void ContenuClient()
        {
            commande commandeSelectionnee = (commande)DataGridCommandes.SelectedItem;
            int idCommande = commandeSelectionnee.un_client_id;
            if (commandeSelectionnee != null)
            {
                if (viewModelClient.getId(idCommande) != null)
                {
                    // Afficher les informations du client dans les TextBox et l'Image
                    TextBoxIdClient.Text = viewModelClient.getId(idCommande);
                    TextBoxNomClient.Text = viewModelClient.getNom(idCommande);
                    TextBoxPrenomClient.Text = viewModelClient.getPrenom(idCommande);
                    TextBoxRoleClient.Text = viewModelClient.getRoles(idCommande);
                    /*if (viewModelClient.getPhtot(idCommande) != null)
                    {
                        using (var ms = new MemoryStream(viewModelClient.getPhtot(idCommande)))
                        {
                            var image = new BitmapImage();
                            image.BeginInit();
                            image.CacheOption = BitmapCacheOption.OnLoad;
                            image.StreamSource = ms;
                            image.EndInit();
                            ImagePhoto.Source = image;
                        }
                    }
                    else
                    {*/
                        ImagePhoto.Source = null; 
                    //}
                }
                else
                {
                    // Afficher un message d'erreur ou une valeur par défaut
                    TextBoxIdClient.Text = "Aucun client sélectionné";
                    TextBoxNomClient.Text = "";
                    TextBoxPrenomClient.Text = "";
                    TextBoxRoleClient.Text = "";
                    ImagePhoto.Source = null;
                }
                
            }
        }

        private void DataGridCommandes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridProduit();
            ContenuClient();
        }
    }
}

