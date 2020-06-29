using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example2.Shared
{
    public class Employee
    {
        public string[] OrgHierarchy { get; set; }

        public string JobTitle { get; set; }

        public string EmploymentType { get; set; }
    }

    public static class EmployeeHierarchy
    {
        /// Returns a new instance of the employee hierarchy.
        /// Converted over from the sample in:
        ///   https://www.ag-grid.com/javascript-grid-tree-data/#example-organisational-hierarchy
        public static Employee[] Employees => new[]
        {
          new Employee
          {
            OrgHierarchy = new[] { "Erica Rogers" },
            JobTitle = "CEO",
            EmploymentType = "Permanent",
          },
          new Employee
          {
            OrgHierarchy = new[] { "Erica Rogers", "Malcolm Barrett" },
            JobTitle = "Exec. Vice President",
            EmploymentType = "Permanent",
          },
          new Employee
          {
            OrgHierarchy = new[] { "Erica Rogers", "Malcolm Barrett", "Esther Baker" },
            JobTitle = "Director of Operations",
            EmploymentType = "Permanent",
          },
          new Employee
          {
            OrgHierarchy = new[] {
              "Erica Rogers",
              "Malcolm Barrett",
              "Esther Baker",
              "Brittany Hanson",
             },
            JobTitle = "Fleet Coordinator",
            EmploymentType = "Permanent",
          },
          new Employee
          {
            OrgHierarchy = new[] {
              "Erica Rogers",
              "Malcolm Barrett",
              "Esther Baker",
              "Brittany Hanson",
              "Leah Flowers",
             },
            JobTitle = "Parts Technician",
            EmploymentType = "Contract",
          },
          new Employee
          {
            OrgHierarchy = new[] {
              "Erica Rogers",
              "Malcolm Barrett",
              "Esther Baker",
              "Brittany Hanson",
              "Tammy Sutton",
             },
            JobTitle = "Service Technician",
            EmploymentType = "Contract",
          },
          new Employee
          {
            OrgHierarchy = new[] {
              "Erica Rogers",
              "Malcolm Barrett",
              "Esther Baker",
              "Derek Paul",
             },
            JobTitle = "Inventory Control",
            EmploymentType = "Permanent",
          },

          new Employee
          {
            OrgHierarchy = new[] { "Erica Rogers", "Malcolm Barrett", "Francis Strickland" },
            JobTitle = "VP Sales",
            EmploymentType = "Permanent",
          },
          new Employee
          {
            OrgHierarchy = new[] {
              "Erica Rogers",
              "Malcolm Barrett",
              "Francis Strickland",
              "Morris Hanson",
             },
            JobTitle = "Sales Manager",
            EmploymentType = "Permanent",
          },
          new Employee
          {
            OrgHierarchy = new[] {
              "Erica Rogers",
              "Malcolm Barrett",
              "Francis Strickland",
              "Todd Tyler",
             },
            JobTitle = "Sales Executive",
            EmploymentType = "Contract",
          },
          new Employee
          {
            OrgHierarchy = new[] {
              "Erica Rogers",
              "Malcolm Barrett",
              "Francis Strickland",
              "Bennie Wise",
             },
            JobTitle = "Sales Executive",
            EmploymentType = "Contract",
          },
          new Employee
          {
            OrgHierarchy = new[] {
              "Erica Rogers",
              "Malcolm Barrett",
              "Francis Strickland",
              "Joel Cooper",
             },
            JobTitle = "Sales Executive",
            EmploymentType = "Permanent",
          },
        };
    }
}
