using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResearchToText : MonoBehaviour {

	// Use this for initialization
	void Update () {
        GetComponent<Text>().text = BuildCamera.research + "";
    }
}
