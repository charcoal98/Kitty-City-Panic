using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Global : MonoBehaviour
{
    public static Global global;

    AudioSource audioSource;
    public AudioClip berdDed;
    public AudioClip carDed;
    public AudioClip win;
    public AudioClip collect;

    void Awake(){
        global = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void skinACat(){
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
    public void berdDyded(){
        audioSource.PlayOneShot(berdDed);
    }
    public void victory(){
        audioSource.PlayOneShot(win);
    }
    public void paperGet(){
        audioSource.PlayOneShot(collect);
    }
    public void papersPlease(){
        audioSource.PlayOneShot(carDed);
    }

    public IEnumerator WaitForDeathSound(){
        yield return new WaitForSeconds(.2f);
        
    }
}
