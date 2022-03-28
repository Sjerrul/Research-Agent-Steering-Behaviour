using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionController : MonoBehaviour
{
    public int numberOfBoids = 1;
    public Boid boidPrefab;
    
    private Boid[] boids;

    void Awake()
    {
        boids = new Boid[numberOfBoids];

        var points = PoissonDiscSampling.GeneratePoints(0.2f, new Vector2(5, 5), 10);
        for (int i = 0; i < numberOfBoids; i++)
        {
            var boid = Instantiate(boidPrefab);
            boid.transform.position = points[i];
            
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
