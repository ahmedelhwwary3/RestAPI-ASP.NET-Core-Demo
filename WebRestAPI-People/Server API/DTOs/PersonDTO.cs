using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DTOs
{
    public class PersonDTO
    {


        public int PersonID { get; set; }

        public string NationalNo { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ThirdName { get; set; }

        public string LastName { get; set; }

        public int Gendor { get; set; }

        public int NationalityCountryID { get; set; }
 
        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
        [AllowNull]
        public string? ImagePath { get; set; }


        public PersonDTO(int PersonID, string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, int NationalityCountryID,
            DateTime DateOfBirth, int Gendor, string Address, string Phone,
            string Email, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalityCountryID = NationalityCountryID;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.ImagePath = ImagePath;
        }


    }
}
