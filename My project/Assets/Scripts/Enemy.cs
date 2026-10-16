using UnityEngine;



public class Enemy : MonoBehaviour
{
    public int enemyHealth = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
            die();
    }

    void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.layer == 13){
            Global.global.skinACat();
        }
    }

    public void enemyDamage(int damage){
        enemyHealth -= damage;
    }

    public void die(){
        Global.global.berdDyded();
        Destroy(gameObject);
    }
}
