using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TransitMTL
{
    public class Bus
    {
        string busNumber;
        string busName;
        public Bus(string busNumber, string busName)
        {
            this.busNumber = busNumber;
            this.busName = busName;
        }

        public string getBusNumber()
        {
            return busNumber;
        }

        public string getBusName()
        {
            return busName;
        }
    }
}
