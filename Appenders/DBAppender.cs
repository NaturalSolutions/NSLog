using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NS.Logs.DbConnexion;

namespace NS.Logs
{
    /// <summary>
    /// Appender pour enregistrer les messages dans un fichier plat
    /// </summary>
    public class DBAppender : LogAppender
    {
        /// <summary>
        /// Nom du fichier de log (avec chemin)
        /// </summary>
        public string ConnectionString { get; set; }

        protected DBCnx _MyCon;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="InName">Nom de l'appender</param>
        /// <param name="InLogFile"> Nom du fichier de log (avec chemin)</param>
        public DBAppender(string InName, string InConnectionString)
            : base(InName)
        {

            ConnectionString = InConnectionString;
            _MyCon = DbCnxManager.CreateConnectionFromConnectingString(InConnectionString);

        }// end constructeur


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="InName">Nom de l'appender</param>
        /// <param name="InLogFile"> Nom du fichier de log (avec chemin)</param>
        public DBAppender(string InName, DBCnx InMyConnn)
            : base(InName)
        {

            ConnectionString = InMyConnn.zeConn.ConnectionString;
            _MyCon = InMyConnn;

        }// end constructeur




        /// <summary>
        /// Enregistre Le message dans un fichier de Log
        /// </summary>
        /// <param name="MonLevel">Niveau de criticité</param>
        /// <param name="MonMessage">Message à enresgitrer</param>
        /// <param name="MonContexte">Contexte d'exécution</param>
        public override void SendMessage(LogLevel MonLevel, ILogMessage MonMessage, ILogContexte MonContexte)
        {
            _MyCon.SQL_Execute("PR_LOG_MESSAGE", "LOG_LEVEL", MonLevel, "Origin", MonContexte.Origin, "scope", MonContexte.Scope, "loguser", MonContexte.CurrentUser, "domaine", MonMessage.Domaine, "MESSAGE_NUMBER", MonMessage.NumMessage, "LOG_MESSAGE", MonMessage.TexteMessage);

            //throw new Exception("Not impemented Yet");
            //File.AppendAllText(LogFile, ListLogLevel.GetName(MonLevel)  + "::" + MonContexte.Origin + "::" + MonContexte.Scope + "::" +  MonMessage.TexteMessage + "\r\n");
        }


    }

}
