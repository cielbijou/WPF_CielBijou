using CielBijou.Model;
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

namespace CielBijou.View
{
    /// <summary>
    /// Logique d'interaction pour PromotionView.xaml
    /// </summary>
    public partial class PromotionView : UserControl
    {
        private ViewModelPromotion viewModelPromotion;
        private ViewModelCategorie viewModelCategorie;
        private string Mode;
        public PromotionView()
        {
            InitializeComponent();
            viewModelPromotion = new ViewModelPromotion();
            viewModelCategorie = new ViewModelCategorie();
            this.Mode = null;
            DataGridPromotions.ItemsSource = viewModelPromotion.getLesPromotions();
        }

        private void ActualiserDataGrid()
        {
            DataGridPromotions.ItemsSource = null;
            DataGridPromotions.ItemsSource = viewModelPromotion.getLesPromotions();

            if (DataGridPromotions.Items.Count > 0)
            {
                DataGridPromotions.SelectedIndex = 0;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataGridPromotions.Items.Count > 0)
            {
                DataGridPromotions.SelectedIndex = 0;
            }
        }

        private promotion CreerObjetPromotionDepuisTextBox()
        {
            DateTime dateDebut = DateTime.MinValue;
            DateTime dateFin = DateTime.MinValue;

            DateTime? date_debut_promo = DatePickerDebutPromo.SelectedDate;
            if (!date_debut_promo.HasValue)
            {
                MessageBox.Show("La date de début de la promotion est invalide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
            dateDebut = date_debut_promo.Value;

            DateTime? date_fin_promo = DatePickerFinPromo.SelectedDate;
            if (!date_fin_promo.HasValue)
            {
                MessageBox.Show("La date de fin de la promotion est invalide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
            dateFin = date_fin_promo.Value;

            int id;
            if (!int.TryParse(TextBoxId.Text, out id))
            {
                MessageBox.Show("L'identifiant doit être un nombre valide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }


            decimal remise;
            string remiseText = TextBoxRemise.Text.Replace('.', ',');
            if (!decimal.TryParse(remiseText, out remise))
            {
                MessageBox.Show("La remise doit être un nombre décimal valide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }

            int idCat;
            if (Mode == "Ajout" || Mode == "Modification")
                idCat = viewModelCategorie.getIdCategorie(ComboBoxCategorie.SelectedValue.ToString());
            else
                idCat = int.Parse(TextBoxLibelle.Text);

            return new promotion
            {

                id = id,
                une_categorie_id = idCat,
                date_debut_promo = dateDebut,
                date_fin_promo = dateFin,
                remise_promo = remise
            };
        }



        private void ButtonAjouter_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxCategorie.Visibility = Visibility.Visible;
            ComboBoxCategorie.IsEnabled = true;
            TextBoxLibelle.Visibility = Visibility.Hidden;
            remiseAZero();
            DataGridPromotions.SelectedIndex = -1;
            BasculerBoutons();
            DeverouillerVerrouiller();
            Mode = "Ajout";  
            TextBoxId.IsEnabled = true;
            TextBoxId.Focus();
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

        // Fonction pour verrouiller/déverrouiller les contrôles
        private void DeverouillerVerrouiller()
        {
            GrillePromotionq.IsEnabled = ButtonValider.Visibility == Visibility.Visible;
        }

        private void remiseAZero()
        {
            TextBoxId.Clear();
            TextBoxLibelle.Clear();
            DatePickerDebutPromo.SelectedDate = null;
            DatePickerFinPromo.SelectedDate = null;
            TextBoxRemise.Clear();
        }

        // Fonction de vérification des champs saisis
        private bool Verif()
        {
            if (string.IsNullOrWhiteSpace(TextBoxId.Text) ||ComboBoxCategorie.SelectedItem ==null || !DatePickerDebutPromo.SelectedDate.HasValue || !DatePickerFinPromo.SelectedDate.HasValue || string.IsNullOrWhiteSpace(TextBoxRemise.Text))
            {
                MessageBox.Show("Veuillez compléter tous les champs", "AVERTISSEMENT", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }
            return true;
        }

        private void ButtonModifier_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPromotions.SelectedIndex != -1)
            {
                ComboBoxCategorie.Visibility = Visibility.Visible;
                ComboBoxCategorie.IsEnabled = true;
                TextBoxLibelle.Visibility = Visibility.Hidden;
                Mode = "Modification";  
                BasculerBoutons();
                DeverouillerVerrouiller();
                TextBoxId.IsEnabled = false; 
            }
            else
            {
                MessageBox.Show("Aucune promotions sélectionnée", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPromotions.SelectedIndex != -1)
            {
                var selectedPromotion = (promotion)DataGridPromotions.SelectedItem;
                if (MessageBox.Show($"Etes-vous sûr de vouloir supprimer {selectedPromotion.id} ?", "CONFIRMATION", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        viewModelPromotion.deletePromotions(selectedPromotion);
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
                MessageBox.Show("Aucune promotions sélectionnée", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Verif())  // Vérifie que les champs sont corrects
                {
                    var promotion = CreerObjetPromotionDepuisTextBox();  // Crée l'objet catégorie à partir des TextBox
                    if (Mode == "Ajout")
                    {
                        viewModelPromotion.insertPromotions(promotion);  // Ajout via le ViewModel
                        MessageBox.Show("Ajout de la catégorie effectué", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        viewModelPromotion.updatePromotions(promotion);  // Modification via le ViewModel
                        MessageBox.Show("Modification de la catégorie effectuée", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    ComboBoxCategorie.Visibility = Visibility.Hidden;
                    ComboBoxCategorie.IsEnabled = false;
                    TextBoxLibelle.Visibility = Visibility.Visible;
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

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            ActualiserDataGrid();
            BasculerBoutons();
            remiseAZero();
            DeverouillerVerrouiller();
        }

        private void DataGridPromotions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridPromotions.SelectedItem != null)
            {
                var selectedItem = DataGridPromotions.SelectedItem;

                if (selectedItem is promotion unePromo)
                {
                    int id = unePromo.une_categorie_id;
                    TextBoxLibelle.Text = viewModelCategorie.getNomCategorie(id);
                }
            }
        }
    }
}
