using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _rotationSpeed = 10f;

    [SerializeField]
    private GameObject _explosion;

    [SerializeField] private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Laser"))
        {
            GameObject explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();
            Destroy(explosion,1.5f);
            Destroy(gameObject, 1f);
        }
    }
}
