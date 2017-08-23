using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour {

    public GameObject projectile;
    public int projectilesCount = 20;

    private List<GameObject> projectiles;
    private int projectilePointer = 0;

	// Use this for initialization
	void Start () {
        projectiles = new List<GameObject>();
        for (int i = 0; i < projectilesCount; i++)
            AddProjectile();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddProjectile()
    {
        projectiles.Add(Instantiate(projectile) as GameObject);
    }

    public void Shoot(Transform parent)
    {
        projectiles[projectilePointer].GetComponent<ProjectileScipt>().Shoot(parent);
        projectilePointer++;
        if (projectilePointer >= projectilesCount)
            projectilePointer = 0;
    }
}
