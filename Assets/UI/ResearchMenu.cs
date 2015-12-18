using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResearchMenu : MonoBehaviour {
    public static bool bInResearchMenu;
    public GameObject panelParent;
    public GameObject[] buildButtons;
    public int[] researchCosts;
    public Text[] costs;
    public Button[] buyButtons;

    void Start()
    {
        for(int i = 0; i < costs.Length; i++)
        {
            if (costs[i])
            {
                costs[i].text = "Cost: " + researchCosts[i];
            }
        }
    }

	public void Research(int index)
    {
        if(researchCosts[index] < BuildCamera.research)
        {
            BuildCamera.research -= researchCosts[index];
            buildButtons[index].transform.parent = panelParent.transform;
            buyButtons[index].interactable = false;
            costs[index].text = "";
        }
    }

    public void CloseMenu()
    {
        GetComponent<Canvas>().enabled = false;
        bInResearchMenu = false;
    }

    public void OpenMenu()
    {
        GetComponent<Canvas>().enabled = true;
        bInResearchMenu = true;
    }
}
