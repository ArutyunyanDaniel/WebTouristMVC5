﻿using GMap.NET;
using GMap.NET.MapProviders;
using System.Collections.Generic;
using System.Globalization;

namespace WebTourist.Models
{
    static public class Map
    {
        static Map()
        {
            m_gDiractiaon = new GDirections();
        }

        static public double GetRouteDistance(PointLatLng start, PointLatLng finish)
        {
            DirectionsStatusCode statucCode = DirectionsStatusCode.NOT_FOUND;
            while (statucCode != DirectionsStatusCode.OK)
                statucCode = GMapProviders.GoogleMap.GetDirections(out m_gDiractiaon, start, finish, true, true, false, false, true);

            string distance = Helper.DeleteLetterFromString(m_gDiractiaon.Distance);
            return double.Parse(distance, CultureInfo.InvariantCulture);
        }


        static public GDirections GetDiraction(PointLatLng start, PointLatLng finish)
        {
            DirectionsStatusCode statucCode = DirectionsStatusCode.NOT_FOUND;
            while (statucCode != DirectionsStatusCode.OK)
                statucCode = GMapProviders.GoogleMap.GetDirections(out m_gDiractiaon, start, finish, true, true, false, false, true);
         
            return m_gDiractiaon;
        }

        static private GDirections m_gDiractiaon;
    }
}