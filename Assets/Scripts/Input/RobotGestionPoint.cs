using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class RobotGestionPoint : MonoBehaviour {

    private int point;
    private float timerPoints;
    private int playerId;
    private int idHUD;
    private GameObject barrePointMax;

	// Use this for initialization
	void Start () {
        point = 0;
        timerPoints = 0f;
        this.playerId = this.GetComponent<RobotController>().playerId;
        
        idHUD = playerId + 1;
        barrePointMax = GameObject.Find("fondPointJoueur" + idHUD);
	}
	
	// Update is called once per frame
	void Update () {


        //On rajoute 1 point toutes les secondes au robot
       if (timerPoints < 1f)
       {
           timerPoints += Time.deltaTime;
       }
       else
       {
           point = point + 1;
           timerPoints = 0;
       }
       GameObject.Find("textPointJoueur" + idHUD).GetComponent<Text>().text= point+"/100";



       GameObject.Find("pointJoueur" + idHUD).GetComponent<RectTransform>().sizeDelta = new Vector2(95 * (100-point) / 100, 15);
       GameObject.Find("pointJoueur" + idHUD).GetComponent<RectTransform>().position = new Vector3(barrePointMax.GetComponent<RectTransform>().position.x + point / 2, barrePointMax.GetComponent<RectTransform>().position.y, 0);
	}



    public void reducePoint(int _numberPointLess)
    {
        if (_numberPointLess > this.point)
        {
            this.point = 0;
        }
        else
        {
            this.point -= _numberPointLess;
        }
    }
    public int getPoint()
    {
        return this.point;
    }
}
