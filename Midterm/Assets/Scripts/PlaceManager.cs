using Esri.GameEngine.Geometry;
using System.Collections.Generic;
using System.Linq;
using Esri.ArcGISMapsSDK.Components;
using UnityEngine;

public class PlaceManager : MonoBehaviour
{
    public ArcGISMapComponent arcgisMap;
    
    private DataLoader dataLoader;
    public List<Place> places;


    void Awake()
    {
        arcgisMap = FindFirstObjectByType<ArcGISMapComponent>();
        places = new List<Place>();
        dataLoader = new DataLoader();
        StartCoroutine(dataLoader.GetFeatures());
    }

    void Start()
    {
        
    }

    private void SpawnPlaces()
    {
        
    }
    
    private void SpawnConnections()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
