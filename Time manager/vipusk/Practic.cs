using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using Microsoft.VisualBasic;
namespace vipusk
{
    public partial class Practic : Form
    {
		bool isConnected = false;
		string recieveddata;
		string[] string1;
		string[][] data;
		int timeH=0;
		int timeM=0;
		int k;
		string status;
		string print;
		
		public Practic()
        {
            InitializeComponent();
        }

		private void button1_Click(object sender, EventArgs e)
		{
			comboBox1.Items.Clear();
			string[] portnames = SerialPort.GetPortNames();
			if (portnames.Length == 0)
			{
				MessageBox.Show("COM PORT not found");
			}
			foreach (string portName in portnames)
			{      
				comboBox1.Items.Add(portName);
				if (portnames[0] != null)
				{
					comboBox1.SelectedItem = portnames[0];
				}
			}
		}

		private void Practic_Load(object sender, EventArgs e)
		{
			dataGridView1.RowCount=8;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (!isConnected)
			{
				connect();
			}
			else
			{
				disconnect();
			}
		}

		void connect()
		{
			isConnected = true;
			string selectedPort = comboBox1.GetItemText(comboBox1.SelectedItem);
			serialPort1.PortName = selectedPort;
			serialPort1.BaudRate = 2000000;
			serialPort1.Open();
			button2.Text = "Disconnect";
		}

		void disconnect()
		{
			button2.Text = "Connect";
			isConnected = false;
			serialPort1.Close();

		}

		private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			recieveddata = serialPort1.ReadExisting();
			this.Invoke(new EventHandler(PrintData));
		}

		private void PrintData(object sender, EventArgs e)
		{
			string1 = recieveddata.Split('\n');
			data = new String[string1.Length][];
			dataGridView1.Rows.Clear();

			for (int i = 0; i < string1.Length; i++)
			{
				data[i] = string1[i].Split(' ');
			}
			for (int i = 0; i < string1.Length - 1; i++)
			{
				dataGridView1.Rows.Add(1);
				for (int j = 1; j < 3; j++)
				{
					timeH = Convert.ToInt32(data[i][j]) / 3600;
					timeM = (Convert.ToInt32(data[i][j]) % 3600) / 60;
					print = "" + timeH + "год " + timeM + "хв";
					dataGridView1.Rows[i].Cells[j].Value = print;
				}
			}

			for (int i = 0; i < string1.Length - 1; i++)
			{
				if (Convert.ToInt32(data[i][4]) == 1) dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.Lime;
				if (Convert.ToInt32(data[i][4]) == 0) dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.Gold;
				dataGridView1.Rows[i].Cells[0].Value = data[i][0];
			}
		}

		private void exit_Click(object sender, EventArgs e)
		{
			Form1 f1 = new Form1();
			this.Hide();
			f1.Show();
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			status = comboBox2.GetItemText(comboBox2.SelectedItem);
			filter();
		}

		void filter()
		{
			k = 0;
	
			dataGridView1.Rows.Clear();
			// dataGridView1.ColumnCount = 5;
			
			switch (status)
			{
				case "Закінчені":
					
					for (int i = 0; i< string1.Length - 1; i++)
					{
						if(Convert.ToInt32(data[i][4]) == 1)
							{
							k++;
							print ="";
							dataGridView1.Rows.Add(1);
							for (int j = 1; j < 3; j++)
								{
								
								timeH = Convert.ToInt32(data[i][j]) / 3600;
									timeM = (Convert.ToInt32(data[i][j]) % 3600) / 60;
									print = "" + timeH + "год " + timeM+"хв";
									dataGridView1.Rows[k-1].Cells[j].Value = print;
								}
							
							dataGridView1.Rows[k-1].Cells[0].Value = data[i][0];
						}
					}
					break;

				case "Незакінчені":
					
					for (int i = 0; i < string1.Length - 1; i++)
					{
						if (Convert.ToInt32(data[i][4]) == 0)
						{
							k++;
							print = "";
							dataGridView1.Rows.Add(1);
							for (int j = 1; j < 3; j++)
							{

								timeH = Convert.ToInt32(data[i][j]) / 3600;
								timeM = (Convert.ToInt32(data[i][j]) % 3600) / 60;
								print = "" + timeH + "год " + timeM + "хв";
								dataGridView1.Rows[k - 1].Cells[j].Value = print;
							}

							dataGridView1.Rows[k - 1].Cells[0].Value = data[i][0];
			
						}
					}
					break;

				case "Усі":
					for (int i = 0; i < string1.Length - 1; i++)
					{

						print = "";
							dataGridView1.Rows.Add(1);
						if (Convert.ToInt32(data[i][4]) == 1) dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.Lime;
						if (Convert.ToInt32(data[i][4]) == 0) dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.Gold;
						for (int j = 1; j < 3; j++)
							{

								timeH = Convert.ToInt32(data[i][j]) / 3600;
								timeM = (Convert.ToInt32(data[i][j]) % 3600) / 60;
							print = "" + timeH + "год " + timeM + "хв";
							dataGridView1.Rows[i].Cells[j].Value = print;
							}

							dataGridView1.Rows[i].Cells[0].Value = data[i][0];
					}
					break;
			}
				
		}

		private void button3_Click(object sender, EventArgs e)
		{
			string[] tasksOUT = new string[string1.Length];
			for (int i = 0; i < string1.Length-1; i++)
			{
				tasksOUT[i] = dataGridView1.Rows[i].Cells[0].Value.ToString();
				serialPort1.Write(tasksOUT[i]);
				System.Threading.Thread.Sleep(100);
			}
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
