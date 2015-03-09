using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NS.Logs
{
    /// <summary>
    /// Definit un contexte d'exécution
    /// </summary>
    public interface ILogContexte
    {
        /// <summary>
        ///  Nom de l'application qui enresgitre l'erreur
        /// </summary>
        string Origin { get; set; }

        /// <summary>
        /// périmètre fonctionnel l'application (departement au sein du SI)
        /// </summary>
        string Scope { get; set; }

        string CurrentUser { get; set; }

        /// <summary>
        /// Information complémentaire sour forme clef/valeur
        /// </summary>
        Dictionary<string, string> InfosComplementaires { get; set; }


    }
}
