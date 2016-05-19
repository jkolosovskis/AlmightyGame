using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class VictoryWork: MonoBehaviour {
    private List<GameObject> consumablesList = new List<GameObject>();
    private CircleCollider2D radiusMonitor;
    private VictoryManager victoryManager;
    private int timeCounter = 0;
    private bool alreadyFinishedFlag = false;

    public float workTargetAreaRadius = 2f;
    public int workTargetPercent = 50;


	// Use this for initialization
	void Start () {
        // Here we build an array of all consumables that we are supposed to move.
        consumablesList = GameObject.FindGameObjectsWithTag("WorkTargetObjects").ToList();
        radiusMonitor = gameObject.GetComponent<CircleCollider2D>();
        radiusMonitor.radius = workTargetAreaRadius;
        victoryManager = gameObject.GetComponent<VictoryManager>();
    }

	void FixedUpdate () {
        // Here we examine each second if the desired percentage of work target objects are within their designated
        // target area.
        timeCounter++;
        if (timeCounter >= 50) {
            timeCounter = 0;
            checkWorkCompleted();
        }
	}
    private void checkWorkCompleted() {
        // Here we get the number of work objects in designated target area and comapre the count to target count
        Collider2D[] workObjectsInTarget = Physics2D.OverlapCircleAll(radiusMonitor.transform.position, 
                                                                      workTargetAreaRadius,
                                                                      LayerMask.NameToLayer("WorkObjLayer"));
        if (workObjectsInTarget.Length / consumablesList.Count * 100 < workTargetPercent
            & alreadyFinishedFlag == false) {
            // Not enough work objects in target area - do nothing
        }
        else if (alreadyFinishedFlag == false) {
            // Sufficient amount of objects detected - victory!
            victoryManager.notifyWork(true);
            Debug.Log("Work victory conditions satisfied");
            alreadyFinishedFlag = true;
        }
    }
}
