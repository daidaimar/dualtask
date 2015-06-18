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
    public  sealed partial class Experiment : Form
    {
        public int interval_max;
        public int interval_min;
        public int appear_time;
        public int number_max;
        public int number_min;

        public int stimulus_id;
        public String res_time;
        public char res_char;
        public String res_number;
        public String res_IsCorrect;

        System.Random ran = new System.Random();
        System.Diagnostics.Stopwatch sw1 = new System.Diagnostics.Stopwatch();// sw1は実験全体の時間の計測
        System.Diagnostics.Stopwatch sw2 = new System.Diagnostics.Stopwatch();// sw2は反応時間の計測
        System.IO.StreamWriter swriter;
        public bool Is_appear_num;// trueのときだけキー入力を受け取るようにする

        private Experiment()
        {
            InitializeComponent();
        }

        public void Ex_Initialize(){

            InitializeTimer();
            hide_number();
            stimulus_id = 1;
            string folderPath = System.IO.Directory.GetCurrentDirectory();
            sw1.Start();
            swriter = new System.IO.StreamWriter(folderPath + @"\data.csv", true, System.Text.Encoding.GetEncoding("shift_jis"));
            swriter.WriteLine("刺激No.,提示された時間(秒),刺激の内容,反応時間(ミリ秒),応答内容,正誤");

            this.WindowState = FormWindowState.Maximized;
        }

        private static Experiment _instance = new Experiment();

        public static Experiment Instance
        {
            get
            {
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            swriter.Close();
            timer1.Enabled = false;
            timer1.Dispose();
            timer2.Enabled = false;
            timer2.Dispose();
            sw1.Stop();
            sw1.Reset();
            sw2.Stop();
            sw2.Reset();
            this.Hide();
        }

        public void InitializeTimer()
        {
            timer1.Interval = ran.Next(interval_min, interval_max);
            timer1.Enabled = true;

            timer2.Interval = appear_time;
        }

        // timer1は刺激の表示を管理
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = ran.Next(interval_min, interval_max);
            show_number();
            sw2.Start();

            timer2.Enabled = true;

            Console.WriteLine("sw1 : " + sw1.ElapsedMilliseconds / 1000);
        }

        private void Experiment_KeyDown(object sender, KeyEventArgs e)
        {
            if (Is_appear_num)
            {
                Console.WriteLine(e.KeyCode);
                disappear_number(e);
            }
        }

        // timer2は刺激の消去を管理
        private void timer2_Tick(object sender, EventArgs e)
        {
            disappear_number();
        }

        private void disappear_number(KeyEventArgs e = null)
        {
            sw2.Stop();
            res_time = Convert.ToString(sw2.ElapsedMilliseconds);
            hide_number();
            timer2.Enabled = false;

            

            if (e != null)
            {
                res_char = (Convert.ToChar(e.KeyCode));
                res_number = res_char.ToString();
                
            }
            else
            {
                res_number = "なし";
                res_time = "";
            }
            
            Console.WriteLine("sw2 : " + res_time);
            res_IsCorrect = label1.Text == res_number ? "正" : "誤" ;
            Console.WriteLine("応答内容 : " + res_number);

            swriter.WriteLine(stimulus_id + "," + sw1.ElapsedMilliseconds / 1000 + "," + label1.Text + "," + res_time + "," + res_number + "," + res_IsCorrect);
            stimulus_id++;
            sw2.Reset();
        }

        private void show_number()
        {
            Is_appear_num = true;
            this.label1.Text = Convert.ToString(ran.Next(number_min, number_max));
            this.label1.Show();
        }

        private void hide_number()
        {
            Is_appear_num = false;
            this.label1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
