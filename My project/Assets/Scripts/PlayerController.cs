using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    public bool Grounded = true;
    const float groundDist = .03f;
    public float horForce = 20f;
    public float jumpForce = 20f; 

    Vector3 bottomRight;
    Vector3 bottomLeft;
    Vector3 leftRightOffset;
    Vector3 bottomOffset;
    Vector3 topOffset;

    public float colDist = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();   
        leftRightOffset = new Vector3(.375f,0f,0f);
        bottomOffset = new Vector3(0f,-1f,0f);
        topOffset = new Vector3(0f,0.8125f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastVis(bottomOffset, Vector2.down, groundDist, Color.green);
        bottomRight = transform.position + leftRightOffset + bottomOffset;
        bottomLeft = transform.position - leftRightOffset + bottomOffset;
        
        if (Keyboard.current.aKey.isPressed){
            _rb.AddForce(horForce * Time.deltaTime * Vector2.left, ForceMode2D.Impulse);
        }
        if (Keyboard.current.dKey.isPressed){
            _rb.AddForce(horForce * Time.deltaTime * Vector2.right, ForceMode2D.Impulse);
        }
        if (Grounded){
            horForce = 4f;
            if (Keyboard.current.spaceKey.wasPressedThisFrame){
                Grounded = false;
                _rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
                Grounded = false;
                
                print("jump");
            }
        }else
            horForce = 2f;
       
        
        RaycastVis(bottomOffset, Vector2.down, groundDist, Color.yellow);
    }

    void FixedUpdate(){
        //move to fixed update to see if that fixed anything... It didn't
         groundedCheck();
    }
    //Also does'nt work it has the same bug that my normal grounded check has but only activates when touching a wall 
    /**void OnCollisionStay2D(Collision2D collision) {
		// Check that we collided with Ground or oneway platform
        //print(collision.gameObject.layer);
		if(collision.gameObject.layer >= 6 && collision.gameObject.layer <= 7) {
			// Check what is directly below our character's feet
            //print("Grounded");
			RaycastHit2D[] hits = Physics2D.RaycastAll(bottomOffset, Vector2.down, grounded);
			
			// We might have multiple things below our character's feet
			for(int i = 0; i < hits.Length; i++) {
				RaycastHit2D hit = hits[i];
                print(hit.collider.gameObject.layer + ":" + i );
				if(hit.collider.gameObject.layer >= 6 && hit.collider.gameObject.layer <= 7) {
					jump = true;
                    //print("Jump reset");
				}
			}
		}
	}*/


    public void groundedCheck(){
        RaycastHit2D[] hits = Physics2D.RaycastAll(bottomOffset, Vector2.down, groundDist);
		// We might have multiple things below our character's feet
		for(int i = 0; i < hits.Length; i++) {
			RaycastHit2D hit = hits[i];
            /*print(hit.collider.gameObject.layer + ":" + i );
            print(LayerMask.NameToLayer("Ground"));*/
			if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
				Grounded = true;
                //print("Jump reset");
			}
		}
    }

    //Debug meths
    public void RaycastVis(Vector3 Origin, Vector2 direction, float distance, Color color){
        Vector3 dir = new Vector3(direction.x, direction.y, 0);
        Vector3 end = Origin + (dir * distance);
        Debug.DrawLine(Origin + transform.position, end + transform.position, color);
    }
    
}
