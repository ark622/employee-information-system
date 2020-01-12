using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employees_Information_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnWrite_Click(object sender, EventArgs e) // Write button actions
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true}) // Opens the save file dialog box
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var sw = new StreamWriter(sfd.FileName))
                    {
                        using(var writer = new CsvWriter(sw))
                        {
                            var records = employeeBindingSource.DataSource as List<Employee>; // Store data written in GridView in a List
                            writer.WriteRecords(records);  // Write the list data in csv file
                        }
                        MessageBox.Show("Your data has been successfully saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show Confirmation

                    }
                }
            }
        }

        private void btnRead_Click(object sender, EventArgs e) // Read button actions
        {
            using(OpenFileDialog ofd = new OpenFileDialog() { Filter = "CSV|*.csv", ValidateNames = true}) // Show open file dialog box
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (var sr = new StreamReader(new FileStream(ofd.FileName, FileMode.Open)))
                    {
                        using (var csv = new CsvReader(sr))
                        {
                            employeeBindingSource.DataSource = csv.GetRecords<Employee>().ToList(); // Load data from csv file into GridView

                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            employeeBindingSource.DataSource = new List<Employee>(); // Initialize data source as a list of Employee class
        }

        private void btnExit_Click(object sender, EventArgs e) // Exit button action
        {
            Application.Exit();
        }
    }
}
