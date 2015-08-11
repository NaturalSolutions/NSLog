using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NS.Logs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public delegate string GetUser();

    /// <summary>
    /// Classe de logage
    /// </summary>
    public class Logger : ILogger
    {
        #region "Propriétés  privées"

        /// <summary>
        /// Liste des destination des Logs
        /// </summary>
        protected Dictionary<string, ILogAppender> _Appenders;

        public GetUser MyUserFunction { get; set; }


        public static string StandardGetUser()
        {
            return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        /// <summary>
        /// Destinaion si aucune destinatation ne fonctionne
        /// </summary>
        protected Dictionary<string, ILogAppender> _BackupAppenders;

        /// <summary>
        /// Delegate de création du Contexte
        /// </summary>
        protected CreateContexte _CreateContexteFunction;

        #endregion


        #region "Propriétés  publiques"
        /// <summary>
        /// Nom du Logger
        /// </summary>
        protected string Name { get; set; }

        /// <summary>
        /// Origine de l'application (Domaine fonctionnel)
        /// </summary>
        protected string Origin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected string Scope { get; set; }


        /// <summary>
        /// Niveau courant de criticité déclenchant un Log
        /// </summary>
        public LogLevel CurLogLevel { get; set; }

        /// <summary>
        /// Liste des destination des Logs
        /// </summary>
        public Dictionary<string, ILogAppender> Appenders
        {
            get
            {
                return _Appenders;
            }
        }
        #endregion

        #region "Constructeurs "
        public Logger(string InLoggerName, LogLevel InCurLogLevel, string InOrigin, string InScope, CreateContexte InCreateContexteFunction = null)
        {
            CurLogLevel = InCurLogLevel;
            Origin = InOrigin;
            Scope = InScope;
            _CreateContexteFunction = InCreateContexteFunction;
            _Appenders = new Dictionary<string, ILogAppender>();
            MyUserFunction = StandardGetUser;

        }

        #endregion


        #region "Méthodes privées"
        protected void SendLog(LogLevel ActualLevel, ILogMessage msg, Dictionary<string, string> InInfosComplementaires)
        {
            ILogContexte MonContexte = CreateContexte(msg, InInfosComplementaires);
            List<ILogAppender> WorkingAppender = new List<ILogAppender>();
            List<LogMessage> AppenderErrors = new List<LogMessage>();


            // Gérer ici ml'envoie aux appenders
            foreach (ILogAppender CurAppender in this._Appenders.Values)
            {
                if ((CurAppender.MiniLogLevel != LogLevel.Herited && CurAppender.MiniLogLevel <= ActualLevel) || (CurAppender.MiniLogLevel == LogLevel.Herited && CurLogLevel <= ActualLevel))
                {
                    try
                    {
                        CurAppender.SendMessage(ActualLevel, msg, MonContexte);
                        WorkingAppender.Add(CurAppender);
                        if (AppenderErrors.Count >0) {
                            foreach (LogMessage Curmsg in AppenderErrors)
                            {
                                CurAppender.SendMessage(LogLevel.Notice, Curmsg,  MonContexte);
                            }
                            AppenderErrors.Clear();
                        }
                    }


                    catch (Exception ex)
                    {
                        AppenderErrors.Add(new LogMessage(" Exception occured while sending to appender  " + CurAppender.Name + "=>" + ex.Message, msg.Domaine));
                    }

                }

            }
            try
            {
                if (AppenderErrors.Count > 0)
                {

                    foreach (LogMessage Curmsg in AppenderErrors)
                    {
                        WorkingAppender.First().SendMessage(LogLevel.Notice, Curmsg, MonContexte);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected ILogContexte CreateContexte(ILogMessage msg, Dictionary<string, string> InInfosComplementaires)
        {
            if (_CreateContexteFunction == null)
            {

                return new LogContexte(Origin, Scope, MyUserFunction.Invoke(), InInfosComplementaires);
            }
            else
            {
                return _CreateContexteFunction.Invoke(msg, Origin, Scope, InInfosComplementaires);
            }

        }

        #endregion


        #region "Methodes publiques"



        #region "Gestion des appenders"
        /// <summary>
        /// Ajoute un appender 
        /// </summary>
        /// <param name="MonAppender"></param>
        public void AddAppender(ILogAppender MonAppender)
        {

            this._Appenders.Add(MonAppender.Name, MonAppender);

        }

        /// <summary>
        /// Supprime l'appender nommé
        /// </summary>
        public void RemoveAppender(string AppenderName)
        {
        }


        #endregion


        /// <summary>
        /// Envoie un message à l'ensemble des appenders
        /// </summary>
        /// <param name="ActualLevel">Niveau de criticité du message</param>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        public void SendLog(LogLevel ActualLevel, LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null)
        {

            SendLog(ActualLevel, new LogMessage(NumMessage, MonDomaine, InInfosComplementaires), InInfosComplementaires);

            /* Gestion de l'ensemble des Appenders*/


        }




        /// <summary>
        /// Envoie un message custom à l'ensemble des appenders
        /// </summary>
        /// <param name="ActualLevel">Niveau de criticité du message</param>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Numéro d'erreur/information</param>
        public void SendLog(LogLevel ActualLevel, LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            SendLog(ActualLevel, new LogMessage(TextMessage, MonDomaine), InInfosComplementaires);


        }



        /// <summary>
        /// Log une exception
        /// </summary>
        /// <param name="ActualLevel">Niveau de criticité du message</param>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="Ex">Exception à Logguer</param>
        public void SendException(LogLevel ActualLevel, LogDomaine MonDomaine, Exception Ex)
        {
            Dictionary<string, string> InfosComplementaires = new Dictionary<string, string>();
            InfosComplementaires.Add("StackTrace", Ex.StackTrace);
            InfosComplementaires.Add("TargetSite", Ex.TargetSite.Name);
            if (Ex.InnerException != null)
            {
                InfosComplementaires.Add("InnerException", Ex.InnerException.Message);
            }
            SendLog(ActualLevel, MonDomaine, Ex.Message, InfosComplementaires);
        }


        #region "Methode par criticité"

        /// <summary>
        /// Envoi d'un message de type Debug
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        public void SendDebug(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            SendLog(LogLevel.Debug, MonDomaine, NumMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message custom de type Debug
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte libre du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        public void SendDebug(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            SendLog(LogLevel.Debug, MonDomaine, TextMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message de type Info
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        public void SendInfo(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            SendLog(LogLevel.Info, MonDomaine, NumMessage, InInfosComplementaires);
        }


        /// <summary>
        /// Envoi d'un message custom de type Info
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte libre du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        public void SendInfo(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            SendLog(LogLevel.Info, MonDomaine, TextMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message de type Notification
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        public void SendNotice(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            SendLog(LogLevel.Notice, MonDomaine, NumMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message custom de type Notification
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte libre du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        public void SendNotice(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            SendLog(LogLevel.Notice, MonDomaine, TextMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message de type Warning
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        public void SendWarning(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            SendLog(LogLevel.Warning, MonDomaine, NumMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message custom de type Warning
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte libre du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        public void SendWarning(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            SendLog(LogLevel.Warning, MonDomaine, TextMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message de type Erreur
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        public void SendError(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            SendLog(LogLevel.Error, MonDomaine, NumMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message custom de type Erreur
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte libre du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        public void SendError(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            SendLog(LogLevel.Error, MonDomaine, TextMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message de type Critique/fatal
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="NumMessage">Numéro d'erreur/information</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        public void SendCritical(LogDomaine MonDomaine, int NumMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            SendLog(LogLevel.Critical, MonDomaine, NumMessage, InInfosComplementaires);
        }

        /// <summary>
        /// Envoi d'un message custom de type Critique/fatal
        /// </summary>
        /// <param name="MonDomaine">Domaine du message</param>
        /// <param name="TextMessage">Texte du message</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs d'informations supplémentaires à Logguer</param>
        public void SendCritical(LogDomaine MonDomaine, string TextMessage, Dictionary<string, string> InInfosComplementaires = null)
        {
            SendLog(LogLevel.Critical, MonDomaine, TextMessage, InInfosComplementaires);
        }
        #endregion


        #endregion
    }// end Logger
}




