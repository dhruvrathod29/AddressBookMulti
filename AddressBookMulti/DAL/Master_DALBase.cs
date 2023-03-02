using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace AddressBookMulti.DAL
{
    public class Master_DALBase
    {
        public object UserID { get; private set; }


        #region Method: dbo_PR_SEC_User_SelectByPK
        public DataTable dbo_PR_User_Master_SelectByPK(string ConnStr, int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_User_Master_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion




        public DataTable dbo_PR_User_Master_SelectByUserNamePassword(string connstr, string UserName, string Password)
        {

            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connstr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_User_Master_SelectByUserNamePassword");
                
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.NVarChar, UserName);

                sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.NVarChar, Password);


                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }


        }


        #region Method: dbo_PR_SEC_User_Insert
        public decimal? dbo_PR_User_Master_Insert(string ConnStr, string UserName, string Password, string DisplayName, string Email, string MobileNo, DateTime? CreationDate, DateTime? ModificationDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_User_Master_Insert");
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, UserName);
                sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, Password);
                sqlDB.AddInParameter(dbCMD, "DisplayName", SqlDbType.VarChar, DisplayName);

                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, Email);
                sqlDB.AddInParameter(dbCMD, "MobileNo", SqlDbType.VarChar, MobileNo);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.DateTime, CreationDate);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.DateTime, ModificationDate);

                var vResult = sqlDB.ExecuteScalar(dbCMD);
                if (vResult == null)
                    return null;

                return (decimal)Convert.ChangeType(vResult, vResult.GetType());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
