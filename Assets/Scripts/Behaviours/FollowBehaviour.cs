using UnityEngine;using System.Collections;public class FollowBehaviour : MonoBehaviour {    private GameObject[] followTargetObject;    private Rigidbody2D swarmBody;    private FuelMonitor fuelObject;    public float maxFollowForce = 50f;    public float maxFollowDistance = 5f;	    // Use this for initialization	void Start ()    {        swarmBody = GetComponent<Rigidbody2D>();        followTargetObject = GameObject.FindGameObjectsWithTag("SwarmElements");        fuelObject = GetComponent<FuelMonitor>();    }		// Update is called once per frame	void Update ()    {
        for (int i=0; i< followTargetObject.Length; i++)        {
            if (Vector2.Distance(swarmBody.transform.position, followTargetObject[i].transform.position) < maxFollowDistance)
            {
                Vector3 followForce = Vector3.Lerp(swarmBody.transform.position, followTargetObject[i].transform.position, maxFollowDistance - Vector2.Distance(swarmBody.transform.position, followTargetObject[i].transform.position)) - swarmBody.transform.position;
                swarmBody.AddForce(followForce);
                fuelObject.updateFuel(Mathf.Abs(Vector3.Distance(Vector3.zero, followForce)));
            }
        }
    }}