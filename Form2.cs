using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private ArduinoData arduinoData;
        private int toolStatus = 0;
        private Form1 form1;
        private SerialPort myport;
        public Form2(ArduinoData data, Form1 parentForm)
        {
            InitializeComponent();
            arduinoData = data;
            form1 = parentForm;
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(myport_datarecieved); // Seri port veri alım olayını ekle
            serialPort1.Open(); // Seri portu aç


        }
        private void myport_datarecieved(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPort1.ReadLine().Trim(); // Seri porttan veriyi oku
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => ParseData(data))); // Ana thread'de ProcessData'yı çağır
                }
                else
                {
                    ParseData(data);
                }


            }
            catch (TimeoutException)
            {
                // Okuma sırasında zaman aşımı olursa buraya girer.
            }
        }
        public void ParseData(string data)
        {
            if (data.StartsWith("Switch States: "))
            {
                string[] pinData = data.Substring(15).Split(','); // "Switch States: " kısmını çıkar
                for (int i = 0; i < pinData.Length; i++)
                {
                    string[] pinState = pinData[i].Split(':');
                    string switchName = pinState[0].Trim(); // "switchX" kısmını al
                    int state = int.Parse(pinState[1].Trim());
                    arduinoData.PinStates[i] = state;
                }
            }
        }
            private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (toolStatus == 0)
            {
                if (arduinoData.PinStates[0] == 1)
                {
                    MessageBox.Show("You picked the wrong tool", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You picked right tool. Do you want another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {
                        
                        this.Hide(); // Mevcut formu gizle
                        form1.Show(); // Form2'yi modal olarak göster
                        
                    }
                }
            }
            else
            {
                if (arduinoData.PinStates[0] == 0)
                {
                    MessageBox.Show("You dropped the wrong place", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You dropped right place. Do you want to drop another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {
                        
                        this.Hide(); // Mevcut formu gizle
                        form1.Show(); // Form2'yi modal olarak göster
                        
                    }
                }
            }
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (toolStatus == 0)
            {
                if (arduinoData.PinStates[3] == 1)
                {
                    MessageBox.Show("You picked the wrong tool", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You picked right tool. Do you want another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {

                        this.Hide(); // Mevcut formu gizle
                        form1.Show(); // Form2'yi modal olarak göster
                        
                    }
                }
            }
            else
            {
                if (arduinoData.PinStates[3] == 0)
                {
                    MessageBox.Show("You dropped the wrong place", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You dropped right place. Do you want to drop another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {

                        this.Hide(); // Mevcut formu gizle
                        form1.Show(); // Form2'yi modal olarak göster
                        
                    }
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (toolStatus == 0)
            {
                if (arduinoData.PinStates[4] == 1)
                {
                    MessageBox.Show("You picked the wrong tool", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You picked right tool. Do you want another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {

                        this.Hide(); // Mevcut formu gizle
                        form1.Show(); // Form2'yi modal olarak göster
                        
                    }
                }
            }
            else
            {
                if (arduinoData.PinStates[4] == 0)
                {
                    MessageBox.Show("You dropped the wrong place", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You dropped right place. Do you want to drop another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {

                        this.Hide(); // Mevcut formu gizle
                        form1.Show(); // Form2'yi modal olarak göster
                        
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (toolStatus == 0)
            {
                if (arduinoData.PinStates[9] == 1)
                {
                    MessageBox.Show("You picked the wrong tool", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You picked right tool. Do you want another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {

                        this.Hide(); // Mevcut formu gizle
                        form1.Show();
                    }
                }
            }
            else
            {
                if (arduinoData.PinStates[9] == 0)
                {
                    MessageBox.Show("You dropped the wrong place", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You dropped right place. Do you want to drop another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {

                        this.Hide(); // Mevcut formu gizle
                        form1.Show(); // Form2'yi modal olarak göster
                        
                    }
                }
            }
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close(); // Form kapatılırken seri portu kapat
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
                if (checkBox1.Text == "Take the tool")
                {
                    checkBox1.Text = "Drop the tool";
                checkBox1.BackColor = Color.FromArgb(255, 128, 128);
                toolStatus = 1;
                }

                else if (checkBox1.Text == "Drop the tool")
                {


                checkBox1.Text = "Take the tool";
                checkBox1.BackColor = Color.FromArgb(192, 255, 192); ;
                toolStatus = 0;
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (toolStatus == 0)
            {
                if (arduinoData.PinStates[1] == 1)
                {
                    MessageBox.Show("You picked the wrong tool", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You picked right tool. Do you want another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {

                        this.Hide(); // Mevcut formu gizle
                        form1.Show(); // Form2'yi modal olarak göster
                        
                    }
                }
            }
            else
            {
                if (arduinoData.PinStates[1] == 0)
                {
                    MessageBox.Show("You dropped the wrong place", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You dropped right place. Do you want to drop another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {

                        this.Hide(); // Mevcut formu gizle
                        form1.Show(); // Form2'yi modal olarak göster
                        
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (toolStatus == 0)
            {
                if (arduinoData.PinStates[2] == 1)
                {
                    MessageBox.Show("You picked the wrong tool", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You picked right tool. Do you want another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {

                        this.Hide(); // Mevcut formu gizle
                        form1.Show(); // Form2'yi modal olarak göster
                        
                    }
                }
            }
            else
            {
                if (arduinoData.PinStates[2] == 0)
                {
                    MessageBox.Show("You dropped the wrong place", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You dropped right place. Do you want to drop another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {

                        this.Hide(); // Mevcut formu gizle
                        form1.Show(); // Form2'yi modal olarak göster
                        
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (toolStatus == 0)
            {
                if (arduinoData.PinStates[5] == 1)
                {
                    MessageBox.Show("You picked the wrong tool", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You picked right tool. Do you want another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {

                        this.Hide(); // Mevcut formu gizle
                        form1.Show(); // Form2'yi modal olarak göster
                        
                    }
                }
            }
            else
            {
                if (arduinoData.PinStates[5] == 0)
                {
                    MessageBox.Show("You dropped the wrong place", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You dropped right place. Do you want to drop another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {

                        this.Hide(); // Mevcut formu gizle
                        form1.Show(); // Form2'yi modal olarak göster
                        
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (toolStatus == 0)
            {
                if (arduinoData.PinStates[6] == 1)
                {
                    MessageBox.Show("You picked the wrong tool", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You picked right tool. Do you want another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {

                        this.Hide(); // Mevcut formu gizle
                        form1.Show(); // Form2'yi modal olarak göster
                        
                    }
                }
            }
            else
            {
                if (arduinoData.PinStates[6] == 0)
                {
                    MessageBox.Show("You dropped the wrong place", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You dropped right place. Do you want to drop another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {

                        this.Hide(); // Mevcut formu gizle
                        form1.Show(); // Form2'yi modal olarak göster
                        
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            //Control.CheckForIllegalCrossThreadCalls = false;
            this.Hide(); // Mevcut formu gizle

            form3.ShowDialog(); // Form2'yi modal olarak göster
            this.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (toolStatus == 0)
            {
                if (arduinoData.PinStates[8] == 1)
                {
                    MessageBox.Show("You picked the wrong tool", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You picked right tool. Do you want another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {

                        this.Hide(); // Mevcut formu gizle
                        form1.Show();
                    }
                }
            }
            else
            {
                if (arduinoData.PinStates[8] == 0)
                {
                    MessageBox.Show("You dropped the wrong place", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("You dropped right place. Do you want to drop another tool?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                    }
                    else
                    {

                        this.Hide(); // Mevcut formu gizle
                        form1.Show();
                    }
                }
            }
        }
    }
}
