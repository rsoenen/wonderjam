using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class RobotGestionPoint : MonoBehaviour {

    public delegate void PointChangedEventHandler(int _old, int _new);
    public event PointChangedEventHandler PointChanged;

    public delegate void PowerupChangedEventHandler(PickupPower _power);
    public event PowerupChangedEventHandler PowerupChanged;

    private int point;
    
    private float timerPoints;
    private int playerId;
    private GameObject barrePointMax;

    private PickupPower powerup;

    public PickupPower Powerup
    {
        get { return powerup; }
        set
        {
            powerup = value;
            OnPowerupChanged(powerup);
        }
    }

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
       if (timerPoints < 0.5f)
       {
           timerPoints += Time.deltaTime;
       }
		else 
       {
			if (GetComponent<RobotController>().enabled)
			{
				reducePoint(1);

				Debug.Log(GetComponentsInChildren<LightningBolt>().Length);
	       		foreach(LightningBolt bolt in GetComponentsInChildren<LightningBolt>())
				{
					float distanceMax = bolt.maxDistance;
					float distance = Vector3.Distance(bolt.emitter.position,transform.position);

					if (distance < distanceMax/3)
					{
						Point +=4;
					}
					else if (distance < distanceMax*2/3)
					{
						Point +=3;
					}
					else
					{
						Point+=2;
					}
				}
           		
			}
           timerPoints = 0;
       }

       if (Point >= 100)
       {
           Point = 100;
       }
		else if (Point < 0)
		{
			Point = 0;
		}
	}

    public void reducePoint(int _numberPointLess)
    {
        if (_numberPointLess > this.point)
        {
            this.Point = 0;
			if (GetComponent<EnergyLossBehaviour>()==null)
			{
            gameObject.AddComponent<EnergyLossBehaviour>();
			}
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

    public void ActivatePowerup()
    {
        if(Powerup != null)
        {
            Powerup.Activate(this);
            Powerup = null;
        }
    }

    private void OnPointChanged(int _old, int _new)
    {
        if (PointChanged != null)
            PointChanged(_old, _new);
    }

    private void OnPowerupChanged(PickupPower _power)
    {
        if (PowerupChanged != null)
            PowerupChanged(_power);
    }
    public GameObject getOwner()
    {
        return this.gameObject;
    }

}
