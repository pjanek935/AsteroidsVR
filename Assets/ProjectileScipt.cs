using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScipt : MonoBehaviour {

    public float speed = 10f;

    private bool active = false;
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
        if (active)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    public void Shoot()
    {
        gameObject.SetActive(true);
        active = true;
    }

    public void Shoot(Transform parent)
    {
        Shoot();
        transform.position = parent.position;
        transform.rotation = parent.rotation;
    }

    public void DestroyProjectile()
    {
        active = false;
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer != playerLayer)
            DestroyProjectile();
    }

    private void OnCollisionExit(Collision collision)
    {
      
    }


}
