using UnityEngine;
using System.Collections;

public class SolarPanel : Building {

    public override void Awake()
    {
        type = SolarPanelType.singleton;
        base.Awake();
    }

    public override void Tick()
    {
        resourcesOut[(int)type.resourceTypeOut[0]] = 4 + level;
        lastOutputs = "";
        foreach (ResourceTypes r in SolarPanelType.singleton.resourceTypeOut)
        {
            lastOutputs = lastOutputs + Mathf.RoundToInt(resourcesOut[(int)r]) + " " + r.ToString() + "\n";
        }
        PostTick();
    }

    public override int GetCost()
    {
        return SolarPanelType.singleton.buildCost;
    }

    public override string GetDiscription()
    {
        return "Generates Power";
    }

    public override int GetUpgradeCost()
    {
        return SolarPanelType.singleton.baseUpgradeCost + (SolarPanelType.singleton.aditionalUpgradeCost * level);
    }

    public override string GetInputs()
    {
        string s = "";
        foreach (ResourceTypes r in SolarPanelType.singleton.resourceTypeIn)
        {
            s = s + Mathf.RoundToInt(lastResourcesIn[(int)r]) + "/" + (SolarPanelType.singleton.resourceInTypeMaxes[(int)r] + SolarPanelType.singleton.resourceTypeMaxesIncreasePerLevel[(int)r] * level) + " " + r.ToString() + "\n";
        }
        return s;
    }

    public override string GetName()
    {
        return "Solar Panel";
    }
}
