using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.GameEngine.Geometry;
using UnityEngine;

public class Place : MonoBehaviour
{
    public PlaceData placeData;
    
    public ArcGISLocationComponent arcgisLocation;

    void Awake()
    {
        arcgisLocation = GetComponent<ArcGISLocationComponent>();
    }

    void Start()
    {
        arcgisLocation.Position = new ArcGISPoint(placeData.Longitude, placeData.Latitude, 0, ArcGISSpatialReference.WGS84());
    }
    
    private void OnMouseDown()
    {
        Debug.Log("CLICK");
    }
}
