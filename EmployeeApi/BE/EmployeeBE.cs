using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EmployeeApi.DAL;
using EmployeeApi.Models;


namespace EmployeeApi.BE
{
    //business layer implements all the methods from IEmployee repository
    public class EmployeeBE : IEmployeeRepository
    {
        private readonly EmployeeDAL objDAL = new EmployeeDAL();
        public List<EmployeeModel> GetEmployeesList()
        {
            return objDAL.GetEmployeesList();

        }
        public EmployeeModel GetEmployeeRecord(int id)
        {
            return objDAL.GetEmployeeRecord(id);
        }
        public string InsertEmployeeRecord(EmployeeModel model)
        {
            return objDAL.InsertEmployeeRecord(model);
        }
        public string UpdateEmployeeRecord(EmployeeModel model)
        {
            return objDAL.UpdateEmployeeRecord(model);
        }
        public string DeleteEmployeeRecord(int id)
        {
            return objDAL.DeleteEmployeeRecord(id);
        }

    }
}