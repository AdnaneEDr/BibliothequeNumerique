using System;

namespace BibliothequeNumerique.Models
{
    /// <summary>
    /// Classe représentant un livre
    /// Hérite de Document et ajoute NombrePages
    /// </summary>
    public class Livre : Document
    {
        // Propriété spécifique aux livres
        public int NombrePages { get; set; }

        /// <summary>
        /// Constructeur pour créer un nouveau livre
        /// </summary>
        public Livre(string titre, string auteur, int annee, int nombrePages)
            : base(titre, auteur, annee) // Appelle le constructeur de la classe parent
        {
            NombrePages = nombrePages;
        }

        /// <summary>
        /// Constructeur avec ID (pour charger depuis fichier)
        /// </summary>
        public Livre(Guid id, string titre, string auteur, int annee, int nombrePages)
            : base(id, titre, auteur, annee)
        {
            NombrePages = nombrePages;
        }

        /// <summary>
        /// Implémentation de la méthode abstraite
        /// Affiche les détails du livre
        /// </summary>
        public override void AfficherDetails()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║              📖 LIVRE                         ║");
            Console.WriteLine("╠═══════════════════════════════════════════════╣");
            Console.WriteLine($"║ ID          : {Id}");
            Console.WriteLine($"║ Titre       : {Titre}");
            Console.WriteLine($"║ Auteur      : {Auteur}");
            Console.WriteLine($"║ Année       : {Annee}");
            Console.WriteLine($"║ Pages       : {NombrePages}");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");
        }

        /// <summary>
        /// Retourne le type pour la sauvegarde
        /// </summary>
        public override string GetTypeDocument()
        {
            return "LIVRE";
        }

        /// <summary>
        /// Format CSV pour la sauvegarde
        /// Format: TYPE;ID;Titre;Auteur;Annee;ProprieteSpecifique
        /// </summary>
        public string ToCSV()
        {
            return $"{GetTypeDocument()};{Id};{Titre};{Auteur};{Annee};{NombrePages}";
        }
    }
}