using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionEffect : MonoBehaviour {
    private bool isExplosionComplete = false;
    private float explosionFuel = 20f;
    private float shockwaveScaleFactor = 0.5f;
    private float shockwaveScale;
    private float shockwavePropagationModifier = 3f;
    private int requestCounter = 1;
    private List<bool> explosionTargetEffectCompleted = new List<bool>();
    public float explosionForceMultiplier = 100f;

    private GameObject shockwave;
    private GameObject[] explosionTargetObject;

    private float shockwaveCurrentScale = 0f;
    private float shockwaveCurrentOpacity = 1f;

    public void setExplosionFuel(float fuel){
        // Note that here we set a minimum explosion magnitude equal to 20 fuel units.
        Debug.Log("Shockwave explosion effect script received fuel level request of " + fuel.ToString() + " Request number " + requestCounter.ToString());
        if (fuel > explosionFuel) explosionFuel = fuel;
        Debug.Log("Shockwave explosion requested for fuel level " + explosionFuel.ToString() + " Request number " + requestCounter.ToString());
        requestCounter++;
    }
	void Start () {
        // First, we calculate the maximum shockwave size based on explosion fuel.
        shockwave = this.gameObject.transform.GetChild(0).gameObject;
        // Before any further logic is executed, we want to draw the explosion shockwave as transparent
        shockwave.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, shockwaveCurrentOpacity);
        shockwaveScale = Mathf.Sqrt(Mathf.Sqrt(explosionFuel)) * shockwaveScaleFactor;
        Debug.Log("Shockwave size determined as " + shockwaveScale.ToString());
        //Finally we search for swarm objects in the scene and add a force proportional to the shockwave total energy and its propagation
        explosionTargetObject = GameObject.FindGameObjectsWithTag("SwarmElements");
        for (int i = 0; i < explosionTargetObject.Length; i++){
            explosionTargetEffectCompleted.Add(false);
        }
    }
	
	void FixedUpdate () {
        // Increase the shockwave sprite scale until maximum scale is reached
        // Also, reduce the opacity of the sprite according to maximum explosion scale
        if (isExplosionComplete == false){
            // Calculate the current scale and opacity of the shockwave sprite:
            shockwaveCurrentScale += (Time.fixedDeltaTime * shockwavePropagationModifier);
            shockwaveCurrentOpacity = Mathf.Abs(1f - shockwaveCurrentScale / shockwaveScale);
            shockwave.transform.localScale = Vector3.one * shockwaveCurrentScale;
            shockwave.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, shockwaveCurrentOpacity);
            // Next we add shockwave force to all affected swarm objects
            for (int i = 0; i < explosionTargetObject.Length; i++){
                // First, let's see if any objects with swarm element tags are in range...
                if (!(explosionTargetObject[i] == null) & explosionTargetEffectCompleted[i] == false){
                    // Precalculate distance between two bodies in question for later use:
                    float distBetweenObjs = Vector2.Distance(gameObject.transform.position, explosionTargetObject[i].transform.position);
                    // Check if any affectable bodies are within maximum range
                    if (distBetweenObjs <= shockwaveCurrentScale){
                        // Determine distance vector between two bodies:
                        Vector3 explosionForce = (gameObject.transform.position - explosionTargetObject[i].transform.position);
                        // Then normalise the distance vector to ensure max range does not affect max force:
                        explosionForce.Normalize();
                        // After normalisation, we apply distance scaling (the closer the distance, the higher the force):
                        explosionForce = explosionForce * (1 - (distBetweenObjs / shockwaveScale)) * -1f;
                        // Finally, we apply our predefined multiplier of explosion interaction
                        explosionForce = explosionForce * shockwaveScale * explosionForceMultiplier;
                        // Now that we have calculated the precise force, we apply it to the target object.
                        explosionTargetObject[i].GetComponent<FuelMonitor>().AddForce(explosionForce, true);
                        Debug.Log("Passed force equal to " + explosionForce.ToString() + " to a swarm object");
                        float explosionDamageCoefficient = explosionTargetObject[i].GetComponent<AttributesManager>().absorptionCoefficient;
                        explosionTargetObject[i].GetComponent<DamageCounter>().addDamage(explosionForce.magnitude * explosionDamageCoefficient);
                        explosionTargetEffectCompleted[i] = true;
                    }
                }
            }
            // Finally, we check if the shockwave has fully died down.
            if (shockwaveCurrentScale >= shockwaveScale){
                isExplosionComplete = true;
                shockwave.SetActive(false);
            }
        }
	}
}
