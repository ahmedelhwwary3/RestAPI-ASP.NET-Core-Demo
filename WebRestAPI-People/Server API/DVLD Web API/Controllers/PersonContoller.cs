
using Microsoft.AspNetCore.Mvc;
using DTOs;
using static BLL.clsPerson;
using BLL;
 
namespace DVLD_Web_API.Controllers
{
    [Route("api/People")]
    [ApiController]
    public class PersonContoller : ControllerBase
    {



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PersonDTO>>GetAll()
        {
            var lst=clsPerson.GetAll();
            if (lst?.Count() == 0)
                return NotFound("People list is empty");
            return Ok(lst);
        }



        [HttpGet("{ID}",Name ="GetByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonDTO>GetByID(int ID)
        {
            if (ID < 1)
                return BadRequest($"Invalid ID {ID}");
            clsPerson person=clsPerson.GetByID(ID);
            if (person == null)
                return NotFound($"Person with ID {ID} is not found");
            return Ok(person.PersonDTO);
        }



        [HttpDelete("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int ID)
        {
            if (ID < 1)
                return BadRequest($"Invalid ID {ID}");
            if (!clsPerson.IsExisted(ID))
                return NotFound($"Person with ID {ID} is not found");
            if(!clsPerson.Delete(ID))
                return StatusCode(500,$"Person with ID {ID} delete failed");
            return Ok($"Person with ID {ID} was deleted");
        }




        [HttpPut("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PersonDTO> Update(int ID, PersonDTO DTO)
        {
            if (ID<1|| string.IsNullOrEmpty(DTO.NationalNo) || DTO.NationalityCountryID < 1 ||
             string.IsNullOrEmpty(DTO.SecondName) || string.IsNullOrEmpty(DTO.FirstName) ||
             string.IsNullOrEmpty(DTO.LastName) || string.IsNullOrEmpty(DTO.Address) ||
             string.IsNullOrEmpty(DTO.Email) || string.IsNullOrEmpty(DTO.Phone) || DTO.Gendor > 1
             || (DTO.DateOfBirth.AddYears(18) > DateTime.Now))
                return BadRequest($"Invalid Data");
            clsPerson person = clsPerson.GetByID(ID);
            if (person==null)
                return NotFound($"Person with ID {ID} is not found");
            person.FirstName = DTO.FirstName;
            person.LastName = DTO.LastName;
            person.Address = DTO.Address;
            person.Email = DTO.Email;
            person.Phone = DTO.Phone;
            person.DateOfBirth = person.DateOfBirth;
            person.FirstName = DTO.FirstName;
            person.LastName = DTO.LastName;
            person.ThirdName = DTO.ThirdName;
            person.SecondName= DTO.SecondName;
            person.Gendor = DTO.Gendor;
            person.ImagePath = DTO.ImagePath;
            person.NationalNo= DTO.NationalNo;
            person.NationalityCountryID = DTO.NationalityCountryID;
            if (!person.Save())
                return StatusCode(500,$"Update person with ID {ID} failed");
            return Ok(person.PersonDTO);
        }



        [HttpPost("NewPerson")]
        [ProducesResponseType(typeof(PersonDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PersonDTO> Add(PersonDTO DTO)
        {
            if (string.IsNullOrEmpty(DTO.NationalNo) || DTO.NationalityCountryID < 1 ||
             string.IsNullOrEmpty(DTO.SecondName) || string.IsNullOrEmpty(DTO.FirstName) ||
             string.IsNullOrEmpty(DTO.LastName) || string.IsNullOrEmpty(DTO.Address) ||
             string.IsNullOrEmpty(DTO.Email) || string.IsNullOrEmpty(DTO.Phone) || DTO.Gendor > 1
                   || ((DateTime.Now.Year- DTO.DateOfBirth.Year)<18)//age<18
                   )
                return BadRequest($"Invalid Data ");
            if (clsPerson.IsExisted(DTO.NationalNo))
                return BadRequest($"Person with NationalNo {DTO.NationalNo} is already existed");
            clsPerson person = new clsPerson(DTO,enMode.AddNew);
            if (!person.Save())
                return StatusCode(500, $"Add person failed");

            return CreatedAtRoute("GetByID",new {ID= person.PersonID }, person.PersonDTO);
        }

        [HttpPost("UploadImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImage(IFormFile ImageFile)
        {
            if (ImageFile == null || ImageFile.Length == 0)
                return BadRequest("Image is not valid");

            var ImageName = Guid.NewGuid().ToString()+ Path.GetExtension(ImageFile.FileName);
            var directory = "F:\\ServerImages";
            if(!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var FullPath=Path.Combine(directory,ImageName);
            using(var stream=new FileStream(FullPath,FileMode.Create))
            {
               await ImageFile.CopyToAsync(stream);
            }
            return Ok(new ImageDTO (){ ImageName= ImageName });
        }

        [HttpGet("GetImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetImage([FromQuery]string ImageName)
        {
            if (string.IsNullOrEmpty(ImageName) || ImageName.Length == 0)
                return BadRequest("Path is not valid");
            string directory = "F:\\ServerImages";
            string FullPath = Path.Combine(directory, ImageName);
            if (!System.IO.File.Exists(FullPath))
                return NotFound("Image is not found");

            var ImageFile = System.IO.File.OpenRead(FullPath);
            var ImageType = GetMymeType(FullPath);
            return File(ImageFile,ImageType);

        }
        private string GetMymeType(string path)
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return ext switch
            {
                ".png" => "image/png",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };
        }


    }
}
