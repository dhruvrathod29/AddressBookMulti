using AddressBookMulti.DAL;
using AddressBookMulti.Areas.LOC_State.Models;
using MetronicAddressBook.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static AddressBookMulti.Areas.LOC_Country.Models.LOC_CountryModel;
using AddressBookMulti.Areas.LOC_Country.Models;

namespace AddressBookMulti.Areas.LOC_State.Controllers
{
    [CheckAccess]
    
    [Area("LOC_State")]
    [Route("LOC_State/[Controller]/[action]")]
    public class LOC_StateController : Controller
    {
        #region DAL Object

        LOC_DAL dalLOC = new LOC_DAL();

        #endregion

        #region Constructor

        public LOC_StateController()
        {
            
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            DataTable dt = dalLOC.dbo_PR_LOC_State_SelectAll();
            return View("LOC_StateList", dt);

        }
        #endregion

        #region Add
        public IActionResult Add(int StateID)
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
       
            #endregion

            #region Select By PK

            if (StateID != null)
            {

                DataTable dt = dalLOC.dbo_PR_LOC_State_SelectByPK(StateID);
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

                if (modelLOC_State.StateID == null)
                {

                    if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_State_Insert(modelLOC_State)))
                    {
                        TempData["StateInsertMessage"] = "Record inserted successfully";

                    }
                }
                else
                {
                    if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_State_UpdateByPK(modelLOC_State)))
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
           
            if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_State_DeleteByPK(StateID)))
            {
                return RedirectToAction("Index");
            }
            return View("Index");

        }
        #endregion

        #region Filter Records
        public IActionResult Filter(LOC_StateModel modelLOC_State)
        {
            DataTable dt = dalLOC.dbo_PR_LOC_State_FilterCountryNameAndCode(modelLOC_State.CountryName, modelLOC_State.StateName, modelLOC_State.StateCode);

            return View("LOC_StateList", dt);
        }
        #endregion

    }
}
