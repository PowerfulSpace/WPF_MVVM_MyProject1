﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp4.Models
{
    internal class PlaceInfo
    {
        public string Name { get; set; }
        public Point  Location { get; set; }
        public IEquatable<ConfirmedCount> Counts { get; set; }
    }

}
