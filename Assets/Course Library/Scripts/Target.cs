using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EZCameraShake;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 12f;
    private float maxSpeed = 16f;
    private float maxTorque = 10f;
    private float xRange = 4f;
    private float ySpawnPos = -2f;

    public ParticleSystem explosionParticle;
    public int pointValue;
    public TextMeshPro floatingTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        if (gameObject.CompareTag("Bad"))
        {

            gameManager.sfx07.Play();
            gameManager.sfx08.Play();

        }
        else 
        {

            gameManager.sfx01.Play();

        }
    }

    Vector3 RandomForce()
    {

        return Vector3.up * Random.Range(minSpeed, maxSpeed);

    }

    float RandomTorque()
    {

        return Random.Range(-maxTorque, maxTorque);

    }

    Vector3 RandomSpawnPos()
    {

        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            
            if (gameObject.CompareTag("Good"))
            {
                gameManager.sfx02.Play();
            }
            if (gameObject.CompareTag("Bad"))
            {
                CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
                gameManager.sfx03.Play();
            }

            if (gameObject.CompareTag("Especial"))
            {
                gameManager.sfx05.Play();
            }

            if (floatingTextPrefab)
            {
                ShowFloatingText();
            }
        }
        
    }

    private void ShowFloatingText()
    {
        if (pointValue >= 0)
        {
            var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            go.GetComponent<TextMeshPro>().text = "+" + pointValue.ToString();
        }
        else
        {
            var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            go.GetComponent<TextMeshPro>().text = pointValue.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
            gameManager.track02.Stop();
            gameManager.track03.gameObject.SetActive(true);
        }

    }

}
