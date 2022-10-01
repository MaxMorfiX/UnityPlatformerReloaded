using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

    private Rigidbody2D rBody;
    [SerializeField]private GameObject cameraObj;
    [SerializeField]private Text rotatePlayerOrNotButtonText;

    [Range(5, 500)]public float jumpForce = 100f;
    [Range(50, 4000)]public float xSpeed = 500f;
    public LayerMask whatIsGround;

    public int startLives = 1;
    public int lives = 1;
    private Vector2 freezedVelocity;
    private bool isFreezed = false;

    private bool isGrounded;
    private float gravityScale;

    private bool rotatePlayer = false;

    private void Awake() {
        rBody = GetComponent<Rigidbody2D>();

        lives = startLives;
        gravityScale = GetComponent<Rigidbody2D>().gravityScale;
    }

    private void FixedUpdate() {

        if(lives <= 0 || isFreezed) {
            return;
        }

        handleX();
        handleY();
        
    }

    private void handleY() {
        Vector2 pointA = new Vector2(transform.position.x - 0.45f, transform.position.y - 0.5f);
        Vector2 pointB = new Vector2(transform.position.x + 0.45f, transform.position.y + 0.45f);

        isGrounded = Physics2D.OverlapArea(pointA, pointB, whatIsGround, -Mathf.Infinity, Mathf.Infinity);

        if(isGrounded && Input.GetKey(KeyCode.Space))
            rBody.velocity = new Vector2(rBody.velocity.x, jumpForce);
    }
    private void handleX() {
        float horisontal = Input.GetAxisRaw("Horizontal") * xSpeed * Time.fixedDeltaTime;
        rBody.velocity = new Vector2(horisontal, rBody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "obstacle") {
            lives -= 1;
            if(lives > 0) {
                return;
            }

            cameraObj.GetComponent<gameСontroller>().gameOver();
            Debug.Log("wasted");
            
            freezePlayer();
        }
        if(col.tag == "finish") {
            lives = 0;

            cameraObj.GetComponent<gameСontroller>().levelCompleted();
            Debug.Log("won");
            
            freezePlayer();

        }
    }

    public void freezePlayer() {
        freezedVelocity = GetComponent<Rigidbody2D>().velocity;
        isFreezed = true;

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
    }
    public void unfreezePlayer(bool returnVelocityOrNot) {
        if(returnVelocityOrNot)
            GetComponent<Rigidbody2D>().velocity = freezedVelocity;

        isFreezed = false;

        GetComponent<Rigidbody2D>().gravityScale = gravityScale;
    }

    public void restartGame() {
        lives = startLives;
        unfreezePlayer(false);

        transform.position = new Vector2(0, 0);
    }

    public void toggleRotatePlayerFlag() {
        rotatePlayer = !rotatePlayer;

        if(rotatePlayer) {
            rotatePlayerOrNotButtonText.text = "Don't Rotate Player";
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
        else if(!rotatePlayer) {
            rotatePlayerOrNotButtonText.text = "Rotate Player";
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    
}
