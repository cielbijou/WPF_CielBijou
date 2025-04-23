using CielBijou.View;
using PPE.Wpf.Windows;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CielBijou
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoginWindow login = new LoginWindow();
            login.ShowDialog();

            if (login.DialogResult == false)
            {
                this.Close();
            }

        }


        private void buttonClient_click(object sender, RoutedEventArgs e)
        {
            DockPanel.Children.Clear();
            DockPanel.Children.Add(new ClientsView());
        }

        private void MenuItemQuitter_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Commentaire_Click(object sender, RoutedEventArgs e)
        {
            DockPanel.Children.Clear();
            DockPanel.Children.Add(new CommentaireView());
        }

        private void MenuItemCommande_Click(object sender, RoutedEventArgs e)
        {
            DockPanel.Children.Clear();
            DockPanel.Children.Add(new CommandeView());
        }

        private void MenuItemLigneCommande_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Événement déclenché lors du clic pour la fermeture de l'application.
        /// </summary>
        private void CloseMain_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment quitter?", "Attention", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        private void ButtonAcceuil_Click(object sender, RoutedEventArgs e)
        {
            DockPanel.Children.Clear();
            DockPanel.Children.Add(new AcceuilView());
        }

        private void Button_CommandeClick(object sender, RoutedEventArgs e)
        {
            DockPanel.Children.Clear();
            DockPanel.Children.Add(new CommandeView());
        }

        private void Button_Promotions_click(object sender, RoutedEventArgs e)
        {
            DockPanel.Children.Clear();
            DockPanel.Children.Add(new PromotionView());
        }

        private void Button_Produit_Click(object sender, RoutedEventArgs e)
        {
            DockPanel.Children.Clear();
            DockPanel.Children.Add(new ProduitView());
        }

        private void Button_Deconnexion_click(object sender, RoutedEventArgs e)
        {
            LoginWindow.utilConnecte = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DockPanel.Children.Clear();
            DockPanel.Children.Add(new CategorieView());
        }
    }
}
