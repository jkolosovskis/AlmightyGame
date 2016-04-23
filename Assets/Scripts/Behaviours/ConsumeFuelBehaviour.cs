using UnityEngine;
using System.Collections;

public class ConsumeFuelBehaviour : MonoBehaviour
{
    public float fuelAmount = 500f;
    // Use this for initialization
    void Start(){
        // Do nothing
    }

    // Update is called once per frame
    void Update(){
        // Do nothing
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("SwarmElements")){
            other.gameObject.GetComponent<FuelMonitor>().updateFuel(0f - fuelAmount);
            Destroy(this.gameObject);
        }
    }
}
