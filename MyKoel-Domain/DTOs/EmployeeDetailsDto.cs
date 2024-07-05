using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoel_Domain.DTOs
{
    public class EmployeeDetailsDto
    {
#nullable disable
        public string EmployeeName { get; set; }
        public string BU { get; set; }
        public string Grade { get; set; }
        public string CostCentre { get; set; }
        public string Department { get; set; }
        public string EmpTicketNo { get; set; }
    }


    public class EmployeeDetails
    {
        public List<Employee> Table { get; set; }
    }

    public class Employee
    {
        public int EMPID { get; set; }
        public string EmpName { get; set; }
        public string TicketNo { get; set; }
        public string Grade { get; set; }
        public string SBUNo { get; set; }
        public string CostCode { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string EmailID { get; set; }
        public int MANAGERID { get; set; }
        public string AppName { get; set; }
        public string ManagerEmailID { get; set; }
        public string ManagerTicketNo { get; set; }
        public DateTime DOB {get; set; }

    }

}