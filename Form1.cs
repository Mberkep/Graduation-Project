using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private SerialPort myport;
        public ArduinoData arduinoData = new ArduinoData();
        public event EventHandler<string> DataReceived; // Veri alımı için event tanımı

        public Form1()
        {
            InitializeComponent();
             // COM port ve baud rate ayarı
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(myport_datarecieved); // Seri port veri alım olayını ekle
            serialPort1.Open(); // Seri portu aç
        }

        private void myport_datarecieved(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPort1.ReadLine().Trim(); // Seri porttan veriyi oku
                //this.Invoke(new Action(() => ParseData(data))); // Ana thread'de ParseData'yı çağır
                //DataReceived?.Invoke(this, data); // DataReceived olayını tetikleyin

                if (data == "Unknown UID")
                {
                    MessageBox.Show("Please try again", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (data == "Berke")
                {
                    MessageBox.Show("Welcome Berke", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Kullanıcı 'OK' butonuna bastığında Form2'yi aç
                    serialPort1.Close();
                    Form2 form2 = new Form2(arduinoData, this);
                    Control.CheckForIllegalCrossThreadCalls = false;
                    this.Hide(); // Mevcut formu gizle
                    
                    form2.ShowDialog(); // Form2'yi modal olarak göster
                    this.Show(); // Form2 kapatıldığında Form1'i tekrar göster
                }
                else if (data == "Mete")
                {
                    MessageBox.Show("Welcome Mete", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Kullanıcı 'OK' butonuna bastığında Form2'yi aç
                    serialPort1.Close();
                    Form2 form2 = new Form2(arduinoData, this);
                    Control.CheckForIllegalCrossThreadCalls = false;
                    this.Hide(); // Mevcut formu gizle
                    
                    form2.ShowDialog(); // Form2'yi modal olarak göster
                    this.Show(); // Form2 kapatıldığında Form1'i tekrar göster
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close(); // Form kapatılırken seri portu kapat
            }
        }
    }
}
