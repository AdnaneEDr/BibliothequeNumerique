using System;

namespace BibliothequeNumerique.Models
{
    /// <summary>
    /// Classe représentant un document PDF
    /// Hérite de Document et ajoute TailleEnMo (taille en mégaoctets)
    /// </summary>
    public class DocumentPDF : Document
    {
        // Propriété spécifique aux PDF
        public double TailleEnMo { get; set; }

        /// <summary>
        /// Constructeur pour créer un nouveau PDF
        /// </summary>
        public DocumentPDF(string titre, string auteur, int annee, double tailleEnMo)
            : base(titre, auteur, annee)
        {
            TailleEnMo = tailleEnMo;
        }

        /// <summary>
        /// Constructeur avec ID (pour charger depuis fichier)
        /// </summary>
        public DocumentPDF(Guid id, string titre, string auteur, int annee, double tailleEnMo)
            : base(id, titre, auteur, annee)
        {
            TailleEnMo = tailleEnMo;
        }

        /// <summary>
        /// Implémentation de la méthode abstraite
        /// Affiche les détails du PDF
        /// </summary>
        public override void AfficherDetails()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║              📄 DOCUMENT PDF                  ║");
            Console.WriteLine("╠═══════════════════════════════════════════════╣");
            Console.WriteLine($"║ ID          : {Id}");
            Console.WriteLine($"║ Titre       : {Titre}");
            Console.WriteLine($"║ Auteur      : {Auteur}");
            Console.WriteLine($"║ Année       : {Annee}");
            Console.WriteLine($"║ Taille      : {TailleEnMo} Mo");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");
        }

        /// <summary>
        /// Retourne le type pour la sauvegarde
        /// </summary>
        public override string GetTypeDocument()
        {
            return "PDF";
        }

        /// <summary>
        /// Format CSV pour la sauvegarde
        /// Utilise la culture invariante pour le double (point au lieu de virgule)
        /// </summary>
        public string ToCSV()
        {
            return $"{GetTypeDocument()};{Id};{Titre};{Auteur};{Annee};{TailleEnMo.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
        }
    }
}