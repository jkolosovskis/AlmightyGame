using UnityEngine;
using System.Collections;

public class ConsumeBehaviour : MonoBehaviour {

    HingeJoint2D hinge;
	// Use this for initialization
	void Start () {
        hinge = this.gameObject.GetComponent<HingeJoint2D>();
    }
	
	// Update is called once per frame
	void Update () {
	    // Do nothing
	}
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collision with a consumable object detected");
        if (other.gameObject.CompareTag("SwarmElements")){
            other.gameObject.GetComponent<AttributesManager>().setConsumedBody(this.gameObject);
            hinge.enabled = true;
            hinge.connectedBody = other.gameObject.GetComponent<Rigidbody2D>();
            hinge.connectedAnchor = Vector2.zero;
        }
    }
}
