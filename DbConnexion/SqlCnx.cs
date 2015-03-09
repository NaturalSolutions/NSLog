using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace NS.Logs.DbConnexion
{
    /// <summary>
    /// Classe permattant la connexion à BDD avec client OleDB
    /// </summary>
    public class SqlCnx : DBCnx, IDisposable
    {
        



        /// <summary>
        /// Constructeur: récupère la chaine de connexion et ouvre la connexion
        /// </summary>
        /// <param name="strConnectionString">Connection string version OleDb. Attention préciser le oledbprovider au début de la chaine</param>
        public SqlCnx(string strConnectionString)
            : base(strConnectionString)
        // constructeur
        {
            MonType = CnxType.CnxSqlServer;
        }// end constructeur



        /// <summary>
        /// Crée les objets Connection,AccessComanmd,DataAdapter typé en fonction de MonType
        /// </summary>
        protected override void CreateCnxAndObjects()
        {
            if (MyConn != null) MyConn.Close();
            MyConn = new SqlConnection(MyConnectionString);
            myAccessCommand = new SqlCommand();
            myDataAdapter = new SqlDataAdapter((SqlCommand)myAccessCommand);
            myAccessCommand.Connection = (SqlConnection)MyConn;

        }// end 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPS"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public DataTable GetDataTableFromPS(string strPS, params object[] args)
        {

            InitCommand();

            DataSet MyDs = new DataSet();
            myAccessCommand.CommandText = strPS;
            myAccessCommand.CommandType = CommandType.StoredProcedure;
            FillParameters(args);


            myDataAdapter.SelectCommand = myAccessCommand;

            myDataAdapter.Fill(MyDs);
            MyConn.Dispose();
            return MyDs.Tables[0];
        }

        /// <summary>
        /// Execute un PS qui renvoie une valeur Scalaire
        /// </summary>
        /// <param name="_SQLProcstock">Nom de la PS</param>
        /// <param name="args">Liste des paramètres</param>
        /// <returns>Valeur retournée</returns>
        public object ExecutePSScalar(string _SQLProcstock, params object[] args)
        {
            DataTable MaTbl = GetDataTableFromPS(_SQLProcstock, args);
            if (MaTbl.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return MaTbl.Rows[0][0];
            }
        }

        /// <summary>
        /// Execute un PS qui retourne un parametre
        /// </summary>
        /// <param name="p_SQLProcstock">Nom de la PS</param>
        /// <param name="ParametreOut">Paramètres à retourner</param>
        /// <param name="args">Liste des paramètres</param>
        /// <returns>Retourne la valeur de ParametreOut renvoyé pas la PS</returns>
        public object SQL_GetOutParameter(string p_SQLProcstock, SqlParameter ParametreOut, params object[] args)
        {
            InitCommand();
            FillParameters(args);

            ParametreOut.Direction = ParameterDirection.Output;
            myAccessCommand.Parameters.Add(ParametreOut);
            myAccessCommand.CommandText = p_SQLProcstock;
            myAccessCommand.CommandType = CommandType.StoredProcedure;

            myAccessCommand.ExecuteNonQuery();
            MyConn.Dispose();
            return ((SqlParameter)myAccessCommand.Parameters[ParametreOut.ParameterName]).Value;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="InTimeOut"></param>
        protected void CreateCnxAndObjects(int InTimeOut)
        {
            CreateCnxAndObjects();
            if (InTimeOut > 0)
            {
                myAccessCommand.CommandTimeout = InTimeOut;
            }
        }


    }// end SqlCnx

}
