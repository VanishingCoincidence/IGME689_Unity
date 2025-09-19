using UnityEngine;
using UnityEngine.UI;
using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Geometry;
using Esri.HPFramework;
using Unity.Mathematics;

public class ShowData : MonoBehaviour
{
    public ReadCSV readCSV;
    private ArcGISMapComponent mapComponent;

    public Button confirmedCasesButton;

    public GameObject sphere;

    void Awake()
    {
        mapComponent = FindFirstObjectByType<ArcGISMapComponent>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnSpheres();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSpheres()
    {
        for (int i = 0; i < readCSV.covidCaseList.size; i++)
        {
            // chose 100 to be over top of the map
            //double3 position = new double3((double)readCSV.covidCaseList.covidCase[i].latitude, (double)readCSV.covidCaseList.covidCase[i].longitude, 100);
            //double3 worldPosition = math.inverse(mapComponent.WorldMatrix).HPTransform(position);
            //ArcGISPoint geoPosition = mapComponent.View.WorldToGeographic(worldPosition);
            //ArcGISPoint offsetPosition = new ArcGISPoint(geoPosition.X, geoPosition.Y, geoPosition.Z, geoPosition.SpatialReference);
            ArcGISPoint point = new ArcGISPoint(readCSV.covidCaseList.covidCase[i].longitude, readCSV.covidCaseList.covidCase[i].latitude, 500, ArcGISSpatialReference.WGS84());
            double3 position = mapComponent.View.GeographicToWorld(point);


            Instantiate(sphere, 
                new Vector3((float)position.x, (float)position.y, (float)position.z), 
                transform.rotation,
                transform);

            Debug.Log(readCSV.covidCaseList.covidCase[i].longitude + ","+
                readCSV.covidCaseList.covidCase[i].latitude);
        }
    }

    public void ShowConfirmedCases()
    {
        for(int i = 0; i < readCSV.covidCaseList.size; i++)
        {

        }
        
    }
}
