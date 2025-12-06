using System;

namespace BibliothequeNumerique.Models
{
    /// <summary>
    /// Classe abstraite représentant un document générique
    /// Elle sert de base pour tous les types de documents
    /// </summary>
    public abstract class Document
    {
        // Propriétés automatiques
        public Guid Id { get; set; }
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public int Annee { get; set; }

        /// <summary>
        /// Constructeur paramétré
        /// Génère automatiquement un ID unique avec Guid.NewGuid()
        /// </summary>
        /// <param name="titre">Titre du document</param>
        /// <param name="auteur">Auteur du document</param>
        /// <param name="annee">Année de publication</param>
        protected Document(string titre, string auteur, int annee)
        {
            Id = Guid.NewGuid(); // Génère un ID unique automatiquement
            Titre = titre;
            Auteur = auteur;
            Annee = annee;
        }

        /// <summary>
        /// Constructeur avec ID existant (pour le chargement depuis fichier)
        /// </summary>
        protected Document(Guid id, string titre, string auteur, int annee)
        {
            Id = id;
            Titre = titre;
            Auteur = auteur;
            Annee = annee;
        }

        /// <summary>
        /// Méthode abstraite - chaque classe dérivée doit l'implémenter
        /// Affiche les détails spécifiques du document
        /// </summary>
        public abstract void AfficherDetails();

        /// <summary>
        /// Retourne le type du document (utilisé pour la sauvegarde)
        /// </summary>
        public abstract string GetTypeDocument();
    }
}