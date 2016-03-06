using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class widthHUD : MonoBehaviour {

	// Use this for initialization
	void Start () {

        float widthScreen = Screen.width;
        GameObject.Find("Background").GetComponent<RectTransform>().position = new Vector3(.25f * widthScreen, 0.40f * widthScreen, 0);
        GameObject.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.40f, widthScreen * 0.40f);

        


        
	}
	
	// Update is called once per frame
	void Update () {
        float widthScreen = Screen.width;
        GameObject.Find("Background").GetComponent<RectTransform>().position = new Vector3(.25f * widthScreen, 0.30f * widthScreen, 0);
        GameObject.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.40f, widthScreen * 0.40f);

        GameObject.Find("MenuButtons").GetComponent<RectTransform>().position = new Vector3(.24f * widthScreen, 0.26f * widthScreen, 0);
        GameObject.Find("MenuButtons").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.25f, widthScreen * 0.25f);
	}
}
