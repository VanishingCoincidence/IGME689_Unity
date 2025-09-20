using UnityEngine;
using UnityEngine.UI;
using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Geometry;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using TMPro;

public class ReadInput : MonoBehaviour
{
    private string input;
    public TMP_Text info;

    public ReadCSV readCSV;
    public ArcGISMapComponent mapComponent;
    public ArcGISCameraComponent cameraComponent;

    public Image[] virusImages = new Image[10];
    public Image[] deathImages = new Image[10];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetImages();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input);
        ShowInfo(input);
    }

    public void ShowInfo(string input)
    {
        ResetImages();
        bool found = false;
        for (int i = 0; i < readCSV.covidCaseList.size; i++)
        {
            if ((readCSV.covidCaseList.covidCase[i].county.ToLower()).Equals(input.ToLower()))
            {
                Debug.Log("Here! We found ti!");
                // go to the county
                mapComponent.OriginPosition = new ArcGISPoint(readCSV.covidCaseList.covidCase[i].longitude, readCSV.covidCaseList.covidCase[i].latitude, 0, ArcGISSpatialReference.WGS84());
                mapComponent.UpdateHPRoot();
                ArcGISLocationComponent cameraLocation = cameraComponent.GetComponent<ArcGISLocationComponent>();
                cameraLocation.Position = new ArcGISPoint(readCSV.covidCaseList.covidCase[i].longitude, readCSV.covidCaseList.covidCase[i].latitude, cameraLocation.Position.Z, ArcGISSpatialReference.WGS84());

                // show the info
                info.text = "Number of Confirmed COIVD Cases: " + readCSV.covidCaseList.covidCase[i].confirmedCasesAmt
                    + "\nNumber of COVID Deaths: "+ readCSV.covidCaseList.covidCase[i].deathsAmt
                    + "\nIncident of COVID rate: " + readCSV.covidCaseList.covidCase[i].incidentRate;

                ShowViruses(readCSV.covidCaseList.covidCase[i].confirmedCasesAmt);
                ShowSkulls(readCSV.covidCaseList.covidCase[i].deathsAmt);

                found = true;
                break;
            }
        }

        if(!found)
        {
            Debug.Log("Here! We didn't find it!");
            info.text = "County not found";
        }

    }

    public void ResetImages()
    {
        for(int i = 0; i < virusImages.Length; i++)
        {
            virusImages[i].enabled = false;
            deathImages[i].enabled = false;
        }
    }

    public void ShowViruses(float cases)
    {
        int count;
        if(cases > 50000)
        {
            count = virusImages.Length;
        }
        else if(cases > 40000)
        {
            count = virusImages.Length - 1;
        }
        else if (cases > 30000)
        {
            count = virusImages.Length - 2;
        }
        else if (cases > 20000)
        {
            count = virusImages.Length - 3;
        }
        else if (cases > 10000)
        {
            count = virusImages.Length - 4;
        }
        else if (cases > 9000)
        {
            count = virusImages.Length - 5;
        }
        else if (cases > 7000)
        {
            count = virusImages.Length - 6;
        }
        else if (cases > 6000)
        {
            count = virusImages.Length - 7;
        }
        else if (cases > 5000)
        {
            count = virusImages.Length - 8;
        }
        else
        {
            count = virusImages.Length - 9;
        }

        for(int i = 0; i < count; i++)
        {
            virusImages[i].enabled = true;
        }

    }

    public void ShowSkulls(float cases)
    {
        int count;
        if (cases > 8000)
        {
            count = deathImages.Length;
        }
        else if (cases > 6000)
        {
            count = deathImages.Length - 1;
        }
        else if (cases > 3000)
        {
            count = deathImages.Length - 2;
        }
        else if (cases > 1000)
        {
            count = deathImages.Length - 3;
        }
        else if (cases > 600)
        {
            count = deathImages.Length - 4;
        }
        else if (cases > 300)
        {
            count = deathImages.Length - 5;
        }
        else if (cases > 200)
        {
            count = deathImages.Length - 6;
        }
        else if (cases > 100)
        {
            count = deathImages.Length - 7;
        }
        else if (cases > 50)
        {
            count = deathImages.Length - 8;
        }
        else
        {
            count = deathImages.Length - 9;
        }

        for (int i = 0; i < count; i++)
        {
            deathImages[i].enabled = true;
        }

    }
}
