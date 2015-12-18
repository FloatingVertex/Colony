using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LockViewToggle : MonoBehaviour {

    public static bool locaView;

	public void Toggle()
    {
        locaView = GetComponent<Switch>().isOn;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.D))
        {
            locaView = locaView ? false : true;
            GetComponent<Switch>().isOn = locaView;
        }
    }
}
