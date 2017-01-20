using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

    public int indX;
    public int indY;

    public int Strength {
        get {
            return strength;
        }
        set {
            strength = value;
            if (strength == 0) {
                ApplyStyle(strength);
                SetVisible();
            } else {
                ApplyStyle(strength);
                SetVisible();
            }
        }
    }

    public bool HasPlayer {
        get
        {
            return hasPlayer;
        } set
        {
            hasPlayer = value;
            if (hasPlayer)
                PlayerImage.enabled = true;
            else
                PlayerImage.enabled = false;
        }
    }

    private int strength;

    private bool hasPlayer;

    private Text TileText;
    private Image TileImage;
    private Image PlayerImage;

    void Awake() {
        TileText = GetComponentInChildren<Text> ();
        TileImage = transform.Find("TilePresentation").GetComponent<Image> ();
        PlayerImage = transform.Find("PlayerPresentation").GetComponent<Image>();
    }

    void ApplyStyleFromHolder(int index) {
        if (index == 0)
            TileText.text = "";
        else if (index == 1)
            TileText.text = "";
        else
            TileText.text = TileStyleHolder.Instance.TileStyles[index].Number.ToString();
        TileText.color = TileStyleHolder.Instance.TileStyles[index].TextColor;
        TileImage.color = TileStyleHolder.Instance.TileStyles[index].TileColor;
    }

    void ApplyStyle (int num) {
        switch (num) {
            case -1:
                ApplyStyleFromHolder(0);
                break;
            case 0:
                ApplyStyleFromHolder(1);
                break;
            case 1:
                ApplyStyleFromHolder(2);
                break;
            case 2:
                ApplyStyleFromHolder(3);
                break;
            case 3:
                ApplyStyleFromHolder(4);
                break;
            case 4:
                ApplyStyleFromHolder(5);
                break;
            case 5:
                ApplyStyleFromHolder(6);
                break;
            case 6:
                ApplyStyleFromHolder(7);
                break;
            default:
                Debug.LogError("Check the numbers you pass to ApplyStyle!");
                break;
        }
    }

    private void SetVisible() {
        TileImage.enabled = true;
        TileText.enabled = true;
    }

    private void SetEmpty()
    {
        TileImage.enabled = false;
        TileText.enabled = false;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
