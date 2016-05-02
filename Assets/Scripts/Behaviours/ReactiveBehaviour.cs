using UnityEngine;
using System.Collections;

public class ReactiveBehaviour : MonoBehaviour {
    private FuelMonitor fuelMonitor;
    public float reactiveForceMultiplier = 5f;
    public Vector2 reactiveForce;
    public float dotProduct;
    public float bodyVelocity;
    private bool isPositiveRotation = false;

    // Use this for initialization
    private Vector2 rotatePlus90(Vector2 source){
        Vector2 result = new Vector2(source.y * -1f, source.x);
        return result;
    }
    private Vector2 rotateMinus90(Vector2 source){
        Vector2 result = new Vector2(source.y, source.x * -1f);
        return result;
    }
    void Start () {
        fuelMonitor = this.gameObject.GetComponent<FuelMonitor>();
	}
	
	void FixedUpdate () {
        // Here we retrieve the total external forces acted on this swarm object
        // and evaluate if the forces are acting against the current movement direction.
        dotProduct = Vector2.Dot(this.gameObject.GetComponent<Rigidbody2D>().velocity, fuelMonitor.getExternalForces());
        bodyVelocity = this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
        if (dotProduct < bodyVelocity) {
            // Now that we have determined that we need to react to an external force,
            // we determine which direction to turn to to more effectively avoid the external force
            if (Vector2.Dot(rotatePlus90(fuelMonitor.getExternalForces()), this.gameObject.GetComponent<Rigidbody2D>().velocity) >
                Vector2.Dot(rotateMinus90(fuelMonitor.getExternalForces()), this.gameObject.GetComponent<Rigidbody2D>().velocity))
                isPositiveRotation = true;
            else isPositiveRotation = false;
            // Now we apply either +90 degrees shift or -90 degrees shift depending on last result.
            if (isPositiveRotation){
                reactiveForce = rotatePlus90(fuelMonitor.getExternalForces());
            }
            else reactiveForce = rotateMinus90(fuelMonitor.getExternalForces());
            reactiveForce = reactiveForce * reactiveForceMultiplier;
            fuelMonitor.AddForce(reactiveForce, false);
            fuelMonitor.updateFuel(reactiveForce.magnitude);
        }
    }
}
