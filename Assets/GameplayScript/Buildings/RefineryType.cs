using UnityEngine;
using System.Collections;

public class RefineryType : BuildingType {

    public static RefineryType singleton;

    protected override void Awake()
    {
        if (singleton)
        {
            Debug.LogError("Multiple of this type");
        }
        singleton = this;
        resourceTypeIn = new ResourceTypes[] { ResourceTypes.Power ,ResourceTypes.People};
        resourceTypeOut = new ResourceTypes[] {  };
        name = "Refinery";
        base.Awake();
        resourceInTypeMaxes[(int)resourceTypeIn[0]] = 200;
        resourceInTypeMaxes[(int)resourceTypeIn[1]] = 90;
        resourceTypeMaxesIncreasePerLevel[(int)resourceTypeIn[0]] = 30;
        resourceTypeMaxesIncreasePerLevel[(int)resourceTypeIn[1]] = 15;
    }
}
