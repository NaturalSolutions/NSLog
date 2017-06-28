using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace NS.Logs
{
    /// <summary>
    /// Delegate Fonction pour généra un contexte de Log
    /// </summary>
    /// <param name="msg">Message en cours d'ecriture</param>
    /// <param name="InOrigin">Nom de l'application qui enresgitre l'erreur</param>
    /// <param name="InScope">périmètre fonctionnel l'application (departement au sein du SI)</param>
    /// <param name="InfosComplementaires">Information complémentaire sour forme clef/valeur</param>
    /// <returns></returns>
    public delegate ILogContexte CreateContexte(ILogMessage msg, string InOrigin, string InScope, Dictionary<string, string> InfosComplementaires);


    /// <summary>
    /// Type de Logger
    /// </summary>
    public enum LoggerType
    {
        /// <summary>
        /// 
        /// </summary>
        FileLogger = 1,
        /// <summary>
        /// 
        /// </summary>
        DbLogger = 2,
        /// <summary>
        /// 
        /// </summary>
        SystemLogger = 3,
        /// <summary>
        /// 
        /// </summary>
        MailLogger = 4,
        /// <summary>
        /// 
        /// </summary>
        PromoDbLogger = 100
    }


    /// <summary>
    /// Calsse statique de gestion des Logs
    /// </summary>
    public static class LogManager
    {

        public static LogLevel DefaultLogLevel { get { return LogLevel.Notice; } } 

        private static ILogger _ZeLogger = null;

        /// <summary>
        /// Logger Courant
        /// </summary>
        public static ILogger ZeLogger
        {
            get
            {
                return _ZeLogger;
            }
        }

        /// <summary>
        /// Borne supérieur pour les message prédefinis
        /// </summary>
        public static int CustomMessageNumber
        {
            get
            {
                return 500;
            }
        }

        /// <summary>
        /// Nom de l'application courante
        /// </summary>
        public static string Origin { get; set; }


        /// <summary>
        /// périmètre fonctionnel de l'application (Nom du SI)
        /// </summary>
        public static string Scope { get; set; }


        /// <summary>
        /// Initialise la gestion des log
        /// </summary>
        /// <param name="LoggerName">Nom du Looger</param>
        /// <param name="MonType">Type d'appender pour le Logger</param>
        /// <param name="DestInfo">Information de destination pour l'appender (nom du fichier, connection BDD, etc.)</param>
        /// <param name="Origin">Nom de l'application courante</param>
        /// <param name="Scope">périmètre fonctionnel de l'application (Nom du SI)</param>
        /// <param name="CurLogLevel">Niveau de Log minimum déclenchant l'enregistrement d'un message</param>
        public static void InitZeLogger(string LoggerName, LoggerType MonType, Object DestInfo, string Origin, string Scope, Nullable<LogLevel> CurLogLevel = null)
        {

            if (CurLogLevel == null) { CurLogLevel = DefaultLogLevel; };

            _ZeLogger = new Logger(LoggerName, (LogLevel) CurLogLevel, Origin, Scope, null);


            ILogAppender MonAppender;

            switch (MonType)
            {
                    
                case LoggerType.FileLogger:
                    
                    MonAppender = new TextFileAppender(LoggerName, (string)DestInfo);
                    break;
                default:
                    MonAppender = new TextFileAppender(LoggerName);
                    break;
            }

            _ZeLogger.AddAppender(MonAppender);

        }


        /// <summary>
        /// Permet d'obetnir le texte d'un message prédéfini à partir du fichier de ressource
        /// </summary>
        /// <param name="NumMessage">Numéro du message</param>
        /// <param name="Domaine">Domaine du message</param>
        /// <param name="ParamMessage">Ensemble de clefs/valeurs à remplacer dans le texte du message </param>
        /// <returns></returns>
        public static string GetMessage(int NumMessage, LogDomaine Domaine, Dictionary<string, string> ParamMessage)
        {
            return "";
        }


        /// <summary>
        /// Envoi d'un message de type Debug
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        ///  <code title="Exemple de Code " lang="VB">
        /// LogManager.Debug(LogDomaine.WebApplication, 312, New Dictionary(Of String, String) From {{"Colis", "3126658"}})
        /// </code>
        /// <code title="Exemple de Code" lang="C#">
        /// LogManager.Debug(LogDomaine.WebApplication,312,new Dictionary<string,string>(){{"Colis","3126658"}});
        /// </code>
        public static void SendDebug(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            ZeLogger.SendDebug(MonDomaine, NumMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message custom de type Debug
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte libre du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        /// <summary>
        ///  <code title="Exemple de Code " lang="VB">
        /// LogManager.Debug(LogDomaine.WebApplication, " Entrée dans la procédure TaitementColis() ", New Dictionary(Of String, String) From {{"Colis", "3126658"}})
        /// </code>
        /// <code title="Exemple de Code" lang="C#">
        /// LogManager.Debug(LogDomaine.WebApplication," Entrée dans la procédure TaitementColis() ",new Dictionary<string,string>(){{"Colis","3126658"}});
        /// </code>        
        public static void SendDebug(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            ZeLogger.SendDebug(MonDomaine, TextMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message de type Info
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        ///  <code title="Exemple de Code " lang="VB">
        /// LogManager.Info(LogDomaine.WebApplication, 312, New Dictionary(Of String, String) From {{"Colis", "3126658"}})
        /// </code>
        /// <code title="Exemple de Code" lang="C#">
        /// LogManager.Info(LogDomaine.WebApplication,312,new Dictionary<string,string>(){{"Colis","3126658"}});
        /// </code>      
        public static void SendInfo(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            ZeLogger.SendInfo(MonDomaine, NumMessage, InInfosComplementaires);
        }


        /// <summary>
        /// Envoi d'un message custom de type Info
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte libre du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        ///  <code title="Exemple de Code " lang="VB">
        /// LogManager.Info(LogDomaine.WebApplication, " Debut du traitement d'un colis ", New Dictionary(Of String, String) From {{"Colis", "3126658"}})
        /// </code>
        /// <code title="Exemple de Code" lang="C#">
        /// LogManager.Info(LogDomaine.WebApplication," Debut du traitement d'un colis ",new Dictionary<string,string>(){{"Colis","3126658"}});
        /// </code>      
        public static void SendInfo(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            ZeLogger.SendInfo(MonDomaine, TextMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message de type Notification
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        ///  <code title="Exemple de Code " lang="VB">
        /// LogManager.Notice(LogDomaine.WebApplication, 312, New Dictionary(Of String, String) From {{"Colis", "3126658"}})
        /// </code>
        /// <code title="Exemple de Code" lang="C#">
        /// LogManager.Notice(LogDomaine.WebApplication,312,new Dictionary<string,string>(){{"Colis","3126658"}});
        /// </code> 
        public static void SendNotice(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            ZeLogger.SendNotice(MonDomaine, NumMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message custom de type Notification
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte libre du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        ///  <code title="Exemple de Code " lang="VB">
        /// LogManager.Info(LogDomaine.WebApplication, " Debut du traitement d'un colis DEMAT ", New Dictionary(Of String, String) From {{"Colis", "3126658"}})
        /// </code>
        /// <code title="Exemple de Code" lang="C#">
        /// LogManager.Info(LogDomaine.WebApplication," Debut du traitement d'un colis DEMAT ",new Dictionary<string,string>(){{"Colis","3126658"}});
        /// </code> 
        public static void SendNotice(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            ZeLogger.SendNotice(MonDomaine, TextMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message de type Warning
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        ///  <code title="Exemple de Code " lang="VB">
        /// LogManager.Warning(LogDomaine.WebApplication, 312, New Dictionary(Of String, String) From {{"Colis", "3126658"}})
        /// </code>
        /// <code title="Exemple de Code" lang="C#">
        /// LogManager.Warning(LogDomaine.WebApplication,312,new Dictionary<string,string>(){{"Colis","3126658"}});
        /// </code> 
        public static void SendWarning(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            ZeLogger.SendWarning(MonDomaine, NumMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message custom de type Warning
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte libre du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        ///  <code title="Exemple de Code " lang="VB">
        /// LogManager.Info(LogDomaine.WebApplication, " colis DEMAT  sur un point de vente ne faisant pas de DEMAT", New Dictionary(Of String, String) From {{"Colis", "3126658"},{"ptvte", "312"}})
        /// </code>
        /// <code title="Exemple de Code" lang="C#">
        /// LogManager.Info(LogDomaine.WebApplication,"colis DEMAT  sur un point de vente ne faisant pas de DEMAT ",new Dictionary<string,string>(){{"Colis","3126658"},{"ptvte", "312"}});
        /// </code> 
        public static void SendWarning(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            ZeLogger.SendWarning(MonDomaine, TextMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message de type Erreur
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        ///  <code title="Exemple de Code " lang="VB">
        /// LogManager.Error(LogDomaine.WebApplication, 312, New Dictionary(Of String, String) From {{"Colis", "3126658"}})
        /// </code>
        /// <code title="Exemple de Code" lang="C#">
        /// LogManager.Error(LogDomaine.WebApplication,312,new Dictionary<string,string>(){{"Colis","3126658"}});
        /// </code> 


        public static void SendError(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            ZeLogger.SendError(MonDomaine, NumMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message custom de type Erreur
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte libre du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        ///  <code title="Exemple de Code " lang="VB">
        /// LogManager.Error(LogDomaine.WebApplication, "Colis absent", New Dictionary(Of String, String) From {{"Table", "colis"}})
        /// </code>
        /// <code title="Exemple de Code" lang="C#">
        /// LogManager.Error(LogDomaine.WebApplication,"Colis absent",new Dictionary<string,string>(){{"Table","colis"}});
        /// </code>

        public static void SendError(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            ZeLogger.SendError(MonDomaine, TextMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message de type Critique/fatal
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        ///  <code title="Exemple de Code " lang="VB">
        /// LogManager.Critical(LogDomaine.WebApplication, 813, New Dictionary(Of String, String) From {{"Table", "Colis"}})
        /// </code>
        /// <code title="Exemple de Code" lang="C#">
        /// LogManager.Critical(LogDomaine.WebApplication,813,new Dictionary<string,string>(){{"Table","Colis"}});
        /// </code> 
        public static void SendCritical(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            ZeLogger.SendCritical(MonDomaine, NumMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message custom de type Critique/fatal
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        ///  <code title="Exemple de Code " lang="VB">
        /// LogManager.Critical(LogDomaine.WebApplication, " Table inexistante", New Dictionary(Of String, String) From {{"Table", "Colis"}})
        /// </code>
        /// <code title="Exemple de Code" lang="C#">
        /// LogManager.Critical(LogDomaine.WebApplication, " Table inexistante",new Dictionary<string,string>(){{"Table","Colis"}});
        /// </code> 
        public static void SendCritical(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            ZeLogger.SendCritical(MonDomaine, TextMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Log une exception
        /// </summary>
        /// <param name="ActualLevel">Niveau de criticité du message</param>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="Ex">Exception à Logguer</param>
        ///  <code title="Exemple de Code " lang="VB">
        ///         Try
        ///         ....
        ///        Catch ex As Exception
        ///            LogManager.Exception(LogLevel.Critical, LogDomaine.WebApplication, ex)
        ///        End Try
        /// </code>
        /// <code title="Exemple de Code" lang="C#">
        /// try
        ///    {
        ///    ....
        ///   }
        ///     catch (Exception ex)
        ///    {
        ///        LogManager.Exception(LogLevel.Critical, LogDomaine.WebApplication, ex);
        ///    }
        /// </code> 
        public static void SendException(LogLevel ActualLevel, LogDomaine MonDomaine, Exception Ex)
        {
            ZeLogger.SendException(ActualLevel, MonDomaine, Ex);
        }

    }

}
