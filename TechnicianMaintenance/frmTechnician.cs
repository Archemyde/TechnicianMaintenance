// Amber Holcomb-Stone
// Lab 7
// This lab incorporates the use of the TechSupport database and use of SQL Express.
// Code adapted and modified from: https://github.com/jameswillhoite/CITC2311

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechnicianMaintenance
{
    public partial class frmTechnicianMaintenance : Form
    {
        public frmTechnicianMaintenance()
        {
            InitializeComponent();
        }

        private void techniciansBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (!this.ValidateFields())
                return;

            try
            {
                this.Validate();
                this.techniciansBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.techSupportDataSet);
            }
            catch (NoNullAllowedException)
            {
                MessageBox.Show("Please fill in all of the fields");
                return;
            }
            catch (DBConcurrencyException)
            {
                MessageBox.Show("A concurrency error occurred. " +
                "Some rows were not updated.", "Concurrency Exception");
                this.techniciansTableAdapter.Fill(this.techSupportDataSet.Technicians);
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
                techniciansBindingSource.CancelEdit();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number +
                ": " + ex.Message, ex.GetType().ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("There was an error. Please close and try again.");
            }

            bindingNavigatorMovePreviousItem.Enabled = true;
            bindingNavigatorMoveNextItem.Enabled = true;
            bindingNavigatorMoveLastItem.Enabled = true;
            bindingNavigatorMoveFirstItem.Enabled = true;
            bindingNavigatorAddNewItem.Enabled = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'techSupportDataSet.Technicians' table. You can move, or remove it, as needed.
            this.techniciansTableAdapter.Fill(this.techSupportDataSet.Technicians);

        }

        protected bool ValidateFields()
        {

            // name 100
            if (nameTextBox.Text.Length > 100)
            {
                
                MessageBox.Show("The name you entered is too long. Please enter only up to 100 characters");
            }
            // email 50
            if (emailTextBox.Text.Length > 50)
            {
                MessageBox.Show("The email address you entered is too long. Please enter only up to 50 characters");
                return false;
            }
            // phone 20
            if (phoneTextBox.Text.Length > 20)
            {
                MessageBox.Show("The phone number you entered is too long. Please only enter up to 20 characters");
                return false;
            }

            return true;

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorMovePreviousItem.Enabled = false;
            bindingNavigatorMoveNextItem.Enabled = false;
            bindingNavigatorMoveLastItem.Enabled = false;
            bindingNavigatorMoveFirstItem.Enabled = false;
            bindingNavigatorAddNewItem.Enabled = false;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorMovePreviousItem.Enabled = false;
            bindingNavigatorMoveNextItem.Enabled = false;
            bindingNavigatorMoveLastItem.Enabled = false;
            bindingNavigatorMoveFirstItem.Enabled = false;
        }
    }
}
