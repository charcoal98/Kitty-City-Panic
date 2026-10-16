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
    public string nextScene;
    //public Scene next;

    void Start(){
        updateCollectable();
        print(SceneManager.GetActiveScene().name);
    }

    public void updateCollectable(){
        string textCollect = "DOCUMENTS: " + keyCollect.ToString("0") + "/" + lockNum.ToString("0");
        keysCollected.text = textCollect;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == 13) {
            if (keyCollect >= lockNum){
                Global.global.victory();
                StartCoroutine(WinJingleWait());
                
            }
            else 
            {
                Global.global.papersPlease();
            }
        }
    }

    public IEnumerator WinJingleWait(){
        yield return new WaitForSeconds(.6f);
        string currentScene = SceneManager.GetActiveScene().name; 
        SceneManager.LoadScene(nextScene);
    }
}
