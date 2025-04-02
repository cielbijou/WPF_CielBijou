using CielBijou.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CielBijou.ViewModel
{
    internal class ViewModelCommentaire : INotifyPropertyChanged
    {
        cielbijouEntities2 db;

        public event PropertyChangedEventHandler PropertyChanged;

        private List<commentaire> lesCommentaires;
        private commentaire unCommentaire;

        public ViewModelCommentaire()
        {
            db = new cielbijouEntities2();
            lesCommentaires = getLesCommentaires();
            unCommentaire = null;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<commentaire> LesCommentaires
        {
            get => lesCommentaires;
            set { if (lesCommentaires != value) { lesCommentaires = value; OnPropertyChanged(); } }
        }
        public commentaire leCommentaire
        {
            get => unCommentaire;
            set { if (unCommentaire != value) { unCommentaire = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// Charge la collection des catégories à partir de la table Categorie
        /// </summary>
        /// <returns></returns>
        public List<commentaire> getLesCommentaires()
        {
            try
            {
                lesCommentaires = db.commentaire.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getLesProduits : " + ex.Message);
            }
            return lesCommentaires;
        }


        /// <summary>
        /// Retourne une catégorie précise
        /// </summary>
        /// <param name="identifiant"></param>
        /// <returns>Revue</returns>
        public commentaire getUnCommentaire(int identifiant)
        {
            try
            {
                unCommentaire = db.commentaire.FirstOrDefault(c => c.id == identifiant);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getUnCommentaire : " + ex.Message);
            }
            return unCommentaire;
        }

        /// <summary>
        /// Permet d'ajouter la catégorie passée en paramètre dans la table CATEGORIE de la base de données
        /// </summary>
        /// <param name="unCommentaire">Objet Catégorie instancié</param>
        public void insertCommentaire(commentaire unCommentaire)
        {
            try
            {
                db.commentaire.Add(unCommentaire);
                db.SaveChanges();
                lesCommentaires.Add(unCommentaire);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur insertCommentaire : " + ex.Message);
            }


        }
        /// <summary>
        /// /// Permet de modifier la catégorie passée en paramètre dans la table CATEGORIE de la base de données
        /// </summary>
        /// <param name="unCommentaire">La catégorie à modifier</param>
        public void updateCommentaire(commentaire unCommentaire)
        {
            try
            {
                commentaire unCom = getUnCommentaire(unCommentaire.id);
                //categorie uneCat = db.categorie.FirstOrDefault(c => c.id == identifiant);
                unCom.produit = unCommentaire.produit;
                unCom.client = unCommentaire.client;
                unCom.date_commentaire = unCommentaire.date_commentaire;
                unCom.status_commentaire = unCommentaire.status_commentaire;
                unCom.un_client_id = unCommentaire.un_client_id;
                unCom.un_produit_id = unCommentaire.un_produit_id;

                db.SaveChanges();
                lesCommentaires = getLesCommentaires();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur updateCommentaires : " + ex.Message);
            }
        }

        /// <summary>
        /// Permet de supprimer la catégorie passée en paramètre dans la table CATEGORIE de la base de données
        /// </summary>
        /// <param name="id">ID de la catégorie : ENTIER</param>
        public void deleteCommentaire(int id)
        {
            try
            {
                commentaire unCom = getUnCommentaire(unCommentaire.id);
                db.commentaire.Remove(unCom);
                db.SaveChanges();
                lesCommentaires = getLesCommentaires();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur deleteProduit : " + ex.Message);
            }

        }
    }
}
