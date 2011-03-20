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

namespace CtaTrainTracker.ViewModels
{
    public class route
    {
        public string Name { get; set; }
        public string RouteId { get; set; }
        public string Subtext { get; set; }
        public string Color
        {
            get
            {
                return "#00a1de";
            }
            set
            {
                Color = value;
            }
        }
    }
}
