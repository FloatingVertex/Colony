using UnityEngine;
using System.Collections;

public class MapTileType : MonoBehaviour
{
    public string[] allow;
    public string[] disallow;
    public bool allowByDefault = true;
    // Use this for initialization
    void Start()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseOver()
    {

    }

    public void OnMouseUpAsButton()
    {
        if (!ResearchMenu.bInResearchMenu && BuildCamera.singleton.onGUIs == 0)
        {
            if (allowByDefault)
            {
                for (int i = 0; i < disallow.Length; i++)
                {
                    if (disallow[i].Equals(BuildCamera.singleton.buildingNames[BuildCamera.indexOfSelectedToBuild]))
                    {
                        return;
                    }
                }
                BuildCamera.singleton.BuildAt(transform.position);
            }
            else
            {
                for (int i = 0; i < allow.Length; i++)
                {
                    if (allow[i].Equals(BuildCamera.singleton.buildingNames[BuildCamera.indexOfSelectedToBuild]))
                    {
                        BuildCamera.singleton.BuildAt(transform.position);
                    }
                }
            }
        }
        
    }
}
