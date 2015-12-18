using UnityEngine;
using System.Collections;

public class Road : Building {

    public override void Awake()
    {
        dynamicDistrbution = true;
        type = RoadType.singleton;
        base.Awake();
    }

    public override void Tick()
    {
        for (int i = 0; i < type.resourceTypeOut.Length; i++)
        {
            resourcesOut[(int)type.resourceTypeOut[i]] = indirectResourcesIn[(int)type.resourceTypeOut[i]] + resourcesIn[(int)type.resourceTypeOut[i]];
        }
        PostTick();
    }

    public override int GetCost()
    {
        return RoadType.singleton.buildCost;
    }

    public override string GetDiscription()
    {
        return "Allows people to travel";
    }

    public override int GetUpgradeCost()
    {
        return RoadType.singleton.baseUpgradeCost + (RoadType.singleton.aditionalUpgradeCost * level);
    }

    public override string GetInputs()
    {
        string s = "";
        foreach (ResourceTypes r in RoadType.singleton.resourceTypeIn)
        {
            s = s + Mathf.RoundToInt(lastResourcesIn[(int)r]) + "/" + (RoadType.singleton.resourceInTypeMaxes[(int)r] + RoadType.singleton.resourceTypeMaxesIncreasePerLevel[(int)r] * level) + " " + r.ToString() + "\n";
        }
        return s;
    }

    public override string GetName()
    {
        return "Road";
    }
}
