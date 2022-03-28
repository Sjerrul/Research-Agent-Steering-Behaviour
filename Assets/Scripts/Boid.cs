using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Vector2 velocity;
    public Vector2 acceleration;
    
    public float maximumSpeed = 2.0f;
    public float maximumForce = 0.02f;
    public float pursuitDistanceEstimation = 3f;
    public float visionDistance = 1f;
    
    void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.velocity = new Vector2(0, 0);
        this.acceleration = new Vector2(0, 0);
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var directionVector = new Vector3(this.velocity.x, this.velocity.y, this.transform.position.z);
        this.velocity += this.acceleration;
        this.velocity = this.velocity.normalized * maximumSpeed;
        this.transform.position += directionVector * Time.deltaTime;
        this.acceleration = Vector2.zero;
        
        float angle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
 
        Vector3 pointOnScreen = Camera.main.WorldToScreenPoint(spriteRenderer.bounds.center);
        if (pointOnScreen.x < 0 || pointOnScreen.x > Screen.width)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }
        
        if (pointOnScreen.y < 0 || pointOnScreen.y > Screen.height)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
        }
    }

    public void ApplyForce(Vector2 force)
    {
        this.acceleration += force;
    }

    public Vector2 Seek(Vector2 target)
    {
        Vector2 desiredVector = target - new Vector2(transform.position.x, transform.position.y);
        desiredVector = desiredVector.normalized * maximumSpeed;

        var steering = desiredVector - this.velocity;
        steering = steering.normalized * maximumForce;
        return steering;
    }
    
    public Vector2 Flee(Vector2 target)
    {
        return Seek(target) * -1;
    }
    
    public Vector2 Pursue(Boid target)
    {
        Vector2 pursuePoint = target.transform.position;
        pursuePoint = pursuePoint + target.velocity * pursuitDistanceEstimation;

        return Seek(pursuePoint);
    }
    
    public Vector2 Evade(Boid target)
    {
        Vector2 force = Pursue(target);

        return force * -1;
    }
}
