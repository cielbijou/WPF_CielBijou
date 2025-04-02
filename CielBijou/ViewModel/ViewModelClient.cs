using CielBijou.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CielBijou.ViewModel
{
    internal class ViewModelClient : INotifyPropertyChanged
    {
        cielbijouEntities2 db;

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<client> lesClients;
        private client unClient;
        private client selectedClient;

        public ViewModelClient()
        {
            db = new cielbijouEntities2();
            lesClients = getLesClients();
            unClient = null;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<client> LesClients
        {
            get => lesClients;
            set { if (lesClients != value) { lesClients = value; OnPropertyChanged(); } }
        }
        public client UnClient
        {
            get => unClient;
            set { if (unClient != value) { unClient = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// Charge la collection des clients à partir de la table Client
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<client> getLesClients()
        {
            try
            {
                LesClients = new ObservableCollection<client>(db.client.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getLesClients : " + ex.Message);
            }
            return LesClients;
        }

        public client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }

        public string getId(int id)
        {
           var idCli = db.client.FirstOrDefault(c => c.id.Equals(id));
            if(idCli != null)
            {
                return idCli.id.ToString();
            } return "";
        }

        public string getNom(int id)
        {
            var idCli = db.client.FirstOrDefault(c => c.id.Equals(id));
            if (idCli != null)
            {
                return idCli.nom;
            }
            return "";
        }

        public string getPrenom(int id)
        {
            var idCli = db.client.FirstOrDefault(c => c.id.Equals(id));
            if (idCli != null)
            {
                return idCli.prenom;
            }
            return "";
        }

        public string getRoles(int id)
        {
            var idCli = db.client.FirstOrDefault(c => c.id.Equals(id));
            if (idCli != null)
            {
                return idCli.roles;
            }
            return "";
        }

        public String getPhtot(int id)
        {
            var idCli = db.client.FirstOrDefault(c => c.id ==id);
            if (idCli != null)
            {
                return idCli.photo;
            }
            return null;
        }


        public void getLesClients(int idClient)
        {
            try
            {
                SelectedClient = db.client.FirstOrDefault(c => c.id.Equals(idClient));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getLesClients : " + ex.Message);
            }
        }

        /// <summary>
        /// Charge la collection des clients à partir de la table Client
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<client> getLesClients(String nom, String prenom, String mail, String groupe)
        {
            try
            {
                LesClients = new ObservableCollection<client>(db.client.Where(c=>c.nom.Contains(nom) && c.prenom.Contains(prenom) && c.email.Contains(mail) && c.roles.Contains(groupe)).ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getLesClients : " + ex.Message);
            }
            return LesClients;
        }

        /// <summary>
        /// Retourne un client précis en fonction de son identifiant
        /// </summary>
        /// <param name="identifiant"></param>
        /// <returns>Client</returns>
        public client getUnClient(int identifiant)
        {
            try
            {
                unClient = db.client.FirstOrDefault(c => c.id == identifiant);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getUnClient : " + ex.Message);
            }
            return UnClient;
        }

        public client getUnClientByMail(string mail)
        {
            try
            {
                unClient = db.client.FirstOrDefault(c => c.email.Equals(mail));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur getUnClient : " + ex.Message);
            }
            return UnClient;
        }

        /// <summary>
        /// Permet d'ajouter un client dans la table CLIENT de la base de données
        /// </summary>
        /// <param name="unClient">Objet Client instancié</param>
        public void insertClient(client unClient)
        {
            try
            {
                db.client.Add(unClient);
                db.SaveChanges();
                lesClients.Add(unClient);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur insertClient : " + ex.Message);
            }
        }

        /// <summary>
        /// Permet de modifier un client dans la table CLIENT de la base de données
        /// </summary>
        /// <param name="unClient">Le client à modifier</param>
        public void updateClient(client unClient)
        {
            try
            {
                client clientAModifier = db.client.Single(c => c.id == unClient.id);
                // Modifications des propriétés du client
                clientAModifier.nom = unClient.nom;
                    clientAModifier.prenom = unClient.prenom;
                    clientAModifier.email = unClient.email;
                    clientAModifier.commande = unClient.commande;
                    clientAModifier.roles = unClient.roles;
                    clientAModifier.password = unClient.password;
                    clientAModifier.photo = unClient.photo;

                db.SaveChanges();


                client unUtil = LesClients.FirstOrDefault(c => c.id == unClient.id);
                if (unUtil != null)
                {
                    unUtil.nom = unClient.nom;
                    unUtil.prenom = unClient.prenom;
                    unUtil.roles = unClient.roles;
                    unUtil.email = unClient.email;
                    unUtil.photo = unClient.photo;
                    unUtil.password = unClient.password;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur updateClient : " + ex.Message);
            }
        }

        /// <summary>
        /// Permet de supprimer un client de la table CLIENT de la base de données
        /// </summary>
        /// <param name="id">ID du client à supprimer</param>
        public void deleteClient(int id)
        {
            try
            {
                client clientASupprimer = getUnClient(id);
                if (clientASupprimer != null)
                {
                    db.client.Remove(clientASupprimer);
                    db.SaveChanges();
                    lesClients = getLesClients();
                }
                else
                {
                    MessageBox.Show("Client non trouvé");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur deleteClient : " + ex.Message);
            }
        }
    }
}

