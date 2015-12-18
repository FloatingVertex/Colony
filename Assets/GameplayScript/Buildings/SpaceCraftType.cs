using UnityEngine;
using System.Collections;

public class SpaceCraftType : BuildingType {

    public static SpaceCraftType singleton;

    protected override void Awake()
    {
        if (singleton)
        {
            Debug.LogError("Multiple of this type");
        }
        singleton = this;
        resourceTypeIn = new ResourceTypes[] { ResourceTypes.People };
        resourceTypeOut = new ResourceTypes[] { };
        name = "SpaceCraft";
        base.Awake();
        resourceInTypeMaxes[(int)ResourceTypes.People] = 200;
        resourceTypeMaxesIncreasePerLevel[(int)resourceTypeIn[0]] = 200;
    }
}
