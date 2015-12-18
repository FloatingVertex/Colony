using UnityEngine;
using System.Collections;

public class SurfaceMineType : BuildingType {

    public static SurfaceMineType singleton;

    protected override void Awake()
    {
        if (singleton)
        {
            Debug.LogError("Multiple of this type");
        }
        singleton = this;
        resourceTypeIn = new ResourceTypes[] { ResourceTypes.People, ResourceTypes.Power };
        resourceTypeOut = new ResourceTypes[] { };
        name = "SurfaceMine";
        base.Awake();
        resourceInTypeMaxes[(int)ResourceTypes.People] = 20;
        resourceInTypeMaxes[(int)ResourceTypes.Power] = 20;
        resourceTypeMaxesIncreasePerLevel[(int)resourceTypeIn[0]] = 10;
        resourceTypeMaxesIncreasePerLevel[(int)resourceTypeIn[1]] = 10;
    }
}
