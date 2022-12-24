using UnityEngine;
using UnityEngine.InputSystem;

public class PlayController : MonoBehaviour
{
    //private
    private CharacterController cc;
    [SerializeField] private float moveSpeed,jumpForce,gravity;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    private Vector3 moveDir;
    private float AxisZ,AxisX;
    private Animator isWalkingAnim;
    private bool isWalking;


    //public
    // public int PlayerHP { get; private set; }
    public static PlayController playerController;
    public int camActive;

    // Function
    void Start() {
        cc = GetComponent<CharacterController>();
        isWalkingAnim = GetComponent<Animator>();
        isWalking = false;
    }

    void Update() {
        if(!PauseScript.pauseScript.isPaused){
            isWalkingAnim.SetBool("IsWalking",isWalking);
            //Gravity
            if(moveDir.y > -gravity) {
                moveDir.y -= gravity * Time.deltaTime;
            }

            //Rotation And Animation Walk
            if(moveDir.x != 0 && PlayerCollide.playerCollider.lockRotation == false || moveDir.z != 0 && PlayerCollide.playerCollider.lockRotation == false) {
                isWalking = true;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveDir.x, 0 ,moveDir.z)), 0.15f);
            } else
                isWalking = false;
            //Movements

            switch(camActive){
                case 1:
                    moveDir = new Vector3(-AxisZ * moveSpeed,moveDir.y,AxisX * moveSpeed);
                break;
                case 2:
                    moveDir = new Vector3(-AxisX * moveSpeed,moveDir.y,-AxisZ * moveSpeed);
                break;
                default:
                    moveDir = new Vector3(AxisX * moveSpeed,moveDir.y,AxisZ * moveSpeed);
                break;
            }
            cc.Move(moveDir * Time.deltaTime);
        }

    }
    
    //Jump Function
    public void OnJump() {
        if(cc.isGrounded) {
            moveDir.y = jumpForce;
            audioSource.PlayOneShot(jumpSound);
        }
    }
    public void OnUpDownMoves(InputValue moves) {
            AxisZ = moves.Get<float>();
    }                                         
    public void OnSideMoves(InputValue moves) {
            AxisX = moves.Get<float>();
    }
}
