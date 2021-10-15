using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;

    private Animator _animator;

    private Player _player;
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        transform.position = new Vector3(Random.Range(-7.8f, 7.8f), 6.5f, transform.position.z);
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5)
        {
            float randomX = Random.Range(-7.8f, 7.8f);
            transform.position = new Vector3(randomX, 6.5f, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            if (other.GetComponent<Player>() != null)
            {
                other.GetComponent<Player>().DamagePlayer();
                Debug.Log("Booom we hit " + other.transform.name);
                _animator.SetTrigger("OnEnemyDeath");
                GetComponent<PolygonCollider2D>().enabled = false;
                _speed = 0.5f;
                Destroy(gameObject, 2.5f);
            }
        }

        if (other.tag.Equals("Laser"))
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _animator.SetTrigger("OnEnemyDeath");
            Debug.Log("Booom we dead by " + other.transform.name);
            GetComponent<PolygonCollider2D>().enabled = false;
            _speed = 0.5f;
            Destroy(gameObject, 2.5f);
        }
    }

    
}
