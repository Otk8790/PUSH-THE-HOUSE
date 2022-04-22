using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI startText, escapeText;
    public Button restartButton;
    public bool isGameActive;
    public GameObject titleScreen;
    public GameObject button1, button2, button3;
    public AudioSource track01;
    public AudioSource track02;
    public AudioSource track03;
    public AudioSource sfx01;
    public AudioSource sfx02;
    public AudioSource sfx03;
    public AudioSource sfx04;
    public AudioSource sfx05;
    public AudioSource sfx06;
    public AudioSource sfx07;
    public AudioSource sfx08;
    public Animator transition;
    public float transitionTime = 1f;
    public Image blurTransition;
    public Image pauseButton;

    private int score;
    private float spawnRate = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
    
        scoreText.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        startText.gameObject.SetActive(false);
        escapeText.gameObject.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);

        track01.gameObject.SetActive(true);
        track02.gameObject.SetActive(true);
        track02.Stop();
        track03.gameObject.SetActive(false);
        sfx01.gameObject.SetActive(true);
        sfx01.Stop();
        sfx02.gameObject.SetActive(true);
        sfx02.Stop();
        sfx03.gameObject.SetActive(true);
        sfx03.Stop();
        sfx04.gameObject.SetActive(true);
        sfx04.Stop();
        sfx05.gameObject.SetActive(true);
        sfx05.Stop();
        sfx06.gameObject.SetActive(false);
        sfx07.gameObject.SetActive(true);
        sfx07.Stop();
        sfx08.gameObject.SetActive(true);
        sfx08.Stop();

        startText.gameObject.SetActive(true);
        escapeText.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            //Debug.Log("Exit");
            Application.Quit();
        }
        
        if (Input.GetKey("return"))
        {
            startText.gameObject.SetActive(false);
            escapeText.gameObject.SetActive(false);
            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            button3.gameObject.SetActive(true);
            sfx06.gameObject.SetActive(true);
        }
    }

    IEnumerator SpawnTarget()
    {

        while (isGameActive)
        {

            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

        }

    }
    
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
    }

    IEnumerator FalseBlur()
    {
        yield return new WaitForSeconds(2f);
        blurTransition.gameObject.SetActive(false);
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd; 
        scoreText.text = "Score: " + score;

    }

    public void GameOver()
    {

        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        blurTransition.gameObject.SetActive(true);
        
        sfx01.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        isGameActive = false;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {

        isGameActive = true;

        StartCoroutine(SpawnTarget());
        score = 0;

        spawnRate /= difficulty;
        UpdateScore(0);

        titleScreen.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);
        track01.Stop();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        StartCoroutine(FalseBlur());

    }
}
