using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public Canvas ResultCanvas;
    public static GameManager instance;
    public Text ResultText;
    public AudioClip tdMusic;
    private AudioSource _auS;

    void Awake()
    {
        if (instance != null) return;
        instance = this;
        
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        ResultCanvas.enabled = false;
        _auS = GetComponent<AudioSource>();
        _auS.clip = tdMusic;
        _auS.loop = true;
        _auS.Play();
    }
    public void LoseLogic()
    {
        ResultText.text = "YOU LOST !";
        ResultText.color = Color.red;
        ResultCanvas.enabled = true;
        _auS.Stop();
    }

    public void WinLogic()
    {
        ResultText.text = "YOU WON !";
        ResultText.color = Color.green;
        ResultCanvas.enabled = true;
        _auS.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
