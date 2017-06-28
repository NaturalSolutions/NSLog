using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace NS.Logs.DbConnexion
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbCnxManager
    {
        #region "Propriétés"
        private static bool _IsSpecificUsed = true;

        #endregion


        /// <summary>
        /// Si vrai, on utilise les classe spécifiques lors de connexion sinon on utilise la connexion OleDB
        /// </summary>

        public static bool IsSpecificUsed
        {
            get
            {
                return _IsSpecificUsed;
            }
            set
            {
                _IsSpecificUsed = value;
            }

        }








        ///  <summary>
        /// Crée une connection à partir d'une connectionstring 
        /// et ajoute cette connection à la liste des connections du manager
        /// </summary>
        /// <param name="ConnectionString">ConnectionString incluant le Provider</param>
        /// <param name="ConnectionName">Nom de la connection dans le manager</param>
        /// <returns></returns>

        public static DBCnx CreateConnectionFromConnectingString(string ConnectionString)
        {
            DBCnx MyConn = null;




            if (_IsSpecificUsed)
            {
                // on change rien pour l'instant
                if (ConnectionString.IndexOf("System.Data.SqlClient") > 0)
                {

                    MyConn = new SqlCnx(ConnectionString);
                }
                else
                {
                    MyConn = new OleDBCnx(ConnectionString);
                }
            }
            else
            {
                MyConn = new OleDBCnx(ConnectionString);
            }
            return MyConn;
        }// end GetConnectionFromParam










        /*
        ///  <summary>
        /// Crée une connection à partir d'une connectionstring 
        /// et ajoute cette connection à la liste des connections du manager
        /// </summary>
        /// <param name="ConnectionString">ConnectionString incluant le Provider</param>
        /// <param name="ConnectionName">Nom de la connection dans le manager</param>
        ///  </param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static DBCnx CreateConnectionFromPostGreConnectingString(string ConnectionName, string ConnectionString)
        {
            //PostGreCnx  MyConn;

            InitMyConn();


            if (MesCnx.ContainsKey(ConnectionName))
            {
                MesCnx[ConnectionName].Dispose();
                MesCnx.Remove(ConnectionName);
                //Exception ex = new Exception("La connection " + ConnectionName + " Existe déjà impossible de créer à nouveau ");
                //throw ex;
            }

            MyConn = new PostGreCnx(ConnectionString);


            MesCnx.Add(ConnectionName, MyConn);
            return MyConn;

        }// end GetConnectionFromParam
        */

        /// <summary>
        /// Crée une connection à partir d'un entrée connectionstring dans le fichier de paramètrage
        /// et ajoute cette connection à la liste des connections du manager
        /// </summary>
        /// <param name="ParamConnnectionName">Nom de l'entré dans le fichier de configuration</param>
        /// <param name="ConnectionName">Paramètre optionel, nom de la connection dans le manager, si pas remplie, le nom sera automatiquement le même que ParamConnnectionName</param>
        /// <returns></returns>

        public static DBCnx CreateConnectionFromParam(string ParamConnnectionName)
        {
            string ConnectionString = "";


            if (_IsSpecificUsed)
            {
                ConnectionString = ConfigurationManager.ConnectionStrings[ParamConnnectionName].ConnectionString;
                if (ConfigurationManager.ConnectionStrings[ParamConnnectionName].ProviderName.IndexOf("System.Data.SqlClient") >= 0)
                {
                    return (new SqlCnx(ConnectionString));
                }
            }

            ConnectionString = "Provider=" + ConfigurationManager.ConnectionStrings[ParamConnnectionName].ProviderName + ";" + ConfigurationManager.ConnectionStrings[ParamConnnectionName].ConnectionString;
            return (new OleDBCnx(ConnectionString));

        }// end GetConnectionFromParam



        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="Champ"></param>
        /// <returns></returns>

        public static string GetParamFromConnectionString(string ConnectionString, string Champ)
        {
            string strReturnedValue;
            int indexDebut = ConnectionString.IndexOf(Champ + "=", StringComparison.CurrentCultureIgnoreCase);

            if (indexDebut < 0) return "";
            strReturnedValue = ConnectionString.Substring(indexDebut + Champ.Length + 1);

            if (strReturnedValue.Length <= 0) return "";
            if (strReturnedValue.IndexOf(";", StringComparison.CurrentCultureIgnoreCase) < 0)
            {
                return strReturnedValue;
            }
            else
            {
                return strReturnedValue.Substring(0, strReturnedValue.IndexOf(";"));
            }





        }




    }//end class

}
