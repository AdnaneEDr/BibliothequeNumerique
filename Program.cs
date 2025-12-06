using System;
using BibliothequeNumerique.Models;
using BibliothequeNumerique.Exceptions;

namespace BibliothequeNumerique
{
    class Program
    {
        static void Main(string[] args)
        {
            // Créer une instance de la bibliothèque
            Bibliotheque biblio = new Bibliotheque();
            bool continuer = true;

            AfficherBanniere();

            // Boucle principale du menu
            while (continuer)
            {
                AfficherMenu();
                string choix = Console.ReadLine() ?? "";

                try
                {
                    switch (choix)
                    {
                        case "1":
                            AjouterDocument(biblio);
                            break;
                        case "2":
                            biblio.AfficherTous();
                            break;
                        case "3":
                            RechercherDocument(biblio);
                            break;
                        case "4":
                            SupprimerDocument(biblio);
                            break;
                        case "5":
                            SauvegarderBibliotheque(biblio);
                            break;
                        case "6":
                            ChargerBibliotheque(biblio);
                            break;
                        case "7":
                            continuer = false;
                            Console.WriteLine("\n👋 Au revoir ! Merci d'avoir utilisé la bibliothèque numérique.");
                            break;
                        default:
                            Console.WriteLine("\n❌ Choix invalide. Veuillez choisir une option entre 1 et 7.");
                            break;
                    }
                }
                catch (DocumentNonTrouveException ex)
                {
                    Console.WriteLine($"\n❌ {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Une erreur s'est produite : {ex.Message}");
                }

                if (continuer)
                {
                    Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Affiche la bannière de l'application
        /// </summary>
        static void AfficherBanniere()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                          ║");
            Console.WriteLine("║           📚 BIBLIOTHÈQUE NUMÉRIQUE 📚                   ║");
            Console.WriteLine("║                                                          ║");
            Console.WriteLine("║              Système de Gestion de Documents            ║");
            Console.WriteLine("║                                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Affiche le menu principal
        /// </summary>
        static void AfficherMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n┌─────────────────────────────────────┐");
            Console.WriteLine("│         MENU PRINCIPAL              │");
            Console.WriteLine("└─────────────────────────────────────┘");
            Console.ResetColor();

            Console.WriteLine("\n  1. ➕  Ajouter un document");
            Console.WriteLine("  2. 📋  Afficher tous les documents");
            Console.WriteLine("  3. 🔍  Rechercher par mot-clé");
            Console.WriteLine("  4. 🗑️   Supprimer un document");
            Console.WriteLine("  5. 💾  Sauvegarder dans un fichier");
            Console.WriteLine("  6. 📂  Charger depuis un fichier");
            Console.WriteLine("  7. 🚪  Quitter");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n👉 Votre choix : ");
            Console.ResetColor();
        }

        /// <summary>
        /// Gère l'ajout d'un nouveau document
        /// </summary>
        static void AjouterDocument(Bibliotheque biblio)
        {
            Console.Clear();
            Console.WriteLine("┌─────────────────────────────────────┐");
            Console.WriteLine("│      AJOUTER UN DOCUMENT            │");
            Console.WriteLine("└─────────────────────────────────────┘\n");

            Console.WriteLine("Quel type de document voulez-vous ajouter ?");
            Console.WriteLine("  1. 📖 Livre");
            Console.WriteLine("  2. 📰 Magazine");
            Console.WriteLine("  3. 📄 Document PDF");
            Console.Write("\nVotre choix : ");

            string typeChoix = Console.ReadLine() ?? "";

            try
            {
                // Saisie des informations communes
                Console.Write("\nTitre : ");
                string titre = Console.ReadLine() ?? "";

                Console.Write("Auteur : ");
                string auteur = Console.ReadLine() ?? "";

                Console.Write("Année de publication : ");
                int annee = int.Parse(Console.ReadLine() ?? "0");

                Document? document = null;

                // Création selon le type
                switch (typeChoix)
                {
                    case "1":
                        Console.Write("Nombre de pages : ");
                        int pages = int.Parse(Console.ReadLine() ?? "0");
                        document = new Livre(titre, auteur, annee, pages);
                        break;

                    case "2":
                        Console.Write("Numéro du magazine : ");
                        int numero = int.Parse(Console.ReadLine() ?? "0");
                        document = new Magazine(titre, auteur, annee, numero);
                        break;

                    case "3":
                        Console.Write("Taille en Mo : ");
                        double taille = double.Parse(Console.ReadLine() ?? "0");
                        document = new DocumentPDF(titre, auteur, annee, taille);
                        break;

                    default:
                        Console.WriteLine("\n❌ Type de document invalide.");
                        return;
                }

                if (document != null)
                {
                    biblio.AjouterDocument(document);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\n❌ Erreur : Format de données incorrect. Veuillez entrer des valeurs valides.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Erreur lors de l'ajout : {ex.Message}");
            }
        }

        /// <summary>
        /// Gère la recherche de documents
        /// </summary>
        static void RechercherDocument(Bibliotheque biblio)
        {
            Console.Clear();
            Console.WriteLine("┌─────────────────────────────────────┐");
            Console.WriteLine("│      RECHERCHER UN DOCUMENT         │");
            Console.WriteLine("└─────────────────────────────────────┘\n");

            Console.Write("Entrez un mot-clé (titre ou auteur) : ");
            string motCle = Console.ReadLine() ?? "";

            try
            {
                var resultats = biblio.Rechercher(motCle);

                Console.WriteLine($"\n🔍 {resultats.Count} résultat(s) trouvé(s) :\n");

                foreach (var doc in resultats)
                {
                    doc.AfficherDetails();
                    Console.WriteLine();
                }
            }
            catch (DocumentNonTrouveException ex)
            {
                Console.WriteLine($"\n❌ {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n❌ {ex.Message}");
            }
        }

        /// <summary>
        /// Gère la suppression d'un document
        /// </summary>
        static void SupprimerDocument(Bibliotheque biblio)
        {
            Console.Clear();
            Console.WriteLine("┌─────────────────────────────────────┐");
            Console.WriteLine("│      SUPPRIMER UN DOCUMENT          │");
            Console.WriteLine("└─────────────────────────────────────┘\n");

            // Afficher d'abord tous les documents
            biblio.AfficherTous();

            Console.Write("\nEntrez l'ID du document à supprimer : ");
            string idString = Console.ReadLine() ?? "";

            try
            {
                Guid id = Guid.Parse(idString);

                Console.Write($"\n⚠️  Êtes-vous sûr de vouloir supprimer ce document ? (o/n) : ");
                string confirmation = Console.ReadLine()?.ToLower() ?? "";

                if (confirmation == "o" || confirmation == "oui")
                {
                    biblio.SupprimerDocument(id);
                }
                else
                {
                    Console.WriteLine("\n❌ Suppression annulée.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\n❌ Erreur : Format d'ID invalide.");
            }
            catch (DocumentNonTrouveException ex)
            {
                Console.WriteLine($"\n❌ {ex.Message}");
            }
        }

        /// <summary>
        /// Gère la sauvegarde de la bibliothèque
        /// </summary>
        static void SauvegarderBibliotheque(Bibliotheque biblio)
        {
            Console.Clear();
            Console.WriteLine("┌─────────────────────────────────────┐");
            Console.WriteLine("│      SAUVEGARDER LA BIBLIOTHÈQUE    │");
            Console.WriteLine("└─────────────────────────────────────┘\n");

            Console.Write("Nom du fichier (par défaut: bibliotheque_sauvegarde.txt) : ");
            string nomFichier = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(nomFichier))
            {
                nomFichier = "bibliotheque_sauvegarde.txt";
            }

            biblio.Sauvegarder(nomFichier);
        }

        /// <summary>
        /// Gère le chargement de la bibliothèque
        /// </summary>
        static void ChargerBibliotheque(Bibliotheque biblio)
        {
            Console.Clear();
            Console.WriteLine("┌─────────────────────────────────────┐");
            Console.WriteLine("│      CHARGER LA BIBLIOTHÈQUE        │");
            Console.WriteLine("└─────────────────────────────────────┘\n");

            Console.Write("Nom du fichier à charger : ");
            string nomFichier = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(nomFichier))
            {
                Console.WriteLine("\n❌ Nom de fichier invalide.");
                return;
            }

            if (biblio.Count > 0)
            {
                Console.Write("\n⚠️  La bibliothèque actuelle contient des documents. Voulez-vous vraiment la remplacer ? (o/n) : ");
                string confirmation = Console.ReadLine()?.ToLower() ?? "";

                if (confirmation != "o" && confirmation != "oui")
                {
                    Console.WriteLine("\n❌ Chargement annulé.");
                    return;
                }
            }

            biblio.Charger(nomFichier);
        }
    }
}