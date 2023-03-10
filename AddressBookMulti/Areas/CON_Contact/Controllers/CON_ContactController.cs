using AddressBookMulti.DAL;
using AddressBookMulti.Areas.CON_Contact.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using MetronicAddressBook.BAL;
using AddressBookMulti.Areas.LOC_Country.Models;
using AddressBookMulti.Areas.LOC_State.Models;
using AddressBookMulti.Areas.LOC_City.Models;
using AddressBookMulti.Areas.MST_ContactCategory.Models;

namespace AddressBookMulti.Areas.CON_Contact.Controllers
{
    [CheckAccess]
    [Area("CON_Contact")]
    [Route("CON_Contact/[Controller]/[action]")]

    public class CON_ContactController : Controller
    {
        #region DAL Object
        
      

        CON_DAL dalCON = new CON_DAL();

        #endregion

        #region Constructor

        public CON_ContactController()

        {
            
        }
        #endregion

       

        #region Index
        public IActionResult Index()
        {
            #region SelectAll
          
            DataTable dt = dalCON.dbo_PR_CON_Contact_SelectAll();

            return View("CON_ContactList", dt);
           
            #endregion
        }
        #endregion

        #region Add
        public IActionResult Add(int ContactID)
        {

            #region Country Drop Down

            DataTable dtCountry = dalCON.dbo_PR_LOC_Country_SelectByDropdownList();


            List<LOC_Country_SelectForDropDownModel> list = new List<LOC_Country_SelectForDropDownModel>();
            foreach (DataRow dr in dtCountry.Rows)
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

            DataTable dtContactCategory = dalCON.dbo_PR_MST_ContactCategory_SelectByDropdownList();

           /* string connectionstr4 = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt4 = new DataTable();

            SqlConnection conn4 = new SqlConnection(connectionstr4);

            conn4.Open();

            SqlCommand objCmd4 = conn4.CreateCommand();
            objCmd4.CommandType = CommandType.StoredProcedure;
            objCmd4.CommandText = "PR_MST_ContactCategory_SelectForDropDown";
            SqlDataReader objSDR4 = objCmd4.ExecuteReader();
            dt4.Load(objSDR4);*/



            List<MST_ContactCategory_SelectForDropDownModel> list4 = new List<MST_ContactCategory_SelectForDropDownModel>();
            foreach (DataRow dr in dtContactCategory.Rows)
            {
                MST_ContactCategory_SelectForDropDownModel vlst4 = new MST_ContactCategory_SelectForDropDownModel();
                vlst4.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                vlst4.ContactCategoryName = dr["ContactCategoryName"].ToString();
                list4.Add(vlst4);
            }
            ViewBag.ContactCategoryList = list4;
          
            #endregion

            #region Select By PK
            if (ContactID != null)
            {

                

                DataTable dt = dalCON.dbo_PR_CON_Contact_SelectByPK(ContactID);
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



          
               
                if (modelCON_Contact.ContactID == null)
                {

                    if (Convert.ToBoolean(dalCON.dbo_PR_CON_Contact_Insert(modelCON_Contact)))
                    {
                        TempData["CountryInsertMessage"] = "Record inserted successfully";

                    }
                }
                else
                {
                    if (Convert.ToBoolean(dalCON.dbo_PR_CON_Contact_UpdateByPK(modelCON_Contact)))
                    {

                        TempData["CountryUpdateMessage"] = "Record Update Successfully";

                    }
                    return RedirectToAction("Index");
                }

               
            

            return RedirectToAction("Add");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactID)
        {
            

            if (Convert.ToBoolean(dalCON.dbo_PR_CON_Contact_DeleteByPK(ContactID)))
            {
                return RedirectToAction("Index");
            }
            return View("Index");
           
        }
        #endregion

        #region DropDownByCountry
        [HttpPost]
        public IActionResult DropDownByCountry(int CountryID)
        {
            #region State Drop Down

            DataTable dt1 = dalCON.dbo_PR_LOC_State_SelectByDropdownList(CountryID);

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

            DataTable dtCity = dalCON.dbo_PR_LOC_City_SelectByDropdownList(StateID);


            /*string connectionstr1 = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt1 = new DataTable();

            SqlConnection conn1 = new SqlConnection(connectionstr1);

            conn1.Open();

            SqlCommand objCmd1 = conn1.CreateCommand();
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.CommandText = "PR_LOC_City_SelectForDropDown";
            objCmd1.Parameters.AddWithValue("@StateID", StateID);
            SqlDataReader objSDR1 = objCmd1.ExecuteReader();
            dt1.Load(objSDR1);

            conn1.Close();*/

            List<LOC_City_SelectForDropDownModel> list2 = new List<LOC_City_SelectForDropDownModel>();
            foreach (DataRow dr in dtCity.Rows)
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


        #region Filter Records
        public IActionResult Filter(CON_ContactModel modelCON_Contact)
        {
            
            DataTable dt = dalCON.PR_CON_Contact_FilterByContactNameAndMobileNo(modelCON_Contact.ContactName, modelCON_Contact.MobileNo);


            return View("CON_ContactList", dt);

        }
        #endregion

    }
}
