using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace NS.Logs.DbConnexion
{
    /// <summary>
    /// Classe permattant la connexion à BDD avec client OleDB
    /// </summary>
    public class OleDBCnx : DBCnx, IDisposable
    {






        /// <summary>
        /// Constructeur: récupère la chaine de connexion et ouvre la connexion
        /// </summary>
        /// <param name="strConnectionString">Connection string version OleDb. Attention préciser le oledbprovider au début de la chaine</param>
        public OleDBCnx(string strConnectionString)
            : base(strConnectionString)
        // constructeur
        {
            MonType = CnxType.CnxOleDb;
        }// end constructeur


        /// <summary>
        /// Crée les objets Connection,AccessComanmd,DataAdapter typé en fonction de MonType
        /// </summary>
        protected override void CreateCnxAndObjects()
        {
            base.CreateCnxAndObjectStandard();
        }// end 

    }
}
