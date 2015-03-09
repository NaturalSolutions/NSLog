using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace NS.Logs.DbConnexion
{
    /// <summary>
    /// 
    /// </summary>
    public enum CnxType
    {
        /// <summary>
        /// Connexion OleDB (attention indiquer le provider dans la connectionString
        /// </summary>
        CnxOleDb = 1,
        /// <summary>
        /// Connexion SQL Server
        /// </summary>
        CnxSqlServer = 2,
       
    }

    /// <summary>
    /// Classe permattant la connexion à BDD avec client OleDB
    /// </summary>
    public abstract class DBCnx : IDisposable
    {
        #region "Propriétés privées"
        /// <summary>
        /// 
        /// </summary>
        protected CnxType MonType;
        /// <summary>
        /// 
        /// </summary>
        protected string MyConnectionString;
        /// <summary>
        /// 
        /// </summary>
        protected IDbConnection MyConn;
        /// <summary>
        /// 
        /// </summary>
        protected IDbCommand myAccessCommand;
        /// <summary>
        /// 
        /// </summary>
        protected IDbDataAdapter myDataAdapter;

        /// <summary>
        /// 
        /// </summary>
        protected int _TimeOut;
        #endregion


        #region "Propriétés publiques "

        /// <summary>
        /// Connection BDD utilisé par la classe
        /// </summary>

        public IDbConnection zeConn
        {
            get
            {
                return MyConn;
            }
        }

        /// <summary>
        /// Definit le timeout des commandes, si valeur inférieure à 0 timeout par défaut
        /// </summary>
        public int TimeOut
        {
            get
            {
                return _TimeOut;
            }
            set
            {
                _TimeOut = value;
            }
        }

        #endregion

        #region "Constructeurs"

        /// 
        /// <summary>
        /// Constructeur: récupère la chaine de connexion et ouvre la connexion
        /// </summary>
        /// <param name="strConnectionString">Connection string version OleDb. Attention préciser le oledbprovider au début de la chaine</param>
        public DBCnx(string strConnectionString)
        // constructeur
        {
            MyConnectionString = strConnectionString;
            CreateCnxAndObjects();
            MyConn.Dispose();

        }// end constructeur


        #endregion


        #region "Méthodes publiques"


        #region "Méthode execution"

        /// <summary>
        /// Exécute une procédure stockée sans retour, les paramètres sont non-typé et sont indiqués directement dans l'appel de procédure
        /// </summary>
        /// <param name="MyProcedure">Appel de la procédure avec paramètres</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ExecuteStoProc(string MyProcedure)
        {
            InitCommand();
            myAccessCommand.CommandText = MyProcedure;
            myAccessCommand.CommandType = CommandType.StoredProcedure;
            myAccessCommand.ExecuteNonQuery();
            MyConn.Dispose();
        }// end 


        #region "Appel de procédures"
        /// <summary>
        /// Execute la procédure stockée _SQLProcstock avec les paramètres args
        /// </summary>
        /// <param name="_SQLProcstock">Procédure stockée</param>
        /// <param name="args">Paramètres de l'appel de la PS</param>
        public void SQL_Execute(string _SQLProcstock, params object[] args)
        {
            int i;
            IDbDataParameter MonParam;

            InitCommand();
            myAccessCommand.CommandType = CommandType.StoredProcedure;
            myAccessCommand.CommandText = _SQLProcstock;
            myAccessCommand.CommandTimeout = 600;
            for (i = 0; i < args.Length; i += 2)
            {
                MonParam = myAccessCommand.CreateParameter();
                MonParam.ParameterName = (string)args[i];
                MonParam.Value = args[i + 1];
                myAccessCommand.Parameters.Add(MonParam);

            }

            myAccessCommand.Connection = MyConn;

            myAccessCommand.ExecuteNonQuery();

            MyConn.Dispose();
        }// end SQL_Execute




        /// <summary>
        /// Exécute une procédure stockée sans retour, les paramètres sont non-typé et sont indiqués directement dans l'appel de procédure
        /// </summary>
        /// <param name="MySQLStatement">Requête SQL d'UPDATE/DELETE à exécuter</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void ExecuteQuery(string MySQLStatement)
        {

            InitCommand();

            myAccessCommand.CommandText = MySQLStatement;
            myAccessCommand.CommandType = CommandType.Text;

            myAccessCommand.ExecuteNonQuery();

            MyConn.Dispose();
        }// end 

        /// <summary>
        /// Exécute une procédure stockée sans retour avec des paramètres
        /// </summary>
        /// <param name="MySQLStatement">Requête SQL d'UPDATE/DELETE à exécuter</param>
        /// <param name="args">Paramètres de la requête avec le nom, puis la valeur</param>
        public void ExecuteQueryWithArgs(string MySQLStatement, params object[] args)
        {
            InitCommand();

            myAccessCommand.CommandText = MySQLStatement;
            FillParameters(args);

            myAccessCommand.ExecuteNonQuery();
            MyConn.Dispose();
        }// end 

        /// <summary>
        /// Exécute une procédure stockée sans retour avec les infos paramètres
        /// </summary>
        /// <param name="MesInfos">Infos d'exécution</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void ExecuteQueryWithArgs(SQLExecInfo MesInfos)
        {
            ExecuteQueryWithArgs(MesInfos.strSQL, MesInfos.mesArgs);

        }// end SQL_Execute


        #endregion
        #endregion

        #region "Méthode de recupération de données"


        /// <summary>
        /// Retourne une valeur unique résultat d'un requête, première ligne, première colonne
        /// </summary>
        /// <param name="strQuery">Requête à exécuter</param>       /// <param name="args"></param>
        /// <returns></returns>
        public string ExecuteScalar(string strQuery, params object[] args)
        {
            DataSet MyDs = new DataSet();
            InitCommand();
            FillParameters(args);
            myAccessCommand.CommandText = strQuery;
            //myAccessCommand.CommandType = CommandType.Text;

            string Retour = myAccessCommand.ExecuteScalar().ToString();
            MyConn.Dispose();
            return Retour;

        }



        #endregion

        #region "Méthode de recupération de données spécifiques"


        /// <summary>
        /// Retourne une DataTable à partir d'un requête
        /// </summary>
        /// <param name="strQuery">Requête à exécuter</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual DataTable GetDataTableFromCnx(string strQuery)
        {
            InitCommand();

            DataSet MyDs = new DataSet();
            myAccessCommand.CommandText = strQuery;
            myAccessCommand.CommandType = CommandType.Text;
            myDataAdapter.SelectCommand = myAccessCommand;

            myDataAdapter.Fill(MyDs);
            MyConn.Dispose();
            return MyDs.Tables[0];

        }

        /// <summary>
        /// Exécuté la requête strQuery avec les arguments args et renvoie le DataTable associé
        /// </summary>
        /// <param name="strQuery"> Requete à exécuter</param>
        /// <param name="args">Tableau des paramètres</param>
        /// <returns>DataTable contenant les données de la requête</returns>
        public DataTable GetDataTableFromCnxWithArgs(string strQuery, params object[] args)
        {
            InitCommand();

            DataSet MyDs = new DataSet();
            myAccessCommand.CommandText = strQuery;
            myAccessCommand.CommandType = CommandType.Text;
            FillParameters(args);


            myDataAdapter.SelectCommand = myAccessCommand;

            myDataAdapter.Fill(MyDs);
            MyConn.Dispose();
            return MyDs.Tables[0];
        }


        /// <summary>
        /// Obtient un objet dictionnaire de string avec clef string depuis une requête
        /// </summary>
        /// <param name="strQuery">Requete à exécuter, celle-ci doit renvoyer au moins les 2 champ strColumnKeyName et strColumnValueName </param>
        /// <param name="strColumnKeyName">champ clef du dictionnaire</param>
        /// <param name="strColumnValueName">Champ valeur du dictionnaire</param>
        ///  <param name="args">Paramètres d'exécution</param>
        /// <returns>Objet dictionnaire</returns>
        public Dictionary<string, string> GetDicFromQuery(string strQuery, string strColumnKeyName, string strColumnValueName, params object[] args)
        {

            Dictionary<string, string> MyDic = new Dictionary<string, string>();
            DataTable MaTable = GetDataTableFromCnxWithArgs(strQuery, args);

            return MaTable.AsEnumerable().ToDictionary(row => row[strColumnKeyName].ToString(),
                                                        row => row[strColumnValueName].ToString());

        }// end GetDicFromQuery

       



        /// <summary>
        /// Obtient un objet dictionnaire de string avec clef int depuis une requête
        /// </summary>
        /// <param name="strQuery">Requete à exécuter, celle-ci doit renvoyer au moins les 2 champ strColumnKeyName et strColumnValueName </param>
        /// <param name="strColumnKeyName">champ clef du dictionnaire (doit être de type int)</param>
        /// <param name="strColumnValueName">Champ valeur du dictionnaire</param>
        /// <param name="args"></param>
        /// <returns>Objet dictionnaire</returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Dictionary<int, string> GetIntDicFromQuery(string strQuery, string strColumnKeyName, string strColumnValueName, params object[] args)
        {

            Dictionary<int, string> MyDic = new Dictionary<int, string>();
            DataTable MaTable = GetDataTableFromCnxWithArgs(strQuery, args);

            return MaTable.AsEnumerable().ToDictionary(row => int.Parse(row[strColumnKeyName].ToString()),
                                                        row => row[strColumnValueName].ToString());

        }// end GetDicFromQuery



        

        


        #endregion

        #region "Methode  Gestion transactions"

        /// <summary>
        /// Execute les différentes instrcution dans la liste en renvoie le resultat de la première exécution
        /// </summary>
        /// <param name="ListInfo"></param>
        /// <param name="IndexResultat"></param>
        /// <returns></returns>

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Object MultipleExecInTran(SQLExecInfo[] ListInfo, int IndexResultat = 0)
        {
            if (ListInfo.Length <= 0 || ListInfo.Length < IndexResultat) throw (new Exception("Erreur Argument MultipleExecInTran. SQLInfo vide ou trop petit par rapport à IndexResultat"));
            int i;
            InitCommand();

            object MonResultat = null;
            IDbTransaction MyTran = MyConn.BeginTransaction();
            myAccessCommand.Transaction = MyTran;
            try
            {
                for (i = 0; i < ListInfo.Length; i++)
                {
                    if (ListInfo[i].mesArgs != null)
                    {
                        FillParameters(ListInfo[i].mesArgs);
                    }
                    myAccessCommand.CommandText = ListInfo[i].strSQL;

                    if (i == IndexResultat)
                    {
                        MonResultat = myAccessCommand.ExecuteScalar();
                    }
                    else
                    {
                        myAccessCommand.ExecuteNonQuery();
                    }

                }// end for
                MyTran.Commit();
                MyConn.Dispose();
                return MonResultat;
            }
            catch (Exception ex)
            {
                MyTran.Rollback();
                MyConn.Dispose();
                throw ex;
            }

        }// end MultipleExecInTran


        /// <summary>
        /// Execute les différentes instrcution dans la liste en renvoie le resultat de la première exécution
        /// </summary>
        /// <param name="ListInfo"></param>
        /// <returns></returns>
        public void MultipleExecInTran(List<SQLExecInfo> ListInfo)
        {
            MultipleExecInTran(ListInfo.ToArray(), -1);
        }// end MultipleExecInTran

        #endregion



        /// <summary>
        /// Fournie une condition de clause de where sur une date
        /// </summary>
        /// <param name="NomChamp">Nom du champ dans la clause Where</param>
        /// <param name="MaDate">Variable Date à comparer avec NomChamp</param>
        /// <param name="IsOnlyDay">Si vrai la comparaison se fera uniquement sur la date, sinon elle se fera sur date et heure</param>
        /// <returns>Condition à ajouter dans une clause WHERE de requête </returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public string GetDateComparaison(string NomChamp, DateTime MaDate, bool IsOnlyDay)
        {
            switch (MonType)
            {
                case CnxType.CnxSqlServer:
                    return "";
                case CnxType.CnxOleDb:
                default:
                    return "";
            }
        }

        #endregion


        #region "Méthodes privées"

        /// <summary>
        /// Crée les objets Connection,AccessComanmd,DataAdapter typé en fonction de MonType
        /// </summary>
        protected abstract void CreateCnxAndObjects();


        /// <summary>
        /// Crée les objets Connection,AccessComanmd,DataAdapter pour connexion oleDb
        /// </summary>
        protected void CreateCnxAndObjectStandard()
        {
            MyConn = new OleDbConnection(MyConnectionString);
            myAccessCommand = new OleDbCommand();
            myDataAdapter = new OleDbDataAdapter((OleDbCommand)myAccessCommand);
            myAccessCommand.Connection = (OleDbConnection)MyConn;

        }// end 


        /// <summary>
        /// Rempli myAccessCommand avec les paramètres passé en arguments
        /// </summary>
        /// <param name="args">indices impairs: nom du paramètre, indices pairs: valeur du paramètres</param>
        protected void FillParameters(params object[] args)
        {
            myAccessCommand.Parameters.Clear();

            IDbDataParameter MonParam;
            for (int i = 0; i < args.Length; i += 2)
            {
                MonParam = myAccessCommand.CreateParameter();
                MonParam.ParameterName = (string)args[i];
                MonParam.Value = args[i + 1];
                myAccessCommand.Parameters.Add(MonParam);
            }
        }



        /// <summary>
        /// Initialise la commande courante en ouvrant la connexion au besoin
        /// </summary>
        protected void InitCommand()
        {
            CreateCnxAndObjects();
            MyConn.Open();
            if (_TimeOut > 0)
            {
                myAccessCommand.CommandTimeout = _TimeOut;
            }
        }

       

        #endregion







        #region "Méthode statiques"
        /// <summary>
        /// Modifie une chaine de caractère pour l'intégrer dans une chaine pour une reqête. Typiquement double les ' pour éviter les erreurs de syntaxe dans la requête
        /// exemple d'utilisation strQuery = INSERT INTO MACHIN VALUES('" + ConvertStringInQuery(Machaine) + "') ;"
        /// </summary>
        /// <param name="MyQuery">Chaine incluse dans uen requête </param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static string ConvertStringInQuery(string MyQuery)
        {
            return MyQuery.Replace("'", "''");
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (MyConn != null)
            {

                MyConn.Dispose();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="MaLigne"></param>
        /// <param name="Separateur"></param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]

        public static string[] DataRowToStringArray(DataRow MaLigne, string Separateur = ";")
        {


            string[] row = new String[MaLigne.ItemArray.Length];
            for (int x = 0; x < MaLigne.ItemArray.Length; x++)
            {
                row[x] = MaLigne[x].ToString();
            }

            return row;
        }


    }// end DbCnx

    /// <summary>
    /// Contient les informations d'exécution SQL
    /// </summary>
    public struct SQLExecInfo
    {
        /// <summary>
        /// Requête à exécuter
        /// </summary>
        public string strSQL;

        /// <summary>
        /// Paramètres de la requête
        /// </summary>
        public object[] mesArgs;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="MaRequete"></param>
        /// <param name="MesAguments"></param>
        public SQLExecInfo(string MaRequete, object[] MesAguments = null)
        {
            strSQL = MaRequete;
            mesArgs = MesAguments;
        }
    }

}
