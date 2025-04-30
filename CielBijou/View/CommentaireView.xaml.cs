using CielBijou.Model;
using CielBijou.ViewModel;
using System.Web.UI.WebControls;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CielBijou.View
{
    /// <summary>
    /// Logique d'interaction pour CommentaireView.xaml
    /// </summary>
    public partial class CommentaireView : UserControl
    {
        cielbijouEntities2 db = new cielbijouEntities2();

        private string Mode;
        private ViewModelClient viewModelClient;
        private ViewModelCommentaire viewModelCommentaire;
        private ViewModelProduit viewModelProduit;

        public CommentaireView()
        {
            InitializeComponent();

            this.viewModelProduit = new ViewModelProduit();
            this.viewModelClient = new ViewModelClient();
            this.viewModelCommentaire = new ViewModelCommentaire();
            this.Mode = null;

            // Désactiver la modification des champs
            TextBoxNomIdCom.IsEnabled = false;
            TextBoxNomClient.IsEnabled = false;
            TextBoxPrenomClient.IsEnabled = false;
            TextBoxNomProduit.IsEnabled = false;
            TextBoxlaCategorieProduit.IsEnabled = false;
            TextBoxContenuCom.IsEnabled = false;
            TextBoxNoteCom.IsEnabled = false;

            // Initialiser les données du DataGrid
            DataGridCommentaires.ItemsSource = viewModelCommentaire.getLesCommentaires();
        }

        private void TextBoxNomIdCommentaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataGridCommentaires.ItemsSource = null;
            DataGridCommentaires.ItemsSource = viewModelCommentaire.getLesCommentaires();
        }

        private void remiseAZero()
        {
            TextBoxNomClient.Clear();
            TextBoxPrenomClient.Clear();
            TextBoxlaCategorieProduit.Clear();
            TextBoxNomProduit.Clear();
            TextBoxContenuCom.Clear();
            TextBoxDateCom.Clear();
            TextBoxNoteCom.Clear();
        }

        private void Vider_Click(object sender, RoutedEventArgs e)
        {
            remiseAZero();
        }

        private void DataGridCommentaires_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridCommentaires.SelectedItem != null)
            {
                var selectedItem = DataGridCommentaires.SelectedItem;

                if (DataGridCommentaires.SelectedItem is commentaire unCommentaire)
                {
                    // Récupération du client
                    int clientId = unCommentaire.un_client_id;
                    client leClient = viewModelClient.getUnClient(clientId);
                    if (leClient != null)
                    {
                        TextBoxNomClient.Text = leClient.nom;
                        TextBoxPrenomClient.Text = leClient.prenom;
                    }
                    else
                    {
                        TextBoxNomClient.Text = "";
                        TextBoxPrenomClient.Text = "";
                    }

                    // Récupération du produit
                    int produitId = unCommentaire.un_produit_id;
                    produit leProduit = viewModelProduit.getUnProduit(produitId);
                    if (leProduit != null)
                    {
                        TextBoxNomProduit.Text = leProduit.nom_prod;
                        var categorie = db.categorie.FirstOrDefault(c => c.id == leProduit.une_categorie_id);
                        TextBoxlaCategorieProduit.Text = categorie?.libelle_cat ?? "";
                    }
                    else
                    {
                        TextBoxNomProduit.Text = "";
                        TextBoxlaCategorieProduit.Text = "";
                    }

                    // Remplissage des champs du commentaire
                    TextBoxNomIdCom.Text = unCommentaire.id.ToString();
                    TextBoxContenuCom.Text = unCommentaire.contenu_commentaire;
                    TextBoxNoteCom.Text = unCommentaire.note_commentaire.ToString();
                    TextBoxDateCom.Text = unCommentaire.date_commentaire.ToString("dd/MM/yyyy");
                }
            }
        }
    }
}
