using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFund.ConsoleApp
{
    // Parent Class
    internal class Person
    {
        public String FirstName { get; private set; }
        public String LastName { get; private set; }

        public void GetFullName(String firstName, String lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("invalid Name");
            }

            FirstName = firstName;
            LastName = lastName;

        }

        public String Address { get; private set; }
        public void SetAddress(String address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Invalid Address");
            }
            Address = address;
        }

        public DateOnly BirthDate { get; private set; }
        public decimal Salary { get; set; }
        public int TaxPercentage { get; set; }

        public void SetBirthDate(DateOnly birthDate)
        {
            if (birthDate > DateOnly.FromDateTime(DateTime.Now))
                throw new Exception("Invalid Birthdate");

            BirthDate = birthDate;
        }
    }
}
