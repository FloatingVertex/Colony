using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class Building : MonoBehaviour
{
    public Canvas canvas;
    public Text text;
    public bool dynamicDistrbution = false;
    [HideInInspector]
    public BuildingType type;
    [HideInInspector]
    public int range = 1;

    public float[] resourcesIn = new float[(int)ResourceTypes.Last];
    public float[] lastResourcesIn = new float[(int)ResourceTypes.Last];
    public float[] indirectResourcesIn = new float[(int)ResourceTypes.Last];

    public float[] resourcesOut = new float[(int)ResourceTypes.Last];


    public string lastOutputs = "None";

    public Building[] directNeighbors = new Building[4];//north,south,east,west,self

    public int level;
    public GameObject bar;

    public virtual void Awake()
    {
        if (BuildCamera.resources < type.buildCost)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            BuildCamera.resources -= type.buildCost;
        }
        directNeighbors = new Building[4];
        GetComponent<Collider2D>().enabled = false;
        Vector2 offset = new Vector2(0.5f, 0.5f);
        //Check North
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + offset, Vector2.up, 1f, type.buildingsMask, -1.5f, -0.5f);
        Building neighbor = CheckForNeightbors(hit);
        directNeighbors[0] = neighbor;
        if (neighbor)
        {
            neighbor.directNeighbors[1] = this;
        }
        //check south
        hit = Physics2D.Raycast((Vector2)transform.position + offset, -Vector2.up, 1f, type.buildingsMask, -1.5f, -0.5f);
        neighbor = CheckForNeightbors(hit);
        directNeighbors[1] = neighbor;
        if (neighbor)
        {
            neighbor.directNeighbors[0] = this;
        }
        //check east
        hit = Physics2D.Raycast((Vector2)transform.position + offset, Vector2.right, 1f, type.buildingsMask, -1.5f, -0.5f);
        neighbor = CheckForNeightbors(hit);
        directNeighbors[2] = neighbor;
        if (neighbor)
        {
            neighbor.directNeighbors[3] = this;
        }
        //check west
        hit = Physics2D.Raycast((Vector2)transform.position + offset, Vector2.left, 1f, type.buildingsMask, -1.5f, -0.5f);
        neighbor = CheckForNeightbors(hit);
        directNeighbors[3] = neighbor;
        if (neighbor)
        {
            neighbor.directNeighbors[2] = this;
        }
        GetComponent<Collider2D>().enabled = true;

        //directNeighbors[4] = this;
        //PreTick();
        if (bar && type.resourceTypeIn.Length > 0)
        {
            Vector3 scale = bar.transform.localScale;
            scale.x = ((float)resourcesIn[(int)type.resourceTypeIn[0]]) / MaxInOfType(type.resourceTypeIn[0]);
            bar.transform.localScale = scale;
        }
    }

    void Start()
    {
        text.text = "" + (level + 1);
        canvas.enabled = true;
    }

    private Building CheckForNeightbors(RaycastHit2D hit)
    {
        if (hit.collider)
        {
            if (hit.collider.GetComponent<Building>())
            {
                return hit.collider.GetComponent<Building>();
            }
        }
        return null;
    }

    void FixedUpdate()
    {
        PreTick();
    }

    public virtual void PreTick()
    {
        lastResourcesIn = resourcesIn;
        Tick();
    }

    public virtual void Tick()
    {

    }

    public virtual int MaxInOfType(ResourceTypes rType)
    {
        /*if (!type.bResourceTypeIn[(int)rType])
        {
            return 0;
        }//*/
        return type.resourceInTypeMaxes[(int)rType] + (type.resourceTypeMaxesIncreasePerLevel[(int)rType] * level);
    }

    public virtual void PostTick()
    {
        PostTick2();
        //StartCoroutine(PostTick2());
    }

    public virtual void PostTick2()
    {
        if (bar && type.resourceTypeIn.Length > 0)
        {
            Vector3 scale = bar.transform.localScale;
            scale.x = (resourcesIn[(int)type.resourceTypeIn[0]]) / MaxInOfType(type.resourceTypeIn[0]);
            bar.transform.localScale = scale;
        }
        //TODO Change this to reset to 0s to save on GC
        resourcesIn = new float[(int)ResourceTypes.Last];


        RedistributeResources();
    }

    protected void RedistributeResources()
    {
        //reallocate resources
        float[] requests = new float[type.resourceTypeOut.Length];
        for (int i = 0; i < directNeighbors.Length; i++)
        {
            if (directNeighbors[i])
            {
                for (int i2 = 0; i2 < type.resourceTypeOut.Length; i2++)
                {
                    if (directNeighbors[i].type.bResourceTypeIn[(int)type.resourceTypeOut[i2]])
                    {
                        if (dynamicDistrbution)
                        {
                            if(!directNeighbors[i].dynamicDistrbution)
                            requests[i2] += directNeighbors[i].MaxInOfType(type.resourceTypeOut[i2]) - directNeighbors[i].resourcesIn[(int)type.resourceTypeOut[i2]];
                        }
                        else
                        {
                            requests[i2]++;
                        }
                    }
                }
            }
        }
        if (dynamicDistrbution)
        {
            lastOutputs = "";
        }
        for (int i2 = 0; i2 < type.resourceTypeOut.Length; i2++)
        {
            if (!dynamicDistrbution)
            {
                for (int i = 0; i < directNeighbors.Length; i++)
                {
                    if (directNeighbors[i] && directNeighbors[i].type.bResourceTypeIn[(int)type.resourceTypeOut[i2]] && requests[i2] > 0.001f)
                    {
                        directNeighbors[i].resourcesIn[(int)type.resourceTypeOut[i2]] = Mathf.Clamp(
                            directNeighbors[i].resourcesIn[(int)type.resourceTypeOut[i2]] + (resourcesOut[(int)type.resourceTypeOut[i2]] / requests[i2])
                            , 0, directNeighbors[i].MaxInOfType(type.resourceTypeOut[i2]));
                    }
                }
            }
            else
            {
                float rSum = 0;
                for (int i = 0; i < directNeighbors.Length; i++)
                {
                    if (directNeighbors[i] && !directNeighbors[i].dynamicDistrbution && directNeighbors[i].type.bResourceTypeIn[(int)type.resourceTypeOut[i2]] && requests[i2] > 0.01f)
                    {
                        float r = ((((float)(directNeighbors[i].MaxInOfType(type.resourceTypeOut[i2]) - directNeighbors[i].resourcesIn[(int)type.resourceTypeOut[i2]])) / requests[i2]) * resourcesOut[(int)type.resourceTypeOut[i2]]);
                        requests[i2] -= directNeighbors[i].MaxInOfType(type.resourceTypeOut[i2]) - directNeighbors[i].resourcesIn[(int)type.resourceTypeOut[i2]];
                        r = Mathf.Clamp(r, 0, directNeighbors[i].MaxInOfType(type.resourceTypeOut[i2]) - directNeighbors[i].resourcesIn[(int)type.resourceTypeOut[i2]]);
                        rSum += r;
                        directNeighbors[i].resourcesIn[(int)type.resourceTypeOut[i2]] += r;
                        resourcesOut[(int)type.resourceTypeOut[i2]] -= r;
                    }
                }
                lastOutputs = lastOutputs + Mathf.RoundToInt(rSum) + " Direct " + type.resourceTypeOut[i2].ToString() + "\n";
            }
        }
        if (dynamicDistrbution)
        {
            for (int i = 0; i < type.resourceTypeOut.Length; i++)
            {
                if (type.bResourceTypeIn[(int)type.resourceTypeOut[i]])
                {
                    indirectResourcesIn[(int)type.resourceTypeOut[i]] = resourcesOut[(int)type.resourceTypeOut[i]];
                }
            }

            requests = new float[type.resourceTypeOut.Length];
            float[] maxes = new float[type.resourceTypeOut.Length];
            for (int i2 = 0; i2 < type.resourceTypeOut.Length; i2++)
            {
                if (type.bResourceTypeIn[(int)type.resourceTypeOut[i2]])
                {
                    if (dynamicDistrbution)
                    {
                        requests[i2] += indirectResourcesIn[(int)type.resourceTypeOut[i2]];
                        maxes[i2] += MaxInOfType(type.resourceTypeOut[i2]);
                    }
                }
            }
            for (int i = 0; i < directNeighbors.Length; i++)
            {
                if (directNeighbors[i])
                {
                    for (int i2 = 0; i2 < type.resourceTypeOut.Length; i2++)
                    {
                        if (directNeighbors[i].type.bResourceTypeIn[(int)type.resourceTypeOut[i2]] && type.bResourceTypeIn[(int)type.resourceTypeOut[i2]])
                        {
                            if (directNeighbors[i].dynamicDistrbution)
                            {
                                requests[i2] += directNeighbors[i].indirectResourcesIn[(int)type.resourceTypeOut[i2]];
                                maxes[i2] += directNeighbors[i].MaxInOfType(type.resourceTypeOut[i2]);
                            }
                        }
                    }
                }
            }
            for (int i2 = 0; i2 < type.resourceTypeOut.Length; i2++)
            {
                if (type.bResourceTypeIn[(int)type.resourceTypeOut[i2]])
                {
                    if (dynamicDistrbution)
                    {
                        indirectResourcesIn[(int)type.resourceTypeOut[i2]] = (requests[i2] / maxes[i2]) * MaxInOfType(type.resourceTypeOut[i2]);
                    }
                }
            }
            for (int i = 0; i < directNeighbors.Length; i++)
            {
                if (directNeighbors[i])
                {
                    for (int i2 = 0; i2 < type.resourceTypeOut.Length; i2++)
                    {
                        if (directNeighbors[i].type.bResourceTypeIn[(int)type.resourceTypeOut[i2]] && type.bResourceTypeIn[(int)type.resourceTypeOut[i2]])
                        {
                            if (directNeighbors[i].dynamicDistrbution)
                            {
                                directNeighbors[i].indirectResourcesIn[(int)type.resourceTypeOut[i2]] = (requests[i2] / maxes[i2]) * directNeighbors[i].MaxInOfType(type.resourceTypeOut[i2]); ;
                            }
                        }
                    }
                }
            }
            
        }
    }

    public void OnMouseUpAsButton()
    {
        if (true)//Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftCommand) || UpgradeMode.bUpgradeMode)
        {
            if (BuildCamera.resources >= type.baseUpgradeCost + (type.aditionalUpgradeCost * level))
            {
                BuildCamera.resources -= type.baseUpgradeCost + (type.aditionalUpgradeCost * level);
                level++;
                text.text = "" + (level + 1);
            }
        }
    }

    public void OnMouseEnter()
    {
        InfoPanel.info = this;
    }

    public void OnMouseExit()
    {
        InfoPanel.info = null;
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        /*if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftCommand) || UpgradeMode.bUpgradeMode)
        {
            canvas.enabled = true;
            text.text = "" + level;
        }
        else
        {
            canvas.enabled = false;
        }//*/
    }

    public virtual string GetName()
    {
        return "@@@";
    }

    public virtual int GetCost()
    {
        return 0;
    }

    public virtual string GetDiscription()
    {
        return "@@@";
    }

    public virtual int GetUpgradeCost()
    {
        return 0;
    }

    public virtual string GetInputs()
    {
        return "@@@";
    }

    public virtual string GetOutputs()
    {
        return lastOutputs;
    }


    public virtual string GetUpgradesInfo()
    {
        return "@@@";
    }
}
