using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScipt : MonoBehaviour {

    public float speed = 10f;

    private int playerLayer = 0;

	// Use this for initialization
	void Start () {
        DestroyProjectile();
        playerLayer = LayerMask.NameToLayer("Player");
	}
	
	// Update is called once per frame
	void Update () {
        MoveForward();
	}

    void MoveForward()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void Shoot()
    {
        gameObject.SetActive(true);
    }

    public void Shoot(Transform parent)
    {
        Shoot();
        transform.position = parent.position;
        transform.rotation = parent.rotation;
    }

    public void DestroyProjectile()
    {
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer != playerLayer)
            DestroyProjectile();
    }
}
