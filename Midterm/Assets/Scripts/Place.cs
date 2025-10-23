using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.GameEngine.Geometry;
using UnityEngine;

public class Place : MonoBehaviour
{
    [SerializeField] private PlaceData placeData;
    
    public ArcGISLocationComponent arcgisLocation;

    public GameObject locationObject;
    public int size;
    
    void Awake()
    {
        arcgisLocation = GetComponent<ArcGISLocationComponent>();
    }

    void Start()
    {
        //arcgisLocation.Position = new ArcGISPoint();
    }
    
    private void OnMouseDown()
    {
        Debug.Log("CLICK");
    }
}
