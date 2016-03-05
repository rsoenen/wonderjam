using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class RobotGestionPoint : MonoBehaviour {

    public delegate void PointChangedEventHandler(int _old, int _new);
    public event PointChangedEventHandler PointChanged;

    private int point;
    private float timerPoints;
    private int playerId;
    private GameObject barrePointMax;

    public int Point
    {
        get { return point; }
        private set
        {
            if (point != value)
            {
                int old = point;
                point = value;
                OnPointChanged(old, point);
            }
        }
    }

    // Use this for initialization
    void Start () {
        Point = 0;
        timerPoints = 0f;
        this.playerId = this.GetComponent<RobotController>().playerId;
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
           
           float distance = GameManager.Instance().lengthTotemRobot(this.gameObject);
           if (distance < 3)
           {
               Point += 3;
           }
           if (distance >= 3 && distance < 6)
           {
               Point += 2;
           }
           if (distance >= 6 && distance < 10)
           {
               Point++;
           }
           timerPoints = 0;
       }

       if (Point >= 100)
       {
           Point = 100;
       }
	}



    public void reducePoint(int _numberPointLess)
    {
        if (_numberPointLess > this.point)
        {
            this.Point = 0;
        }
        else
        {
            this.Point -= _numberPointLess;
        }
    }
    public int getPoint()
    {
        return this.Point;
    }

    private void OnPointChanged(int _old, int _new)
    {
        if (PointChanged != null)
            PointChanged(_old, _new);
    }
}
