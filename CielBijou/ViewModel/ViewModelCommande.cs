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
    internal class ViewModelCommande : INotifyPropertyChanged
    {
        cielbijouEntities2 db;

        public event PropertyChangedEventHandler PropertyChanged;

        private List<commande> lesCommandes;
        private commande uneCommande;

        public ViewModelCommande()
        {
            db = new cielbijouEntities2();
            lesCommandes = getLesCommandes();
            uneCommande = null;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<commande> LesCommandes
        {
            get => lesCommandes;
            set { if (lesCommandes != value) { lesCommandes = value; OnPropertyChanged(); } }
        }
        public commande UneCommande
        {
            get => uneCommande;
            set { if (uneCommande != value) { uneCommande = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// Charge la collection des commandes à partir de la table Commande
        /// </summary>
        /// <returns></returns>
        public List<commande> getLesCommandes()
        {
            try
            {
                lesCommandes = db.commande.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getLesCommandes : " + ex.Message);
            }
            return lesCommandes;
        }

        /// <summary>
        /// Charge la collection des commandes à partir de la table Commande
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<commande> getLesCommandes(string id, string date)
        {
            ObservableCollection<commande> lesCommandes = new ObservableCollection<commande>();

                try
                {
                    lesCommandes = new ObservableCollection<commande>(db.commande.Where(c => c.id.ToString().Contains(id) && c.date_commande.ToString().Contains(date)).ToList());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur getLesCommandes : " + ex.Message);
                }

                return lesCommandes;
        }

        /// <summary>
        /// Retourne une commande précise en fonction de son identifiant
        /// </summary>
        /// <param name="identifiant"></param>
        /// <returns>Commande</returns>
        public commande getUneCommande(int identifiant)
        {
            try
            {
                uneCommande = db.commande.FirstOrDefault(c => c.id == identifiant);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getUneCommande : " + ex.Message);
            }
            return uneCommande;
        }

        /// <summary>
        /// Permet d'ajouter une commande dans la table COMMANDE de la base de données
        /// </summary>
        /// <param name="uneCommande">Objet Commande instancié</param>
        public void insertCommande(commande uneCommande)
        {
            try
            {
                db.commande.Add(uneCommande);
                db.SaveChanges();
                lesCommandes.Add(uneCommande);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur insertCommande : " + ex.Message);
            }
        }

        /// <summary>
        /// Permet de modifier une commande dans la table COMMANDE de la base de données
        /// </summary>
        /// <param name="uneCommande">La commande à modifier</param>
        public void updateCommande(commande uneCommande)
        {
            try
            {
                commande commandeAModifier = getUneCommande(uneCommande.id);
                // Modifications des propriétés de la commande
                commandeAModifier.date_commande = uneCommande.date_commande;
                commandeAModifier.un_client_id = uneCommande.un_client_id;

                db.SaveChanges();
                lesCommandes = getLesCommandes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur updateCommande : " + ex.Message);
            }
        }

        /// <summary>
        /// Permet de supprimer une commande de la table COMMANDE de la base de données
        /// </summary>
        /// <param name="id">ID de la commande à supprimer</param>
        public void deleteCommande(int id)
        {
            try
            {
                commande commandeASupprimer = getUneCommande(id);
                if (commandeASupprimer != null)
                {
                    db.commande.Remove(commandeASupprimer);
                    db.SaveChanges();
                    lesCommandes = getLesCommandes();
                }
                else
                {
                    MessageBox.Show("Commande non trouvée");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur deleteCommande : " + ex.Message);
            }
        }
    }
}
