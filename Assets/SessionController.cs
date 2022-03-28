using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionController : MonoBehaviour
{
    public int numberOfBoids = 1;
    public Boid boidPrefab;
    
    private Boid[] boids;
    
    // Start is called before the first frame update
    void Start()
    {
        boids = new Boid[numberOfBoids];

        for (int i = 0; i < numberOfBoids; i++)
        {
            var boid = Instantiate(boidPrefab);
            boids[i] = boid;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        for (int i = 0; i < boids.Length; i++)
        {
            boids[i].Seek(Camera.main.ScreenToWorldPoint(mousePosition));
        }
    }
}
