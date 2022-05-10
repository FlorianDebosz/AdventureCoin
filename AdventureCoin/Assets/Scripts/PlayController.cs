using UnityEngine;
using UnityEngine.InputSystem;

public class PlayController : MonoBehaviour
{
    
    public CharacterController cc;
    //Control
    public float moveSpeed;
    public float jumpForce;
    public float gravity;
    private Vector3 moveDir;
    private float AxisZ,AxisX;
    
        void Update() {
        //Gravity
        if(moveDir.y > -gravity) {
            moveDir.y -= gravity * Time.deltaTime;
        }
                

        //Up Down Movements
        moveDir = new Vector3(AxisX * moveSpeed,moveDir.y,AxisZ * moveSpeed);
        if(moveDir.x != 0 || moveDir.z != 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveDir.x, 0 ,moveDir.z)), 0.15f);
        cc.Move(moveDir * Time.deltaTime);
    }
    public void OnJump() {
        if(cc.isGrounded)
            moveDir.y = jumpForce;
    }
    public void OnUpDownMoves(InputValue moves) {
        AxisZ = moves.Get<float>();
    }
    
    public void OnSideMoves(InputValue moves) {
        AxisX = moves.Get<float>();
    }
}
