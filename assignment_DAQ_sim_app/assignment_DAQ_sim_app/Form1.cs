using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace assignment_DAQ_sim_app
{

public partial class Form1 : Form
    {
        //Declare attribute
        private Sensor[] analogSensors;
        private Sensor[] digitalSensors;
        private double timeLeftSampling;
        private double timeLeftLogging;
        private string logData;
        private string fileName = "DAQ_sampling.txt";
        private int writeCount = 0;
        public Form1()
        {
            InitializeComponent();

            label1.Text = "Current time: " + DateTime.Now.ToString();
            label3.Text = "Filename: " + fileName;                  //Prints out the fileName to the label on top of application
            label5.Text = "Times write: " + writeCount.ToString();  //Count how many times the Sampling button is pushed during a session

            analogSensors = new Sensor[7];                          //Initialize new variable array, with 7 elements

            for (int i = 0; i < analogSensors.Length; i++)
            {
                analogSensors[i] = new Sensor(i);
            }

            digitalSensors = new Sensor[3];                         //Initialize new variable array, with 3 elements
            for (int j = 0; j < digitalSensors.Length; j++)
            {
                digitalSensors[j] = new Sensor(j);
            }

        }

        private void button1_Click(object sender, EventArgs e)      //When klikking the sampling button
        {
            //int test value, test==0: GetValue return random analog values, test==1: GetValue return random digital values 
            int d = 0;
            int a = 1;
            logData = null;

            //Analog values
            for (int i = 0; i < analogSensors.Length; i++)
            {
                double fAnValue = analogSensors[i].GetValue(a);

                //Resolution
                double t = fAnValue - Math.Floor(fAnValue);                 //Checking the decimal precision in the random analog value
                if (t >= 0.3F && t <= 0.7F)
                {
                    fAnValue = Math.Floor(fAnValue) + 0.5F;                 //Round to nearest half
                }
                else if (t > 0.7F)
                    fAnValue = Math.Ceiling(fAnValue);                      //Round to neares full
                else
                fAnValue = Math.Floor(fAnValue);


                string sAnString =  "A_iD_" + analogSensors[i].GetSensId().ToString() + "\t" + fAnValue.ToString("F2") + "\t" +  DateTime.Now.ToString();
                listBox1.Items.Add(sAnString);
                logData += sAnString + "\r\n";
            }

            //Digital values
            for (int j = 0; j < digitalSensors.Length; j++)
            {
                double fDiValue = digitalSensors[j].GetValue(d);
                string sDiString =  "D_iD_" + digitalSensors[j].GetSensId().ToString() + "\t" + fDiValue.ToString("F0") + "\t" + DateTime.Now.ToString();
                listBox1.Items.Add(sDiString);
                logData += sDiString + "\r\n";
            }

            timeLeftSampling = 59;
            tmrSample.Start();
            button1.Enabled = false;


        }
        //Sampling countdown
        private void tmrSample_Tick(object sender, EventArgs e)         
        {
            if (timeLeftSampling > 0)
            {

                timeLeftSampling = timeLeftSampling - 1;
                textBox1.Text = timeLeftSampling/10 + " seconds";
            }

            else
            {
                button1.Enabled = true;
            }

        }


        //Logging button
        private void button2_Click(object sender, EventArgs e)      
        {
            writeToFile();
            timeLeftLogging = 470;
            tmrLogging.Start();
            button2.Enabled = false;
            writeCount++;
            label5.Text = "Times write: " + writeCount.ToString();          //Updating how many times the logging button has been pushed/program written to file

        }

        //Logging countdown
        private void tmrLogging_Tick(object sender, EventArgs e)
        {
            {
                if (timeLeftLogging > 0)
                {
                    timeLeftLogging = timeLeftLogging - 1;                  //While timeleftlogging is over 0, it will reduce one for each iteration
                    textBox2.Text = timeLeftLogging / 10 + " seconds";      //The textbox is showing the time left and a string "seconds"
                }

                else
                {
                    button2.Enabled = true;                         //The button is available to be pushed when 47 seconds has passed
                }
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a help box", "Help");
        }

        //Writing the measurements to a file
        private void writeToFile()
        {
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.Write(logData);
            }
        }

        private void clear_Txt_Click(object sender, EventArgs e)            //Push the button if you want to clear the text in the listbox
        {
            listBox1.Items.Clear();
        }

    }
}
