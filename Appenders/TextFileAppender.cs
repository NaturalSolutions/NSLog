using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace NS.Logs
{
    /// <summary>
    /// Appender pour enregistrer les messages dans un fichier plat
    /// </summary>
    public class TextFileAppender : LogAppender
    {
        /// <summary>
        /// Nom du fichier de log (avec chemin)
        /// </summary>
        public string LogFile { get; set; }



        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="InName">Nom de l'appender</param>
        /// <param name="InLogFile"> Nom du fichier de log (avec chemin)</param>
        public TextFileAppender(string InName, string InLogFile = "", Nullable<LogLevel> SpecifiLogLevel = null)
            : base(InName,SpecifiLogLevel)
        {

            LogFile = InLogFile;

        }// end constructeur


        /// <summary>
        /// Enregistre Le message dans un fichier de Log
        /// </summary>
        /// <param name="MonLevel">Niveau de criticité</param>
        /// <param name="MonMessage">Message à enresgitrer</param>
        /// <param name="MonContexte">Contexte d'exécution</param>
        public override void SendMessage(LogLevel MonLevel, ILogMessage MonMessage, ILogContexte MonContexte)
        {
            File.AppendAllText(LogFile, DateTime.Now.ToString() +  "::" +  ListLogLevel.GetName(MonLevel)  + "::" + MonContexte.Origin + "::" + MonContexte.Scope + "::" + MonContexte.CurrentUser + "::" +  MonMessage.TexteMessage + "\r\n");
        }


    }

}
