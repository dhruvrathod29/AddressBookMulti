using AddressBookMulti.DAL;
using AddressBookMulti.Areas.MST_ContactCategory.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using MetronicAddressBook.BAL;
using AddressBookMulti.Areas.LOC_Country.Models;

namespace AddressBookMulti.Areas.MST_ContactCategory.Controllers
{

    [CheckAccess]
    [Area("MST_ContactCategory")]
    [Route("MST_ContactCategory/[Controller]/[action]")]

    public class MST_ContactCategoryController : Controller
    {
        #region DAL Object

        CON_DAL dalCON = new CON_DAL();

        #endregion

        #region Constructor

        public MST_ContactCategoryController()
        {
           
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            #region SelectAll
           
            DataTable dt = dalCON.dbo_PR_MST_ContactCategory_SelectAll();

            return View("MST_ContactCategoryList", dt);
           
            #endregion
        }

        #endregion

        #region Add
        public IActionResult Add(int ContactCategoryID)
        {
            #region Select By PK
            if (ContactCategoryID != null)
            {
               

                DataTable dt = dalCON.dbo_PR_MST_ContactCategory_SelectByPK(ContactCategoryID);
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
               

                if (modelMST_ContactCategory.ContactCategoryID == null)
                {

                    if (Convert.ToBoolean(dalCON.dbo_PR_MST_ContactCategory_Insert(modelMST_ContactCategory)))
                    {
                        TempData["ContactCategoryInsertMessage"] = "Record inserted successfully";

                    }
                }
                else
                {
                    if (Convert.ToBoolean(dalCON.dbo_PR_MST_ContactCategory_UpdateByPK(modelMST_ContactCategory)))
                    {

                        TempData["ContactCategoryUpdateMessage"] = "Record Update Successfully";

                    }
                    return RedirectToAction("Index");
                }

              
            }


            return RedirectToAction("Add");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactCategoryID)
        {

            if (Convert.ToBoolean(dalCON.dbo_PR_MST_ContactCategory_SelectAll(ContactCategoryID)))
            {
                return RedirectToAction("Index");
            }
            return View("Index");

        }
        #endregion

        #region Filter Records
        public IActionResult Filter(MST_ContactCategoryModel modelMST_ContactCategory)
        {
           

            CON_DAL dalMST = new CON_DAL();
            DataTable dt = dalMST.PR_MST_ContactCategory_FilterByContactCategoryName(modelMST_ContactCategory.ContactCategoryName);


            return View("MST_ContactCategoryList", dt);

        }
        #endregion

    }
}
