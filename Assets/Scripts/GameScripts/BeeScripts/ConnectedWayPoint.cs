using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NPCPATROL POINT
public class ConnectedWayPoint : Waypoint
{

    [SerializeField]
    protected float _connecttivityRadius = 50f;
    //list od connections
    List<ConnectedWayPoint> _connections;

    // Start is called before the first frame update
    void Start()
    {
        //Get all waypoints in the scene
        GameObject[] wayPoints = GameObject.FindGameObjectsWithTag("Waypoint");

        //create a list of waypoints
        _connections = new List<ConnectedWayPoint>();

        for(int i = 0; i< wayPoints.Length; i++){
            //check if they are connected to a way point
            ConnectedWayPoint nextWaypoint = wayPoints[i].GetComponent<ConnectedWayPoint>();
            //way point find
            if(nextWaypoint != null){

                if(Vector3.Distance(this.transform.position, nextWaypoint.transform.position)<= _connecttivityRadius && nextWaypoint != this)
                {
                    _connections.Add(nextWaypoint);
                }

            }

        }
    }

     public virtual void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, drawRadius);

        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _connecttivityRadius);
    
    }

    public ConnectedWayPoint NextWaypoint (ConnectedWayPoint previousWaypoint)
    {
        if(_connections.Count ==0){
            //No way points
            Debug.Log("Insuffiecient waypoint count");
            return null;
        }
        else if(_connections.Count ==1 && _connections.Contains(previousWaypoint))
        {
            //only one waypoint and its the previous one
            return previousWaypoint;
        }
        else // find a random waypoint that isnt the pervious one
        {
            ConnectedWayPoint nextWayPoint;
            int nextIdex = 0;

            do{
                nextIdex = UnityEngine.Random.Range(0, _connections.Count);
                nextWayPoint = _connections[nextIdex];
            } while(nextWayPoint == previousWaypoint);

            return nextWayPoint;
        }
    }
}
