using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class HealthCollectible : MonoBehaviour
// {
//     void OnTriggerEnter2D(Collider2D other)
//     {
//         RubyController controller = other.GetComponent<RubyController>();

//         if (controller != null)
//         {
//             if(controller.health  < controller.maxHealth)
//             {
//                 controller.ChangeHealth(1);
//                 Destroy(gameObject);
//             }
//         }
//     }
// }

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    public GameObject healthEffect;

    Transform transform;
    float elapse_sec = 0;
    int sign = 1;

    double refresh_interval = 0.6;
    float bob_magnitude = 0.075f;

    void Awake()
    {
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        elapse_sec += Time.deltaTime;
        if (elapse_sec > refresh_interval)
        {
            elapse_sec = 0;
            sign *= -1;
            transform.position += new Vector3(0, sign*bob_magnitude, 0);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
            	controller.ChangeHealth(1);
            	Destroy(gameObject);
            
            	controller.PlaySound(collectedClip);
            }
        }

    }
}
