using UnityEngine;
using System.Collections;

public class PowerCableType : BuildingType {

    public static PowerCableType singleton;

    protected override void Awake()
    {
        if (singleton)
        {
            Debug.LogError("Multiple of this type");
        }
        singleton = this;
        resourceTypeIn = new ResourceTypes[] { ResourceTypes.Power };
        resourceTypeOut = new ResourceTypes[] { ResourceTypes.Power };
        name = "PowerCable";
        base.Awake();
        resourceInTypeMaxes[(int)ResourceTypes.Power] = 100;
        resourceTypeMaxesIncreasePerLevel[(int)resourceTypeIn[0]] = 100;
    }
}
