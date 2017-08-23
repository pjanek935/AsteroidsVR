using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {

    public Transform target;
    public bool rotate = false;

    private Vector3 threshold;

	// Use this for initialization
	void Start () {
        threshold = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () {
        Follow();
	}

    void Follow()
    {
        transform.position = target.transform.position + threshold;
        if (rotate)
            transform.rotation = Quaternion.LookRotation(target.forward);
    }
}
