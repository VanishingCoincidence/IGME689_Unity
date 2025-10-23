using System;
using System.Collections.Generic;


public class PlaceData
{
    public string State;
    public string County;
    public double Longitude;
    public double Latitude;
    public float Confirmed;
    public float Deaths;
    public float Incident_Rate;
    public List<PlaceData> Connected_Places;
    
    public PlaceData(string state, string county, double longitude, double latitude, float confirmed, float deaths, float incidentRate)
    {
        
        State = state;
        County = county;
        Longitude = longitude;
        Latitude = latitude;
        Confirmed = confirmed;
        Deaths = deaths;
        Incident_Rate = incidentRate;
        
        //Connected_Places = new List<Place>();
    }
    
    /*public static void ConnectPlaces(Place a, Place b)
    {
        // see if the two places are close enough to one another
        double x = b.Latitude - a.Latitude;
        x = Math.Pow(x, 2);
        
        double y = b.Longitude - a.Longitude;
        y = Math.Pow(y, 2);

        double distance = Math.Sqrt(x + y);
        distance = Math.Abs(distance);

        if (distance < 100)
        {
            a.Connected_Places.Add(b);
            b.Connected_Places.Add(a);
        }
    }

    public bool IsConnected(Place otherPlace)
    {
        return Connected_Places.Contains(otherPlace);
    }*/
    
}
