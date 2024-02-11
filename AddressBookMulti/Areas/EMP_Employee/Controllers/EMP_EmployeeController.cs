using AddressBookMulti.Areas.EMP_Employee.Models;
using AddressBookMulti.Areas.LOC_Country.Models;
using AddressBookMulti.DAL;
using MetronicAddressBook.BAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AddressBookMulti.Areas.EMP_Employee.Controllers
{
  
    [Area("EMP_Employee")]
    [Route("EMP_Employee/[Controller]/[action]")]
    public class EMP_EmployeeController : Controller
    {
        #region DalObj
        EMP_DAL dalEMP = new EMP_DAL();
        #endregion

        #region Index
        public IActionResult Index()
        {
            //FillData();
            return View("EMP_Employee");
        }
        #endregion

        #region FillData
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
        #endregion

        #region AddEmployee
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
        #endregion

        #region SelectByPk
        public JsonResult Edit(int EmployeeID)
        {
            DataTable dt = dalEMP.EMP_EmployeeSelectByPK(EmployeeID);

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
                data = EmployeeList,
                success = true
            });


        }
        #endregion

        #region UpdateEmployee
        [HttpPost]
        public IActionResult Update(EMP_EmployeeModel formdata)
        {
            try
            {
                EMP_EmployeeModel _EmployeeModel = new EMP_EmployeeModel();

                _EmployeeModel.EmployeeID = formdata.EmployeeID;
                _EmployeeModel.EmployeeName = formdata.EmployeeName;
                _EmployeeModel.Address = formdata.Address;

                bool error = Convert.ToBoolean(dalEMP.EMP_EmployeeUpdate(_EmployeeModel));


                if (error)
                {
                    return Json(new
                    {
                        success = true,
                        message = "Record has been update successfully!",
                    });
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = "Failed to update record.",
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


        #endregion

        #region DeleteEmployee
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
        #endregion

       

    }
}
