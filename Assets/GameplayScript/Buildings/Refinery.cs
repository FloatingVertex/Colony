using UnityEngine;
using System.Collections;

public class Refinery : Building
{

    public override void Awake()
    {
        type = RefineryType.singleton;
        base.Awake();
    }

    public override void Tick()
    {
        int output = (int)(Mathf.Sqrt((float)resourcesIn[(int)type.resourceTypeIn[0]] * (float)resourcesIn[(int)type.resourceTypeIn[1]]));
        BuildCamera.resources += output;
        lastOutputs = output + " Resources";
        PostTick();
    }

    public override int GetCost()
    {
        return RefineryType.singleton.buildCost;
    }

    public override string GetDiscription()
    {
        return "Converts raw materials into resources";
    }

    public override int GetUpgradeCost()
    {
        return RefineryType.singleton.baseUpgradeCost + (RefineryType.singleton.aditionalUpgradeCost * level);
    }

    public override string GetInputs()
    {
        string s = "";
        foreach (ResourceTypes r in RefineryType.singleton.resourceTypeIn)
        {
            s = s + Mathf.RoundToInt(lastResourcesIn[(int)r]) + "/" + (RefineryType.singleton.resourceInTypeMaxes[(int)r] + RefineryType.singleton.resourceTypeMaxesIncreasePerLevel[(int)r] * level) + " " + r.ToString() + "\n";
        }
        return s;
    }

    public override string GetName()
    {
        return "Refinery";
    }
}
