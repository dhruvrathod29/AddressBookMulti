using AddressBookMulti.DAL;
using AddressBookMulti.Areas.MST_ContactCategory.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using MetronicAddressBook.BAL;

namespace AddressBookMulti.Areas.MST_ContactCategory.Controllers
{

    [CheckAccess]
    [Area("MST_ContactCategory")]
    [Route("MST_ContactCategory/[Controller]/[action]")]

    public class MST_ContactCategoryController : Controller
    {
        #region Configuration
        private IConfiguration Configuration;
        public MST_ContactCategoryController(IConfiguration _configuration)

        {
            Configuration = _configuration;
        }
        #endregion

   

        #region Index
        public IActionResult Index()
        {
            #region SelectAll
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
            CON_DAL dalCON = new CON_DAL();
            DataTable dt = dalCON.dbo_PR_MST_ContactCategory_SelectAll(connectionstr);

            return View("MST_ContactCategoryList", dt);
            /*DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);

            conn.Open();

            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_MST_ContactCategory_SelectAll";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);

            conn.Close();

            return View("MST_ContactCategoryList", dt);
            #endregion*/

            #endregion
        }

        #endregion

        #region Add
        public IActionResult Add(int ContactCategoryID)
        {
            #region Select By PK
            if (ContactCategoryID != null)
            {
                string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
                CON_DAL dalCON = new CON_DAL();

                DataTable dt = dalCON.dbo_PR_MST_ContactCategory_SelectByPK(connectionstr, ContactCategoryID);
                if (dt.Rows.Count > 0)
                {
                    MST_ContactCategoryModel model = new MST_ContactCategoryModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        model.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                        model.ContactCategoryName = dr["ContactCategoryName"].ToString();

                        model.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                        model.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);

                    }
                    return View("MST_ContactCategoryAddEdit", model);


                }

                /*string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
                SqlConnection conn = new SqlConnection(connectionstr);

                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_MST_ContactCategory_SelectByPK";
                objCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = ContactCategoryID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);

                MST_ContactCategoryModel modelMST_ContactCategory = new MST_ContactCategoryModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelMST_ContactCategory.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelMST_ContactCategory.ContactCategoryName = dr["ContactCategoryName"].ToString();
                    modelMST_ContactCategory.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelMST_ContactCategory.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);

                    return View("MST_ContactCategoryAddEdit", modelMST_ContactCategory);
                }
                conn.Close();*/
            }
            #endregion

            return View("MST_ContactCategoryAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(MST_ContactCategoryModel modelMST_ContactCategory)
        {
            if (ModelState.IsValid)
            {
                string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");

                CON_DAL dalCON = new CON_DAL();


                if (modelMST_ContactCategory.ContactCategoryID == null)
                {

                    if (Convert.ToBoolean(dalCON.dbo_PR_MST_ContactCategory_Insert(connectionstr, modelMST_ContactCategory)))
                    {
                        TempData["ContactCategoryInsertMessage"] = "Record inserted successfully";

                    }
                }
                else
                {
                    if (Convert.ToBoolean(dalCON.dbo_PR_MST_ContactCategory_UpdateByPK(connectionstr, modelMST_ContactCategory)))
                    {

                        TempData["ContactCategoryUpdateMessage"] = "Record Update Successfully";

                    }
                    return RedirectToAction("Index");
                }

                /*  #region Insert
                  string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
                  SqlConnection conn = new SqlConnection(connectionstr);

                  conn.Open();

                  SqlCommand objCmd = conn.CreateCommand();
                  objCmd.CommandType = CommandType.StoredProcedure;

                  if (modelMST_ContactCategory.ContactCategoryID == null)
                  {
                      objCmd.CommandText = "PR_MST_ContactCategory_Insert";

                  }
                  #endregion

                  #region Update By PK
                  else
                  {
                      objCmd.CommandText = "PR_MST_ContactCategory_UpdateByPK";
                      objCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelMST_ContactCategory.ContactCategoryID;

                  }
                  #endregion

                  objCmd.Parameters.Add("@ContactCategoryName", SqlDbType.NVarChar).Value = modelMST_ContactCategory.ContactCategoryName;
                  objCmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
                  objCmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;

                  if (Convert.ToBoolean(objCmd.ExecuteNonQuery()))
                  {
                      if (modelMST_ContactCategory.ContactCategoryID == null)
                          TempData["ContactCategoryInsertMessage"] = "Record Insert Successfully";
                      else
                          TempData["ContactCategoryInsertMessage"] = "Record Update Successfully";
                  }
                  conn.Close();*/
            }


            return RedirectToAction("Add");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactCategoryID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);

            conn.Open();

            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_MST_ContactCategory_DeleteByPK";

            objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID);

            objCmd.ExecuteNonQuery();


            conn.Close();

            return RedirectToAction("Index");
        }
        #endregion

        /*public IActionResult MST_ContactCategoryList()
        {


            return View();
        }*/
    }
}
