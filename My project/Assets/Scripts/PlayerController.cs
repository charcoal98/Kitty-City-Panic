using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    SpriteRenderer _sr;
    Animator _ani;

    //platform gameobject
    public GameObject platform;

    public bool Grounded = true;
    const float groundDist = .03f;
    public float horForce = 20f;
    public float jumpForce = 20f; 

    bool fallCoolDown = false;

    Vector3 leftRightOffset = new Vector3(.375f,0f,0f);
    Vector3 centerOffset = new Vector3(0f,.8125f,0f);
    Vector2 facing = Vector2.right;

    public float colDist = 1f;

    //attack hitbox
    public float attackHitboxRadius;
    public float attackHitboxDistance;

    //animaton
    bool attack;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); 
        _sr = GetComponent<SpriteRenderer>();
        _ani = GetComponent<Animator>();
    }

    void FixedUpdate(){
        float speed = _rb.linearVelocity.magnitude;
		_ani.SetFloat("Speed", speed);
        if(speed > 0.1f && !attack){
			_ani.speed = speed / 3f;}
		else 
			_ani.speed = 1f;
        if (Keyboard.current.jKey.isPressed){
            Attack();
        }

        
        
    }

    void Update()
    {        

        
        if (Keyboard.current.aKey.isPressed){
            _rb.AddForce(horForce * Time.deltaTime * Vector2.left, ForceMode2D.Impulse);
            _sr.flipX = false;
            facing = Vector2.left;
        }

        if (Keyboard.current.dKey.isPressed){
            _rb.AddForce(horForce * Time.deltaTime * Vector2.right, ForceMode2D.Impulse);
            _sr.flipX = true;
            facing = Vector2.right;
        }

        
        

        if(Keyboard.current.sKey.isPressed){
            fallCoolDown = true;
            StartCoroutine(platformFall());
        } else if (!fallCoolDown)
            Physics2D.IgnoreCollision(platform.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), false);



        if (Grounded){
            horForce = 2.5f;
            if (Keyboard.current.spaceKey.wasPressedThisFrame){
                Grounded = false;
                _rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);                
            }
        }else
            horForce = 1.25f;  
    
        RaycastVis(transform.position, Vector2.down, groundDist, Color.yellow); 
    }

    void OnCollisionStay2D(Collision2D collision) {
		// Check that we collided with Ground or oneway platform
		if(collision.gameObject.layer >= 6 && collision.gameObject.layer <= 7) {
			// Check what is directly below our character's feet
			RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(.75f,.75f), 0f,Vector2.down, groundDist);
			// We might have multiple things below our character's feet
			for(int i = 0; i < hits.Length; i++) {
				RaycastHit2D hit = hits[i];
				if(hit.collider.gameObject.layer >= 6 && hit.collider.gameObject.layer <= 7) {
					Grounded = true;
				}
			}
		}

        //Fall Though Platform
        

	}

    //Meths
    public void Attack(){
        StartCoroutine(AttackDelay());
        _ani.speed = 1f;
        _ani.Play("Attack");
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position + centerOffset, 
        attackHitboxRadius, facing, attackHitboxDistance);
        for (int i = 0; i < hits.Length; i++) {
            RaycastHit2D hit = hits[i];
            if(hit.collider.gameObject.layer == 12){
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                enemy.enemyDamage(1);
                break;//only hit one enemy once
            }
        }


    }
    //Set attack to true and wait until animation is done to set attack to false
    public IEnumerator AttackDelay(){
        attack = true;
        yield return new WaitForSeconds(.5f);
        attack = false;
    }

    public IEnumerator platformFall(){
        Physics2D.IgnoreCollision(platform.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        yield return new WaitForSeconds(.17f);
        fallCoolDown = false;
    }

    //Debug meths
    public void RaycastVis(Vector3 Origin, Vector2 direction, float distance, Color color){
        Vector3 dir = new Vector3(direction.x, direction.y, 0);
        Vector3 end = Origin + (dir * distance);
        Debug.DrawLine(Origin, end, color);
    }

    public void CirclecastVis(Vector2 start, float radius, Vector2 direction, float distance)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(start, radius);

        Gizmos.color = Color.yellow;
        Vector2 endPosition = start + direction * distance;
        Gizmos.DrawWireSphere(endPosition, radius);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(start, endPosition);
    }
    
    void OnDrawGizmos(){
        CirclecastVis(transform.position + centerOffset, attackHitboxRadius, facing, attackHitboxDistance);
    }
}
