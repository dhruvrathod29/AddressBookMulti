using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using AddressBookMulti.Areas.LOC_City.Models;
using AddressBookMulti.Areas.EMP_Employee.Models;
using AddressBookMulti.Areas.LOC_Country.Models;

namespace AddressBookMulti.DAL
{
    public class EMP_DALBase : DALHelper
    {
        public DataTable EMP_Employee_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("LOC_Country_SelectAll");

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

        public bool EMP_EmployeeInsert(EMP_EmployeeModel modelEMP_Employee)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_EMP_EMPLOYEE_INSERT");
                sqlDB.AddInParameter(dbCMD, "EmployeeName", SqlDbType.NVarChar, modelEMP_Employee.EmployeeName);
                sqlDB.AddInParameter(dbCMD, "Address", SqlDbType.NVarChar, modelEMP_Employee.Address);
                sqlDB.AddInParameter(dbCMD, "DateOfBirth", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));


                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable EMP_EmployeeSelectByPK(int EmployeeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_EMP_EMPLOYEE_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "EmployeeID", SqlDbType.Int, EmployeeID);

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

        public bool EMP_EmployeeUpdate(EMP_EmployeeModel modelEMP_Employee)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("EMP_EMPLOYEE_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "EmployeeID", SqlDbType.Int, modelEMP_Employee.EmployeeID);
                sqlDB.AddInParameter(dbCMD, "EmployeeName", SqlDbType.NVarChar, modelEMP_Employee.EmployeeName);
                sqlDB.AddInParameter(dbCMD, "Address", SqlDbType.NVarChar, modelEMP_Employee.Address);
                sqlDB.AddInParameter(dbCMD, "DateOfBirth", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EMP_EmployeeDelete(int EmployeeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_EMP_EMPLOYEE_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "EmployeeID", SqlDbType.Int, EmployeeID);
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
