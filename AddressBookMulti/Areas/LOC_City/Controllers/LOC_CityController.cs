using AddressBookMulti.DAL;
using AddressBookMulti.Areas.LOC_City.Models;
using MetronicAddressBook.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static AddressBookMulti.Areas.LOC_Country.Models.LOC_CountryModel;
using static AddressBookMulti.Areas.LOC_State.Models.LOC_StateModel;
using System.Data.SqlClient;
using AddressBookMulti.Areas.LOC_Country.Models;
using AddressBookMulti.Areas.LOC_State.Models;

namespace AddressBookMulti.Areas.LOC_City.Controllers
{
    [CheckAccess]
    [Area("LOC_City")]
    [Route("LOC_City/[Controller]/[action]")]
    public class LOC_CityController : Controller
    {
        #region DAL Object
        LOC_DAL dalLOC = new LOC_DAL();
        #endregion

        #region Constructore

        public LOC_CityController()
        {
            
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            #region SelectAll

            DataTable dt = dalLOC.dbo_PR_LOC_City_SelectAll();
            return View("LOC_CityList", dt);

            #endregion
        }
        #endregion

        #region Add
        public IActionResult Add(int CityID)
        {

            #region Country Drop Down

           DataTable dtCountry = dalLOC.dbo_PR_LOC_Country_SelectByDropdownList();

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



            #endregion


            #region Select By PK

            if (CityID != null)
            {
               

                DataTable dt = dalLOC.dbo_PR_LOC_City_SelectByPK(CityID);
                if (dt.Rows.Count > 0)
                {
                    LOC_CityModel model = new LOC_CityModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        DropDownByCountry(Convert.ToInt32(dr["CountryID"]));
                        model.StateID = Convert.ToInt32(dr["StateID"]);
                        model.CityID = Convert.ToInt32(dr["CityID"]);
                        model.CountryID = Convert.ToInt32(dr["CountryID"]);
                        model.CityName = dr["CityName"].ToString();
                        model.PinCode = dr["PinCode"].ToString();
                        model.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                        model.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                    }
                    return View("LOC_CityAddEdit", model);
                }

            }
            #endregion

            return View("LOC_CityAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(LOC_CityModel modelLOC_City)
        {

            if (modelLOC_City.CityID == null)
            {

                if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_City_Insert(modelLOC_City)))
                {
                    TempData["CityInsertMessage"] = "Record inserted successfully";

                }
            }
            else
            {
                if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_City_UpdateByPK(modelLOC_City)))
                {

                    TempData["CityUpdateMessage"] = "Record Update Successfully";

                }
                return RedirectToAction("Index");
            }


            return RedirectToAction("Add");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int CityID)
        {
           

            if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_City_DeleteByPK(CityID)))
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

            DataTable dt1 = dalLOC.dbo_PR_LOC_State_SelectByDropdownList(CountryID);

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

        #region Filter Records
        public IActionResult Filter(LOC_CityModel modelLOC_City)
        {
            
            DataTable dt = dalLOC.dbo_PR_LOC_City_FilterCountryNameAndCode(modelLOC_City.StateName, modelLOC_City.CityName, modelLOC_City.PinCode);


            return View("LOC_CityList", dt);

        }
        #endregion

    }
}
