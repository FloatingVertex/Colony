using UnityEngine;
using System.Collections;

public class DirtRoad : Building {

    public override void Awake()
    {
        dynamicDistrbution = true;
        type = DirtRoadType.singleton;
        base.Awake();
    }

    public override void Tick()
    {
        for(int i = 0; i < type.resourceTypeOut.Length; i++)
        {
            resourcesOut[(int)type.resourceTypeOut[i]] = indirectResourcesIn[(int)type.resourceTypeOut[i]] + resourcesIn[(int)type.resourceTypeOut[i]];
        }
        PostTick();
    }

    public override int GetCost()
    {
        return DirtRoadType.singleton.buildCost;
    }

    public override string GetDiscription()
    {
        return "Transports people, low capacity and range";
    }

    public override int GetUpgradeCost()
    {
        return DirtRoadType.singleton.baseUpgradeCost + (DirtRoadType.singleton.aditionalUpgradeCost * level);
    }

    public override string GetInputs()
    {
        string s = "";
        foreach (ResourceTypes r in DirtRoadType.singleton.resourceTypeIn)
        {
            s = s + Mathf.RoundToInt(lastResourcesIn[(int)r]) + "/" + (DirtRoadType.singleton.resourceInTypeMaxes[(int)r] + DirtRoadType.singleton.resourceTypeMaxesIncreasePerLevel[(int)r] * level) + " " + r.ToString() + "\n";
        }
        return s;
    }

    public override string GetName()
    {
        return "Dirt Road";
    }
}
