using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;
using Dapper;

namespace Devops_Security_CaseStudy4
{
    public partial class Form1 : Form
    {
        private string dbFilePath = "db.db";
        private string connectionString;

        public Form1()
        {
            InitializeComponent();
            connectionString = $"Data Source={dbFilePath};Version=3;";
        }

        private void ExecuteQueryAndDisplayResults(string query)
        {
            using (IDbConnection dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();

                var results = dbConnection.Query(query);

                DataTable dt = ConvertToDataTable(results);
                dataGridView1.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT voornaam, achternaam,number FROM members WHERE status IN (SELECT statusId FROM status WHERE status = 'Student')";
            ExecuteQueryAndDisplayResults(query);
            alumni.Visible = false;
            teachers.Visible = false;
            students.Visible = true;
            antwerpen.Visible = false;
            westvlaanderen.Visible = false;
            oostvlaanderen.Visible=false;
            limburg.Visible = false;
            vlaamsbrabant.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT voornaam, achternaam,number FROM members WHERE status IN (SELECT statusId FROM status WHERE status = 'Alumni')";
            ExecuteQueryAndDisplayResults(query);
            students.Visible = false;
            teachers.Visible = false;
            alumni.Visible = true;
            antwerpen.Visible = false;
            westvlaanderen.Visible = false;
            oostvlaanderen.Visible = false;
            limburg.Visible = false;
            vlaamsbrabant.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "SELECT voornaam, achternaam,number FROM members WHERE status IN (SELECT statusId FROM status WHERE status = 'Teacher')";
            ExecuteQueryAndDisplayResults(query);
            students.Visible = false;
            alumni.Visible = false;
            teachers.Visible = true;
            antwerpen.Visible = false;
            westvlaanderen.Visible = false;
            oostvlaanderen.Visible = false;
            limburg.Visible = false;
            vlaamsbrabant.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "SELECT voornaam, achternaam,number FROM members WHERE provincie IN (SELECT provincieId FROM provincie WHERE provincie = 'Antwerpen')";
            ExecuteQueryAndDisplayResults(query);
            students.Visible = false;
            alumni.Visible = false;
            teachers.Visible = false;
            antwerpen.Visible = true;
            westvlaanderen.Visible = false;
            oostvlaanderen.Visible = false;
            limburg.Visible = false;
            vlaamsbrabant.Visible = false;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            string query = "SELECT voornaam, achternaam,number FROM members WHERE provincie IN (SELECT provincieId FROM provincie WHERE provincie = 'Oost-Vlaanderen')";
            ExecuteQueryAndDisplayResults(query);
            students.Visible = false;
            alumni.Visible = false;
            teachers.Visible = false;
            antwerpen.Visible = false;
            westvlaanderen.Visible = false;
            oostvlaanderen.Visible = true;
            limburg.Visible = false;
            vlaamsbrabant.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = "SELECT voornaam, achternaam,number FROM members WHERE provincie IN (SELECT provincieId FROM provincie WHERE provincie = 'West-Vlaanderen')";
            ExecuteQueryAndDisplayResults(query);
            students.Visible = false;
            alumni.Visible = false;
            teachers.Visible = false;
            antwerpen.Visible = false;
            westvlaanderen.Visible = true;
            oostvlaanderen.Visible = false;
            limburg.Visible = false;
            vlaamsbrabant.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string query = "SELECT voornaam, achternaam,number FROM members WHERE provincie IN (SELECT provincieId FROM provincie WHERE provincie = 'Limburg')";
            ExecuteQueryAndDisplayResults(query);
            students.Visible = false;
            alumni.Visible = false;
            teachers.Visible = false;
            antwerpen.Visible = false;
            westvlaanderen.Visible = false;
            oostvlaanderen.Visible = false;
            limburg.Visible = true;
            vlaamsbrabant.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string query = "SELECT voornaam, achternaam,number FROM members WHERE provincie IN (SELECT provincieId FROM provincie WHERE provincie = 'Vlaams-Brabant')";
            ExecuteQueryAndDisplayResults(query);
            students.Visible = false;
            alumni.Visible = false;
            teachers.Visible = false;
            antwerpen.Visible = false;
            westvlaanderen.Visible = false;
            oostvlaanderen.Visible = false;
            limburg.Visible = false;
            vlaamsbrabant.Visible = true;
        }
        private DataTable ConvertToDataTable(IEnumerable<dynamic> data)
        {
            DataTable table = new DataTable();

            if (data != null && data.Any())
            {
                var firstItem = (IDictionary<string, object>)data.First();

                foreach (var property in firstItem.Keys)
                {
                    table.Columns.Add(property, firstItem[property].GetType());
                }

                foreach (var item in data)
                {
                    DataRow row = table.NewRow();
                    var dictionaryItem = (IDictionary<string, object>)item;

                    foreach (var property in dictionaryItem.Keys)
                    {
                        row[property] = dictionaryItem[property];
                    }

                    table.Rows.Add(row);
                }
            }

            return table;
        }

    }
}
