using Microsoft.Data.SqlClient;
using DTOs;
using DAL.Helpers;
using System.Data;
using System.Xml;
namespace DAL
{
    public class clsPersonData
    {
        public static int Add(PersonDTO DTO)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDBSettings.CN))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_AddPerson", conn))
                    {
                        conn?.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters?.AddWithValue("@Email", DTO.Email);
                        cmd.Parameters?.AddWithValue("@Address", DTO.Address);
                        cmd.Parameters?.AddWithValue("@FirstName", DTO.FirstName);
                        cmd.Parameters?.AddWithValue("@SecondName", DTO.SecondName);
                        cmd.Parameters?.AddWithValue("@ImagePath", string.IsNullOrEmpty(DTO.ImagePath) ? DBNull.Value : DTO.ImagePath);
                        cmd.Parameters?.AddWithValue("@ThirdName", string.IsNullOrEmpty(DTO.ThirdName) ? "" : DTO.ThirdName);
                        cmd.Parameters?.AddWithValue("@LastName", DTO.LastName);
                        cmd.Parameters?.AddWithValue("@DateOfBirth", DTO.DateOfBirth);
                        cmd.Parameters?.AddWithValue("@Gendor", DTO.Gendor);
                        cmd.Parameters?.AddWithValue("@NationalNo", DTO.NationalNo);
                        cmd.Parameters?.AddWithValue("@Phone", DTO.Phone);
                        cmd.Parameters?.AddWithValue("@NationalityCountryID", DTO.NationalityCountryID);
                        if (int.TryParse(cmd.ExecuteScalar().ToString(), out int ID))
                            return ID;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }
        public static bool Update(int ID, PersonDTO DTO)
        {
            
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDBSettings.CN))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdatePersonByID", conn))
                    {
                        conn?.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters?.AddWithValue("@PersonID", DTO.PersonID);
                        cmd.Parameters?.AddWithValue("@Email", DTO.Email);
                        cmd.Parameters?.AddWithValue("@Address", DTO.Address);
                        cmd.Parameters?.AddWithValue("@FirstName", DTO.FirstName);
                        cmd.Parameters?.AddWithValue("@SecondName", DTO.SecondName);
                        cmd.Parameters?.AddWithValue("@ImagePath", string.IsNullOrEmpty(DTO.ImagePath) ? DBNull.Value : DTO.ImagePath);
                        cmd.Parameters?.AddWithValue("@ThirdName", string.IsNullOrEmpty(DTO.ThirdName)?"": DTO.ThirdName);
                        cmd.Parameters?.AddWithValue("@LastName", DTO.LastName);
                        cmd.Parameters?.AddWithValue("@DateOfBirth", DTO.DateOfBirth);
                        cmd.Parameters?.AddWithValue("@Gendor", DTO.Gendor);
                        cmd.Parameters?.AddWithValue("@NationalNo", DTO.NationalNo);
                        cmd.Parameters?.AddWithValue("@Phone", DTO.Phone);
                        cmd.Parameters?.AddWithValue("@NationalityCountryID", DTO.NationalityCountryID);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }
        public static bool Delete(int ID)
        {
   
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDBSettings.CN))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DeletePersonByID", conn))
                    {
                        conn?.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters?.AddWithValue("@PersonID",ID);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }
        public static List<PersonDTO>GetAll()
        {
            var lst = new List<PersonDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDBSettings.CN))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetAllPeople", conn))
                    {
                        conn?.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                     
                        using(SqlDataReader reader= cmd?.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int ImageCol = reader.GetOrdinal("ImagePath");
                                string imgPath = reader.IsDBNull(ImageCol) ? null : reader.GetString(ImageCol);
                                lst?.Add(new PersonDTO(
                                    PersonID: reader.GetInt32(reader.GetOrdinal("PersonID"))
                                    , NationalNo: reader.GetString(reader.GetOrdinal("NationalNo"))
                                    , FirstName: reader.GetString(reader.GetOrdinal("FirstName"))
                                    , SecondName: reader.GetString(reader.GetOrdinal("SecondName"))
                                    , ThirdName: reader.GetString(reader.GetOrdinal("ThirdName"))
                                    , LastName: reader.GetString(reader.GetOrdinal("LastName"))
                                    , Gendor: reader.GetInt32(reader.GetOrdinal("Gendor"))
                                    , NationalityCountryID: reader.GetInt32(reader.GetOrdinal("NationalityCountryID"))
                                    , DateOfBirth: reader.GetDateTime(reader.GetOrdinal("DateOfBirth"))
                                    , Address: reader.GetString(reader.GetOrdinal("Address"))
                                    , Phone: reader.GetString(reader.GetOrdinal("Phone"))
                                    , Email: reader.GetString(reader.GetOrdinal("Email"))
                                    , ImagePath: imgPath
                                    ));
                            }
                            return lst;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }
        public static PersonDTO GetByID(int ID)
        {
            PersonDTO person;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDBSettings.CN))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetPersonByPersonID", conn))
                    {
                        conn?.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters?.AddWithValue("@PersonID", ID);
             
                        using (SqlDataReader reader = cmd?.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int ImageCol = reader.GetOrdinal("ImagePath");
                                string imgPath=reader.IsDBNull(ImageCol)?null: reader.GetString(ImageCol);
                                return new PersonDTO(
                                    PersonID: reader.GetInt32(reader.GetOrdinal("PersonID"))
                                    , NationalNo: reader.GetString(reader.GetOrdinal("NationalNo"))
                                    , FirstName: reader.GetString(reader.GetOrdinal("FirstName"))
                                    , SecondName: reader.GetString(reader.GetOrdinal("SecondName"))
                                    , ThirdName: reader.GetString(reader.GetOrdinal("ThirdName"))
                                    , LastName: reader.GetString(reader.GetOrdinal("LastName"))
                                    , Gendor: reader.GetInt32(reader.GetOrdinal("Gendor"))
                                    , NationalityCountryID: reader.GetInt32(reader.GetOrdinal("NationalityCountryID"))
                                    , DateOfBirth: reader.GetDateTime(reader.GetOrdinal("DateOfBirth"))
                                    , Address: reader.GetString(reader.GetOrdinal("Address"))
                                    , Phone: reader.GetString(reader.GetOrdinal("Phone"))
                                    , Email: reader.GetString(reader.GetOrdinal("Email"))
                                    , ImagePath: imgPath);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }
        public static bool IsExisted(int ID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDBSettings.CN))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_IsPersonExistedByID", conn))
                    {
                        conn?.Open();
                        cmd.Parameters?.AddWithValue("@PersonID", ID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        return cmd.ExecuteScalar() != null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }
        public static bool IsExisted(string NationalNo)
        {
            
                try
            {
                using (SqlConnection conn = new SqlConnection(clsDBSettings.CN))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_IsPersonExistedByNationalNo", conn))
                    {
                        conn?.Open(); 
                        cmd.Parameters?.AddWithValue("@NationalNo", NationalNo);
                        cmd.CommandType = CommandType.StoredProcedure;
                        return cmd.ExecuteScalar()!=null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }
    }
}
