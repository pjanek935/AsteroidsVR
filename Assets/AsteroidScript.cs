using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour {

    public bool big = false;
    public float forceFactor = 10;

    private AsteroidManager asteroidManager;
    private int projectileLayer = 0;

	// Use this for initialization
	void Start () {
        asteroidManager = GameObject.Find("GameManager").GetComponent<AsteroidManager>();
        projectileLayer = LayerMask.NameToLayer("Projectile");

        if(big)
            AddForceTowardsCenter();
	}

    private void AddForceTowardsCenter()
    {
        Vector3 center = GameObject.Find("SpawnOrigin").transform.position;
        Vector3 forceDirection = center - transform.position;
        forceDirection.Normalize();
        GetComponent<Rigidbody>().AddForce(forceDirection * forceFactor, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void DestroyAsteroid()
    {
        if (big)
        {
            if(asteroidManager == null)
                asteroidManager = GameObject.Find("GameManager").GetComponent<AsteroidManager>();
            asteroidManager.SpawnSmallAsteroids(transform.position);
        }
            
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        if(big)
            AddForceTowardsCenter();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == projectileLayer)
            DestroyAsteroid();
    }
}
