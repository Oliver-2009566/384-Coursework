using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAsteroids : MonoBehaviour
{
    public float verticalBoundary =  5.5f;
    public float horizontalBoundary = 10.65f;
    public GameObject asteroidPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnAsteroids());
    }

    // Update is called once per frame
    IEnumerator spawnAsteroids()
    {
        for(int i = 0; i < 100; i++)
        {
            int spawnLocation = Random.Range(0, 3);
            Vector2 asteroidPos;
            float scale = Random.Range(0.5f, 5f);
            if (spawnLocation == 0)
            {
                // Left side
                asteroidPos = new Vector2(-(horizontalBoundary), Random.Range(-(verticalBoundary), verticalBoundary));
            }
            else if (spawnLocation == 1)
            {   
                // Right side
                asteroidPos = new Vector2(horizontalBoundary, Random.Range(-(verticalBoundary), verticalBoundary));
            }
            else if (spawnLocation == 2)
            {
                // Top side
                asteroidPos = new Vector2(Random.Range(-(horizontalBoundary), horizontalBoundary), verticalBoundary);
            }
            else
            {
                // Bottom side
                asteroidPos = new Vector2(Random.Range(-(horizontalBoundary), horizontalBoundary), -(verticalBoundary));
            }
            GameObject asteroid = Instantiate(asteroidPrefab);
            asteroid.transform.localPosition = asteroidPos;
            asteroid.transform.localScale = new Vector3(scale, scale, 1);

            yield return new WaitForSeconds(2f);
        }
    }
}
