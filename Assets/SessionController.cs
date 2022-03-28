using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionController : MonoBehaviour
{
    private static SessionController instance;
    
    public int numberOfBoids = 1;
    public Boid boidPrefab;
    
    private Boid[] boids;
    private Boid target;
    
    public static SessionController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                instance = go.AddComponent<SessionController>();
            }

            return instance;
        }
    }

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

        target = Instantiate(boidPrefab);
        target.GetComponent<SpriteRenderer>().color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        for (int i = 0; i < boids.Length; i++)
        {
            boids[i].Seek(target.transform.position + (target.transform.right * 2));
        }
        
        target.Seek(Camera.main.ScreenToWorldPoint(mousePosition));
    }
}
