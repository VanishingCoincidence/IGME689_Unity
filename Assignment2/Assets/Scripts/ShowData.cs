using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.GameEngine.Geometry;
using Esri.HPFramework;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

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

            float minX = readCSV.covidCaseList.covidCase[i].latitude - 1f;
            float maxX = readCSV.covidCaseList.covidCase[i].latitude;
            float minY = readCSV.covidCaseList.covidCase[i].longitude - 1f;
            float maxY = readCSV.covidCaseList.covidCase[i].longitude;
            
        }
    }

    public void ShowConfirmedCases()
    {
        for(int i = 0; i < readCSV.covidCaseList.size; i++)
        {

        }
        
    }
}
