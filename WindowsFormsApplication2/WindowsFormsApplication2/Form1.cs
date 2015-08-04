using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.IO.Ports;
using MelsecPLC;


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private int counter = 0;
       private List<string> linije = File.ReadLines("C:\\Users\\Korisnik\\Documents\\visual studio 2012\\Projects\\WindowsFormsApplication2\\WindowsFormsApplication2\\tekst.txt").ToList();
       private int brojac = 0;
        public MelsecPLC.Winsock winsock1;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (winsock1.GetState.ToString() != "Connected")
                {

                    winsock1Connect();

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            /*DateTime trenutno = DateTime.Now;
            string tren = trenutno.ToString("HH:mm:ss");
            string sat = tren.Substring(0, 2);
            string minuta = tren.Substring(3, 2);
            string sekunda = tren.Substring(6, 2);

            System.IO.StreamWriter tekst = new System.IO.StreamWriter("C:\\Users\\Korisnik\\Documents\\visual studio 2012\\Projects\\WindowsFormsApplication2\\WindowsFormsApplication2\\tekst.txt", true);
            List<string> linije = File.ReadLines("C:\\Users\\Korisnik\\Documents\\visual studio 2012\\Projects\\WindowsFormsApplication2\\WindowsFormsApplication2\\tekst.txt").ToList();

            foreach (string lajn in linije) {
                string[] parametri = lajn.Split(' ');
                if (Convert.ToInt32(parametri[0]) < Convert.ToInt32(sat) && Convert.ToInt32(parametri[1]) > Convert.ToInt32(sat)) {
                    label2.Text = parametri[2];
                }
            }
               */ 
           
            
            

            /* winsock1Connect();
            winsock1.DataArrival += new MelsecPLC.Winsock.DataArrivalEventHandler(winsock1_DataArrival);
            timer1.Enabled = true;
            timer1.Start();*/
           
        }

        private void winsock1Connect()
        {
            try
            {
                if (winsock1.GetState.ToString() != "Connected")
                {

                    winsock1.LocalPort = 1027;

                    winsock1.RemoteIP = textBox1.Text.Trim();
                    int a = 8000;

                    winsock1.RemotePort = 8000;

                    winsock1.Connect();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (winsock1.GetState.ToString() != "Connected")
            {
                winsock1Connect();
            }
            //String cmd = "500000FF03FF000018000A04010000D*0095000001";
            String cmd = "";
            String OutAddress = "0001";
            cmd = "";
            cmd = cmd + "5000";// sub HEAD (NOT)
            cmd = cmd + "00";//   network number (NOT)
            cmd = cmd + "FF";//PLC NUMBER
            cmd = cmd + "03FF";// DEMAND OBJECT MUDULE I/O NUMBER
            cmd = cmd + "00";//  DEMAND OBJECT MUDULE DEVICE NUMBER
            cmd = cmd + "001C";//  Length of demand data
            cmd = cmd + "000A";//  CPU inspector data
            cmd = cmd + "0401";//  Read command
            cmd = cmd + "0000";//  Sub command
            cmd = cmd + "D*";//   device code
            cmd = cmd + "009500"; //adBase 
            cmd = cmd + OutAddress;  //BASE ADDRESS           
            winsock1.Send(cmd);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*if (winsock1.GetState.ToString() != "Connected")
            {
                winsock1Connect();
            }
            String cmd = "";
            String OutAddress = txtWrite.Text.Trim();
            cmd = "";
            cmd = cmd + "5000";// sub HEAD (NOT)
            cmd = cmd + "00";//   network number (NOT)
            cmd = cmd + "FF";//PLC NUMBER
            cmd = cmd + "03FF";// DEMAND OBJECT MUDULE I/O NUMBER
            cmd = cmd + "00";//  DEMAND OBJECT MUDULE DEVICE NUMBER
            cmd = cmd + "001C";//  Length of demand data
            cmd = cmd + "000A";//  CPU inspector data
            cmd = cmd + "1401";//  Write command
            cmd = cmd + "0000";//  Sub command
            cmd = cmd + "D*";//   device code
            cmd = cmd + "009501"; //adBase 
            cmd = cmd + OutAddress;  //BASE ADDRESS
            winsock1.Send(cmd);
        }*/

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {


            string[] parametri = linije[0].Split(' ');
            label2.Text = parametri[1];
            timer1.Start();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string[] parametri = linije[brojac].Split(' ');
            label3.Text = (counter+1).ToString();
            counter++;
            if (brojac + 1 < linije.Count)
            {
                if (counter == Convert.ToInt32(parametri[0]))
                {
                    brojac++;
                    counter = 0;
                    string[] parametri1 = linije[brojac].Split(' ');

                    label2.Text = parametri1[1];
                }
            }
            else
            {
                timer1.Stop();

                MessageBox.Show("Nema više linija");
            }

        }
    }
}
