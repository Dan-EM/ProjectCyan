using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    //boundaries for the camera  
    [SerializeField]//makes private variables visible + declarable in inspector.
    private float xMax;
    [SerializeField]
    private float xMin;
    [SerializeField]
    private float yMax;
    [SerializeField]
    private float yMin;

    public float buffer;

    //player is target of camera
    private Transform target;

    // Use this for initialization
    void Start () {
        //player is the target of camera 
        target = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        //vector 3 and z position are used to ensure the camera remain at default/set z position
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y + buffer, yMin, yMax), transform.position.z);
	}
}
