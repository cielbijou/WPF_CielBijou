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
    public class ViewModelPromotion : INotifyPropertyChanged
    {
        cielbijouEntities2 db;

        public event PropertyChangedEventHandler PropertyChanged;

        private List<promotion> lesPromotions;
        private promotion unePromotion;

        public ViewModelPromotion()
        {
            db = new cielbijouEntities2();
            lesPromotions = getLesPromotions();
            unePromotion = null;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<promotion> LesPromotions
        {
            get => lesPromotions;
            set { if (lesPromotions != value) { lesPromotions = value; OnPropertyChanged(); } }
        }
        public promotion UnePromotion
        {
            get => unePromotion;
            set { if (unePromotion != value) { unePromotion = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// Charge la collection des catégories à partir de la table Categorie
        /// </summary>
        /// <returns></returns>
        public List<promotion> getLesPromotions()
        {
            try
            {
                LesPromotions = db.promotion.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getLesProduits : " + ex.Message);
            }
            return LesPromotions;
        }


        /// <summary>
        /// Retourne une catégorie précise
        /// </summary>
        /// <param name="identifiant"></param>
        /// <returns>Revue</returns>
        public promotion getUnePromotions(int identifiant)
        {
            try
            {
                unePromotion = db.promotion.FirstOrDefault(c => c.id == identifiant);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getUnProduit : " + ex.Message);
            }
            return unePromotion;
        }

        /// <summary>
        /// Permet d'ajouter la catégorie passée en paramètre dans la table CATEGORIE de la base de données
        /// </summary>
        /// <param name="unePromotion">Objet Catégorie instancié</param>
        public void insertPromotions(promotion unePromotion)
        {
            try
            {
                db.promotion.Add(unePromotion);
                db.SaveChanges();
                lesPromotions.Add(unePromotion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur insertProduit : " + ex.Message);
            }


        }
        /// <summary>
        /// /// Permet de modifier la catégorie passée en paramètre dans la table CATEGORIE de la base de données
        /// </summary>
        /// <param name="unePromotion">La catégorie à modifier</param>
        public void updatePromotions(promotion unePromotion)
        {
            try
            {
                promotion unePromo = getUnePromotions(unePromotion.id);
                //categorie uneCat = db.categorie.FirstOrDefault(c => c.id == identifiant);
                unePromo.date_fin_promo = unePromotion.date_fin_promo;
                unePromo.remise_promo = unePromotion.remise_promo;
                unePromo.date_debut_promo = unePromotion.date_debut_promo;
                db.SaveChanges();
                lesPromotions = getLesPromotions();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur updateProduits : " + ex.Message);
            }
        }

        /// <summary>
        /// Permet de supprimer la catégorie passée en paramètre dans la table CATEGORIE de la base de données
        /// </summary>
        /// <param name="id">ID de la catégorie : ENTIER</param>
        public void deletePromotions(promotion unePromo)
        {
            try
            {
                db.promotion.Remove(unePromo);
                db.SaveChanges();
                lesPromotions = getLesPromotions();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur deletePromotions : " + ex.Message);
            }

        }
    }
}
