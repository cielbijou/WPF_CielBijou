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
    /// Logique d'interaction pour CommentaireView.xaml
    /// </summary>
    public partial class CommentaireView : UserControl
    {
        private string Mode;
        private ViewModelLigneCommande viewModelLigneCommande;
        private ViewModelClient viewModelClient;
        private ViewModelCommande viewModelCommande;
        private ViewModelProduit viewModelProduit;

        public CommentaireView()
        {
            InitializeComponent();
            this.viewModelProduit = new ViewModelProduit();
            this.viewModelClient = new ViewModelClient();
            this.viewModelCommande = new ViewModelCommande();
            this.viewModelLigneCommande = new ViewModelLigneCommande();
            this.Mode = null;

        }
    }
}
