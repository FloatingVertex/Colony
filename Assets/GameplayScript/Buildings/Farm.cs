using UnityEngine;
using System.Collections;

public class Farm : Building
{

    public override void Awake()
    {
        type = FarmType.singleton;
        base.Awake();
    }

    public override void Tick()
    {
        resourcesOut[(int)type.resourceTypeOut[0]] = 4 + level;
        lastOutputs = "";
        foreach (ResourceTypes r in FarmType.singleton.resourceTypeOut)
        {
            lastOutputs = lastOutputs + resourcesOut[(int)r] + " " + r.ToString() + "\n";
        }
        PostTick();
    }

    public override int GetCost()
    {
        return FarmType.singleton.buildCost;
    }

    public override string GetDiscription()
    {
        return "Generates Food";
    }

    public override int GetUpgradeCost()
    {
        return FarmType.singleton.baseUpgradeCost + (FarmType.singleton.aditionalUpgradeCost * level);
    }

    public override string GetInputs()
    {
        string s = "";
        foreach (ResourceTypes r in FarmType.singleton.resourceTypeIn)
        {
            s = s + Mathf.RoundToInt(lastResourcesIn[(int)r]) + "/" + (FarmType.singleton.resourceInTypeMaxes[(int)r] + FarmType.singleton.resourceTypeMaxesIncreasePerLevel[(int)r] * level) + " " + r.ToString() + "\n";
        }
        return s;
    }

    public override string GetName()
    {
        return "Farm";
    }
}
