using UnityEngine;
using System.Collections;

public enum ResourceTypes { People, Food, Power, RawMaterials,Last};

public class BuildingType : MonoBehaviour {
    //common to all of same subclass
    public new string name;
    [HideInInspector]
    public ResourceTypes[] resourceTypeIn;
    public bool[] bResourceTypeIn = new bool[(int)ResourceTypes.Last];
    public int[] resourceInTypeMaxes = new int[(int)ResourceTypes.Last];
    [HideInInspector]
    public ResourceTypes[] resourceTypeOut;
    public bool[] bResourceTypeOut = new bool[(int)ResourceTypes.Last];
    [HideInInspector]
    public LayerMask buildingsMask;
    public int[] resourceTypeMaxesIncreasePerLevel = new int[(int)ResourceTypes.Last];
    public int buildCost = 100;
    public int baseUpgradeCost = 100;
    public int aditionalUpgradeCost = 50;
    protected virtual void Awake()
    {
        foreach(ResourceTypes i in resourceTypeIn)
        {
            bResourceTypeIn[(int)i] = true;
        }
        foreach (ResourceTypes i in resourceTypeOut)
        {
            bResourceTypeOut[(int)i] = true;
        }
        buildingsMask = 1 << 8;
    }
}
