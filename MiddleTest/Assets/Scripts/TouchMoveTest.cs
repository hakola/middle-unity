using UnityEngine;
using System.Collections;

public class TouchMoveTest : MonoBehaviour
{

    // Update is called once per frame
    public float speed = 1000000F;

    private GameManager gm;

    void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    /* Not working properly */
    void Update()
    {
        if (gm.State == GameState.LevelSelect && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(touchDeltaPosition.x * speed, touchDeltaPosition.y * 0, 0);
        }
    }
}
