using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemoDataSetTableGrid
{
    public partial class Form1 : Form
    {
        DataTable dtElements = new DataTable("Elements");

        public Form1()
        {
            InitializeComponent();
        }

        public void dtElementsCreateColumns()
        {
            dtElements.Clear();
            dtElements.Columns.Clear();

            DataColumn dc = new DataColumn("AtomicNumber", System.Type.GetType("System.Int32"));
            dc.Caption = "Атомний номер";
            dc.DefaultValue = 0;
            dtElements.Columns.Add(dc);

            dc = new DataColumn("Element", System.Type.GetType("System.String"));
            dc.DefaultValue = string.Empty;
            dc.Caption = "Елемент";
            dtElements.Columns.Add(dc);

            dc = new DataColumn("Symbol", System.Type.GetType("System.String"));
            dc.DefaultValue = string.Empty;
            dc.Caption = "Символ";
            dtElements.Columns.Add(dc);

            dc = new DataColumn("AtomicMass", System.Type.GetType("System.Decimal"));
            dc.DefaultValue = 0.0;
            dc.Caption = "Атомна маса";
            dtElements.Columns.Add(dc);

            dc = new DataColumn("DateSintez");
            dc.DataType = typeof(System.DateTime);
            dc.Caption = "Якась дата";
            dc.DefaultValue = DateTime.Now;
            dtElements.Columns.Add(dc);
        }

        public void dtElementsCreateRows()
        {
            DataRow dr;
            dr = dtElements.NewRow();
            dr["AtomicNumber"] = 1;
            dr["Element"] = "Hydrogen";
            dr["Symbol"] = "H";
            dr["AtomicMass"] = 1.0078;
            dr["DateSintez"] = DateTime.MaxValue;
            dtElements.Rows.Add(dr);
            try
            {
                LoadElementDataRow(dtElements, 5, "Lithium", "Li", 2.004, DateTime.Now);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private DataRow LoadElementDataRow(DataTable dt,
           int AtomicNbr, string Element,
        string Symbol, double AtomicMass, DateTime DateSintez)
        {
            DataRow dr;
            // Turns off event notifications,
            // index maintenance, and constraints
            // while loading data
            dt.BeginLoadData();
            // Add the row values to the rows collection and
            // return the DataRow. If the second
            // argument is set to true, then dt.AcceptChanges() is called
            //otherwise new rows are
            // marked as additions and changes to existing rows are marked as
            //modifications.
            try
            {
                dr = dt.LoadDataRow(new object[] { AtomicNbr, Element, Symbol, AtomicMass, DateSintez }
                 , false);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                dr = null;
            }
            // Turns on event notifications, index maintenance, and constraints
            // that were turned off
            // with the BeginLoadData() method
            dt.EndLoadData();
            return dr; // returns the DataRow filled
            // with the new values
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            dtElementsCreateColumns();
            dtElementsCreateRows();
            dataGridView1.DataSource = dtElements;
            //заголовок dataGridView
            foreach (DataColumn dc in dtElements.Columns)
            {
                dataGridView1.Columns[dc.Ordinal].HeaderText = dtElements.Columns[dc.Ordinal].Caption;
            }
            dataGridView1.Columns[0].Visible = false;
        }
    }
}
