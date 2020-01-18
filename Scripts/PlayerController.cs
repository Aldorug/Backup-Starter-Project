using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text score;
    public Text endText;
    public Text timerText;
    public GameObject player;
    public GameObject timeDelete;

    public AudioSource musicSource;
    public AudioClip mainMusic;
    public AudioClip endMusic;
    public AudioClip winMusic;
    public AudioClip coinSound;
    public AudioClip debuffSound;


    private Rigidbody2D rb2d;
    private int scoreValue = 0;
    private float timeLeft = 12f;



    void winCondition()
    {
        if (scoreValue == 4)
        {
            endText.text = "You Win! Game Created by Colin Hummel! Press R for restart.";
            timeLeft += 1000;
            musicSource.clip = winMusic;
            musicSource.Play();
            timeDelete.SetActive(false);


        }
    }


    void timeOver()
    {
        if (timeLeft < 0)
        {
            endText.text = "You Lose! Game Created by Colin Hummel! Press R for restart.";
            musicSource.Stop();
            AudioSource.PlayClipAtPoint(endMusic, transform.position);
            player.SetActive(false);



        }
    }



    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        endText.text = "";
        timerText.text = "Time until gameover: " + timeLeft.ToString("F0");
        musicSource.clip = mainMusic;
        musicSource.PlayDelayed(2.0f);
    }


    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("StarterProject");
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);

        timeLeft -= Time.deltaTime;
        timerText.text = "Time until gameover: " + timeLeft.ToString("F0");

        timeOver();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            other.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            winCondition();
        }
        if (other.gameObject.CompareTag("Debuff"))
        {
            speed -= 3;
            other.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(debuffSound, transform.position);
        }
    }
}
