using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using HtmlAgilityPack;

namespace TransitMTL
{
    public class Stop
    {
        string stopNumber;
        string corner;
        string busNumber;
        List<string> times;

        public Stop(string stopNumber, string corner, string busNumber)
        {
            this.stopNumber = stopNumber;
            this.corner = corner;
            this.busNumber = busNumber;
            times = new List<string>();
        }

        public string getStopNumber()
        {
            return stopNumber;
        }

        public string getCorner()
        {
            return corner;
        }

        public string getBusNumber()
        {
            return busNumber;
        }

        public List<string> getTimes()
        {
            return times;
        }

        public void setTimes(List<string> times)
        {
            this.times = times;
        }
    }
}
