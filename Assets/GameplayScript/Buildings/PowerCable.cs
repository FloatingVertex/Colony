using UnityEngine;
using System.Collections;

public class PowerCable : Building {

    public override void Awake()
    {
        dynamicDistrbution = true;
        type = PowerCableType.singleton;
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
        return PowerCableType.singleton.buildCost;
    }

    public override string GetDiscription()
    {
        return "Allows power to be transmitted";
    }

    public override int GetUpgradeCost()
    {
        return PowerCableType.singleton.baseUpgradeCost + (PowerCableType.singleton.aditionalUpgradeCost * level);
    }

    public override string GetInputs()
    {
        string s = "";
        foreach (ResourceTypes r in PowerCableType.singleton.resourceTypeIn)
        {
            s = s + Mathf.RoundToInt(lastResourcesIn[(int)r]) + "/" + (PowerCableType.singleton.resourceInTypeMaxes[(int)r] + PowerCableType.singleton.resourceTypeMaxesIncreasePerLevel[(int)r] * level) + " " + r.ToString() + "\n";
        }
        return s;
    }

    public override string GetName()
    {
        return "Power Cable";
    }
}
