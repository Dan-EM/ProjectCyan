using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrionFollow : MonoBehaviour {
    public float followUntil;
    public float speed;

    private Transform target;

	// Use this for initialization
	void Start () {
        target = GameObject.Find("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if(Vector2.Distance(transform.position, target.position) > followUntil){
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
		
	}
}
