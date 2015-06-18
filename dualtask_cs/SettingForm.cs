using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dualtask_cs
{
    public partial class SettingForm : Form
    {

        public SettingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.Text == "" || comboBox2.Text == ""){
                return;
            }
            
            Experiment.Instance.Show();
            this.AddOwnedForm(Experiment.Instance);
            Experiment.Instance.interval_max = Convert.ToInt32(this.textBox1.Text) * 1000;
            Experiment.Instance.interval_min = Convert.ToInt32(this.textBox2.Text) * 1000;
            Experiment.Instance.appear_time = Convert.ToInt32(this.textBox3.Text) * 1000;
            Experiment.Instance.number_max = Convert.ToInt32(this.comboBox2.Text);
            Experiment.Instance.number_min = Convert.ToInt32(this.comboBox1.Text);            
            Experiment.Instance.Ex_Initialize();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
