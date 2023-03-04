using AddressBookMulti.DAL;
using AddressBookMulti.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using MetronicAddressBook.BAL;

namespace AddressBookMulti.Controllers
{
    public class CON_ContactController : Controller
    {
        #region Configuration
        private IConfiguration Configuration;
        public CON_ContactController(IConfiguration _configuration)

        {
            Configuration = _configuration;
        }
        #endregion

        [CheckAccess]

        #region Index
        public IActionResult Index()
        {
            #region SelectAll
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");

            CON_DAL dalCON = new CON_DAL();
            DataTable dt = dalCON.dbo_PR_CON_Contact_SelectAll(connectionstr);

            return View("CON_ContactList", dt);
            /*  DataTable dt = new DataTable();
              SqlConnection conn = new SqlConnection(connectionstr);

              conn.Open();

              SqlCommand objCmd = conn.CreateCommand();
              objCmd.CommandType = CommandType.StoredProcedure;
              objCmd.CommandText = "PR_CON_Contact_SelectAll";
              SqlDataReader objSDR = objCmd.ExecuteReader();
              dt.Load(objSDR);

              conn.Close();

              return View("CON_ContactList", dt);*/
            #endregion
        }
        #endregion

        #region Add
        public IActionResult Add(int ContactID)
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
            conn1.Close();



            List<LOC_Country_SelectForDropDownModel> list = new List<LOC_Country_SelectForDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                LOC_Country_SelectForDropDownModel vlst = new LOC_Country_SelectForDropDownModel();
                vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                vlst.CountryName = dr["CountryName"].ToString();
                list.Add(vlst);
            }
            ViewBag.CountryList = list;


            List<LOC_State_SelectForDropDownModel> list1 = new List<LOC_State_SelectForDropDownModel>();
            ViewBag.StateList = list1;
            List<LOC_City_SelectForDropDownModel> list2 = new List<LOC_City_SelectForDropDownModel>();
            ViewBag.CityList = list2;

            #endregion

            #region Contact Category Drop Down
            string connectionstr4 = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt4 = new DataTable();

            SqlConnection conn4 = new SqlConnection(connectionstr4);

            conn4.Open();

            SqlCommand objCmd4 = conn4.CreateCommand();
            objCmd4.CommandType = CommandType.StoredProcedure;
            objCmd4.CommandText = "PR_MST_ContactCategory_SelectForDropDown";
            SqlDataReader objSDR4 = objCmd4.ExecuteReader();
            dt4.Load(objSDR4);



            List<MST_ContactCategory_SelectForDropDownModel> list4 = new List<MST_ContactCategory_SelectForDropDownModel>();
            foreach (DataRow dr in dt4.Rows)
            {
                MST_ContactCategory_SelectForDropDownModel vlst4 = new MST_ContactCategory_SelectForDropDownModel();
                vlst4.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                vlst4.ContactCategoryName = dr["ContactCategoryName"].ToString();
                list4.Add(vlst4);
            }
            ViewBag.ContactCategoryList = list4;
            conn4.Close();
            #endregion

