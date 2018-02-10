using GMap.NET;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System;

namespace WebTourist.Models
{
    static public class Helper
    {
        static public string ListLatLngToString(List<PointLatLng> route)
        {
            var stringBuilder = new StringBuilder();
            int count = 0;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            foreach (var coordinate in route)
            {
                if (count == 0)
                    stringBuilder.Append(coordinate.Lat + " " + coordinate.Lng);
                else
                    stringBuilder.Append("," + coordinate.Lat + " " + coordinate.Lng);
                count++;
            }
            return stringBuilder.ToString();
        }

        static public List<PointLatLng> StringToListLatLng(string route)
        {
            Boolean flag = true;
            string stringLat = String.Empty;
            string stringLon = String.Empty;
            string temp = String.Empty;

            List<PointLatLng> listResult = new List<PointLatLng>();
            foreach (var item in route)
            {
                if (!Char.IsLetter(item))
                {
                    if (item != ',' && item != '(' && item != ')' && item != ' ')
                    {
                        temp += item;
                    }
                    else
                    {
                        if (temp != String.Empty)
                        {
                            if (flag)
                            {
                                stringLat = temp;
                                flag = false;
                            }
                            else
                            {
                                stringLon = temp;
                                listResult.Add(new PointLatLng(Double.Parse(stringLat, CultureInfo.InvariantCulture),
                                    Double.Parse(stringLon, CultureInfo.InvariantCulture)));
                                flag = true;
                            }
                        }
                        temp = String.Empty;
                    }
                }
            }
            return listResult;
        }

        static public string DeleteLetterFromString(string str)
        {
            Boolean flagPunctuation = false;
            var strinBuilder = new StringBuilder();
            foreach (var item in str)
            {
                if (item == ',')
                    flagPunctuation = true;

                if (Char.IsDigit(item) || item == ',' || item == ' ' || item == '.')
                {
                    if (item == ' ' && flagPunctuation == true)
                    {
                        flagPunctuation = false;
                    }
                    else
                    {
                        strinBuilder.Append(item);
                    }
                }
            }

            string resultString = strinBuilder.ToString();
            if (resultString[0] == ' ')
                resultString = resultString.Remove(0, 1);

            return resultString;
        }

    }
}