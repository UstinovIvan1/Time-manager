using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vipusk
{
    public partial class Theory : Form
    {
        public Theory()
        {
            InitializeComponent();
            TreeNode a = treeView1.Nodes.Add("Тайм менеджер");
            a.Tag = 0;
            TreeNode b = treeView1.Nodes.Add("Що таке тайм менеджмент?");
            b.ExpandAll();
            
            b.Nodes.Add("Загальна інформація").Tag = 1;
            b.Nodes.Add("Методики розподілення часу").Tag = 2;
            TreeNode c = treeView1.Nodes.Add("Планування робочого часу фахівця");
            c.ExpandAll();
            c.Tag = 3;
            TreeNode d = treeView1.Nodes.Add("Тайм-менеджер");
            d.ExpandAll();
            d.Tag = 11;
            d.Nodes.Add("Принцип роботи").Tag = 5;
            d.Nodes.Add("Інструкція використання").Tag = 6;
          
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (Convert.ToInt32(e.Node.Tag))
            {

                case 1:
                    richTextBox1.Text = System.IO.File.ReadAllText("2.1.txt");
                    break;
                case 2:
                    richTextBox1.Text = System.IO.File.ReadAllText("2.2.txt");
                    break;
                case 3:
                    richTextBox1.Text = System.IO.File.ReadAllText("3.txt");
                    break;

                case 5:
                    richTextBox1.Text = System.IO.File.ReadAllText("5.txt");
                    break;
                case 6:
                    richTextBox1.Text = System.IO.File.ReadAllText("6.txt");
                    break;

  
                 

            }
            }

		private void button1_Click(object sender, EventArgs e)
		{
			Form1 f1 = new Form1();
			this.Hide();
			f1.Show();
		}
	}
}
