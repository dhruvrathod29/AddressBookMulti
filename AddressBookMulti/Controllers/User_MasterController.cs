using AddressBookMulti.DAL;
using AddressBookMulti.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AddressBookMulti.Controllers
{
    public class User_MasterController : Controller
    {

        private IConfiguration Configuration;

        public User_MasterController(IConfiguration _configuration)
        {
            this.Configuration = _configuration;
        }
       

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User_MasterModel modelUser_Master)
        {
            string connstr = Configuration.GetConnectionString("myConnectionStrings");
            string error = null;
            if (modelUser_Master.UserName == null)
            {
                error += "User Name is required";
            }
            if (modelUser_Master.Password == null)
            {
                error += "<br/>Password is required";
            }

            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("Index");
            }
            else
            {
                Master_DAL dalMaster = new Master_DAL();
                DataTable dt = dalMaster.dbo_PR_User_Master_SelectByUserNamePassword(connstr, modelUser_Master.UserName, modelUser_Master.Password);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                        HttpContext.Session.SetString("DisplayName", dr["DisplayName"].ToString());
                        /* HttpContext.Session.SetString("Email", dr["Email"].ToString());
                         HttpContext.Session.SetString("MobileNo", dr["MobileNo"].ToString());
                         HttpContext.Session.SetString("CreationDate", dr["CreationDate"].ToString());
                         HttpContext.Session.SetString("ModificationDate", dr["ModificationDate"].ToString());
 */

                        break;
                    }
                }
                else
                {
                    TempData["Error"] = "User Name or Password is invalid!";
                    return RedirectToAction("Index");
                }
                if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
