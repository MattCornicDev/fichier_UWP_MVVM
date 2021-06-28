using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace fichier_uwp.modeles
{
    class FicConfig
    {
        private StorageFile ficHier;
        private StorageFolder Dossier;
        private string NomFichier;
        public bool creationOK;
      

        public string MessageErreur;
        public bool exceptionLevee;

        public string Contenu;

        public FicConfig(string NomFic)
        {
            Dossier = ApplicationData.Current.LocalFolder;
            NomFichier = NomFic;

        }
        /// <summary>
        /// Cette méthode permet de créer un fichier afin d'y sauvegarder la configuration du rover
        /// Si le fichier n'existe pas il est créé sinon on récupère le handle du fichier existant
        /// en cas d'erreur on récupère le message correspondant à l'erreur
        /// </summary>
        public async void CreerFic()
        {
            creationOK = false;

            try
            {
                ficHier = await Dossier.CreateFileAsync(NomFichier, CreationCollisionOption.OpenIfExists);
                creationOK = true;
            }

            catch (Exception Erreur)
            {
                MessageErreur = Erreur.Message;
            }
    

        }

        //////     Ecriture dans le fichier     /////

        public async void EcrFic(string Config)
        {
            exceptionLevee = false;
            
            try
            {
                if (!string.IsNullOrEmpty(Config))
                {
                    await FileIO.AppendTextAsync(ficHier, Config); // ecriture dans le fichier designé par FicHier
                  
                }
                else
                    //MessageErreur = "Il n'y a rien a écrire !";
                    throw new ArgumentNullException("Le contenu ne peut être nul");
            }
            catch (FileNotFoundException Erreur)
            {
                MessageErreur = Erreur.Message;
                exceptionLevee = true;
                
            }
            catch(ArgumentNullException Erreur)
            {
                MessageErreur = Erreur.Message;
                exceptionLevee = true;
            }
        }

        /////     Lecture du contenu du fichier     /////
        public async void LecFic()
        {
            exceptionLevee = false;

            try
            {
                Contenu = await FileIO.ReadTextAsync(ficHier);
            }
            catch (FileNotFoundException Erreur)
            {
                MessageErreur = Erreur.Message;
                exceptionLevee = true;
            }
        }
    }
}
