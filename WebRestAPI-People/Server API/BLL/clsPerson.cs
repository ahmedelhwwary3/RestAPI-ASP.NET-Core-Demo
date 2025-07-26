using DAL;
using DTOs;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BLL
{
    public class clsPerson
    {
        public enum enMode { AddNew,Update };
        enMode _Mode= enMode.AddNew;
        public int PersonID { get; set; }

        public string NationalNo { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ThirdName { get; set; }

        public string LastName { get; set; }

        public int Gendor { get; set; }

        public int NationalityCountryID { get; set; }

        public string FullName
            => FirstName + " " + SecondName + " " +
               (string.IsNullOrEmpty(ThirdName) ? "" : ThirdName + " ") + LastName;


        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public PersonDTO PersonDTO { get => new PersonDTO(
           PersonID = this.PersonID,
           NationalNo = this.NationalNo,
           FirstName = this.FirstName,
           SecondName = this.SecondName,
           ThirdName = this.ThirdName,
           LastName = this.LastName,
           NationalityCountryID = this.NationalityCountryID,
           DateOfBirth = this.DateOfBirth,
           Gendor = this.Gendor,
           Address = this.Address,
           Phone = this.Phone,
           Email = this.Email,
           ImagePath = this.ImagePath);
        }
        [AllowNull]
        public string? ImagePath { get; set; }
        

        public clsPerson(PersonDTO person,enMode mode)
        {
            this.PersonID = person.PersonID;
            this.NationalNo = person.NationalNo;
            this.FirstName = person.FirstName;
            this.SecondName = person.SecondName;
            this.ThirdName = person.ThirdName;
            this.LastName = person.LastName;
            this.NationalityCountryID = person.NationalityCountryID;
            this.DateOfBirth = person.DateOfBirth;
            this.Gendor = person.Gendor;
            this.Address = person.Address;
            this.Phone = person.Phone;
            this.Email = person.Email;
            this.ImagePath = person.ImagePath;

            _Mode = mode;
        }
        private bool _Add()
        {
            this.PersonID = clsPersonData.Add(PersonDTO);
            return PersonID != default;
        }
        private bool _Update()
            => clsPersonData.Update(PersonID, PersonDTO);

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if(_Add())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        return false;
                    }
                case enMode.Update:
                    return _Update();
                default:
                    return false;
            }
            
        }
        public static bool IsExisted(int ID)
            =>clsPersonData.IsExisted(ID);
        public static bool IsExisted(string NationalNo)
            => clsPersonData.IsExisted(NationalNo);
        public static clsPerson GetByID(int ID)
        {
            PersonDTO DTO= clsPersonData.GetByID(ID);
            if(DTO!=null)
                return new clsPerson(DTO, enMode.Update);

            return null;
        }
        public static IEnumerable<PersonDTO>GetAll()
            =>clsPersonData.GetAll();
        public static bool Delete(int ID)
            => clsPersonData.Delete(ID);




    }
}
