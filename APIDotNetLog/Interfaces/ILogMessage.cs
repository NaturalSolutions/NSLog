using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NS.Logs
{
    /// <summary>
    /// Interface de classe contenant un message
    /// </summary>
    public interface ILogMessage
    {

        /// <summary>
        /// Texte du message(peut être généré depuis le fichier de ressource si NumMessage est supérieur ou égal à LogManager.MaxDefinedMessageIndex)
        /// </summary>
        string TexteMessage { get; set; }

        /// <summary>
        /// Domaine du message
        /// </summary>
        LogDomaine Domaine { get; set; }


        /// <summary>
        /// Numéro d'erreur compris netre 0 et 999
        /// </summary>
        int NumMessage { get; set; }

    }
}
