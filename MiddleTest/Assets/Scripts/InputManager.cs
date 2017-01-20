using UnityEngine;
using System.Collections;

/* Not used currently */
 
public enum MoveDirection {
    Left, Right, Up, Down
}

public class InputManager : MonoBehaviour {

    private GameManager gm;

    void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManager> ();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Move right
            gm.Move(MoveDirection.Right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Move left
            gm.Move(MoveDirection.Left);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Move up
            gm.Move(MoveDirection.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Move down
            gm.Move(MoveDirection.Down);
        }
    }
}


