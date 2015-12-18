using UnityEngine;
using System.Collections;

public class Tunnel : Building {

    public override void Awake()
    {
        dynamicDistrbution = true;
        type = TunnelType.singleton;
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
        return TunnelType.singleton.buildCost;
    }

    public override string GetDiscription()
    {
        return "Allows people and power to travel thorugh a mountain";
    }

    public override int GetUpgradeCost()
    {
        return TunnelType.singleton.baseUpgradeCost + (TunnelType.singleton.aditionalUpgradeCost * level);
    }

    public override string GetInputs()
    {
        string s = "";
        foreach (ResourceTypes r in TunnelType.singleton.resourceTypeIn)
        {
            s = s + Mathf.RoundToInt(lastResourcesIn[(int)r]) + "/" + (TunnelType.singleton.resourceInTypeMaxes[(int)r] + TunnelType.singleton.resourceTypeMaxesIncreasePerLevel[(int)r] * level) + " " + r.ToString() + "\n";
        }
        return s;
    }

    public override string GetName()
    {
        return "Tunnel";
    }
}
