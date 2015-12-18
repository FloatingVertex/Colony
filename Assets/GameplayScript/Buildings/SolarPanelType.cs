using UnityEngine;
using System.Collections;

public class SolarPanelType : BuildingType {

    public static SolarPanelType singleton;

    protected override void Awake()
    {
        if (singleton)
        {
            Debug.LogError("Multiple of this type");
        }
        singleton = this;
        resourceTypeIn = new ResourceTypes[] { };
        resourceTypeOut = new ResourceTypes[] { ResourceTypes.Power };
        name = "SolarPanel";
        base.Awake();
    }
}
