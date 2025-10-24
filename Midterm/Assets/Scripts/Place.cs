using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.GameEngine.Geometry;
using UnityEngine;

public class Place : MonoBehaviour
{
    public PlaceData placeData;
    public ArcGISLocationComponent arcgisLocation;
    
    private Renderer renderer;
    public Material hoverMaterial;
    public Material defaultMaterial;

    void Awake()
    {
        arcgisLocation = GetComponent<ArcGISLocationComponent>();
    }

    void Start()
    {
        arcgisLocation.Position = new ArcGISPoint(placeData.Longitude, placeData.Latitude, 0, ArcGISSpatialReference.WGS84());
        renderer = GetComponent<Renderer>();
        renderer.material = defaultMaterial;
    }

    private void OnMouseEnter()
    {
        renderer.material = hoverMaterial;
    }

    private void OnMouseExit()
    {
        renderer.material = defaultMaterial;
    }
}
