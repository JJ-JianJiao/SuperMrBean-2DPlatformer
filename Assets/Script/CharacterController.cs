using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{

    public bool isBegin = false;
    public bool hideMenu = false;
    public GameObject title;
    public GameObject start;
    public GameObject quit;
    //HP
    public int Hp = 1;

    //public UIControl ui;
    public bool canFireBall = false;
    public GameObject bullet;
    public float maxSpeed = 3;
    public Transform groundCheck;
    public float jumpForce = 400;
    public bool stop = false;

    protected Animator myAnimator;
    protected Rigidbody2D myRigidBody;
    protected float moveForce = 365;
    protected bool facingRight = true;
    protected bool grounded = false; 
    protected bool jump = false;
    protected float horizontalAxis;

    private Transform Instance;


    public bool isWin = false;

    private void Start()
    {
        Instance = this.transform;
        title = GameObject.Find("Title").gameObject;
        start = GameObject.Find("Start").gameObject;
        quit = GameObject.Find("Quit").gameObject;
    }
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log(myRigidBody.velocity.x);
        if (isBegin && !hideMenu) 
        {
            title.gameObject.SetActive(false);
            start.gameObject.SetActive(false);
            quit.gameObject.SetActive(false);
            hideMenu = true;
            
        }
        if (isBegin) { 
            if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("tube")))
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }

            if (this.transform.position.x >= 44 && !isWin) {
                isWin = true;
                AudioHub.Instance.StopSound();
                AudioHub.Instance.PlaySound("smb_stage_clear");

            }

            if (isWin) {
                title.SetActive(true);
                title.GetComponent<Text>().text = "Victory!!";
                if (!grounded && !stop) { 
                    myRigidBody.velocity = new Vector2(0, myRigidBody.velocity.y);
                }
                else if(grounded && !stop)
                {
                    transform.Translate(Vector2.right * 1 * Time.deltaTime * 1f);
                }
                //if(this.transform.position)
                if (this.transform.position.x >= 47.24) {
                    this.transform.position = new Vector3(transform.position.x, transform.position.y, 2);
                    stop = true;

                    Invoke("ReloadScene", 3);
                }
            }
            else {
                if (Hp > 0)
                {
                    horizontalAxis = Input.GetAxis("Horizontal");
                }
                if (Hp <= 0) { 
                    //TODO: die (animation, sound)
                }


                //layer mask bitwise ops: https://answers.unity.com/questions/8715/how-do-i-use-layermasks.html
                //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
                //grounded = Physics2D.Linecast(transform.position, groundCheck.position, LayerMask.NameToLayer("Ground"));

                //if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("tube")))
                //{
                //    grounded = true;
                //}
                //else {
                //    grounded = false;
                //}
                if (Input.GetButtonDown("Jump") && grounded && Hp>0)
                {
                    jump = true;
                    AudioHub.Instance.PlaySound("jump_sound");
                }

                if (Instance.position.x <= -50.60) {
                    this.transform.position = new Vector3(-50.60f, this.transform.position.y);
                }


                if (canFireBall && Input.GetKeyDown(KeyCode.Z))
                {
                    GameObject playerBullet = Instantiate(bullet);
                    //myAnimator.SetTrigger("Shoot");
                    if (this.transform.localScale.x > 0) {
                        playerBullet.GetComponent<FireBall>().dir = 1;
                    }
                    else if (this.transform.localScale.x < 0)
                    {
                        playerBullet.GetComponent<FireBall>().dir = -1;
                    }
                    playerBullet.transform.position = new Vector3(transform.position.x, transform.position.y);
                    AudioHub.Instance.PlaySound("smb_fireball");
                }
            }
        }

    }

    protected void FlipFacing() {
        facingRight = !facingRight;
        Vector3 scale = this.transform.localScale;
        scale.x *= -1;
        this.transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "enemy" || collision.collider.tag == "DeadZone") {
            Hp--;
            if (Hp <= 0) {
                myAnimator.SetTrigger("Die");
                Destroy(GetComponent<BoxCollider2D>());
                Destroy(GetComponent<CircleCollider2D>());
                myRigidBody.velocity = Vector2.zero;
                myRigidBody.AddForce(Vector2.up * 200f);
                AudioHub.Instance.StopSound();
                AudioHub.Instance.PlaySound("smb_mariodie");

                Invoke("ReloadScene", 3);
            }
        }
    }


    private void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void FixedUpdate()
    {
        if (isBegin) { 
            if (isWin)
            {
                //TODO: load second scene or .....
            }
            else
            {

                if (horizontalAxis > 0 && !facingRight)
                {
                    FlipFacing();
                }
                else if (horizontalAxis < 0 && facingRight) {
                    FlipFacing();
                }

                //myAnimator.SetFloat("Speed", Mathf.Abs(horizontalAxis));
                myAnimator.SetFloat("Speed", Mathf.Abs(horizontalAxis));
                //Have we reach maxSpeed? If not, add force.
                if (horizontalAxis * myRigidBody.velocity.x < maxSpeed)
                {
                    myRigidBody.AddForce(Vector2.right * horizontalAxis * moveForce);
                }

                if (horizontalAxis == 0) {
                   // myRigidBody.velocity = new Vector2(0, myRigidBody.velocity.y);
                }

                //have we exceeded the maxSpeed? Clamp it (set it to maxSpeed).
                if (Mathf.Abs(myRigidBody.velocity.x) > maxSpeed)
                {
                    myRigidBody.velocity = new Vector2(Mathf.Sign(myRigidBody.velocity.x) * maxSpeed, myRigidBody.velocity.y);
                }

                if (jump)
                {
                    myAnimator.SetTrigger("Jump");
                    myRigidBody.AddForce(new Vector2(0, jumpForce));
                    jump = false;
                }
            }
        }
    }
}




