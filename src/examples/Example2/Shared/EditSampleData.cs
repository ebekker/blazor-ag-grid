using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Threading.Tasks;

namespace Example2.Shared
{
    /// <summary>
    /// Sample data adapted from docs sample:
    /// https://www.ag-grid.com/javascript-grid-cell-editing/#example-cell-editing
    /// </summary>
    public class EditSampleData
    {
        public EditSampleData()
        { }

        public EditSampleData(string fn, string ln, string g, int age, string a, string m, string c)
        {
            FirstName = fn;
            LastName = ln;
            Gender = g;
            Age = age;
            Address = a;
            Mood = m;
            Country = c;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Mood { get; set; }
        public string Country { get; set; }

        public static EditSampleData[] Sample => new[]
        {
            new EditSampleData {
                FirstName = "Bob",
                LastName = "Harrison",
                Gender = "Male",
                Address = "1197 Thunder Wagon Common, Cataract, RI, 02987-1016, US, (401) 747-0763",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Mary",
                LastName = "Wilson",
                Gender = "Female",
                Age = 11,
                Address = "3685 Rocky Glade, Showtucket, NU, X1E-9I0, CA, (867) 371-4215",
                Mood = "Sad",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Sadiq",
                LastName = "Khan",
                Gender = "Male",
                Age = 12,
                Address = "3235 High Forest, Glen Campbell, MS, 39035-6845, US, (601) 638-8186",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Jerry",
                LastName = "Mane",
                Gender = "Male",
                Age = 12,
                Address =
                "2234 Sleepy Pony Mall , Drain, DC, 20078-4243, US, (202) 948-3634",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Bob",
                LastName = "Harrison",
                Gender = "Male",
                Address = "1197 Thunder Wagon Common, Cataract, RI, 02987-1016, US, (401) 747-0763",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Mary",
                LastName = "Wilson",
                Gender = "Female",
                Age = 11,
                Address = "3685 Rocky Glade, Showtucket, NU, X1E-9I0, CA, (867) 371-4215",
                Mood = "Sad",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Sadiq",
                LastName = "Khan",
                Gender = "Male",
                Age = 12,
                Address =
                "3235 High Forest, Glen Campbell, MS, 39035-6845, US, (601) 638-8186",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Jerry",
                LastName = "Mane",
                Gender = "Male",
                Age = 12,
                Address =
                "2234 Sleepy Pony Mall , Drain, DC, 20078-4243, US, (202) 948-3634",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Bob",
                LastName = "Harrison",
                Gender = "Male",
                Address =
                "1197 Thunder Wagon Common, Cataract, RI, 02987-1016, US, (401) 747-0763",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Mary",
                LastName = "Wilson",
                Gender = "Female",
                Age = 11,
                Address = "3685 Rocky Glade, Showtucket, NU, X1E-9I0, CA, (867) 371-4215",
                Mood = "Sad",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Sadiq",
                LastName = "Khan",
                Gender = "Male",
                Age = 12,
                Address =
                "3235 High Forest, Glen Campbell, MS, 39035-6845, US, (601) 638-8186",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Jerry",
                LastName = "Mane",
                Gender = "Male",
                Age = 12,
                Address =
                "2234 Sleepy Pony Mall , Drain, DC, 20078-4243, US, (202) 948-3634",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Bob",
                LastName = "Harrison",
                Gender = "Male",
                Address =
                "1197 Thunder Wagon Common, Cataract, RI, 02987-1016, US, (401) 747-0763",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Mary",
                LastName = "Wilson",
                Gender = "Female",
                Age = 11,
                Address = "3685 Rocky Glade, Showtucket, NU, X1E-9I0, CA, (867) 371-4215",
                Mood = "Sad",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Sadiq",
                LastName = "Khan",
                Gender = "Male",
                Age = 12,
                Address =
                "3235 High Forest, Glen Campbell, MS, 39035-6845, US, (601) 638-8186",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Jerry",
                LastName = "Mane",
                Gender = "Male",
                Age = 12,
                Address =
                "2234 Sleepy Pony Mall , Drain, DC, 20078-4243, US, (202) 948-3634",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Bob",
                LastName = "Harrison",
                Gender = "Male",
                Address =
                "1197 Thunder Wagon Common, Cataract, RI, 02987-1016, US, (401) 747-0763",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Mary",
                LastName = "Wilson",
                Gender = "Female",
                Age = 11,
                Address = "3685 Rocky Glade, Showtucket, NU, X1E-9I0, CA, (867) 371-4215",
                Mood = "Sad",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Sadiq",
                LastName = "Khan",
                Gender = "Male",
                Age = 12,
                Address =
                "3235 High Forest, Glen Campbell, MS, 39035-6845, US, (601) 638-8186",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Jerry",
                LastName = "Mane",
                Gender = "Male",
                Age = 12,
                Address =
                "2234 Sleepy Pony Mall , Drain, DC, 20078-4243, US, (202) 948-3634",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Bob",
                LastName = "Harrison",
                Gender = "Male",
                Address =
                "1197 Thunder Wagon Common, Cataract, RI, 02987-1016, US, (401) 747-0763",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Mary",
                LastName = "Wilson",
                Gender = "Female",
                Age = 11,
                Address = "3685 Rocky Glade, Showtucket, NU, X1E-9I0, CA, (867) 371-4215",
                Mood = "Sad",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Sadiq",
                LastName = "Khan",
                Gender = "Male",
                Age = 12,
                Address =
                "3235 High Forest, Glen Campbell, MS, 39035-6845, US, (601) 638-8186",
                Mood = "Happy",
                Country = "Ireland",
            },
            new EditSampleData {
                FirstName = "Jerry",
                LastName = "Mane",
                Gender = "Male",
                Age = 12,
                Address =
                "2234 Sleepy Pony Mall , Drain, DC, 20078-4243, US, (202) 948-3634",
                Mood = "Happy",
                Country = "Ireland",
            },
                };
    }
}
