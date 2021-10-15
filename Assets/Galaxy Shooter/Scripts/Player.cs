using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6f;

    private float _speedMultiplier = 2;

    [SerializeField] 
    private GameObject _laser;
    [SerializeField]
    private GameObject _trippleShot;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire;

    private int _playerHealth = 3;

    [SerializeField] 
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _trippleShotActive = false;
    [SerializeField]
    private bool _speedBoostActive = false;
    [SerializeField] 
    private bool _shieldActive = false;
    [SerializeField]
    private Renderer _shield;

    [SerializeField]
    private int _score;

    [SerializeField] 
    private GameObject[] _engines;

    [SerializeField] 
    private AudioClip[] _soundClips;

    [SerializeField]
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        _shield = GameObject.Find("Shield").GetComponent<Renderer>();

        if (_spawnManager == null)
        {

        }
        this.transform.position = new Vector3(0,0,0);
    }

    void Update()
    {
        CalculateMovement();
        FireLaser();
    }

    void FireLaser()
    {
        if (Input.GetButton("Jump") && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            if (_trippleShotActive)
            {
                Instantiate(_trippleShot, transform.position + new Vector3(-0.08f,0.8f,0) , Quaternion.identity);
            }
            else
            {
                Instantiate(_laser, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            }

            _audioSource.clip = _soundClips[0];
            _audioSource.Play();
        }
    }

    public void DamagePlayer()
    {
        if (_shieldActive == true)
        {
            _shieldActive = false;
            _shield.enabled = false;
            return;
        }
        else
        {
            
            _playerHealth--;
            if (_playerHealth == 2)
            {
                _engines[0].GetComponent<Renderer>().enabled = true;
            }
            else if (_playerHealth == 1)
            {
                _engines[1].GetComponent<Renderer>().enabled = true;
            }

            if (_playerHealth == 0)
            {
                Destroy(gameObject);
                gameObject.GetComponent(typeof(SpawnManager));
                _spawnManager.StopRunning();
            }
        }
       
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.x > 9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
        else if (transform.position.x < -9)
        {
            transform.position = new Vector3(9f, transform.position.y, 0);
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4f, 0), 0);
    }

    public void TriggerTrippleShot()
    {
        PowerUpSound();
        _trippleShotActive = true;
        StartCoroutine(TrippleShotPowerDownRoutine());
    }

    IEnumerator TrippleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _trippleShotActive = false;
    }

    public void TriggerSpeedBoost()
    {
        PowerUpSound();
        _speedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostDownRoutine());
    }

    IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _speedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void TriggerShield()
    {
        PowerUpSound();
        _shieldActive = true;
        _shield.enabled = true;
        StartCoroutine(ShieldDownRoutine());
    }

    private void PowerUpSound()
    {
        _audioSource.clip = _soundClips[2];
        _audioSource.Play();
    }

    IEnumerator ShieldDownRoutine()
    {
        yield return new WaitForSeconds(20f);
        _shieldActive = false;
        _shield.enabled = false;
    }

    public void AddScore(int score)
    {
        _score += score;
        _audioSource.clip = _soundClips[1];
        _audioSource.Play();
    }

    public int GetScore()
    {
        return _score;
    }

    public int GetHealth()
    {
        return _playerHealth;
    }
}
