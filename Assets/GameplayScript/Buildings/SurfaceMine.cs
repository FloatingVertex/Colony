using UnityEngine;
using System.Collections;

public class SurfaceMine : Building {

    public override void Awake()
    {
        type = SurfaceMineType.singleton;
        base.Awake();
    }

    public override void Tick()
    {
        int output = (int)Mathf.Sqrt((float)resourcesIn[(int)type.resourceTypeIn[0]] * (float)resourcesIn[(int)type.resourceTypeIn[1]]);
        BuildCamera.resources += output;
        lastOutputs = output + " Resources";
        PostTick();
    }

    public override int GetCost()
    {
        return SurfaceMineType.singleton.buildCost;
    }

    public override string GetDiscription()
    {
        return "Generates resources";
    }

    public override int GetUpgradeCost()
    {
        return SurfaceMineType.singleton.baseUpgradeCost + (SurfaceMineType.singleton.aditionalUpgradeCost * level);
    }

    public override string GetInputs()
    {
        string s = "";
        foreach (ResourceTypes r in SurfaceMineType.singleton.resourceTypeIn)
        {
            s = s + Mathf.RoundToInt(lastResourcesIn[(int)r]) + "/" + (SurfaceMineType.singleton.resourceInTypeMaxes[(int)r] + SurfaceMineType.singleton.resourceTypeMaxesIncreasePerLevel[(int)r] * level) + " " + r.ToString() + "\n";
        }
        return s;
    }

    public override string GetName()
    {
        return "Surface Mine";
    }
}
