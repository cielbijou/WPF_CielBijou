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
    internal class ViewModelProduit : INotifyPropertyChanged
    {
        cielbijouEntities2 db;

        public event PropertyChangedEventHandler PropertyChanged;

        private List<produit> lesProduits;
        private produit unProduit;

        public ViewModelProduit()
        {
            db = new cielbijouEntities2();
            lesProduits = getLesProduits();
            unProduit = null;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<produit> LesProduits
        {
            get => lesProduits;
            set { if (lesProduits != value) { lesProduits = value; OnPropertyChanged(); } }
        }
        public produit leProduit
        {
            get => unProduit;
            set { if (unProduit != value) { unProduit = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// Charge la collection des catégories à partir de la table Categorie
        /// </summary>
        /// <returns></returns>
        public List<produit> getLesProduits()
        {
            try
            {
                lesProduits = db.produit.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getLesProduits : " + ex.Message);
            }
            return lesProduits;
        }

        public List<produit> getLesProduits(string nom)
        {
            try
            {
                lesProduits = db.produit.Where(p=>p.nom_prod.Contains(nom)).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getLesProduits : " + ex.Message);
            }
            return lesProduits;
        }


        /// <summary>
        /// Retourne une catégorie précise
        /// </summary>
        /// <param name="identifiant"></param>
        /// <returns>Revue</returns>
        public produit getUnProduit(int identifiant)
        {
            try
            {
                unProduit = db.produit.FirstOrDefault(c => c.id.Equals(identifiant));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getUnProduit : " + ex.Message);
            }
            return unProduit;
        }

        /// <summary>
        /// Permet d'ajouter la catégorie passée en paramètre dans la table CATEGORIE de la base de données
        /// </summary>
        /// <param name="unProduit">Objet Catégorie instancié</param>
        public void insertProduit(produit unProduit)
        {
            try
            {
                db.produit.Add(unProduit);
                db.SaveChanges();
                lesProduits.Add(unProduit);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur insertProduit : " + ex.Message);
            }


        }
        /// <summary>
        /// /// Permet de modifier la catégorie passée en paramètre dans la table CATEGORIE de la base de données
        /// </summary>
        /// <param name="unProduit">La catégorie à modifier</param>
        public void updateProduit(produit unProduit)
        {
            try
            {
                produit unProd = getUnProduit(unProduit.id);
                //categorie uneCat = db.categorie.FirstOrDefault(c => c.id == identifiant);
                unProd.prix_prod = unProduit.prix_prod;
                unProd.image_prod = unProduit.image_prod;
                unProd.nom_prod = unProduit.nom_prod;
                unProd.stock_prod = unProduit.stock_prod;

                db.SaveChanges();
                lesProduits = getLesProduits();
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
        public void deleteProduit(int id)
        {
            try
            {
                unProduit = db.produit.Where(p => p.id == id).FirstOrDefault();
                db.produit.Remove(unProduit);
                db.SaveChanges();
                lesProduits = getLesProduits();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur deleteProduit : " + ex.Message);
            }

        }

    }
}
