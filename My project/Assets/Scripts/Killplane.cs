using UnityEngine;

public class Killplane : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == 13) {
            Global.skinACat();
        }
    }
}
