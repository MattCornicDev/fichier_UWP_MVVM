using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fichier_uwp.modeles;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Windows.UI.Popups;

namespace fichier_uwp.VuesModeles
{
    public class MainViewModel : ViewModelBase
    {
        private string nomFichier;
        private string texteEcrit;
        private string texteLu;
        private RelayCommand creerFichier;
        private RelayCommand ecrireFichier;
        private RelayCommand lireFichier;
        private RelayCommand annuler;
        

        private FicConfig Fichier;

       
       

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }
        #region Proprietes

        // Propriétés

      

        /// <summary>
        /// Les propriétés sont exposées à la Vue MainWindows, le Binding est bidirectionnel
        /// Chaque fois qu'une propriété est modifiée elle appelle RaisePropertyChanged pour déclencher
        /// INPC et ainsi provoquer la mise à jour de la propriété Informations
        /// </summary>

        public string NomFichier
        {
            get { return nomFichier; }
            set
            {
                Set(() => NomFichier, ref nomFichier, value);
            }
        }

        public string TexteLu
        {
            get { return texteLu; }
            set
            {
                Set(() => TexteLu, ref texteLu, value);
                RaisePropertyChanged(() => TexteLu);
            }
        }

        public string TexteEcrit
        {
            get { return texteEcrit; }
            set
            {
                Set(() => TexteEcrit, ref texteEcrit, value);

            }
        }

        #endregion


        #region Commandes

        /// <summary>
        /// La commande CreerFichier est une RelayCommand elle est liée au bouton Valider qui permet de créer un fichier si celui ci n'zxiste pas ou de récupérer le handle existant
        /// 
        /// </summary>
        public RelayCommand CreerFichier
        {

            get
            {
                return creerFichier ?? (creerFichier = new RelayCommand(Creation, CanCreation));
            }
        }
        
        public RelayCommand EcrireFichier
        {
            get
            {
                return ecrireFichier ?? (ecrireFichier= new RelayCommand(EcrireTexte, CanEcrire));

            }
        }
        
        public RelayCommand LireFichier
        {
            get
            {
                return lireFichier ?? (lireFichier = new RelayCommand(LireTexte, CanLire));
            }
        }
        
        #endregion

        #region méthodes des commandes

        private async void Creation()
        {
            string MessageRetour;
            
            Fichier = new FicConfig(NomFichier);
            Fichier.CreerFic();
            if(Fichier.creationOK!=true)
            {
                MessageRetour = Fichier.MessageErreur;
                var messageDialog = new MessageDialog("Une erreur est survenue "+MessageRetour);
                await messageDialog.ShowAsync();

            }
            else
            {
                
                var messageDialog = new MessageDialog("Le fichier a été créé avec succès !");
                await messageDialog.ShowAsync();

            }
                

            
            
        }

        private bool CanCreation()
        {
 
            return true;

        }

        private async void EcrireTexte()
        {
            
            
                         
                Fichier.EcrFic(TexteEcrit);
                if(Fichier.exceptionLevee==true)
                {
                    var messageDialog = new MessageDialog(Fichier.MessageErreur);
                    await messageDialog.ShowAsync();
                }

           

        }
      
        private bool CanEcrire()
        {

            return true;

        }

        private async void LireTexte()
        {
            Fichier.LecFic();
            if (Fichier.exceptionLevee == true)
            {
                var messageDialog = new MessageDialog(Fichier.MessageErreur);
                await messageDialog.ShowAsync();
            }
            else
            {
               
                TexteLu = Fichier.Contenu;
                
            }
                
        }
     
        private bool CanLire()
        {


            return true;

        }


        #endregion
    }
}
