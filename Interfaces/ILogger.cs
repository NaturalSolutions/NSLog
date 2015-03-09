
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NS.Logs
{
    /// <summary>
    /// Interface qui definit un Logger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Liste des destination des messages de Log
        /// </summary>
        Dictionary<string, ILogAppender> Appenders { get; }


        GetUser MyUserFunction {get;set;}

        /// <summary>
        /// Niveau courant de log du loggeur, seuls les messages avec un niveau >= au niveau courant seront loggé
        /// </summary>
        LogLevel CurLogLevel { get; set; }

        /// <summary>
        /// Envoie un message à l'ensemble des appenders
        /// </summary>
        /// <param name="ActualLevel">Niveau de criticité du message</param>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        void SendLog(LogLevel ActualLevel, LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null);


        /// <summary>
        /// Envoie un message custom à l'ensemble des appenders
        /// </summary>
        /// <param name="ActualLevel">Niveau de criticité du message</param>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        void SendLog(LogLevel ActualLevel, LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null);

        /// <summary>
        /// Log une exception
        /// </summary>
        /// <param name="ActualLevel">Niveau de criticité du message</param>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="Ex">Exception à Logguer</param>
        void SendException(LogLevel ActualLevel, LogDomaine MonDomaine, Exception Ex);

        /// <summary>
        /// Envoi d'un message de type Debug
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        void SendDebug(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null);



        /// <summary>
        /// Envoi d'un message custom de type Debug
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte libre du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        void SendDebug(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null);




        /// <summary>
        /// Envoi d'un message de type Info
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>

        void SendInfo(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null);





        /// <summary>
        /// Envoi d'un message custom de type Info
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte libre du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        void SendInfo(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null);



        /// <summary>
        /// Envoi d'un message de type Notification
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        void SendNotice(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null);



        /// <summary>
        /// Envoi d'un message custom de type Notification
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte libre du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        void SendNotice(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null);

        /// <summary>
        /// Envoi d'un message de type Warning
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        void SendWarning(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null);


        /// <summary>
        /// Envoi d'un message custom de type Warning
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte libre du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        void SendWarning(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null);


        /// <summary>
        /// Envoi d'un message de type Erreur
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        void SendError(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null);



        /// <summary>
        /// Envoi d'un message custom de type Erreur
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte libre du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        void SendError(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null);



        /// <summary>
        /// Envoi d'un message de type Critique/fatal
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        void SendCritical(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null);

        /// <summary>
        /// Envoi d'un message custom de type Critique/fatal
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        void SendCritical(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null);


        /// <summary>
        /// Ajoute un appender 
        /// </summary>
        /// <param name="MonAppender"></param>
        void AddAppender(ILogAppender MonAppender);


        /// <summary>
        /// Supprime l'appender nommé
        /// </summary>
        /// <param name="AppenderName">Nom de l'appender à supprimer</param>
        void RemoveAppender(string AppenderName);


    }// end ILogger
}
