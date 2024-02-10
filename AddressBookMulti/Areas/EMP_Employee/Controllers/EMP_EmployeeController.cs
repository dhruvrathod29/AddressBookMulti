using AddressBookMulti.Areas.EMP_Employee.Models;
using AddressBookMulti.DAL;
using MetronicAddressBook.BAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AddressBookMulti.Areas.EMP_Employee.Controllers
{
    [CheckAccess]
    [Area("EMP_Employee")]
    [Route("EMP_Employee/[Controller]/[action]")]
    public class EMP_EmployeeController : Controller
    {
        EMP_DAL dalEMP = new EMP_DAL();
        public IActionResult Index()
        {
            //FillData();
            return View("EMP_Employee");
        }

        public JsonResult FillData()
        {
            DataTable dt = dalEMP.EMP_Employee_SelectAll();
            
            List<EMP_EmployeeModel> EmployeeList = new List<EMP_EmployeeModel>();
           
            foreach (DataRow dr in dt.Rows)
            {
                EMP_EmployeeModel EmployeeData = new EMP_EmployeeModel();
                {
                    EmployeeData.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);    
                    EmployeeData.EmployeeName = dr["EmployeeName"].ToString().Trim();
                    EmployeeData.Address = dr["Address"].ToString().Trim();
                    EmployeeData.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);

                };

                EmployeeList.Add(EmployeeData);
            }
            // Convert list of dictionaries to JSON
            return Json(new
            {
                data = EmployeeList
            }) ;
        }

        [HttpPost]
        public IActionResult Add(EMP_EmployeeModel formdata)
        {
            try
            {
                EMP_EmployeeModel _EmployeeModel = new EMP_EmployeeModel();

                _EmployeeModel.EmployeeName = formdata.EmployeeName;
                _EmployeeModel.Address = formdata.Address;
                bool error = Convert.ToBoolean(dalEMP.EMP_EmployeeInsert(_EmployeeModel));


                if (error)
                {
                    return Json(new
                    {
                        success = true,
                        message = "Record has been insert successfully!",
                    });
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = "Failed to insert record.",
                    });
                } 
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "An error occurred while processing the request.",
                    error = ex.Message // This will pass the exception message in the JSON response
                });
            }
        }

        [HttpPost]
        public IActionResult Delete(int employeeID)
        {
            try
            {
                bool error = Convert.ToBoolean(dalEMP.EMP_EmployeeDelete(employeeID));
                if (error)
                {
                    return Json(new
                    {
                        success = true,
                        message = "Record has been Delete successfully!",
                    });
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = "Failed to Delete record.",
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "An error occurred while processing the request.",
                    error = ex.Message // This will pass the exception message in the JSON response
                });
            }
        }

    }
}
