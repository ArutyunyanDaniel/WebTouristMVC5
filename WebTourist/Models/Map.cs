using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System;
using System.Threading.Tasks;

namespace WebTourist.Models
{
    static public class Map
    {
        static public  double GetRouteDistance(PointLatLng start, PointLatLng finish)
        {
            GDirections m_gDiractiaon = new GDirections();
            GMapProviders.GoogleMap.GetDirections(out m_gDiractiaon, start, finish, true, true, true, false, true);
            string distance = Helper.DeleteLetterFromString(m_gDiractiaon.Distance);
            return double.Parse(distance, CultureInfo.InvariantCulture);
        }

        static public List<PointLatLng> GetRoute(PointLatLng start, PointLatLng finish)
        {
            GDirections m_gDiractiaon = new GDirections();
            GMapProviders.GoogleMap.GetDirections(out m_gDiractiaon, start, finish, true, true, true, false, true);
            return m_gDiractiaon.Route;
        }
    }
}