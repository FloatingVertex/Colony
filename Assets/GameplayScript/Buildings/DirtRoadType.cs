using UnityEngine;
using System.Collections;

public class DirtRoadType : BuildingType
{

    public static DirtRoadType singleton;

    protected override void Awake()
    {
        if (singleton)
        {
            Debug.LogError("Multiple of this type");
        }
        singleton = this;
        resourceTypeIn = new ResourceTypes[] { ResourceTypes.People };
        resourceTypeOut = new ResourceTypes[] { ResourceTypes.People };
        name = "DirtRoad";
        base.Awake();
        resourceInTypeMaxes[(int)ResourceTypes.People] = 10;
        resourceTypeMaxesIncreasePerLevel[(int)resourceTypeIn[0]] = 5;
    }
}
