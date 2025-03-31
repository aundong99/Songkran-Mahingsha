using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public float jumpSpeed = 9f;
    public float maxSpeed = 10f;
    float speedMultiplyer = 1f;
    public float JumpPower = 20f;
    public bool grounded;
    public float jumpRate = 1f;
    public float nextJumpPress = 0.0f;
    public float fireRate = 0.2f;
    public float nextFireRate = 0.0f;
    private Rigidbody2D ridigBody2D;
    private Physics2D physic2D;
    public int healthbar = 100;
    public Slider sliderHp;
    public GameObject reactionGroup;
    public TMP_Text Txt_Score;
    public GameManagerScript gameManager;
    [SerializeField] private bool isRight = true;
    Animator animator;


    void Start()
    {
        ridigBody2D = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        sliderHp.maxValue = healthbar;
        sliderHp.value = healthbar;
    }

    void Update()
    {
        sliderHp.value = healthbar;

        animator.SetBool("Grounded", true);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            isRight = false;
            transform.Translate(Vector2.right * speed * speedMultiplyer * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }
        else if (Input.GetAxis("Horizontal") > 0.1f)
        {
            isRight = true;
            transform.Translate(Vector2.right * speed * speedMultiplyer * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (Input.GetButtonDown("Jump") && Time.time > nextJumpPress)
        {
            animator.SetBool("Jump", true);
            nextJumpPress = Time.time + jumpRate;
            ridigBody2D.AddForce(jumpSpeed * (Vector2.up * JumpPower));
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        Debug.Log(ControlData.Score.ToString());
        Txt_Score.text = ControlData.Score.ToString();
        reactionGroup.SetActive(true);
    }

    private void OnEnable()
    {
        SpeedItem.OnSpeedCollected += BoostSpeed;
    }

    private void OnDisable()
    {
        SpeedItem.OnSpeedCollected -= BoostSpeed;
    }

    private void BoostSpeed(float multiplier)
    {
        speedMultiplyer = multiplier;
        StartCoroutine(ResetSpeedBoost());
    }

    private IEnumerator ResetSpeedBoost()
    {
        yield return new WaitForSeconds(2f);
        speedMultiplyer = 1f;
    }

    void TakeDamage(int damage)
    {
        healthbar -= damage;
        sliderHp.value = healthbar; // อัปเดตค่าแถบเลือด
        Debug.Log("Player Health: " + healthbar);

        if (healthbar <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player is Dead!");
        gameManager.gameOver();
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpeedItem"))
        {
            collision.GetComponent<SpeedItem>().Collect();
        }
        if (collision.CompareTag("enemy"))
        {
            TakeDamage(20);
        }
    }
}
