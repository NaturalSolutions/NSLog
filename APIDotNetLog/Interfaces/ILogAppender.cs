using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NS.Logs
{
    /// <summary>
    /// Interface definissant les LogAppender (i.e. destinations de Log)
    /// </summary>
    public interface ILogAppender
    {
        /// <summary>
        /// Nom de l'appender
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Niveau de criticité nécessaire pour enregistrer ce Log
        /// </summary>
        LogLevel MiniLogLevel { get; set; }


        /// <summary>
        /// Enregistre un message dans l'appender
        /// </summary>
        /// <param name="MonLevel">Niveau de Log du message</param>
        /// <param name="MonMessage"></param>
        /// <param name="MonContexteLog"></param>
        void SendMessage(LogLevel MonLevel, ILogMessage MonMessage, ILogContexte MonContexteLog);

    }
}
