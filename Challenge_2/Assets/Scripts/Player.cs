using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Player : MonoBehaviour

{
    private Rigidbody2D rd2d;

    public float speed;
    public float jump;

    public GameObject loseTextObject;

    public GameObject winTextObject;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    private int scoreValue;
    private int livesValue;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    public Animator animator;
    private bool facingRight = true;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreValue = 0;

         rd2d = GetComponent<Rigidbody2D>();
        livesValue = 3;

        SetCountText();
        winTextObject.SetActive(false);

        SetCountText();
        loseTextObject.SetActive(false);

        musicSource.clip = musicClipTwo;
        musicSource.Play();
    }
    void SetCountText()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
        if (scoreValue >= 8)
        {
            winTextObject.SetActive(true);
            
            musicSource.clip = musicClipTwo;
                musicSource.Stop();
                musicSource.clip = musicClipOne;
                musicSource.Play();
                musicSource.loop = false;
                speed = 0;
        
        }
        scoreText.text = "Score: " + scoreValue.ToString();
        if (scoreValue == 4) 
        {
            livesValue = 3;
            transform.position = new Vector2(100f, 0.5f);
        }

        livesText.text = "Lives: " + livesValue.ToString();
        if (livesValue == 0)
        {
            loseTextObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        animator.SetFloat("HorizontalValue", Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.SetFloat("VerticalValue", Input.GetAxis("Vertical"));

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }

        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }
     
        
       
    
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            livesValue = livesValue - 1;

            SetCountText();
        }

    }
   

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
        
            }
        }    
      
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}
 


