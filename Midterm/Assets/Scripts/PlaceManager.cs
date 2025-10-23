using Esri.GameEngine.Geometry;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaceManager : MonoBehaviour
{
    public DataLoader dataLoader;


    void Start()
    {
        dataLoader = new DataLoader();
        StartCoroutine(dataLoader.GetFeatures());
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
