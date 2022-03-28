using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public float speed = 2f;
    public float rotationSpeed = 2f;

    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += transform.right * speed * Time.deltaTime;

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

    public void Seek(Vector3 target)
    {
        Vector3 vectorToTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
    }
    
}
