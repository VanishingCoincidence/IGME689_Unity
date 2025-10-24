using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int playerPoints = 6;
    private int maxCases = 100;
    private int totalCases;
    
    private List<Person> people = new List<Person>();
    public Transform spawnPosition;
    public GameObject personPrefab;
    
    public Camera camera;
    public PlaceManager placeManager;

    public Canvas placeCanvas;
    public TMP_Text placeInfo;
    public TMP_Text currentCaseInfo;
    
    public Canvas personCanvas;
    public TMP_Text personPlaceInfo;
    public TMP_Text personCurrentCaseInfo;
    public TMP_Text personCanTravelInfo;
    
    public TMP_Text pointsInfo;
    public TMP_Text totalCaseInfo;
    public Button endTurnButton;

    public Canvas legendCanvas;
    public Button legendButton;

    public Image winImage;
    public Image loseImage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        placeCanvas.enabled = false;
        loseImage.enabled = false;
        winImage.enabled = false;
        legendCanvas.enabled = false;
        personCanvas.enabled = false;
        
        endTurnButton.onClick.AddListener(EndTurn);
        legendButton.onClick.AddListener(ToggleLegend);
        
        StartCoroutine(DelayedAction());
    }
    
    public IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(1.3f); 

        UpdateTotalCases();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            
            Ray ray = camera.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            
            bool isHit = Physics.Raycast(ray, out hit);

            if (isHit && hit.collider.gameObject.GetComponent<Place>() != null)
            {
                placeCanvas.enabled = true;
                FixPlaceInfo(hit.collider.gameObject.GetComponent<Place>());
            }
            else if (isHit && hit.collider.gameObject.GetComponent<Person>() != null)
            {
                personCanvas.enabled = true;
                FixPersonInfo(hit.collider.gameObject.GetComponent<Person>());
            }
            else
            {
                placeCanvas.enabled = false;
                personCanvas.enabled = false;
            }

        }
    }
    
    
    
    void FixPersonInfo(Person person)
    {
        foreach (Person p in placeManager.people)
        {
            if (p.currentCounty == person.currentCounty)
            {
                personPlaceInfo.text = "Current location: " + p.currentCounty.placeData.State + ", " + p.currentCounty.placeData.State;
                personCurrentCaseInfo.text = "Cases: " + p.currentCounty.placeData.CurrentCases;

                string placesCanTravel = "Places can travel: ";

                foreach (PlaceData place in person.currentCounty.placeData.Connected_Places)
                {
                    placesCanTravel += " " + place.County + ", " + place.State + "|";
                }
                
                personCanTravelInfo.text = placesCanTravel;
                
                break;
            }
        }
    }


    void ToggleLegend()
    {
        if (legendCanvas.enabled)
        {
            legendCanvas.enabled = false;
        }
        else
        {
            legendCanvas.enabled = true;
        }
    }

    void EndTurn()
    {
        playerPoints = 6;

        if (totalCases >= maxCases)
        {
            loseImage.enabled = true;
        }

        if (totalCases == 0)
        {
            winImage.enabled = true;
        }
    }

    void FixPlaceInfo(Place place)
    {
        foreach (Place p in placeManager.places)
        {
            if (p.placeData.County == place.placeData.County)
            {
                placeInfo.text = p.placeData.County + ", " + p.placeData.State;
                currentCaseInfo.text = "Cases: " + p.placeData.CurrentCases;
                break;
            }
        }
    }

    void UpdateTotalCases()
    {
        int tempCases = 0;
        
        foreach (Place p in placeManager.places)
        {
            tempCases += p.placeData.CurrentCases;
        }
        
        totalCases = tempCases;
        totalCaseInfo.text = "Total Cases: " + totalCases;
    }
    
    
}
