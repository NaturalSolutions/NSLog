using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NS.Logs
{
    /// <summary>
    /// Classe contenant les information de contexte d'exécution pour un message à Logger
    /// </summary>
    public class LogContexte : ILogContexte
    {

        /// <summary>
        /// Nom de l'application qui génère le message
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Périmètre fonctionnel (Nom du SI)
        /// </summary>
        public string Scope { get; set; }


        public string CurrentUser { get; set; }

        /// <summary>
        /// Ensemble de clefs/valeurs permettant de detailler le contexte d'exécution
        /// </summary>
        public Dictionary<string, string> InfosComplementaires { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="InOrigin"> Nom de l'application qui génère le message</param>
        /// <param name="InScope">Périmètre fonctionnel (Nom du SI)</param>
        /// <param name="InInfosComplementaires">Ensemble de clefs/valeurs permettant de detailler le contexte d'exécution</param>
        public LogContexte(string InOrigin, string InScope, string UserName,Dictionary<string, string> InInfosComplementaires = null)
        {
            Origin = InOrigin;
            Scope = InScope;
            InfosComplementaires = InInfosComplementaires;
            CurrentUser = UserName;
        }
        public override string ToString()
        {
            string resultat = " Origin : " + Origin + " Scope : " + Scope + "\n";
            resultat += string.Join("\n", InfosComplementaires.Select(i => i.Key + " : " + i.Value).ToArray());
            return resultat;

        }

    }



}
