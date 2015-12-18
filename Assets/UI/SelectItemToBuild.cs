using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectItemToBuild : MonoBehaviour {

    public int index;

    void Update()
    {
        if(BuildCamera.indexOfSelectedToBuild == index)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
    }

	public void SelectItem(int itemIndex)
    {
        if(itemIndex < 0 || index >= BuildCamera.singleton.buildingPrefabs.Length)
        {
            Debug.LogError("Invalid number:" + index);
        }
        BuildCamera.indexOfSelectedToBuild = index;
    }

    public void EnterHover(int index2)
    {
        InfoPanel.info = BuildCamera.singleton.buildingPrefabs[index].GetComponent<Building>();
    }

    public void ExitHover()
    {
        InfoPanel.info = null;
    }
}
