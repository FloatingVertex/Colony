using UnityEngine;
using System.Collections;

public class TunnelType : BuildingType {

    public static TunnelType singleton;

    protected override void Awake()
    {
        if (singleton)
        {
            Debug.LogError("Multiple of this type");
        }
        singleton = this;
        resourceTypeIn = new ResourceTypes[] { ResourceTypes.People };
        resourceTypeOut = new ResourceTypes[] { ResourceTypes.People };
        name = "Tunnel";
        base.Awake();
        resourceInTypeMaxes[(int)ResourceTypes.People] = 10;
        resourceTypeMaxesIncreasePerLevel[(int)resourceTypeIn[0]] = 10;
    }
}
