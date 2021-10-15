using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] 
    private float speed = 1f;

    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y >= 7)
        {
            if (gameObject.transform.parent != null)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
