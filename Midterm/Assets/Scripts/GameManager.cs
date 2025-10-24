using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera camera;

    public PlaceManager placeManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
                Debug.Log(hit.collider.gameObject.name);
            }

        }
    }
}
