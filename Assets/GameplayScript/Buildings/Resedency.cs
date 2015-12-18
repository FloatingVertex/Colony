using UnityEngine;
using System.Collections;

public class Resedency : Building {

    public override void Awake()
    {
        type = ResedencyType.singleton;
        base.Awake();
    }

    public override void Tick()
    {
        resourcesOut[(int)type.resourceTypeOut[0]] = (int)(resourcesIn[(int)type.resourceTypeIn[0]] * 1.5f);
        lastOutputs = "";
        foreach (ResourceTypes r in ResedencyType.singleton.resourceTypeOut)
        {
            lastOutputs = lastOutputs + Mathf.RoundToInt(resourcesOut[(int)r]) + " " + r.ToString() + "\n";
        }
        PostTick();
    }

    public override int GetCost()
    {
        return ResedencyType.singleton.buildCost;
    }

    public override string GetDiscription()
    {
        return "Converts food into people. More efficient and larger than a house";
    }

    public override int GetUpgradeCost()
    {
        return ResedencyType.singleton.baseUpgradeCost + (ResedencyType.singleton.aditionalUpgradeCost * level);
    }

    public override string GetInputs()
    {
        string s = "";
        foreach(ResourceTypes r in ResedencyType.singleton.resourceTypeIn)
        {
            s = s + Mathf.RoundToInt(lastResourcesIn[(int)r]) + "/" + (ResedencyType.singleton.resourceInTypeMaxes[(int)r] + ResedencyType.singleton.resourceTypeMaxesIncreasePerLevel[(int)r] * level) + " " + r.ToString() + "\n";
        }
        return s;
    }

    public override string GetUpgradesInfo()
    {
        return "@@@";
    }

    public override string GetName()
    {
        return "SkyScraper";
    }
}
