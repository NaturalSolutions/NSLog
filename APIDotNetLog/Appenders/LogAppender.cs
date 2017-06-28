using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NS.Logs
{
    /// <summary>
    /// Classe générique pour les Appenders de Log
    /// </summary>
    public abstract class LogAppender : ILogAppender
    {
        /// <summary>
        /// Nom de l'appender
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Niveau de criticité nécessaire pour enregistrer pour cet appender
        /// </summary>
        public LogLevel MiniLogLevel { get; set; }


        /// <summary>
        /// Constcuteur
        /// </summary>
        /// <param name="InName">Nom de l'appender</param>
        /// <param name="inMiniLogLevel">Niveau de criticité nécessaire pour enregistrer cet appender Log </param>
        public LogAppender(string InName, Nullable< LogLevel> inMiniLogLevel = null )
        {
            Name = InName;
            if (inMiniLogLevel == null)
            {
                inMiniLogLevel = LogLevel.Herited;
            }
            MiniLogLevel = (LogLevel) inMiniLogLevel;
        }

        /// <summary>
        /// Enresgitre le message dans l'appender
        /// </summary>
        /// <param name="MonLevel">Niveau de criticité </param>
        /// <param name="MonMessage">Message à enregistrer</param>
        /// <param name="MonContexte">Contexte d'exécution</param>
        public virtual void SendMessage(LogLevel MonLevel, ILogMessage MonMessage, ILogContexte MonContexte)
        {
            throw new Exception(" Fonction  SendMessage n'est pas définie pour cet Appender");
        }




    }

}
