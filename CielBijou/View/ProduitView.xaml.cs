using CielBijou.Model;
using CielBijou.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
    /// Logique d'interaction pour ProduitView.xaml
    /// </summary>
    public partial class ProduitView : UserControl
    {

        private string Mode;
        private ViewModelProduit viewModelProduit;
        private ViewModelCategorie viewModelCategorie;
        public ProduitView()
        {
            InitializeComponent();
            this.Mode = "";
            this.viewModelProduit = new ViewModelProduit();
            this.viewModelCategorie = new ViewModelCategorie();
            DataGridProduit.ItemsSource =viewModelProduit.getLesProduits();
        }

        private produit CreerObjetProduitDepuisTextBox()
        {
            produit produit = new produit();

            if (TextBoxId.Text != "")
                produit.id = int.Parse(TextBoxId.Text);
            else
                produit.id = 0;

            double prix;
            string prixText = TextBoxPrixProd.Text.Replace('.', ','); // Remplacer le point par une virgule

            if (!double.TryParse(prixText, out prix))
            {
                MessageBox.Show("Le prix doit être un nombre décimal valide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
            if (Mode == "Ajout" || Mode == "Modification")
                produit.une_categorie_id = viewModelCategorie.getIdCategorie(ComboBoxCategorie.SelectedValue.ToString());
            else
                produit.une_categorie_id = int.Parse(TextBoxCategorie.Text);
            produit.nom_prod = TextBoxNomProd.Text;
            produit.description = TextBoxDescription.Text;
            produit.stock_prod = int.Parse(TextBoxStock.Text);
            produit.image_prod = null;
            produit.prix_prod = prix;

            return produit;
        }

        private bool Verif()
        {
            if (string.IsNullOrWhiteSpace(TextBoxId.Text) ||
                ComboBoxCategorie.SelectedValue ==null ||
                string.IsNullOrWhiteSpace(TextBoxNomProd.Text) ||
                string.IsNullOrWhiteSpace(TextBoxDescription.Text) ||
                string.IsNullOrWhiteSpace(TextBoxStock.Text) ||
                string.IsNullOrWhiteSpace(TextBoxPrixProd.Text))
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
            DataGridProduit.ItemsSource = null;
            DataGridProduit.ItemsSource = viewModelProduit.getLesProduits(TextBoxFiltreNom.Text);
        }


        private void remiseAZero()
        {
            TextBoxNomProd.Clear();
            TextBoxDescription.Clear();
            TextBoxStock.Clear();
            TextBoxId.Clear();
            TextBoxCategorie.Clear();
            TextBoxPrixProd.Clear();
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

        private void TextBoxFiltreNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActualiserListView();
        }


        private void rechargerDataGrid()
        {
            DataGridProduit.ItemsSource = "";
            DataGridProduit.ItemsSource = viewModelProduit.getLesProduits();
        }

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            ActualiserListView();
            BasculerBoutons();
            remiseAZero();
            Verrouiller();
        }

        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            produit UnProd = null;
            try
            {
                if (Verif())
                {
                    UnProd = CreerObjetProduitDepuisTextBox();

                    if (Mode == "Ajout")
                    {
                        viewModelProduit.insertProduit(UnProd);
                        MessageBox.Show("Ajout du produit effectué", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        viewModelProduit.updateProduit(UnProd);
                        MessageBox.Show("Modification du produit effectuée", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    ComboBoxCategorie.Visibility = Visibility.Hidden;
                    ComboBoxCategorie.IsEnabled = false;
                    TextBoxCategorie.Visibility = Visibility.Visible;
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

        private void ButtonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridProduit.SelectedIndex != -1)
            {
                if (MessageBox.Show($"Êtes-vous sûr de vouloir supprimer ce produit ?", "CONFIRMATION", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        var produit = CreerObjetProduitDepuisTextBox();
                        viewModelProduit.deleteProduit(produit.id);
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
                MessageBox.Show("Aucun produit sélectionné", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAjouter_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxCategorie.Visibility = Visibility.Visible;
            ComboBoxCategorie.IsEnabled = true;
            TextBoxCategorie.Visibility = Visibility.Hidden;
            Mode = "Ajout";
                TextBoxId.IsEnabled = true;
                BasculerBoutons();
                Deverouiller();
        }

        private void ButtonModifier_Click(object sender, RoutedEventArgs e)
        {

            if (DataGridProduit.SelectedIndex != -1)
            {
                ComboBoxCategorie.Visibility = Visibility.Visible;
                ComboBoxCategorie.IsEnabled = true;
                TextBoxCategorie.Visibility = Visibility.Hidden;
                Mode = "Modification";
                TextBoxId.IsEnabled = false;
                BasculerBoutons();
                Deverouiller();
            }
            else
            {
                MessageBox.Show("Aucun produit sélectionné", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridProduit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridProduit.SelectedItem != null)
            {
                var selectedItem = DataGridProduit.SelectedItem;

                if (selectedItem is produit unProd)
                {
                    int id= unProd.une_categorie_id;
                    TextBoxCategorie.Text = viewModelCategorie.getNomCategorie(id);
                }
            }
        }
    }
}
