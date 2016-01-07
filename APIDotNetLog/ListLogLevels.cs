using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NS.Logs
{
    /// <summary>
    /// Niveau de criticité de Log
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 
        /// </summary>
        Herited = -1,
        /// <summary>
        /// 
        /// </summary>
        Debug = 0,
        /// <summary>
        /// 
        /// </summary>
        Info = 1,
        /// <summary>
        /// 
        /// </summary>
        Notice = 2,
        /// <summary>
        /// 
        /// </summary>
        Warning = 3,
        /// <summary>
        /// 
        /// </summary>
        Error = 4,
        /// <summary>
        /// 
        /// </summary>
        Critical = 5
    }


    /// <summary>
    /// Gestion des Liste de Criticité
    /// </summary>
    public static class ListLogLevel
    {
        private static Dictionary<LogLevel, string> _LogLevelsCodes = new Dictionary<LogLevel, string>
                                                                                                    {
                                                                                                        {LogLevel.Debug, "DEBUG"},
                                                                                                        {LogLevel.Info, "INFO"},
                                                                                                        {LogLevel.Notice, "NOTICE"},
                                                                                                        {LogLevel.Warning, "WARN"},
                                                                                                        {LogLevel.Error, "ERR"},
                                                                                                        {LogLevel.Critical, "CRIT"}
    } ;
        
        /*
                    _LogLevelsCodes.Add(LogLevel.Debug, "INFO");
                    _LogLevelsCodes.Add(LogLevel.Debug, "NOTICE");
                    _LogLevelsCodes.Add(LogLevel.Debug, "WARN");
                    _LogLevelsCodes.Add(LogLevel.Debug, "ERR");
                    _LogLevelsCodes.Add(LogLevel.Debug, "CRIT");
                                                                                                    };
        */
        /// <summary>
        ///  Liste des criticité avec leur Code criticité
        /// </summary>

        public static Dictionary<LogLevel, string> LogLevelsCodes
        {
            get
            {
                if (_LogLevelsCodes == null)
                {
                    _LogLevelsCodes = new Dictionary<LogLevel, string>();
                    _LogLevelsCodes.Add(LogLevel.Debug, "DEBUG");
                    _LogLevelsCodes.Add(LogLevel.Debug, "INFO");
                    _LogLevelsCodes.Add(LogLevel.Debug, "NOTICE");
                    _LogLevelsCodes.Add(LogLevel.Debug, "WARN");
                    _LogLevelsCodes.Add(LogLevel.Debug, "ERR");
                    _LogLevelsCodes.Add(LogLevel.Debug, "CRIT");
                }
                return _LogLevelsCodes;
            }// end Get
        }// end LogLevelsCodes

        public static string GetName(LogLevel MyLevel)
        {
            return LogLevelsCodes[MyLevel];
        }

    }
}
