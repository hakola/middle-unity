using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    
    public Tile CurrentTile {
        get {
            return currentTile;
        }
        set {
            currentTile = value;
        }
    }

    public void emptyPosStack()
    {
        while (playerPosStack.Count != 0)
            playerPosStack.Pop();
    }

    public void addPosToStack(Tile pos)
    {
        playerPosStack.Push(pos);
    }

    public Tile getPosFromStack()
    {
        return (Tile)playerPosStack.Pop();
    }

    public bool isPosStackEmpty()
    {
        return (playerPosStack.Count == 0);
    }

    private Tile currentTile;
    private Image playerImage;
    private float distance;
    private bool canMove;
    private Stack playerPosStack = new Stack();

    void Awake() {
        playerImage = transform.Find("PlayerImage").GetComponent<Image> ();

        GameObject grid = GameObject.FindGameObjectWithTag("Grid");
        GridLayoutGroup g = grid.GetComponent<GridLayoutGroup>();
        distance = g.cellSize.x + 5;
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
    }
}
