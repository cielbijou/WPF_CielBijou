using CielBijou.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CielBijou.ViewModel
{
    internal class ViewModelCategorie : INotifyPropertyChanged
    {
        cielbijouEntities2 db;

        public event PropertyChangedEventHandler PropertyChanged;

        private List<categorie> lesCategories;
        private categorie uneCategorie;

        public ViewModelCategorie()
        {
            db = new cielbijouEntities2();
            lesCategories = getLesCategories();
            uneCategorie = null;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<categorie> LesCategories
        {
            get => lesCategories;
            set { if (lesCategories != value) { lesCategories = value; OnPropertyChanged(); } }
        }
        public categorie UneCategorie
        {
            get => uneCategorie;
            set { if (uneCategorie != value) { uneCategorie = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// Charge la collection des catégories à partir de la table Categorie
        /// </summary>
        /// <returns></returns>
        public List<categorie> getLesCategories()
        {
            try
            {
                lesCategories = db.categorie.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getCategories : " + ex.Message);
            }
            return lesCategories;
        }

        public int getIdCategorie(string nomCategorie)
        {
            try
            {
                uneCategorie = db.categorie.FirstOrDefault(c => c.libelle_cat.Equals(nomCategorie));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getUneCategorie : " + ex.Message);
            }
            return uneCategorie.id;
        }

        public string getNomCategorie(int id)
        {
            try
            {
                uneCategorie = db.categorie.FirstOrDefault(c => c.id == id );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getUneCategorie : " + ex.Message);
            }
            string nomCat = "";
            if (uneCategorie != null) nomCat = uneCategorie.libelle_cat;
            return nomCat;
        }

        /// <summary>
        /// Retourne une catégorie précise
        /// </summary>
        /// <param name="identifiant"></param>
        /// <returns>Revue</returns>
        public categorie getUneCategorie(int identifiant)
        {
            try
            {
                uneCategorie = db.categorie.FirstOrDefault(c => c.id == identifiant);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getUneCategorie : " + ex.Message);
            }
            return uneCategorie;
        }

        /// <summary>
        /// Permet d'ajouter la catégorie passée en paramètre dans la table CATEGORIE de la base de données
        /// </summary>
        /// <param name="uneCategorie">Objet Catégorie instancié</param>
        public void insertCategorie(categorie uneCategorie)
        {
            try
            {
                db.categorie.Add(uneCategorie);
                db.SaveChanges();
                lesCategories.Add(uneCategorie);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur insertCategorie : " + ex.Message);
            }


        }
        /// <summary>
        /// /// Permet de modifier la catégorie passée en paramètre dans la table CATEGORIE de la base de données
        /// </summary>
        /// <param name="uneCategorie">La catégorie à modifier</param>
        public void updateCategorie(categorie uneCategorie)
        {
            try
            {
                categorie uneCat = getUneCategorie(uneCategorie.id);
                //categorie uneCat = db.categorie.FirstOrDefault(c => c.id == identifiant);
                uneCat.libelle_cat = uneCategorie.libelle_cat;

                db.SaveChanges();
                lesCategories = getLesCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur updateCategorie : " + ex.Message);
            }
        }

        /// <summary>
        /// Permet de supprimer la catégorie passée en paramètre dans la table CATEGORIE de la base de données
        /// </summary>
        /// <param name="id">ID de la catégorie : ENTIER</param>
        public void deleteCategorie(categorie laCategorie)
        {
            try
            {
                categorie uneCat = db.categorie.SingleOrDefault(c => c.id == laCategorie.id);
                db.categorie.Remove(uneCat);
                db.SaveChanges();
                lesCategories = getLesCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur deleteRevue : " + ex.Message);
            }

        }


    }
}
