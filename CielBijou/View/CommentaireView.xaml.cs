using CielBijou.ViewModel;
using System.Windows.Controls;

namespace CielBijou.View
{
    /// <summary>
    /// Logique d'interaction pour CommentaireView.xaml
    /// </summary>
    public partial class CommentaireView : UserControl
    {
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
            TextBoxNomClient.IsEnabled = false;
            TextBoxPrenomClient.IsEnabled = false;
            TextBoxNomProduit.IsEnabled = false;
            TextBoxlaCategorieProduit.IsEnabled = false;
            TextBoxContenuCommentaire.IsEnabled = false;
            TextBoxNoteCommentaire.IsEnabled = false;
            DataGridCommentaires.ItemsSource = viewModelCommentaire.getLesCommentaires();
        }


    }
}
