using UnityEngine;
using System.Collections;

public class FarmType : BuildingType
{

    public static FarmType singleton;

    protected override void Awake()
    {
        if (singleton)
        {
            Debug.LogError("Multiple of this type");
        }
        singleton = this;
        resourceTypeIn = new ResourceTypes[] { };
        resourceTypeOut = new ResourceTypes[] { ResourceTypes.Food };
        name = "Farm";
        base.Awake();
    }
}
