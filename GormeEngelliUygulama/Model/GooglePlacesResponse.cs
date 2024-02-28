using System;
using System.Collections.Generic;
using System.Text;

namespace GormeEngelliUygulama.Model
{
    public class GooglePlacesResponse
    {
        public List<PlaceResult> Results { get; set; }
        public string Status { get; set; }
    }
    public class PlaceResult
    {
        public string Name { get; set; }
        public string Vicinity { get; set; }
        public Geometry Geometry { get; set; }
    }
    public class Geometry
    {
        public Location Location { get; set; }
    }
    public class Location
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
