using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursovaya
{
    public partial class Autorize : Form
    {
        List<Tuple<string, string, string, string>> data = new List<Tuple<string, string, string, string>>();
        string line;
        public Autorize()
        {
            dataInitialize();
            InitializeComponent();
        }
        public void dataInitialize()
        {
            data = (from line in File.ReadLines(@"pass.txt")
                    let values = line.Split(' ')
                    select Tuple.Create(values[0],
                                        values[1],values[2],values[3])).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Tuple<string, string, string, string> m_data in data)
            {
                if (this.textBox1.Text == m_data.Item1 
                    && this.textBox2.Text == m_data.Item2)
                {
                    line += m_data.Item3 + m_data.Item4;
                    var frm = new Form1();
                        frm.Show();
                        this.Hide();
                    
                        break;
                }
                else this.label3.Text = "Incorrect login or/and password";
            }
        }
        
        public string TheValue
        {
            get { return line;}
        }
    }
}
