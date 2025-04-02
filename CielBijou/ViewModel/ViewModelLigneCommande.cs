using CielBijou.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CielBijou.ViewModel
{
    internal class ViewModelLigneCommande : INotifyPropertyChanged
    {
        cielbijouEntities2 db;

        public event PropertyChangedEventHandler PropertyChanged;

        private List<ligne_commande> lesLignesCommande;
        private ligne_commande uneLigneCommande;

        public ViewModelLigneCommande()
        {
            db = new cielbijouEntities2();
            lesLignesCommande = getLesLignesCommande();
            uneLigneCommande = null;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<ligne_commande> LesLignesCommande
        {
            get => lesLignesCommande;
            set { if (lesLignesCommande != value) { lesLignesCommande = value; OnPropertyChanged(); } }
        }
        public ligne_commande UneLigneCommande
        {
            get => uneLigneCommande;
            set { if (uneLigneCommande != value) { uneLigneCommande = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// Charge la collection des lignes de commande à partir de la table LigneCommande
        /// </summary>
        /// <returns></returns>
        public List<ligne_commande> getLesLignesCommande()
        {
            try
            {
                lesLignesCommande = db.ligne_commande.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getLesLignesCommande : " + ex.Message);
            }
            return lesLignesCommande;
        }


        /// <summary>
        /// Charge la collection des lignes de commande à partir de la table LigneCommande
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ligne_commande> getLesLignesCommande(int id)
        {
            ObservableCollection<ligne_commande> lesLignesCommande = new ObservableCollection<ligne_commande>(); 
            try
            {
                lesLignesCommande = new ObservableCollection<ligne_commande>(db.ligne_commande.Where(c=>c.une_commande_id.Equals(id)).ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getLesLignesCommande : " + ex.Message);
            }
            return lesLignesCommande;
        }

        public List<int> getIdProduit(int idCommande)
        {
            return db.ligne_commande
                     .Where(c => c.une_commande_id == idCommande)
                     .Select(c => c.un_produit_id) 
                     .ToList();
        }





        /// <summary>
        /// Retourne une ligne de commande précise en fonction de son identifiant
        /// </summary>
        /// <param name="identifiant"></param>
        /// <returns>LigneCommande</returns>
        public ligne_commande getUneLigneCommande(int identifiant)
        {
            try
            {
                uneLigneCommande = db.ligne_commande.FirstOrDefault(l => l.id == identifiant);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getUneLigneCommande : " + ex.Message);
            }
            return uneLigneCommande;
        }

        /// <summary>
        /// Permet d'ajouter une ligne de commande dans la table LIGNE_COMMANDE de la base de données
        /// </summary>
        /// <param name="uneLigneCommande">Objet LigneCommande instancié</param>
        public void insertLigneCommande(ligne_commande uneLigneCommande)
        {
            try
            {
                db.ligne_commande.Add(uneLigneCommande);
                db.SaveChanges();
                lesLignesCommande.Add(uneLigneCommande);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur insertLigneCommande : " + ex.Message);
            }
        }

        /// <summary>
        /// Permet de modifier une ligne de commande dans la table LIGNE_COMMANDE de la base de données
        /// </summary>
        /// <param name="uneLigneCommande">La ligne de commande à modifier</param>
        public void updateLigneCommande(ligne_commande uneLigneCommande)
        {
            try
            {
                ligne_commande ligneCommandeAModifier = getUneLigneCommande(uneLigneCommande.id);
                // Modifications des propriétés de la ligne de commande
                ligneCommandeAModifier.quantite = uneLigneCommande.quantite;
                ligneCommandeAModifier.un_produit_id = uneLigneCommande.un_produit_id;
                ligneCommandeAModifier.une_commande_id = uneLigneCommande.une_commande_id;

                db.SaveChanges();
                lesLignesCommande = getLesLignesCommande();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur updateLigneCommande : " + ex.Message);
            }
        }

        /// <summary>
        /// Permet de supprimer une ligne de commande de la table LIGNE_COMMANDE de la base de données
        /// </summary>
        /// <param name="id">ID de la ligne de commande à supprimer</param>
        public void deleteLigneCommande(int id)
        {
            try
            {
                ligne_commande ligneCommandeASupprimer = getUneLigneCommande(id);
                if (ligneCommandeASupprimer != null)
                {
                    db.ligne_commande.Remove(ligneCommandeASupprimer);
                    db.SaveChanges();
                    lesLignesCommande = getLesLignesCommande();
                }
                else
                {
                    MessageBox.Show("Ligne de commande non trouvée");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur deleteLigneCommande : " + ex.Message);
            }
        }
    }
}
