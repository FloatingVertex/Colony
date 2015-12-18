using UnityEngine;
using System.Collections;

public class BuildCamera : MonoBehaviour {

    public static BuildCamera singleton;
    public static int indexOfSelectedToBuild = 1;
    public static int resources = 350;
    public static int research = 00;
    public GameObject[] buildingPrefabs;
    public string[] buildingNames;
    public int onGUIs;
    public float leftMapBorder;
    public float topMapBorder;
    public float bufferSize;
    public float scrollSpeed;
    public float buildingZ;
    public float lowX;
    public float highX;
    public float lowY;
    public float highY;
    public Camera mainCamera;

    void Awake()
    {
        if (singleton)
        {
            Debug.LogError("multiple singletons");
        }
        singleton = this;
        Time.fixedDeltaTime = 1f;
        Application.runInBackground = true;
    }

	// Use this for initialization
	void Start () {
        mainCamera = GetComponent<Camera>();
        //Debug.Log("A" + buildingPrefabs[0].GetComponent<Resedency>().GetCost());
	}

    public void BuildAt(Vector3 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        position.z = buildingZ;
        Instantiate(buildingPrefabs[indexOfSelectedToBuild], position, transform.rotation);
    }
	
	// Update is called once per frame
	void Update () {
        indexOfSelectedToBuild = indexOfSelectedToBuild % buildingPrefabs.Length;
        float x = Input.mousePosition.x / Screen.width;
        float y = Input.mousePosition.y / Screen.height;
        if (!ResearchMenu.bInResearchMenu && x > leftMapBorder && y < topMapBorder && x >= 0 && y >= 0)
        {
            if (!LockViewToggle.locaView)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize + 5, 5, 20);
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize - 5, 5, 20);
                }
                if (1f - x < bufferSize)
                {
                    transform.Translate((x - (1 - bufferSize)) * mainCamera.orthographicSize * scrollSpeed * Time.deltaTime, 0, 0);
                }
                if (x > leftMapBorder && x - leftMapBorder < bufferSize)
                {
                    transform.Translate(((bufferSize + leftMapBorder) - (x)) * mainCamera.orthographicSize * -scrollSpeed * Time.deltaTime, 0, 0);
                }
                if (y < bufferSize)
                {
                    transform.Translate(0, (bufferSize - y) * -scrollSpeed * mainCamera.orthographicSize * Time.deltaTime, 0);
                }
                if (y < topMapBorder && topMapBorder - y < bufferSize)
                {
                    transform.Translate(0, (y - (topMapBorder - bufferSize)) * mainCamera.orthographicSize * scrollSpeed * Time.deltaTime, 0);
                }
            }
            onGUIs = 0;
        }
        else
        {
            onGUIs = 1;
        }
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, lowX, highX);
        position.y = Mathf.Clamp(position.y, lowY, highY);
        transform.position = position;
    }
}
