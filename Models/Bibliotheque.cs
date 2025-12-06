using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using BibliothequeNumerique.Exceptions;

namespace BibliothequeNumerique.Models
{
    /// <summary>
    /// Classe représentant une bibliothèque numérique
    /// Gère une collection de documents avec toutes les opérations CRUD
    /// </summary>
    public class Bibliotheque
    {
        // Liste privée contenant tous les documents
        private List<Document> documents;

        /// <summary>
        /// Constructeur - initialise la liste vide
        /// </summary>
        public Bibliotheque()
        {
            documents = new List<Document>();
        }

        /// <summary>
        /// Ajoute un document à la bibliothèque
        /// </summary>
        /// <param name="document">Document à ajouter</param>
        public void AjouterDocument(Document document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document), "Le document ne peut pas être null.");
            }

            documents.Add(document);
            Console.WriteLine($"\n✅ Document ajouté avec succès ! ID: {document.Id}");
        }

        /// <summary>
        /// Supprime un document par son ID
        /// Lève DocumentNonTrouveException si le document n'existe pas
        /// </summary>
        /// <param name="id">ID du document à supprimer</param>
        public void SupprimerDocument(Guid id)
        {
            var document = documents.FirstOrDefault(d => d.Id == id);
            
            if (document == null)
            {
                throw new DocumentNonTrouveException(id);
            }

            documents.Remove(document);
            Console.WriteLine($"\n✅ Document supprimé avec succès !");
        }

        /// <summary>
        /// Recherche des documents contenant un mot-clé
        /// Cherche dans le titre, l'auteur
        /// </summary>
        /// <param name="motCle">Mot-clé à rechercher</param>
        /// <returns>Liste des documents trouvés</returns>
        public List<Document> Rechercher(string motCle)
        {
            if (string.IsNullOrWhiteSpace(motCle))
            {
                throw new ArgumentException("Le mot-clé ne peut pas être vide.", nameof(motCle));
            }

            var resultats = documents.Where(d =>
                d.Titre.Contains(motCle, StringComparison.OrdinalIgnoreCase) ||
                d.Auteur.Contains(motCle, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            if (resultats.Count == 0)
            {
                throw new DocumentNonTrouveException($"Aucun document trouvé avec le mot-clé '{motCle}'.");
            }

            return resultats;
        }

        /// <summary>
        /// Affiche tous les documents de la bibliothèque
        /// </summary>
        public void AfficherTous()
        {
            if (documents.Count == 0)
            {
                Console.WriteLine("\n📚 La bibliothèque est vide.");
                return;
            }

            Console.WriteLine($"\n📚 Bibliothèque - Total: {documents.Count} document(s)\n");
            foreach (var doc in documents)
            {
                doc.AfficherDetails();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Sauvegarde la bibliothèque dans un fichier CSV
        /// Format: TYPE;ID;Titre;Auteur;Annee;ProprieteSpecifique
        /// </summary>
        /// <param name="cheminFichier">Chemin du fichier de sauvegarde</param>
        public void Sauvegarder(string cheminFichier)
        {
            try
            {
                // using garantit la fermeture automatique des streams
                using (FileStream fs = new FileStream(cheminFichier, FileMode.Create, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    foreach (var doc in documents)
                    {
                        string ligne = "";
                        
                        // Utilise le polymorphisme pour obtenir la bonne représentation
                        if (doc is Livre livre)
                        {
                            ligne = livre.ToCSV();
                        }
                        else if (doc is Magazine magazine)
                        {
                            ligne = magazine.ToCSV();
                        }
                        else if (doc is DocumentPDF pdf)
                        {
                            ligne = pdf.ToCSV();
                        }

                        writer.WriteLine(ligne);
                    }
                }

                Console.WriteLine($"\n💾 Bibliothèque sauvegardée avec succès dans '{cheminFichier}'");
                Console.WriteLine($"📊 {documents.Count} document(s) enregistré(s)");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"\n❌ Erreur : Accès refusé au fichier '{cheminFichier}'");
                Console.WriteLine($"   Détails: {ex.Message}");
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"\n❌ Erreur : Le répertoire n'existe pas");
                Console.WriteLine($"   Détails: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"\n❌ Erreur d'entrée/sortie lors de la sauvegarde");
                Console.WriteLine($"   Détails: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Erreur inattendue lors de la sauvegarde");
                Console.WriteLine($"   Détails: {ex.Message}");
            }
        }

        /// <summary>
        /// Charge la bibliothèque depuis un fichier CSV
        /// </summary>
        /// <param name="cheminFichier">Chemin du fichier à charger</param>
        public void Charger(string cheminFichier)
        {
            try
            {
                if (!File.Exists(cheminFichier))
                {
                    throw new FileNotFoundException($"Le fichier '{cheminFichier}' n'existe pas.");
                }

                // Vider la bibliothèque actuelle
                documents.Clear();
                int ligneNum = 0;

                // using garantit la fermeture automatique des streams
                using (FileStream fs = new FileStream(cheminFichier, FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(fs))
                {
                    string? ligne;
                    while ((ligne = reader.ReadLine()) != null)
                    {
                        ligneNum++;

                        if (string.IsNullOrWhiteSpace(ligne))
                            continue;

                        try
                        {
                            var document = ParseLigne(ligne);
                            documents.Add(document);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"⚠️  Ligne {ligneNum} ignorée : {ex.Message}");
                        }
                    }
                }

                Console.WriteLine($"\n📂 Bibliothèque chargée avec succès depuis '{cheminFichier}'");
                Console.WriteLine($"📊 {documents.Count} document(s) chargé(s)");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"\n❌ Erreur : {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"\n❌ Erreur : Accès refusé au fichier '{cheminFichier}'");
                Console.WriteLine($"   Détails: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"\n❌ Erreur d'entrée/sortie lors du chargement");
                Console.WriteLine($"   Détails: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Erreur inattendue lors du chargement");
                Console.WriteLine($"   Détails: {ex.Message}");
            }
        }

        /// <summary>
        /// Parse une ligne CSV et crée le document correspondant
        /// Format attendu: TYPE;ID;Titre;Auteur;Annee;ProprieteSpecifique
        /// </summary>
        /// <param name="ligne">Ligne CSV à parser</param>
        /// <returns>Document créé</returns>
        private Document ParseLigne(string ligne)
        {
            var parties = ligne.Split(';');

            if (parties.Length < 6)
            {
                throw new FormatException("Format de ligne incorrect : nombre de champs insuffisant");
            }

            string type = parties[0];
            Guid id = Guid.Parse(parties[1]);
            string titre = parties[2];
            string auteur = parties[3];
            int annee = int.Parse(parties[4]);

            switch (type.ToUpper())
            {
                case "LIVRE":
                    int nombrePages = int.Parse(parties[5]);
                    return new Livre(id, titre, auteur, annee, nombrePages);

                case "MAGAZINE":
                    int numero = int.Parse(parties[5]);
                    return new Magazine(id, titre, auteur, annee, numero);

                case "PDF":
                    double taille = double.Parse(parties[5], CultureInfo.InvariantCulture);
                    return new DocumentPDF(id, titre, auteur, annee, taille);

                default:
                    throw new FormatException($"Type de document inconnu : {type}");
            }
        }

        /// <summary>
        /// Retourne le nombre de documents dans la bibliothèque
        /// </summary>
        public int Count => documents.Count;
    }
}