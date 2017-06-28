using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NS.Logs
{
    /// <summary>
    /// Classe contenant les informations d'un message
    /// </summary>
    public class LogMessage : ILogMessage
    {

        /// <summary>
        /// Texte du message(peut être généré depuis le fichier de ressource si NumMessage est inférieur ou égal à LogManager.MaxDefinedMessageIndex)
        /// </summary>
        public string TexteMessage { get; set; }



        /// <summary>
        /// Numéro d'erreur compris netre 0 et 999
        /// </summary>
        public int NumMessage { get; set; }

        /// <summary>
        /// Domaine du message
        /// </summary>
        public LogDomaine Domaine { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="InDebugMessage">Texte de debug</param>
        /// <param name="InNumMessage">Numéro d'erreur compris netre 0 et 999</param>
        /// <param name="InDomaine">Domaine Fonctionnel</param>
        /// <param name="ParamMessage">Liste des valeur à remplacer dans le message</param>
        public LogMessage(int InNumMessage, LogDomaine InDomaine, Dictionary<string, string> ParamMessage)
        {
            Domaine = InDomaine;
            TexteMessage = LogManager.GetMessage(InNumMessage, Domaine, ParamMessage);
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="InDebugMessage">Texte de debug</param>
        /// <param name="InTexteMessage">Numéro d'erreur compris netre 0 et 999</param>
        /// <param name="InDomaine">Domaine Fonctionnel</param>
        /// <param name="ParamMessage">Liste des valeur à remplacer dans le message</param>
        public LogMessage(string InTexteMessage, LogDomaine InDomaine)
        {
            NumMessage = LogManager.CustomMessageNumber;
            Domaine = InDomaine;
            TexteMessage = InTexteMessage;
        }


    }

}
