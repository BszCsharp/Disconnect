using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Disconnect
{
    public partial class Form1 : Form
    {
        DataSet dsBestellung = null;
        OleDbConnection con;
        OleDbDataAdapter adKunde = null;
        OleDbDataAdapter adBestellung = null;
        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            con = new OleDbConnection(Properties.Settings.Default.ConString);
            con.Open();
            dsBestellung = new DataSet();
            adKunde = new OleDbDataAdapter("select * from tKunde", con);
            adKunde.MissingSchemaAction = MissingSchemaAction.Add;
            adBestellung = new OleDbDataAdapter("select * From tBestellung", con);
            adBestellung.MissingSchemaAction = MissingSchemaAction.Add;
        }

        private void buttonFillDataSet_Click(object sender, EventArgs e)
        {
            adKunde.FillSchema(dsBestellung, SchemaType.Source, "Kunde");
            adKunde.Fill(dsBestellung, "Kunde");
            adBestellung.FillSchema(dsBestellung, SchemaType.Source, "Bestellung");
            adBestellung.Fill(dsBestellung, "Bestellung");
            dataGridView1.DataSource = dsBestellung.Tables["Kunde"];
            
        }

        private void buttonSerialize_Click(object sender, EventArgs e)
        {
            dsBestellung.WriteXmlSchema("Bestellung3.xsd");
            dsBestellung.WriteXml("Bestellung.xml");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dsBestellung.ReadXmlSchema("Bestellung3.xsd");
            dsBestellung.ReadXml("Bestellung.xml");

            dataGridView1.DataSource = dsBestellung.Tables["Kunde"];

        }
    }
}
