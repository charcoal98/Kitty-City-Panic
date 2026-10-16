using UnityEngine;

public class collectable : MonoBehaviour
{
    Goal goal;
    public GameObject g;
    void Awake(){
        goal = g.GetComponent<Goal>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        Global.global.paperGet();
        if(collision.gameObject.GetComponent<PlayerController>()) {
            goal.keyCollect += 1;
            goal.updateCollectable();
            Destroy(gameObject);
        }
    }
}
