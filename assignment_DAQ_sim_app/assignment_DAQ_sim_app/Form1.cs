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
        private Sensor[] mySensors;
        private Sensor[] digitalSensors;
        private double timeLeft;
        private double timeLeftLogging;
        private string logData;
        private string fileName = "karina.txt";
        private int writeCount = 0;
        public Form1()
        {
            InitializeComponent();

            label3.Text = "Filename: " + fileName;
            label5.Text = "Times write: " + writeCount.ToString();

            mySensors = new Sensor[7];

            for (int i = 0; i < mySensors.Length; i++)
            {
                mySensors[i] = new Sensor(i);
            }

            digitalSensors = new Sensor[3];
            for (int j = 0; j < digitalSensors.Length; j++)
            {
                digitalSensors[j] = new Sensor(j);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            int d = 0;
            int a = 1;
            logData = null;

            //Analog values
            for (int i = 0; i < mySensors.Length; i++)
            {
                double fValue = mySensors[i].GetValue(a);
                string value =  mySensors[i].GetSensId().ToString() + "\t" + fValue.ToString("F3") +"\t"+  DateTime.Now.ToString();
                listBox1.Items.Add(value);
                logData += value + "\r\n";
            }

            //Digital values
            for (int j = 0; j < digitalSensors.Length; j++)
            {
                double diValue = digitalSensors[j].GetValue(d);
                string digistring =  digitalSensors[j].GetSensId().ToString() + "\t" + diValue.ToString("F0");
                listBox2.Items.Add(digistring);
            }

            timeLeft = 10;
            tmrSample.Start();
            button1.Enabled = false;


        }

        private void tmrSample_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                // Display the new time left
                // by updating the Time Left label.
                timeLeft = timeLeft - 1;
                textBox1.Text = timeLeft/10 + " seconds";
            }

            else
            {
                button1.Enabled = true;
            }


        }

        private void tmrLogging_Tick(object sender, EventArgs e)
        {
            {
                if (timeLeftLogging > 0)
                {
                    // Display the new time left
                    // by updating the Time Left label.
                    timeLeftLogging = timeLeftLogging - 1;
                    textBox2.Text = timeLeftLogging/10 + " seconds";
                }

                else
                {
                    button2.Enabled = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            writeToFile();
            timeLeftLogging = 10;
            tmrLogging.Start();
            button2.Enabled = false;
            writeCount++;
            label5.Text = "Times write: " + writeCount.ToString();

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is help box", "Help");
        }
        private void writeToFile()
        {
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.Write(logData);
            }
        }

    }
}
