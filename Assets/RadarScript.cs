using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarScript : MonoBehaviour {

    public GameObject asteroidModel;
    public Transform player;

    private Queue<GameObject> bigAsteroids;
    private List<GameObject> models;
    

	// Use this for initialization
	void Start () {
        bigAsteroids = GameObject.Find("GameManager").GetComponent<AsteroidManager>().GetBigAsteroids();
        models = new List<GameObject>();

        int modelsCount = GameObject.Find("GameManager").GetComponent<AsteroidManager>().asteroidsCount;
        for (int i=0; i<modelsCount; i++)
        {
            GameObject m = Instantiate(asteroidModel, transform);
            models.Add(m);
            m.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        int index = 0;
        if(bigAsteroids == null) bigAsteroids = GameObject.Find("GameManager").GetComponent<AsteroidManager>().GetBigAsteroids();
        foreach (GameObject asteroid in bigAsteroids)
        {
            Vector3 asteroidPosition = asteroid.transform.position; //Global position
            asteroidPosition = player.position - asteroidPosition; //In ship cooridnates

            float aR = asteroidPosition.magnitude;
            float aFi = Mathf.Atan2(asteroidPosition.x, asteroidPosition.z);
            float aTheta = Mathf.Asin(asteroidPosition.y / aR);

            float sR = player.transform.forward.magnitude;
            float sFi = Mathf.Atan2(player.transform.forward.x, player.transform.forward.z);
            float sTheta = Mathf.Asin(player.transform.forward.y / sR);

            aFi -= sFi;
            aFi += 180 * Mathf.Deg2Rad;
            aTheta += sTheta;
       
            aR /= 30;
            if (aR > 0.5) aR = 0.5f;
            Debug.Log(aR);

            asteroidPosition.x = aR * Mathf.Cos(aTheta) * Mathf.Sin(aFi);
            asteroidPosition.y = aR *  Mathf.Sin(aTheta);
            asteroidPosition.z = aR * Mathf.Cos(aTheta) * Mathf.Cos(aFi);

            asteroidPosition.y = -asteroidPosition.y;

            models[index].transform.localPosition = asteroidPosition;
            models[index].SetActive(asteroid.active);
            index++;
        }

    }
}
