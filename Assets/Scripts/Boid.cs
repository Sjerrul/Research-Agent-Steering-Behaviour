using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public float speed = 2f;
    public float rotationSpeed = 120f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += transform.right * speed * Time.deltaTime;
    }

    public void Seek(Vector3 target)
    {
        Vector3 vectorToTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed * Time.deltaTime);
    }
    
}
