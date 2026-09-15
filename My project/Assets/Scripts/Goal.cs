using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{

    public int lockNum;
    public int keyCollect;
    //public Scene next;

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == 13) {
            if (keyCollect >= lockNum){
                string currentScene = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentScene);
            }
            else 
            {
                print("Papers please");
            }
        }
    }
}
