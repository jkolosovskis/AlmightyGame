using UnityEngine;
using System.Collections;

public class PathOverlayManager : MonoBehaviour {
    public int updateConst = 50;
    private GameObject pathMarker;
    private GameObject temp;
    private int tick = 0;
    public bool isActivated = false;
    public void toggleStatus () {
        isActivated = !isActivated;
    }
	void Start () {
        //Here we load from prefabs the direction indicator sprite and prepare it for deployment
        temp = (GameObject)Resources.Load("prefabs/Overlays/PathMarker");        
    }
    private void drawNextMarker() {
        GameObject[] listOfSwarmObjects = GameObject.FindGameObjectsWithTag("SwarmElements");
        if (listOfSwarmObjects.Length > 0) {
            for (int i = 0; i < listOfSwarmObjects.Length; i++) {
                if (listOfSwarmObjects[i].GetComponent<Rigidbody2D>().velocity.magnitude > 0) {
                    pathMarker = (GameObject)Instantiate(temp, listOfSwarmObjects[i].GetComponent<Rigidbody2D>().position, Quaternion.identity);
                    Quaternion velocityAngle = Quaternion.LookRotation(Vector3.forward,(Vector3)listOfSwarmObjects[i].GetComponent<Rigidbody2D>().velocity);
                    pathMarker.transform.rotation = velocityAngle;
                    pathMarker.GetComponent<SpriteRenderer>().sortingLayerName = "PathOverlay";
                }             
            }
        } 
    }
	void FixedUpdate () {
        //Here we tick time between updates and call for new markers to be drawn if the designated number of ticks have expired
        tick++;
        if(tick > updateConst) {
            tick = 0;
            drawNextMarker();
        }
	}
}
