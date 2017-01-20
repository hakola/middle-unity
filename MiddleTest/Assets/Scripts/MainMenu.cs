using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private Image menuImage;

    public void ToGame()
    {
        GameObject playButton = GameObject.Find("PlayButton");
        playButton.SetActive(false);
    }

    void Awake()
    {
        //menuImage = transform.Find("MenuPanel").GetComponent<Image>();
    }

    // Use this for initialization
    void Start () {
	}

    public void SetVisible()
    {
        menuImage.enabled = true;
    }

    private void SetEmpty()
    {
        menuImage.enabled = false;
    }

    // Update is called once per frame
    void Update () {
	
	}   
}
