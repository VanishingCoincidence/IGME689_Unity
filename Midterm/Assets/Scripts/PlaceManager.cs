using Esri.GameEngine.Geometry;
using System.Collections.Generic;
using System.Linq;
using Esri.ArcGISMapsSDK.Components;
using UnityEngine;

public class PlaceManager : MonoBehaviour
{
    public ArcGISMapComponent arcgisMap;
    public GameObject placePrefab;
    public LineRenderer lineRenderer;
    
    private DataLoader dataLoader;
    public List<Place> places;
    public Transform spawnPosition;


    void Awake()
    {
        arcgisMap = FindFirstObjectByType<ArcGISMapComponent>();
        places = new List<Place>();
        dataLoader = new DataLoader();
        StartCoroutine(dataLoader.GetFeatures());
    }

    void Start()
    {
        SpawnPlaces();
        SpawnConnections();
    }

    private void SpawnPlaces()
    {
        foreach (var county in dataLoader.placeDataList)
        {
            Place place = Instantiate(placePrefab, spawnPosition).GetComponent<Place>();
            place.placeData = county;
            places.Add(place);
        }
    }
    
    private void SpawnConnections()
    {
        int connectionCount = 0;
        foreach (var county in dataLoader.placeDataList)
        {
            foreach (var connection in county.Connected_Places)
            {
                lineRenderer.SetPosition(connectionCount, new Vector3((float)connection.Latitude, (float)connection.Longitude));
                connectionCount++;
            }
        }
        
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
