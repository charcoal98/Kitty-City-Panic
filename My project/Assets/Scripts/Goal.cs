using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Goal : MonoBehaviour
{

    public int lockNum;
    public int keyCollect;
    public TMP_Text keysCollected;
    string textCollect;
    //public Scene next;

    void Start(){
        updateCollectable();
    }

    public void updateCollectable(){
        string textCollect = keyCollect.ToString("0") + "/" + lockNum.ToString("0");
        keysCollected.text = textCollect;
    }

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
