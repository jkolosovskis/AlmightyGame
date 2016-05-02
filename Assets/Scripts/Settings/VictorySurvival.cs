﻿using UnityEngine;
using System.Collections;

public class VictorySurvival : MonoBehaviour {
    public int survivalPerc = 50;
    public float survivalTime = 60f;
    public float survivalTimeCounter = 0f;

    private GameObject[] swarmObjects;
    private int timeDivisor = 50;
    private int timeCounter = 0;
    private bool firstTimeCheckCompleted = false;

    void survivalCheck() {
        // Here we check if the player managed to keep X percent of all swarm elements alive.
        // First we count the number of swarm elements still alive
        int numAliveElements = 0;
        for (int i = 0; i < swarmObjects.Length; i++) {
            if (swarmObjects[i] != null) numAliveElements++;
        }
        Debug.Log("Number of detected alive elements: " + numAliveElements.ToString());
        // Now that we have the number of swarm elements alive, we check if the percentage of alive elements
        // equals or exceeds the victory target.
        float aliveElementsPerc = numAliveElements / swarmObjects.Length * 100;
        if (aliveElementsPerc >= survivalPerc) {
            // Insert victory message here
            Debug.Log("Survival victory conditions satisfied");
        }
        else {
            // Insert defeat message here
            Debug.Log("Survival victory conditions failed");
        }
    }
    void firstTimeEvaluate() {
        swarmObjects = GameObject.FindGameObjectsWithTag("SwarmElements");
        firstTimeCheckCompleted = true;
    }

    // In FixedUpdate we perform a first time evaluation on how many swarm elements we shall monitor
    // and after expiration of the survival time check if the defined percentage of swarm elements
    // are still present in the scene.
    void Start() {
        // Do nothing
    }
    void FixedUpdate () {
        timeCounter++;
        if (timeCounter == timeDivisor) {
            timeCounter = 0;
            survivalTimeCounter++;
            if (firstTimeCheckCompleted == false) firstTimeEvaluate();
            if (survivalTimeCounter >= survivalTime) survivalCheck();
        }
	}
}
