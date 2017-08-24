using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    public new Camera camera;
    public Transform bulletSpawnLeft;
    public Transform bulletSpawnRight;
    public float baseSpeed = 10;
    public float angleThreshold = 30;
    public float turnSpeed = 0.01f;
    public float fireRate = 1;

    private ProjectileManager projectileManager;
    private bool shootSide = false;
    private bool canShoot = true;
    private float shootCounter = 0;
    private Transform bulletSpawnRoot;

    // Use this for initialization
    void Start () {
        projectileManager = GameObject.Find("GameManager").GetComponent<ProjectileManager>();
        bulletSpawnRoot = bulletSpawnLeft.parent;
	}
	
	// Update is called once per frame
	void Update () {

        //TestControls();

        MoveForward();
        Turn();
        Shoot();
        RotateBulletSpawns();
    }

    private void TestControls()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward * vertical * baseSpeed * Time.deltaTime;

        transform.position += forward;
    }

    private void RotateBulletSpawns()
    {
        bulletSpawnRoot.rotation = camera.transform.rotation;
        RaycastHit hit;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        Vector3 pointToLookAt = camera.transform.position + camera.transform.forward * 1000;
        if (Physics.Raycast(ray, out hit))
        {
            float distance = Vector3.Distance(camera.transform.position, hit.point);
            if (distance > 0.1)
                pointToLookAt = hit.point;
        }
        bulletSpawnLeft.LookAt(pointToLookAt);
        bulletSpawnRight.LookAt(pointToLookAt);
    }
    
    void Shoot()
    {
        if (canShoot && Input.GetButton("Fire1"))
        {
            if (shootSide)
                projectileManager.Shoot(bulletSpawnLeft);
            else
                projectileManager.Shoot(bulletSpawnRight);
            shootSide = !shootSide;
            canShoot = false;
            shootCounter = 0;
        }

        shootCounter += Time.deltaTime * fireRate;
        if (shootCounter >= 1)
        {
            shootCounter = 0;
            canShoot = true;
        }
    }

    public void ShootFromClicker()
    {
        if (canShoot)
        {
            if (shootSide)
                projectileManager.Shoot(bulletSpawnLeft);
            else
                projectileManager.Shoot(bulletSpawnRight);
            shootSide = !shootSide;
            canShoot = false;
            shootCounter = 0;
        }
    }

    void MoveForward()
    {
        transform.position += transform.forward * baseSpeed * Time.deltaTime;
    }

    void Turn()
    {
        float angle = Vector3.Angle(camera.transform.forward, transform.forward);
        if (angle > angleThreshold)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, 
                camera.transform.rotation, Time.deltaTime * turnSpeed);
        }

    }
}
