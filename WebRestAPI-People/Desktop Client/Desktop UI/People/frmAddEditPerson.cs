using Desktop_UI.Helpers;
using Desktop_UI.NewFolder;
using DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Windows.Forms;
using static Desktop_UI.Helpers.clsGlobal;

namespace Desktop_UI.People
{
    public partial class frmAddEditPerson : Form
    {
        PersonDTO _PersonDTO;
        public enum enMode { AddNew, Update };
        enMode _Mode = enMode.AddNew;
        int _PersonID = default;
        public frmAddEditPerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddEditPerson(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            _Mode = enMode.Update;
        }

        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            ResetDefaultAddNewValues();
            if (_Mode == enMode.Update)
                LoadData();
        }
        private async void LoadData()
        {
            var response1 = await httpClient.GetAsync($"{_PersonID}");
            if (response1.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                MessageBox.Show($"Person with ID {_PersonID.ToString()} is not found",
                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;//To ensure that no code will run after 
            }
            else if (response1.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                MessageBox.Show($"ID {_PersonID.ToString()} is not valid",
                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;//To ensure that no code will run after 
            }
            else if (response1.IsSuccessStatusCode)
            {
                _PersonDTO = await response1.Content.ReadFromJsonAsync<PersonDTO>();
            }
            if (!string.IsNullOrEmpty(_PersonDTO.ImagePath))
            {
                string ImageName = Uri.EscapeDataString(_PersonDTO.ImagePath);
                string URI = $"GetImage?ImageName={ImageName}";
                var response2 = await httpClient.GetAsync($"{URI}");
                if (response2.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show($"Person Image is not found",
                         "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (response2.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    MessageBox.Show($"Person Image is not valid",
                         "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (response2.IsSuccessStatusCode)
                {
                    var ImageBytes = await response2.Content.ReadAsByteArrayAsync();
                    using (var ms = new MemoryStream(ImageBytes))
                    {
                        pbPersonImage.Image = Image.FromStream(ms);
                    }
                }
            }

            dtpBirth.Value = _PersonDTO.DateOfBirth;
            if (_PersonDTO.Gendor == 0)
                rbMale.PerformClick();
            else
                rbFemale.PerformClick();
            txtEmail.Text = _PersonDTO.Email;
            txtFirst.Text = _PersonDTO.FirstName;
            txtSecond.Text = _PersonDTO.SecondName;
            txtThird.Text = _PersonDTO.ThirdName;
            txtLast.Text = _PersonDTO.LastName;
            txtNationalNo.Text = _PersonDTO.NationalNo;
            txtPhone.Text = _PersonDTO.Phone;
            lblPersonID.Text = _PersonDTO.PersonID.ToString();
            txtAddress.Text = _PersonDTO.Address;
            cbCountries.SelectedIndex = cbCountries.FindString("Egypt");//static

        }

        private void ResetDefaultAddNewValues()
        {

            SetTitle();
            txtAddress.Multiline = true;
            lblPersonID.Text = "N/A";
            txtFirst.Text = "";
            txtSecond.Text = "";
            txtThird.Text = "";
            txtLast.Text = "";
            txtEmail.Text = "";
            txtNationalNo.Text = "";
            txtPhone.Text = "";
            dtpBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpBirth.MinDate = DateTime.Now.AddYears(-100);
            dtpBirth.Value = dtpBirth.MaxDate;//Age=18
            rbMale.Checked = true;
            txtAddress.Text = "";
            FillCountriesInComboBox();
            cbCountries.SelectedIndex = cbCountries.FindString("Egypt");
        }
        void SetTitle()
        {
            lblTitle.Text = _Mode == enMode.Update ? "Update Person" : "Add New Person";
            this.Text = _Mode == enMode.Update ? "Update Person" : "Add New Person";
        }
        void FillCountriesInComboBox()
        {
            cbCountries.Items?.Add("Egypt");
            cbCountries.Items?.Add("Ghaza");
            cbCountries.Items?.Add("Omman");
            cbCountries.Items?.Add("Qatar");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        MultipartFormDataContent HandleImage()
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            if (_PersonDTO.ImagePath != pbPersonImage.ImageLocation)
            {
                string SourceFilePath = pbPersonImage.ImageLocation.ToString();
                _PersonDTO.ImagePath = SourceFilePath;
                var ms = new MemoryStream();
                pbPersonImage.Image.Save(ms, pbPersonImage.Image.RawFormat); // تحويل الصورة إلى بايتات
                ms.Position = 0; // تأكد أن الـ Stream يبدأ من البداية
                var imageContent = new StreamContent(ms);
                imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

                // "ImageFile" لازم يكون هو نفس اسم الباراميتر في API
                content.Add(imageContent, "ImageFile", "person.jpg");
            }
            return content;
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            //Need Validation....
            string ImageName = "";


            var ImageContent = HandleImage();
            var response1 = await httpClient.PostAsync("UploadImage", ImageContent);
            if (response1.IsSuccessStatusCode)
            {
                var ImageDTO = await response1.Content.ReadFromJsonAsync <ImageDTO>();
                ImageName = ImageDTO.ImageName;
                MessageBox.Show($"Upload Image succeeded",
                "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (response1.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                MessageBox.Show($"Image is not valid", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _PersonDTO = new PersonDTO(PersonID: default, NationalNo: txtNationalNo.Text.ToUpper().Trim(), FirstName: txtFirst.Text.Trim(),
                SecondName: txtSecond.Text.Trim(), ThirdName: txtThird.Text.Trim(), LastName: txtLast.Text.Trim(), NationalityCountryID: 1,
                DateOfBirth: dtpBirth.Value, Gendor: rbFemale.Checked ? 1 : 0, Address: txtAddress.Text.Trim(), Phone: txtPhone.Text.Trim(),
                Email: txtEmail.Text.Trim(), ImagePath: ImageName);

            //img is null
            if (_Mode == enMode.AddNew)
            {
                var response = await httpClient.PostAsJsonAsync("", _PersonDTO);
                //var errorDetails =await response.Content.ReadAsStringAsync();
                //MessageBox.Show($"{errorDetails}");
                if (response.IsSuccessStatusCode)
                {
                    _PersonDTO = await response.Content.ReadFromJsonAsync<PersonDTO>();
                    lblPersonID.Text = _PersonDTO.PersonID.ToString();
                    MessageBox.Show($"Person add succeeded with ID {_PersonDTO.PersonID.ToString()}",
                    "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    MessageBox.Show($"Invalid Data or Person with NationalNo{_PersonDTO.NationalNo}" +
                        $" is already existed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var response = await httpClient.PutAsJsonAsync($"{_PersonID}", _PersonDTO);
                if (response.IsSuccessStatusCode)
                {
                    _PersonDTO = await response.Content.ReadFromJsonAsync<PersonDTO>();
                    MessageBox.Show($"Person update succeeded",
                    "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    MessageBox.Show($"Invalid Data or Person with NationalNo{_PersonDTO.NationalNo}" +
                        $" is already existed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
         

        }

        
        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.DefaultExt = ".JPG";
            openFileDialog1.Filter = "All files (*.*)|*.*| (*.PNG)|*.PNG| (*.JPG)|*.JPG|  (*.JPEG)|*.JPEG";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Title = "Choose Image";
            openFileDialog1.InitialDirectory = $"F:\\";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string ImagePath = openFileDialog1.FileName;
                pbPersonImage.ImageLocation = ImagePath;
            }

        }
    }
}