            #region Select By PK
            if (ContactID != null)
            {

                string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
                CON_DAL dalCON = new CON_DAL();

                DataTable dt = dalCON.dbo_PR_CON_Contact_SelectByPK(connectionstr, ContactID);
                if (dt.Rows.Count > 0)
                {
                    CON_ContactModel model = new CON_ContactModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        DropDownByCountry(Convert.ToInt32(dr["CountryID"]));
                        DropDownByState(Convert.ToInt32(dr["StateID"]));
                        model.ContactID = Convert.ToInt32(dr["ContactID"]);
                        model.CountryID = Convert.ToInt32(dr["CountryID"]);
                        model.StateID = Convert.ToInt32(dr["StateID"]);
                        model.CityID = Convert.ToInt32(dr["CityID"]);
                        model.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                        model.ContactName = dr["ContactName"].ToString();
                        model.Address = dr["Address"].ToString();
                        model.PinCode = dr["PinCode"].ToString();
                        model.MobileNo = dr["MobileNo"].ToString();
                        model.AlternetContact = dr["AlternetContact"].ToString();
                        model.Email = dr["Email"].ToString();
                        model.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
                        model.LinkedIn = dr["LinkedIn"].ToString();
                        model.Twitter = dr["Twitter"].ToString();
                        model.Insta = dr["Insta"].ToString();
                        model.Gender = dr["Gender"].ToString();

                        model.PhotoPath = dr["PhotoPath"].ToString();

                        model.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                        model.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);

                    }
                    return View("CON_ContactAddEdit", model);


                }


                /*string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
                SqlConnection conn = new SqlConnection(connectionstr);

                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_CON_Contact_SelectByPK";
                objCmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = ContactID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);

                CON_ContactModel modelCON_ContactModel = new CON_ContactModel();

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownByCountry(Convert.ToInt32(dr["CountryID"]));
                    DropDownByState(Convert.ToInt32(dr["StateID"]));
                    modelCON_ContactModel.ContactID = Convert.ToInt32(dr["ContactID"]);
                    modelCON_ContactModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelCON_ContactModel.StateID = Convert.ToInt32(dr["StateID"]);
                    modelCON_ContactModel.CityID = Convert.ToInt32(dr["CityID"]);
                    modelCON_ContactModel.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelCON_ContactModel.ContactName = dr["ContactName"].ToString();
                    modelCON_ContactModel.Address = dr["Address"].ToString();
                    modelCON_ContactModel.PinCode = dr["PinCode"].ToString();
                    modelCON_ContactModel.MobileNo = dr["MobileNo"].ToString();
                    modelCON_ContactModel.AlternetContact = dr["AlternetContact"].ToString();
                    modelCON_ContactModel.Email = dr["Email"].ToString();
                    modelCON_ContactModel.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
                    modelCON_ContactModel.LinkedIn = dr["LinkedIn"].ToString();
                    modelCON_ContactModel.Twitter = dr["Twitter"].ToString();
                    modelCON_ContactModel.Insta = dr["Insta"].ToString();
                    modelCON_ContactModel.Gender = dr["Gender"].ToString();

                    modelCON_ContactModel.PhotoPath = dr["PhotoPath"].ToString();

                    modelCON_ContactModel.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelCON_ContactModel.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);

                    return View("CON_ContactAddEdit", modelCON_ContactModel);
                }
                conn.Close();*/
            }
            #endregion

            return View("CON_ContactAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(CON_ContactModel modelCON_Contact)
        {
            #region PhotoPath
            if (modelCON_Contact.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, modelCON_Contact.File.FileName);
                modelCON_Contact.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelCON_Contact.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelCON_Contact.File.CopyTo(stream);
                }

            }
            #endregion



          
                string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
                CON_DAL dalCON = new CON_DAL();


                if (modelCON_Contact.ContactID == null)
                {

                    if (Convert.ToBoolean(dalCON.dbo_PR_CON_Contact_Insert(connectionstr, modelCON_Contact)))
                    {
                        TempData["CountryInsertMessage"] = "Record inserted successfully";

                    }
                }
                else
                {
                    if (Convert.ToBoolean(dalCON.dbo_PR_CON_Contact_UpdateByPK(connectionstr, modelCON_Contact)))
                    {

                        TempData["CountryUpdateMessage"] = "Record Update Successfully";

                    }
                    return RedirectToAction("Index");
                }

                /* #region Insert
                string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
               SqlConnection conn = new SqlConnection(connectionstr);

               conn.Open();

               SqlCommand objCmd = conn.CreateCommand();
               objCmd.CommandType = CommandType.StoredProcedure;

               if (modelCON_Contact.ContactID == null)
               {
                   objCmd.CommandText = "PR_CON_Contact_Insert";

               }
               else
               {
                   objCmd.CommandText = "PR_CON_Contact_UpdateByPK";
                   objCmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = modelCON_Contact.ContactID;

               }
               #endregion*/



                #region Update By PK

                #endregion

                /*objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelCON_Contact.CountryID;
                objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelCON_Contact.StateID;
                objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelCON_Contact.CityID;
                objCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelCON_Contact.ContactCategoryID;
                objCmd.Parameters.Add("@ContactName", SqlDbType.NVarChar).Value = modelCON_Contact.ContactName;
                objCmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = modelCON_Contact.Address;
                objCmd.Parameters.Add("@PinCode", SqlDbType.NVarChar).Value = modelCON_Contact.PinCode;
                objCmd.Parameters.Add("@MobileNo", SqlDbType.NVarChar).Value = modelCON_Contact.MobileNo;
                objCmd.Parameters.Add("@AlternetContact", SqlDbType.NVarChar).Value = modelCON_Contact.AlternetContact;
                objCmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = modelCON_Contact.Email;
                objCmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value = modelCON_Contact.BirthDate;
                objCmd.Parameters.Add("@LinkedIn", SqlDbType.NVarChar).Value = modelCON_Contact.LinkedIn;
                objCmd.Parameters.Add("@Twitter", SqlDbType.NVarChar).Value = modelCON_Contact.Twitter;
                objCmd.Parameters.Add("@Insta", SqlDbType.NVarChar).Value = modelCON_Contact.Insta;
                objCmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = modelCON_Contact.Gender;
                objCmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelCON_Contact.PhotoPath;


                objCmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
                objCmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;

                if (Convert.ToBoolean(objCmd.ExecuteNonQuery()))
                {
                    if (modelCON_Contact.ContactID == null)
                        TempData["ContactInsertMessage"] = "Record Insert Successfully";
                    else
                        TempData["ContactInsertMessage"] = "Record Update Successfully";
                }
                conn.Close();*/
            

            return RedirectToAction("Add");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");

            CON_DAL dalCON = new CON_DAL();

            if (Convert.ToBoolean(dalCON.dbo_PR_CON_Contact_DeleteByPK(connectionstr, ContactID)))
            {
                return RedirectToAction("Index");
            }
            return View("Index");
            /*  DataTable dt = new DataTable();
              SqlConnection conn = new SqlConnection(connectionstr);

              conn.Open();

              SqlCommand objCmd = conn.CreateCommand();
              objCmd.CommandType = CommandType.StoredProcedure;
              objCmd.CommandText = "PR_CON_Contact_DeleteByPK";

              objCmd.Parameters.AddWithValue("@ContactID", ContactID);

              objCmd.ExecuteNonQuery();


              conn.Close();

              return RedirectToAction("Index");*/
        }
        #endregion

        #region DropDownByCountry
        [HttpPost]
        public IActionResult DropDownByCountry(int CountryID)
        {
            #region State Drop Down

            string connectionstr1 = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt1 = new DataTable();

            SqlConnection conn1 = new SqlConnection(connectionstr1);

            conn1.Open();

            SqlCommand objCmd1 = conn1.CreateCommand();
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.CommandText = "PR_LOC_State_SelectForDropDown";
            objCmd1.Parameters.AddWithValue("@CountryID", CountryID);
            SqlDataReader objSDR1 = objCmd1.ExecuteReader();
            dt1.Load(objSDR1);

            conn1.Close();

            List<LOC_State_SelectForDropDownModel> list1 = new List<LOC_State_SelectForDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                LOC_State_SelectForDropDownModel vlst = new LOC_State_SelectForDropDownModel();
                vlst.StateID = Convert.ToInt32(dr["StateID"]);
                vlst.StateName = dr["StateName"].ToString();
                list1.Add(vlst);
            }
            ViewBag.StateList = list1;
            var vModel = list1;
            return Json(vModel);

            #endregion
        }
        #endregion

        #region DropDownByState
        [HttpPost]
        public IActionResult DropDownByState(int StateID)
        {
            #region City Drop Down

            string connectionstr1 = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt1 = new DataTable();

            SqlConnection conn1 = new SqlConnection(connectionstr1);

            conn1.Open();

            SqlCommand objCmd1 = conn1.CreateCommand();
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.CommandText = "PR_LOC_City_SelectForDropDown";
            objCmd1.Parameters.AddWithValue("@StateID", StateID);
            SqlDataReader objSDR1 = objCmd1.ExecuteReader();
            dt1.Load(objSDR1);

            conn1.Close();

            List<LOC_City_SelectForDropDownModel> list2 = new List<LOC_City_SelectForDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                LOC_City_SelectForDropDownModel vlst = new LOC_City_SelectForDropDownModel();
                vlst.CityID = Convert.ToInt32(dr["CityID"]);
                vlst.CityName = dr["CityName"].ToString();
                list2.Add(vlst);
            }
            ViewBag.CityList = list2;
            var vModel = list2;
            return Json(vModel);

            #endregion
        }
        #endregion

        /* public IActionResult CON_ContactList()
         {

             return View();
         }*/
    }
}
