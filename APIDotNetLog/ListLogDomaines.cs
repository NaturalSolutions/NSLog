using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NS.Logs
{
    /// <summary>
    /// Domaine du Log
    /// </summary>
    public enum LogDomaine
    {
        /// <summary>
        /// 
        /// </summary>
        DataBase = 0,
        /// <summary>
        /// 
        /// </summary>
        ClientAplication = 1,
        /// <summary>
        /// 
        /// </summary>
        WebApplication = 2,
        /// <summary>
        /// 
        /// </summary>
        WebService = 3,
        /// <summary>
        /// 
        /// </summary>
        External = 4,
        /// <summary>
        /// 
        /// </summary>
        OS = 5,
        /// <summary>
        /// 
        /// </summary>
        Command = 6,
        /// <summary>
        /// 
        /// </summary>
        Ressources = 7,
        /// <summary>
        /// 
        /// </summary>
        BusinessGlobalUnknown = 8
    }


    /// <summary>
    /// Gestion des Liste de LogDomaine
    /// </summary>
    public static class ListLogDomaines
    {

        private static Dictionary<LogDomaine, string> _LogDomainesCodes = null;



        /// <summary>
        ///  Liste des domaines avec leur Code Domaine
        /// </summary>
        public static Dictionary<LogDomaine, string> LogDomainesCodes
        {
            get
            {
                if (_LogDomainesCodes == null)
                {
                    _LogDomainesCodes = new Dictionary<LogDomaine, string>();
                    _LogDomainesCodes.Add(LogDomaine.DataBase, "DB");
                    _LogDomainesCodes.Add(LogDomaine.ClientAplication, "APP");
                    _LogDomainesCodes.Add(LogDomaine.WebApplication, "WEB");
                    _LogDomainesCodes.Add(LogDomaine.WebService, "WS");
                    _LogDomainesCodes.Add(LogDomaine.External, "EXT");
                    _LogDomainesCodes.Add(LogDomaine.OS, "OS");
                    _LogDomainesCodes.Add(LogDomaine.Command, "CMD");
                    _LogDomainesCodes.Add(LogDomaine.Ressources, "RS");
                    _LogDomainesCodes.Add(LogDomaine.BusinessGlobalUnknown, "BUG");
                }
                return _LogDomainesCodes;
            }// end Get
        }// end LogLevelsCodes


    }

    

}
