using UnityEngine;
using System.Collections;

public class MountainMine : Building
{
    public override void Awake()
    {
        type = MountainMineType.singleton;
        base.Awake();
    }

    public override void Tick()
    {
        resourcesOut[(int)type.resourceTypeOut[0]] = (int)Mathf.Sqrt((float)resourcesIn[(int)type.resourceTypeIn[0]] * (float)resourcesIn[(int)type.resourceTypeIn[1]]);
        lastOutputs = "";
        foreach (ResourceTypes r in MountainMineType.singleton.resourceTypeOut)
        {
            lastOutputs = lastOutputs + (int)resourcesOut[(int)r] + " " + r.ToString() + "\n";
        }
        PostTick();
    }

    public override int GetCost()
    {
        return MountainMineType.singleton.buildCost;
    }

    public override string GetDiscription()
    {
        return "Gnerates raw materials, must be build on a mountain";
    }

    public override int GetUpgradeCost()
    {
        return MountainMineType.singleton.baseUpgradeCost + (MountainMineType.singleton.aditionalUpgradeCost * level);
    }

    public override string GetInputs()
    {
        string s = "";
        foreach (ResourceTypes r in MountainMineType.singleton.resourceTypeIn)
        {
            s = s + Mathf.RoundToInt(lastResourcesIn[(int)r]) + "/" + (MountainMineType.singleton.resourceInTypeMaxes[(int)r] + MountainMineType.singleton.resourceTypeMaxesIncreasePerLevel[(int)r] * level) + " " + r.ToString() + "\n";
        }
        return s;
    }

    public override string GetName()
    {
        return "Mountain Mine";
    }
}
