using UnityEngine;
using System.Collections;

public class InertialBehaviour : MonoBehaviour {
    public float inertialCoefficient = 1.0f;
    // Logic for intertial behaviour is implemented in fuel monitoring script
    // since it handles all force application requests.
    public void setInertialCoefficient(float value) {
        inertialCoefficient = value;
    }
    public float getInertialCoefficient(){
        return inertialCoefficient;
    }
}
