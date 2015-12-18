using UnityEngine;
using System.Collections;

public class RoadType : BuildingType {

    public static RoadType singleton;

    protected override void Awake()
    {
        if (singleton)
        {
            Debug.LogError("Multiple of this type");
        }
        singleton = this;
        resourceTypeIn = new ResourceTypes[] { ResourceTypes.People};
        resourceTypeOut = new ResourceTypes[] { ResourceTypes.People};
        name = "Road";
        base.Awake();
        resourceInTypeMaxes[(int)ResourceTypes.People] = 100;
        resourceTypeMaxesIncreasePerLevel[(int)resourceTypeIn[0]] = 100;
    }
}
