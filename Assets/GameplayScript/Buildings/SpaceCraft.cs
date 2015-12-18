using UnityEngine;
using System.Collections;

public class SpaceCraft : Building
{

    public override void Awake()
    {
        type = SpaceCraftType.singleton;
        base.Awake();
    }

    public override void Tick()
    {
        BuildCamera.research += (int)(resourcesIn[(int)type.resourceTypeIn[0]] * .05f);
        BuildCamera.resources += 1 + (int)(resourcesIn[(int)type.resourceTypeIn[0]] * .1f);
        lastOutputs = (int)(resourcesIn[(int)type.resourceTypeIn[0]] * .05f) + " Research\n" + 
            (1 + (int)(resourcesIn[(int)type.resourceTypeIn[0]] * .1f)) + " Resources";
        PostTick();
    }

    new void OnMouseOver()
    {

    }

    public override int GetCost()
    {
        return SpaceCraftType.singleton.buildCost;
    }

    public override string GetDiscription()
    {
        return "Generates research and resources, +1 output for every 10 people";
    }

    public override int GetUpgradeCost()
    {
        return SpaceCraftType.singleton.baseUpgradeCost + (SpaceCraftType.singleton.aditionalUpgradeCost * level);
    }

    public override string GetInputs()
    {
        string s = "";
        foreach (ResourceTypes r in SpaceCraftType.singleton.resourceTypeIn)
        {
            s = s + Mathf.RoundToInt(lastResourcesIn[(int)r]) + "/" + (SpaceCraftType.singleton.resourceInTypeMaxes[(int)r] + SpaceCraftType.singleton.resourceTypeMaxesIncreasePerLevel[(int)r] * level) + " " + r.ToString() + "\n";
        }
        return s;
    }

    public override string GetName()
    {
        return "Lander";
    }
}
