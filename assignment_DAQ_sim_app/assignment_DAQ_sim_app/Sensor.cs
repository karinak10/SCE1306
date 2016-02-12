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
        //Attributes
        double dVal;
        int sId;
        Random rSensVal;

        /// <summary>
        /// Constructure create object of type sensor
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
        public double GetValue(int test)
        {
            if (test == 1)
            {
                dVal = rSensVal.NextDouble() * (5 - 0) + 0;
            }
            else if (test == 0)
            {
                dVal = rSensVal.Next(0, 2);
            }
            return dVal;

            
        }


        /// <summary>
        /// Returnerer sensor ID
        /// </summary>
        /// <returns></returns>
        public int GetSensId()
        {
            return sId;
        }

    }
}
