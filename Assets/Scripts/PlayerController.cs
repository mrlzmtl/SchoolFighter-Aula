using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public float playerSpeed = 0.6f;
    public float currentSpeed;

    public Vector2 playerDirection;

    private bool isWalking;

    private Animator playerAnimator;

    private bool playerFacingRight = true;

    private int punchCount;

    private float timeCross = 1f;

    private bool comboControl;

    private bool isDead;

    public int maxHealth = 10;
    public int currentHealth;
    public Sprite playerImage;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponent<Animator>();

        currentSpeed = playerSpeed;

        currentHealth = maxHealth;
    }
    
    private void Update()
    {
        PlayerMove();
        UpdateAnimator();

        

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (punchCount < 2)
            {
                PlayerJab();
                punchCount++;

                if (!comboControl)
                {
                    StartCoroutine(CrossController());
                }
            }
            else if (punchCount >= 2)
            {
                PlayerCross();
                punchCount = 0;
            }

            StopCoroutine(CrossController());
        }        
    }

    private void FixedUpdate()
    {
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        playerRigidBody.MovePosition(playerRigidBody.position + currentSpeed * Time.fixedDeltaTime * playerDirection);
    }

    void PlayerMove()
    {
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (playerDirection.x < 0 && playerFacingRight)
        {
            Flip();
        }
        else if (playerDirection.x > 0 && !playerFacingRight)
        {
            Flip();
        }

    }

    void UpdateAnimator()
    {
        playerAnimator.SetBool("isWalking", isWalking);
    }

    void Flip()
    {
        playerFacingRight = !playerFacingRight;

        transform.Rotate(0, 180, 0);
    }

    void PlayerJab()
    {
        playerAnimator.SetTrigger("isJab");
    }

    void PlayerCross()
    {
        playerAnimator.SetTrigger("isCross");
    }

    IEnumerator CrossController()
    {
        comboControl = true;
        yield return new WaitForSeconds(timeCross);
        punchCount = 0;
        comboControl = false;
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = playerSpeed;
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            playerAnimator.SetTrigger("HitDamage");
            FindFirstObjectByType<UIManager>().UpdatePlayerHealth(currentHealth);
        }
    }
}
