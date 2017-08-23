using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {

    public Transform spawnOrigin;
    public Vector3 radius = new Vector3(30, 10, 30);
    public int asteroidsCount = 20;
    public int small2BigRatio = 3;
    public GameObject bigAsteroid;
    public GameObject smallAsteroid;
    public float smallAseroidsSpawnRadius = 2;
    public float destroyForceFactor = 2;
    public float rotationFactor = 2;

    private Queue<GameObject> bigAsteroids;
    private Queue<GameObject> smallAsteroids;

    private System.Random rand = new System.Random();

	// Use this for initialization
	void Start () {
        bigAsteroids = new Queue<GameObject>();
        smallAsteroids = new Queue<GameObject>();
        Vector3 origin = spawnOrigin.position;
        for (int i=0; i<asteroidsCount; i++)
        {
            Vector3 pos = new Vector3((float)(rand.NextDouble()*radius.x*2 - radius.x),
                (float)(rand.NextDouble() * radius.y), (float)(rand.NextDouble() * radius.z * 2 - radius.z));
            pos = origin + pos;
            bigAsteroids.Enqueue(Instantiate(bigAsteroid, pos, new Quaternion()));
            for (int j = 0; j < small2BigRatio; j++)
            {
                GameObject a = Instantiate(smallAsteroid) as GameObject;
                smallAsteroids.Enqueue(a);
                a.SetActive(false);
            }
                
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Queue<GameObject> GetBigAsteroids()
    {
        return bigAsteroids;
    }

    public Queue<GameObject> GetSmallAsteroids()
    {
        return smallAsteroids;
    }

    public void SpawnSmallAsteroids(Vector3 position)
    {
        for (int j=0; j<small2BigRatio; j++)
        {
            Vector3 randPos = position + new Vector3((float)(rand.NextDouble()*smallAseroidsSpawnRadius*2 - smallAseroidsSpawnRadius),
                (float)(rand.NextDouble() * smallAseroidsSpawnRadius * 2 - smallAseroidsSpawnRadius),
                (float)(rand.NextDouble() * smallAseroidsSpawnRadius * 2 - smallAseroidsSpawnRadius));
            GameObject asteroid = smallAsteroids.Dequeue();
            asteroid.GetComponent<AsteroidScript>().Activate();
            asteroid.transform.position = randPos;

            Rigidbody rb = asteroid.GetComponent<Rigidbody>();
            Vector3 forceDirection = randPos - position;
            rb.AddForce(forceDirection * destroyForceFactor, ForceMode.Impulse);
            Vector3 randRotation = new Vector3((float)(rand.NextDouble()*rotationFactor - rotationFactor/2),
                (float)(rand.NextDouble() * rotationFactor - rotationFactor / 2),
                (float)(rand.NextDouble() * rotationFactor - rotationFactor / 2));
            rb.angularVelocity = randRotation;

            smallAsteroids.Enqueue(asteroid);
        }
    }
}
