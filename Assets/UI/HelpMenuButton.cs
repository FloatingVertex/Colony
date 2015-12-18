using UnityEngine;
using System.Collections;

public class HelpMenuButton : MonoBehaviour {

    public static bool bInHelpMenu = false;

	public void SetMenuVisible(bool show)
    {
        GetComponent<Canvas>().enabled = show;
        bInHelpMenu = show;
    }
}
