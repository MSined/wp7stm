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
using System.Collections.Generic;

namespace TransitMTL
{
    public class BusStopData
    {
        String busNumber;
        String stopNumber;
        String direction;
        List<String> times;

        public BusStopData(String busNumber, String stopNumber, String direction, List<String> times)
        {
            this.busNumber = busNumber;
            this.stopNumber = stopNumber;
            this.direction = direction;
            this.times = times;
        }

        public String getBusNumber()
        {
            return busNumber;
        }

        public String getStopNumber()
        {
            return stopNumber;
        }

        public String getDirection()
        {
            return direction;
        }

        public List<String> getTimes()
        {
            return times;
        }
    }
}
