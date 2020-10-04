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

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<Entities.User> users = new BindingList<Entities.User>();
        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource.Fullname; // label1
            button1.Text = Resource.Add; // button1
            button2.Text = Resource.Save;

            // listbox1
            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new Entities.User()
            {
                FullName = textBox1.Text
            };
            users.Add(u);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Txt Files (*.txt)|*.txt|Lua Files (*.lua)|*.lua|All Files (*.*)|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Stream s = sfd.OpenFile();
                using (StreamWriter sw = new StreamWriter(s))
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var user in users)
                    {
                        sb.Append(user.ID);
                        sb.Append(";");
                        sb.Append(user.FullName);
                        sb.Append(Environment.NewLine);
                    }
                    sw.WriteLine(sb.ToString());
                }
            }
        }
    }
}
