using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuTurretScript : MonoBehaviour
{

    bool _rotate = false;

    public GameObject PartToRotate;

    public Text PlayText;

    private Color _playTextColor;

    // Start is called before the first frame update
    void Start()
    {
        PlayText.GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!_rotate) return;
        PartToRotate.transform.RotateAroundLocal(new Vector3(0, 0, 1), 0.1f);
    }

    void OnMouseEnter()
    {
        _rotate = true;
        PlayText.GetComponent<Text>().text = "Play !";
    }

    void OnMouseDown()
    {
        SceneManager.LoadScene("MainScene");
    }

    void OnMouseExit()
    {
        _rotate = false;
        PlayText.GetComponent<Text>().text = "";
    }

}
