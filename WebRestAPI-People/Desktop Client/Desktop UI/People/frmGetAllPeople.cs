using Desktop_UI.Helpers;
using Desktop_UI.NewFolder;
using Desktop_UI.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Desktop_UI.Helpers.clsGlobal;
namespace Desktop_UI
{
    public partial class frmGetAllPeople : Form
    {
        public frmGetAllPeople()
        {
            InitializeComponent();
        }

        private async void frmGetAllPeople_Load(object sender, EventArgs e)
        {
            try
            {
                var response = await httpClient.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    dgvPeople.DataSource = await response.Content.ReadFromJsonAsync<List<PersonDTO>>();
                    lblCount.Text = dgvPeople.Rows.Count.ToString();
                }
                else
                {
                    MessageBox.Show("People List is Empty !", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

            }

        }
        void Reload() => frmGetAllPeople_Load(null, null);
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.ShowDialog();
            Reload();
        }

        private void editPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            frmAddEditPerson frm = new frmAddEditPerson(ID);
            frm.ShowDialog();
            Reload();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAdd.PerformClick();
        }

        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this Person ?",
                 "confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                 MessageBoxDefaultButton.Button2) != DialogResult.OK)
                return;
            int ID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            var response =await httpClient.DeleteAsync($"{ID}");
            if(response.IsSuccessStatusCode)
            {
                MessageBox.Show("Person was deleted successfully",
                 "Delete succeeded", MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
                Reload();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                MessageBox.Show($"ID {ID.ToString()} is not valid",
                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(response.StatusCode ==System.Net.HttpStatusCode.NotFound)
            {
                MessageBox.Show($"Person with ID {ID.ToString()} is not found",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
