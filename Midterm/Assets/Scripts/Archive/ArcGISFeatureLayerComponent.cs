// Copyright 2025 Esri.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at: http://www.apache.org/licenses/LICENSE-2.0
//

using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.GameEngine.Geometry;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.Splines;

public class ArcGISFeatureLayerComponent : MonoBehaviour
{
    [System.Serializable]
    public struct QueryLink
    {
        public string Link;
        public string[] RequestHeaders;
    }

    [System.Serializable]
    public class GeometryData
    {
        public double Latitude;
        public double Longitude;
    }

    [System.Serializable]
    public class PropertyData
    {
        public List<string> PropertyNames = new List<string>();
        public List<string> Data = new List<string>();
    }

    [System.Serializable]
    public class FeatureQueryData
    {
        public GeometryData Geometry = new GeometryData();
        public PropertyData Properties = new PropertyData();
    }

    private List<FeatureQueryData> Features = new List<FeatureQueryData>();
    private FeatureData featureInfo;
    [SerializeField] private GameObject featurePrefab;
    private JToken[] jFeatures;
    private float spawnHeight = 10;

    public List<GameObject> FeatureItems = new List<GameObject>();
    public QueryLink WebLink;
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private GameObject point;
    private ArcGISMapComponent mapComponent;

    //public List<Place> placeList;
    
    private LineRenderer lineRenderer;
    [SerializeField] public GameObject Line;
    [SerializeField] public GameObject LineMarker;
    private const float LineWidth = 5f;

    private void Start()
    {
        StartCoroutine(nameof(GetFeatures));
        mapComponent = FindFirstObjectByType<ArcGISMapComponent>();
        //placeList = new List<Place>();

        //lineRenderer = Line.GetComponent<LineRenderer>();
    }

    public void CreateLink(string link)
    {
        if (link != null)
        {
            foreach (var header in WebLink.RequestHeaders)
            {
                if (!link.ToLower().Contains(header))
                {
                    link += header;
                }
            }

            WebLink.Link = link;
        }
    }

    public IEnumerator GetFeatures()
    {
        // To learn more about the Feature Layer rest API and all the things that are possible checkout
        // https://developers.arcgis.com/rest/services-reference/enterprise/query-feature-service-layer-.htm

        UnityWebRequest Request = UnityWebRequest.Get(WebLink.Link);
        yield return Request.SendWebRequest();

        if (Request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(Request.error);
        }
        else
        {
            CreateGameObjectsFromResponse(Request.downloadHandler.text);
        }
    }

    private void CreateGameObjectsFromResponse(string response)
    {
        Debug.Log("Response: " + response);
        // Deserialize the JSON response from the query.
        var jObject = JObject.Parse(response);
        jFeatures = jObject.SelectToken("features").ToArray();
        CreateFeatures();
    }

    private void CreateFeatures()
    {
        foreach (var feature in jFeatures)
        {
            var currentFeature = new FeatureQueryData();
            var featureItem = Instantiate(featurePrefab, this.transform);
            var locationComponent = featureItem.GetComponent<ArcGISLocationComponent>();
            
            // Get coordinates in the Feature Service
            var coordinates = feature.SelectToken("geometry").SelectToken("coordinates").ToArray();
            var properties = feature.SelectToken("properties").ToArray();

            foreach (var value in properties)
            {
                var key = value.ToString();
                var props = key.Split(':');
                currentFeature.Properties.PropertyNames.Add(props[0]);
                currentFeature.Properties.Data.Add(props[1]);
                //featureInfo.Properties.Add(key);
                
                 
            }
            
            //coordinate.ToArray();
            Debug.Log("coordinate: " + coordinates[1] + " " + coordinates[0]);
            currentFeature.Geometry.Latitude = Convert.ToDouble(coordinates[1]);
            currentFeature.Geometry.Longitude = Convert.ToDouble(coordinates[0]);
            
            // Create new ArcGIS Point and pass the Feature Lat and Long to it
            var arcPosition = new ArcGISPoint(currentFeature.Geometry.Longitude, currentFeature.Geometry.Latitude, spawnHeight, ArcGISSpatialReference.WGS84());
            Debug.Log("arcPosition: " + arcPosition.X + " " + arcPosition.Y + " " + arcPosition.Z);

            // Create new Bezier Knot that stores transform data
            BezierKnot bezierKnot = new BezierKnot();

            // Convert ArcGISPoint to Engine Coordinates
            var position = mapComponent.GeographicToEngine(arcPosition);
            Debug.Log("position: " + position);
            GameObject county = Instantiate(point, position, Quaternion.identity);
            //county.GetComponent<ArcGISLocationComponent>().Position = new ArcGISPoint(currentFeature.Geometry.Longitude, currentFeature.Geometry.Latitude, spawnHeight, new ArcGISSpatialReference(3857));

            // Add converted position to the splines container
            splineContainer.Splines[0].Add(bezierKnot);

            /*foreach (var coordinate in coordinates)
            {
                
            }*/
            
            Features.Add(currentFeature);
            FeatureItems.Add(featureItem);
        }
    }
    
    private void RenderLine(ref List<GameObject> featurePoints)
    {
        var allPoints = new List<Vector3>();

        foreach (var point in featurePoints)
        {
            if (point.transform.position.Equals(Vector3.zero))
            {
                Destroy(point);
                continue;
            }
            allPoints.Add(point.transform.position);
        }

        lineRenderer.positionCount = allPoints.Count;
        lineRenderer.SetPositions(allPoints.ToArray());
    }


}
