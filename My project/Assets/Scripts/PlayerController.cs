using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    public bool jump = true;
    const float grounded = .05f;
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
        RaycastVis(bottomOffset, Vector2.down, grounded, Color.green);
        bottomRight = transform.position + leftRightOffset + bottomOffset;
        bottomLeft = transform.position - leftRightOffset + bottomOffset;

        if (Keyboard.current.aKey.isPressed){
            _rb.AddForce(horForce * Time.deltaTime * Vector2.left, ForceMode2D.Impulse);
        }
        if (Keyboard.current.dKey.isPressed){
            _rb.AddForce(horForce * Time.deltaTime * Vector2.right, ForceMode2D.Impulse);
        }
        if (jump){
            if (Keyboard.current.spaceKey.wasPressedThisFrame){
                jump = false;
                _rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
                print("jump");
            }
        }
        
        //Debug.DrawLine(Vector3.zero, Vector3.down * grounded, Color.green);
        //Debug.DrawLine(bottomRight,bottomLeft, Color.blue);
    }
    void OnCollisionStay2D(Collision2D collision) {
		// Check that we collided with Ground or oneway platform
        print(collision.gameObject.layer);
		if(collision.gameObject.layer >= 6 && collision.gameObject.layer <= 8) {
			// Check what is directly below our character's feet
            
			RaycastHit2D[] hits = Physics2D.RaycastAll(bottomOffset, Vector2.down, grounded);
			
			// We might have multiple things below our character's feet
			for(int i = 0; i < hits.Length; i++) {
				RaycastHit2D hit = hits[i];
                print(hit.collider.gameObject.layer);
				if(hit.collider.gameObject.layer >= 6 && hit.collider.gameObject.layer <= 8) {
					jump = true;
				}
			}
		}
        //Check if we collided with a slope to change how physics works
        /**print(LayerMask.NameToLayer("Slope")+ "slope");
        print(LayerMask.NameToLayer("Ground")+ "ground");
        
        if(collision.gameObject.layer == LayerMask.NameToLayer("Slope")) {
            Vector3 start = bottomOffset + new Vector3(0f,.1f,0f);
			//Calculate Slope Angle
            RaycastVis(start, Vector2.right, colDist, Color.green);
            RaycastVis(start, Vector2.down, colDist, Color.green);
            RaycastHit2D leftHit = Physics2D.Raycast(start, Vector2.left, colDist);
            RaycastHit2D rightHit = Physics2D.Raycast(start, Vector2.right, colDist);
            //move into ifstatement
            RaycastHit2D downHit = Physics2D.Raycast(start, Vector2.down, colDist);

            //WE DOING TRIG BOIS

            float slopeAngle = Mathf.Atan(rightHit.distance/downHit.distance);
            print(rightHit.distance + rightHit.transform.name);
            print(downHit.distance + downHit.transform.name);
		}*/
	}


    //Debug meths
    public void RaycastVis(Vector3 Origin, Vector2 direction, float distance, Color color){
        Vector3 dir = new Vector3(direction.x, direction.y, 0);
        Vector3 end = Origin + (dir * distance);
        Debug.DrawLine(Origin + transform.position, end + transform.position, color);
    }
}
