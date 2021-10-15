using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;

    [SerializeField]
    private int _powerID;


    void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        if (transform.position.y < -6.8)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            if (other.GetComponent<Player>() != null)
            {
                switch (_powerID)
                {
                    case 0:
                        other.GetComponent<Player>().TriggerTrippleShot();
                        break;
                    case 1:
                        other.GetComponent<Player>().TriggerSpeedBoost();
                        Debug.Log("Speed");
                        break;
                    case 2: 
                        other.GetComponent<Player>().TriggerShield();
                        Debug.Log("Shield");
                        break;
                }
                Destroy(gameObject);
            }
        }
    }
}
