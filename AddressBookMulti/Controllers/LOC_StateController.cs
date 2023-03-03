using AddressBookMulti.DAL;
using AddressBookMulti.Models;
using MetronicAddressBook.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static AddressBookMulti.Models.LOC_CountryModel;

namespace AddressBookMulti.Controllers
{
    public class LOC_StateController : Controller
    {
        #region Configuration

        private IConfiguration Configuration;
        public LOC_StateController(IConfiguration _configuration)

        {
            Configuration = _configuration;
        }
        #endregion

        [CheckAccess]

        #region Index
        public IActionResult Index()
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.dbo_PR_LOC_State_SelectAll(connectionstr);
            return View("LOC_StateList", dt);

            /* DataTable dt = new DataTable(); 

             SqlConnection conn = new SqlConnection(connectionstr);

             conn.Open();

             SqlCommand objCmd = conn.CreateCommand();
             objCmd.CommandType = CommandType.StoredProcedure;
             objCmd.CommandText = "PR_LOC_State_SelectAll";
             SqlDataReader objSDR = objCmd.ExecuteReader();

             dt.Load(objSDR);

             conn.Close();*/

        }
        #endregion

        #region Add
        public IActionResult Add(int StateID)
        {
            #region Country Drop Down

            string connectionstr1 = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt1 = new DataTable();

            SqlConnection conn1 = new SqlConnection(connectionstr1);

            conn1.Open();

            SqlCommand objCmd1 = conn1.CreateCommand();
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.CommandText = "PR_LOC_Country_SelectForDropDown";
            SqlDataReader objSDR1 = objCmd1.ExecuteReader();
            dt1.Load(objSDR1);



            List<LOC_Country_SelectForDropDownModel> list = new List<LOC_Country_SelectForDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                LOC_Country_SelectForDropDownModel vlst = new LOC_Country_SelectForDropDownModel();
                vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                vlst.CountryName = dr["CountryName"].ToString();
                list.Add(vlst);
            }
            ViewBag.CountryList = list;
            conn1.Close();
            #endregion

            #region Select By PK

            if (StateID != null)
            {

                string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
                LOC_DAL dalLOC = new LOC_DAL();

                DataTable dt = dalLOC.dbo_PR_LOC_State_SelectByPK(connectionstr, StateID);
                if (dt.Rows.Count > 0)
                {
                    LOC_StateModel model = new LOC_StateModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        model.CountryID = Convert.ToInt32(dr["CountryID"]);
                        model.StateID = Convert.ToInt32(dr["StateID"]);
                        model.StateName = dr["StateName"].ToString();
                        model.StateCode = dr["StateCode"].ToString();
                        model.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                        model.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);

                    }
                    return View("LOC_StateAddEdit", model);
                }

                /*string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
                SqlConnection conn = new SqlConnection(connectionstr);

                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_LOC_State_SelectByPK";
                objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);


                
                LOC_StateModel modelLOC_State = new LOC_StateModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_State.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_State.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_State.StateName = dr["StateName"].ToString();
                    modelLOC_State.StateCode = dr["StateCode"].ToString();
                    modelLOC_State.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_State.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);

                    return View("LOC_StateAddEdit", modelLOC_State);
                }
               
               
                conn.Close();*/
            }
            #endregion

            return View("LOC_StateAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(LOC_StateModel modelLOC_State)
        {

            #region Insert

           
                string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");

                LOC_DAL dalLOC = new LOC_DAL();


                if (modelLOC_State.StateID == null)
                {

                    if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_State_Insert(connectionstr, modelLOC_State)))
                    {
                        TempData["StateInsertMessage"] = "Record inserted successfully";

                    }
                }
                else
                {
                    if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_State_UpdateByPK(connectionstr, modelLOC_State)))
                    {

                        TempData["StateUpdateMessage"] = "Record Update Successfully";

                    }
                    return RedirectToAction("Index");
                }
            

            return RedirectToAction("Add");

            #endregion
        }
        #endregion

        #region Delete
        public IActionResult Delete(int StateID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
            LOC_DAL dalLOC = new LOC_DAL();

            if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_State_DeleteByPK(connectionstr, StateID)))
            {
                return RedirectToAction("Index");
            }
            return View("Index");


            /* DataTable dt = new DataTable();
             SqlConnection conn = new SqlConnection(connectionstr);

             conn.Open();

             SqlCommand objCmd = conn.CreateCommand();
             objCmd.CommandType = CommandType.StoredProcedure;
             objCmd.CommandText = "PR_LOC_State_DeleteByPK";

             objCmd.Parameters.AddWithValue("@StateID", StateID);

             objCmd.ExecuteNonQuery();

             conn.Close();
 */


        }
        #endregion




    }
}
