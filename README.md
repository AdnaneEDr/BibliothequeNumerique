# Bibliothèque Numérique - Projet DOTNET

## 📚 Description
Système de gestion de bibliothèque numérique développé en C# dans le cadre de l'examen pratique de Programmation Objet.

## 🎯 Fonctionnalités
- ✅ Gestion de documents (Livres, Magazines, Documents PDF)
- ✅ Ajout, suppression et recherche de documents
- ✅ Sauvegarde et chargement depuis fichier CSV
- ✅ Gestion robuste des erreurs avec exceptions personnalisées
- ✅ Interface console interactive avec menu

## 🛠️ Technologies utilisées
- **Langage** : C# (.NET 6+)
- **Concepts POO** : Héritage, Abstraction, Polymorphisme, Encapsulation
- **Gestion de fichiers** : FileStream, StreamWriter/Reader
- **Gestion d'erreurs** : Exceptions personnalisées, try-catch-finally
- **Format de données** : CSV

## 📁 Structure du projet
```
BibliothequeNumerique/
│
├── Models/
│   ├── Document.cs              # Classe abstraite de base
│   ├── Livre.cs                 # Classe dérivée pour les livres
│   ├── Magazine.cs              # Classe dérivée pour les magazines
│   ├── DocumentPDF.cs           # Classe dérivée pour les PDF
│   └── Bibliotheque.cs          # Classe de gestion de la collection
│
├── Exceptions/
│   └── DocumentNonTrouveException.cs  # Exception personnalisée
│
├── Program.cs                   # Programme principal avec menu
└── bibliotheque_sauvegarde.txt  # Fichier de sauvegarde (généré)
```

## 🚀 Compilation et exécution

### Prérequis
- .NET 6 SDK ou supérieur

### Commandes
```bash
# Cloner le projet
git clone https://github.com/VotreUsername/BibliothequeNumerique.git
cd BibliothequeNumerique

# Compiler
dotnet build

# Exécuter
dotnet run
```

## 📖 Utilisation

### Menu principal
1. **Ajouter un document** - Ajouter un livre, magazine ou PDF
2. **Afficher tous les documents** - Voir la collection complète
3. **Rechercher par mot-clé** - Recherche dans titres et auteurs
4. **Supprimer un document** - Suppression par ID
5. **Sauvegarder dans un fichier** - Export au format CSV
6. **Charger depuis un fichier** - Import depuis CSV
7. **Quitter** - Fermer l'application

### Format de sauvegarde CSV
```
TYPE;ID;Titre;Auteur;Annee;ProprieteSpecifique

Exemple :
LIVRE;a1b2c3d4-e5f6-7890-abcd-ef1234567890;Le Petit Prince;Antoine de Saint-Exupéry;1943;96
MAGAZINE;b2c3d4e5-f6a7-8901-bcde-f12345678901;National Geographic;Divers;2024;150
PDF;c3d4e5f6-a7b8-9012-cdef-123456789012;Guide C#;Microsoft;2023;15.5
```

## 🎓 Contexte académique
**Établissement** : EMSI (École Marocaine des Sciences de l'Ingénieur)  
**Niveau** : 4ème année GI (Génie Informatique)  
**Matière** : Programmation Objet C# & Gestion des ressources  
**Professeur** : Abderrahman EZZAMRI  
**Type** : Examen pratique (2 jours)

## 📋 Critères d'évaluation couverts
- ✅ Modélisation POO (classe abstraite, héritage, polymorphisme)
- ✅ Collection de documents avec CRUD complet
- ✅ Gestion robuste des erreurs (exceptions personnalisées)
- ✅ Flux de données (FileStream, StreamWriter/Reader)
- ✅ Libération propre des ressources (using statements)
- ✅ Sérialisation/désérialisation CSV
- ✅ Programme principal avec menu interactif

## 👨‍💻 Auteur
**Nom** : Adnane Edrissi 
**Année** : 2024-2025  
**Filière** : Génie Informatique 4ème année

## 📧 Contact
- Email : edradnane@gmail.com
- GitHub : [@VotreUsername](https://github.com/AdnaneEDr)

## 📄 Licence
Projet académique - EMSI 2024-2025
