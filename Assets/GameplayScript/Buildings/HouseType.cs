using UnityEngine;
using System.Collections;

public class HouseType : BuildingType {

    public static HouseType singleton;

    protected override void Awake()
    {
        if (singleton)
        {
            Debug.LogError("Multiple of this type");
        }
        singleton = this;
        resourceTypeIn = new ResourceTypes[] { ResourceTypes.Food };
        resourceTypeOut = new ResourceTypes[] { ResourceTypes.People };
        name = "House";
        base.Awake();
        resourceInTypeMaxes[(int)ResourceTypes.Food] = 4;
        resourceTypeMaxesIncreasePerLevel[(int)resourceTypeIn[0]] = 2;
    }
}
