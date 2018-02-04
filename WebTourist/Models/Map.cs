using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System;

namespace WebTourist.Models
{
    static public class Map
    {
        static public double GetRouteDistance(PointLatLng start, PointLatLng finish)
        {
            GDirections m_gDiractiaon = new GDirections();
            GMapProviders.GoogleMap.GetDirections(out m_gDiractiaon, start, finish, true, true, true, false, true);
            string distance = Helper.DeleteLetterFromPointString(m_gDiractiaon.Distance);
            return double.Parse(distance, CultureInfo.InvariantCulture);
        }

        static public List<PointLatLng> GetRoute(PointLatLng start, PointLatLng finish)
        {
            GDirections m_gDiractiaon = new GDirections();
            GMapProviders.GoogleMap.GetDirections(out m_gDiractiaon, start, finish, true, true, true, false, false);
            return m_gDiractiaon.Route;
        }

        static public PointLatLng FindShortestWay(PointLatLng userLocation, List<PointLatLng> pointsStartedWay)
        {
            double maxDistance = 1000000;
            double distance;
            var temp = new PointLatLng();
            foreach (var point in pointsStartedWay)
            {
                distance = GetRouteDistance(userLocation, point);
                if (distance < maxDistance)
                {
                    temp = point;
                    maxDistance = distance;
                }
            }
            return temp;
        }

    }
}