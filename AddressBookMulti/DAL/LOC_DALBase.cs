using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using AddressBookMulti.Models;

namespace AddressBookMulti.DAL
{
    public class LOC_DALBase
    {
        #region LOC_SelectAll

        #region dbo.PR_LOC_Country_SelectAll
        public DataTable dbo_PR_LOC_Country_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_Country_SelectAll");

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

        #endregion

        #region LOC_Delete

        #region dbo.PR_LOC_Country_Delete
        public bool dbo_PR_LOC_Country_DeleteByPK(string conn, int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_Country_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        #endregion

        #endregion

        #region LOC_SelectByPK

        #region LOC_Country_SelectByPK

        public DataTable dbo_PR_LOC_Country_SelectByPK(string conn, int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);

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

        #endregion

        #region LOC_Insert
        #region LOC_Country_Insert

        public bool dbo_PR_LOC_Country_Insert(string str, LOC_CountryModel modelLOC_Country)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_Country_Insert");
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);
                sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.NVarChar, modelLOC_Country.CountryCode);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion
        #endregion

        #region LOC_UpdateByPK
        #region LOC_Country_UpdateByPK
        public bool dbo_PR_LOC_Country_UpdateByPK(string str, LOC_CountryModel modelLOC_Country)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_Country_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_Country.CountryID);
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);
                sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.NVarChar, modelLOC_Country.CountryCode);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #endregion
    }
}
