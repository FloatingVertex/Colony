using UnityEngine;
using System.Collections;

public class House : Building {
    public override void Awake()
    {
        type = HouseType.singleton;
        base.Awake();
    }

    public override void Tick()
    {
        resourcesOut[(int)type.resourceTypeOut[0]] = resourcesIn[(int)type.resourceTypeIn[0]];
        lastOutputs = "";
        foreach (ResourceTypes r in HouseType.singleton.resourceTypeOut)
        {
            lastOutputs = lastOutputs + Mathf.RoundToInt(resourcesOut[(int)r]) + " " + r.ToString() + "\n";
        }
        PostTick();
    }

    public override int GetCost()
    {
        return HouseType.singleton.buildCost;
    }

    public override string GetDiscription()
    {
        return "Converts food into people";
    }

    public override int GetUpgradeCost()
    {
        return HouseType.singleton.baseUpgradeCost + (HouseType.singleton.aditionalUpgradeCost * level);
    }

    public override string GetInputs()
    {
        string s = "";
        foreach (ResourceTypes r in HouseType.singleton.resourceTypeIn)
        {
            s = s + Mathf.RoundToInt(lastResourcesIn[(int)r]) + "/" + (HouseType.singleton.resourceInTypeMaxes[(int)r] + HouseType.singleton.resourceTypeMaxesIncreasePerLevel[(int)r] * level) + " " + r.ToString() + "\n";
        }
        return s;
    }

    public override string GetName()
    {
        return "House";
    }
}
