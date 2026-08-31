using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    public bool jump = true;
    const float grounded = .1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.aKey.isPressed){
            _rb.AddForce(20f * Time.deltaTime * Vector2.left, ForceMode2D.Impulse);
        }
        if (Keyboard.current.dKey.isPressed){
            _rb.AddForce(20f * Time.deltaTime * Vector2.right, ForceMode2D.Impulse);
        }
        if (jump){
            if (Keyboard.current.spaceKey.wasPressedThisFrame){
                jump = false;
                _rb.AddForce(20f * Vector2.up, ForceMode2D.Impulse);
                print("jump");
            }
        }
        Debug.DrawLine(Vector3.zero, Vector3.down * grounded, Color.green);
    }
    void OnCollisionStay2D(Collision2D collision) {
			// Check that we collided with Ground
			if(collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
				// Check what is directly below our character's feet
				RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, grounded);
				// Debug.DrawRay(transform.position, Vector2.down * 0.65f); // Visualize Raycast
				
				// We might have multiple things below our character's feet
				for(int i = 0; i < hits.Length; i++) {
					RaycastHit2D hit = hits[i];
					// Check that we collided with ground below our feet
					if(hit.collider.gameObject.layer == 7) {
						// Reset jump count
                        print("can Jump");
						jump = true;
					}
				}
			}
		}
}
