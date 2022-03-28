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
    
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        for (int i = 0; i < boids.Length; i++)
        {
            var pursueForce = boids[i].Seek(target.transform.position);
            boids[i].ApplyForce(pursueForce);
            
            List<Collider2D> results = new List<Collider2D>();
            Vector2 pos = new Vector2(boids[i].transform.position.x, boids[i].transform.position.y);
            Physics2D.OverlapCircle(pos, boids[i].visionDistance, new ContactFilter2D(), results);
            foreach (var boid in results)
            {
                var evadeForce = boids[i].Evade(boid.GetComponent<Boid>());
                boids[i].ApplyForce(evadeForce);
            }
        }
        
        var steeringForce = target.Seek(Camera.main.ScreenToWorldPoint(mousePosition));
        target.ApplyForce(steeringForce);
    }
}
