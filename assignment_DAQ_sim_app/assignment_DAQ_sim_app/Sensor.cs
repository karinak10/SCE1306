using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment_DAQ_sim_app
{
    class Sensor
    {
        /// <summary>
        /// Attributes
        /// </summary>
        double dVal;
        int sId;
        Random rSensVal;

        /// <summary>
        /// Constructor: create object of type sensor
        /// </summary>
        /// <param name="id"></param>
        public Sensor(int id)
        {
            sId = id;
            rSensVal = new Random(id);
            dVal = 0.0F;
        }

        /// <summary>
        /// Get current analog sensor values
        /// </summary>
        /// <returns></returns>
        public double GetValue(int test)        //Making a test to use GetValue for analog and digital values
        {
            if (test == 1)
            {
                dVal = rSensVal.NextDouble() * (5 - 0) + 0;         //Analog
            }
            else if (test == 0)
            {
                dVal = rSensVal.Next(0, 2);                         //Digital
            }
            return dVal;

        }

        /// <summary>
        /// Return sensor ID
        /// </summary>
        /// <returns></returns>
        public int GetSensId()
        {
            return sId;
        }

    }
}
