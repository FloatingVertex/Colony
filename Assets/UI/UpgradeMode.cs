using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradeMode : MonoBehaviour {

    public static bool bUpgradeMode;

	// Use this for initialization
	public void Toggled()
    {
        bUpgradeMode = GetComponent<Switch>().isOn;
    }
}
