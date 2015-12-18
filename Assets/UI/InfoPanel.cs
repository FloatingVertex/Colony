using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanel : MonoBehaviour {

    public static Building info;
    public Text nameBox;
    public Text cost;
    public Text discription;
    public Text upgradeCost;
    public Text inputs;
    public Text outputs;
    public Text Upgrades;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (info) {
            UpdateInfo();
        }
        else
        {
            info = BuildCamera.singleton.buildingPrefabs[BuildCamera.indexOfSelectedToBuild].GetComponent<Building>();
            UpdateInfo();
            info = null;
        }
	}

    void UpdateInfo()
    {
        nameBox.text = info.GetName();
        cost.text = info.GetCost() + " Resources";
        if(info.GetCost() > BuildCamera.resources)
        {
            cost.color = Color.red;
        }
        else
        {
            cost.color = Color.green;
        }
        discription.text = info.GetDiscription();
        outputs.text = info.GetOutputs();
        inputs.text = info.GetInputs();
        upgradeCost.text = info.GetUpgradeCost() + " Resources";
        if (info.GetUpgradeCost() > BuildCamera.resources)
        {
            upgradeCost.color = Color.red;
        }
        else
        {
            upgradeCost.color = Color.green;
        }
    }
}
