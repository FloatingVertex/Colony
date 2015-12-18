using UnityEngine;
using System.Collections;

public class ResedencyType : BuildingType
{

    public static ResedencyType singleton;

    protected override void Awake()
    {
        if (singleton)
        {
            Debug.LogError("Multiple of this type");
        }
        singleton = this;
        resourceTypeIn = new ResourceTypes[] { ResourceTypes.Food};
        resourceTypeOut = new ResourceTypes[] { ResourceTypes.People };
        name = "Resedency";
        base.Awake();
        resourceInTypeMaxes[(int)ResourceTypes.Food] = 20;
        resourceTypeMaxesIncreasePerLevel[(int)resourceTypeIn[0]] = 10;
    }
}
