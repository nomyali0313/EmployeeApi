using EmployeeApi.BE;
using EmployeeApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace EmployeeApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private EmployeeBE objBE = new EmployeeBE();

        // GET api/Employee/GetEmployeesList
        [HttpGet]
        [Route("GetEmployeesList")]
        public List<EmployeeModel> GetEmployeesList()
        {
            List<EmployeeModel> list = new List<EmployeeModel>();
            list = objBE.GetEmployeesList();
            return list;
        }

        // GET api/Employee/GetEmployee/5
        [HttpGet]
        [Route("GetEmployee")]
        public EmployeeModel GetEmployee(int id)
        {
            return objBE.GetEmployeeRecord(id);
        }

        [HttpPost]
        [Route("InsertOrUpdateEmployeeRecord")]
        public string InsertOrUpdateEmployeeRecord(EmployeeModel model)
        {
            string result;
            if (model.Id == 0) //if id = 0 that means new employee record is to be inserted
            {
                result = objBE.InsertEmployeeRecord(model);
                return result;
            }
            else
            {// to update the employee record
                result = objBE.UpdateEmployeeRecord(model);
                return result;
            }

        }

        [HttpGet]
        [Route("DeleteEmployeeRecord")]
        //to delete the employee record against the employee id
        public string DeleteEmployeeRecord(int id)
        {
            return objBE.DeleteEmployeeRecord(id);
        }
    }
}