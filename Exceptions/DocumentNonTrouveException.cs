using System;

namespace BibliothequeNumerique.Exceptions
{
    /// <summary>
    /// Exception personnalisée levée quand un document n'est pas trouvé
    /// Hérite de Exception pour créer notre propre type d'exception
    /// </summary>
    public class DocumentNonTrouveException : Exception
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public DocumentNonTrouveException()
            : base("Le document recherché n'a pas été trouvé dans la bibliothèque.")
        {
        }

        /// <summary>
        /// Constructeur avec message personnalisé
        /// </summary>
        /// <param name="message">Message d'erreur personnalisé</param>
        public DocumentNonTrouveException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructeur avec message et exception interne
        /// Utile pour encapsuler une autre exception
        /// </summary>
        /// <param name="message">Message d'erreur</param>
        /// <param name="innerException">Exception d'origine</param>
        public DocumentNonTrouveException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructeur avec ID du document non trouvé
        /// </summary>
        /// <param name="id">ID du document recherché</param>
        public DocumentNonTrouveException(Guid id)
            : base($"Le document avec l'ID '{id}' n'a pas été trouvé.")
        {
        }
    }
}