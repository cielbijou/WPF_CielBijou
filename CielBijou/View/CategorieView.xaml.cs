using CielBijou.Model;
using CielBijou.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CielBijou.View
{
    /// <summary>
    /// Logique d'interaction pour CategorieView.xaml
    /// </summary>
    public partial class CategorieView : UserControl
    {
        private ViewModelCategorie viewModelCategories;  // Le ViewModel qui gère les catégories
        private string Mode;  // Variable pour savoir si on est en mode Ajout ou Modification

        public CategorieView()
        {
            InitializeComponent();
            viewModelCategories = new ViewModelCategorie();  // Initialisation du ViewModel
            this.Mode = null;  // Initialisation de la variable Mode
            DataGridCategories.ItemsSource = viewModelCategories.getLesCategories();  // Affichage des catégories
        }

        // Actualiser le DataGrid avec les dernières catégories
        private void ActualiserDataGrid()
        {
            DataGridCategories.ItemsSource = null;
            DataGridCategories.ItemsSource = viewModelCategories.getLesCategories();

            if (DataGridCategories.Items.Count > 0)
            {
                DataGridCategories.SelectedIndex = 0;
            }
        }

        // Chargement initial du contrôle
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataGridCategories.Items.Count > 0)
            {
                DataGridCategories.SelectedIndex = 0;
            }
        }

        // Créer un objet catégorie depuis les TextBox
        private categorie CreerObjetCategorieDepuisTextBox()
        {
            return new categorie
            {
                id = int.Parse(TextBoxId.Text),
                libelle_cat = TextBoxLibelle.Text
            };
        }

        // Bouton Ajouter une nouvelle catégorie
        private void ButtonAjouter_Click(object sender, RoutedEventArgs e)
        {
            remiseAZero();
            DataGridCategories.SelectedIndex = -1;
            BasculerBoutons();
            DeverouillerVerrouiller();
            Mode = "Ajout";  // Mode Ajout
            TextBoxId.IsEnabled = true;
            TextBoxId.Focus();  // Focus sur le champ Id
        }

        // Bouton Modifier une catégorie existante
        private void ButtonModifier_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridCategories.SelectedIndex != -1)
            {
                Mode = "Modification";  // Mode Modification
                BasculerBoutons();
                DeverouillerVerrouiller();
                TextBoxId.IsEnabled = false;  // Le code ne peut pas être modifié
            }
            else
            {
                MessageBox.Show("Aucune catégorie sélectionnée", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Bouton Supprimer une catégorie sélectionnée
        private void ButtonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridCategories.SelectedIndex != -1)
            {
                var selectedCategory = (categorie)DataGridCategories.SelectedItem;
                if (MessageBox.Show($"Etes-vous sûr de vouloir supprimer {selectedCategory.libelle_cat} ?", "CONFIRMATION", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        viewModelCategories.deleteCategorie(selectedCategory);  
                        ActualiserDataGrid();  
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
                MessageBox.Show("Aucune catégorie sélectionnée", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Bouton Valider (ajout ou modification)
        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Verif())  // Vérifie que les champs sont corrects
                {
                    var category = CreerObjetCategorieDepuisTextBox();  // Crée l'objet catégorie à partir des TextBox
                    if (Mode == "Ajout")
                    {
                        viewModelCategories.insertCategorie(category);  // Ajout via le ViewModel
                        MessageBox.Show("Ajout de la catégorie effectué", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        viewModelCategories.updateCategorie(category);  // Modification via le ViewModel
                        MessageBox.Show("Modification de la catégorie effectuée", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    ActualiserDataGrid();  // Actualisation du DataGrid
                    BasculerBoutons();
                    DeverouillerVerrouiller();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Bouton Annuler (retour en arrière sans valider)
        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            ActualiserDataGrid();
            BasculerBoutons();
            remiseAZero();
            DeverouillerVerrouiller();
        }

        // Fonction pour basculer entre les boutons (Ajout/Modifier/Supprimer/Valider)
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

        // Fonction pour verrouiller/déverrouiller les contrôles
        private void DeverouillerVerrouiller()
        {
            GrilleCategories.IsEnabled = ButtonValider.Visibility == Visibility.Visible;
        }

        // Fonction pour remettre à zéro les champs de saisie
        private void remiseAZero()
        {
            TextBoxId.Clear();
            TextBoxLibelle.Clear();
        }

        // Fonction de vérification des champs saisis
        private bool Verif()
        {
            if (string.IsNullOrWhiteSpace(TextBoxId.Text) || string.IsNullOrWhiteSpace(TextBoxLibelle.Text))
            {
                MessageBox.Show("Veuillez compléter tous les champs", "AVERTISSEMENT", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }
            return true;
        }
    }
}
