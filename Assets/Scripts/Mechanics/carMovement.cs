using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMovement : MonoBehaviour
{
    [SerializeField] private float currentSpeed;
    [SerializeField] private float intervalSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float currentHorizSpeed;
    [SerializeField] private float horizSpeed;
    
    [Header("Powerup_speed")]
    [SerializeField] private float currentPowerUpSpeed;
    [SerializeField] private float powerUpSpeed;
    [SerializeField] private float powerUpSpeed_time;

    [Header("Main")]
    [SerializeField] private float moveHoriz;
    [SerializeField] private float nextTime;

    [Header("Score")]
    [SerializeField] public float score;
    [SerializeField] private Vector3 initPos;
    [SerializeField] public bool gameover;

    [Header("SFX")]
    [SerializeField] private AudioSource engineSound;
    [SerializeField] private AudioClip crashSound;

    private void Start()
    {
        currentSpeed = speed;
        currentHorizSpeed = horizSpeed;
        initPos = transform.position;
    }

    private void Update()
    {
        score = Mathf.CeilToInt(transform.position.z - initPos.z) * 0.1f;
        print(score);

        moveHoriz = Input.GetAxis("Horizontal") * currentHorizSpeed * Time.deltaTime;
        if(!(currentSpeed >= maxSpeed))
        {
            currentSpeed += Time.deltaTime * intervalSpeed;
        }
        move();

        if(Time.time >= nextTime && currentPowerUpSpeed > 0)
        {
            currentPowerUpSpeed = 0;
        }

        float vel = 1;
        float _cs = currentSpeed;
        engineSound.pitch = Mathf.SmoothDamp(_cs + 1, 3, ref vel, Time.deltaTime);
    }

    private void move()
    {
        transform.Translate(0, 0, currentSpeed + currentPowerUpSpeed);
        transform.Translate(moveHoriz, 0, 0);
    }

    public List<Generator> genRefs = new List<Generator>();

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Road")
        {
            foreach (Generator gen in genRefs)
            {
                gen.optimSync();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pu_speed")
        {
            currentPowerUpSpeed = powerUpSpeed;
            nextTime = Time.time + powerUpSpeed_time;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Hinder")
        {
            AudioSource.PlayClipAtPoint(crashSound, collision.transform.position);
            engineSound.volume = 0.1f;
            engineSound.enabled = false;
            Time.timeScale = 0;
            currentSpeed = 0;
            maxSpeed = 0;
            intervalSpeed = 0;
            transform.GetComponent<carMovement>().enabled = false;
            gameover = true;
        }
    }

    public void _Reset()
    {
        gameover = false;
    }
}
