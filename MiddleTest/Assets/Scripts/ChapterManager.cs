using UnityEngine;
using System.Collections;

public enum ChapterState
{
    Lion,
    Penguin
}


public class ChapterManager : MonoBehaviour {

    public ChapterState State;

    private Animator anim;
    private bool levelHolderOpen = false;

	void Awake () {
        anim = GetComponent<Animator>();
        State = ChapterState.Lion;
	}
	
    /* Animation trigger functions, hope to get rid of them some day */
    public void LionToPenguin()
    {
        State = ChapterState.Penguin;
        anim.SetTrigger("LionToPenguin");
    }

    public void PenguinToLion()
    {
        State = ChapterState.Lion;
        anim.SetTrigger("PenguinToLion");
    }

    public void BounceLeft()
    {
        anim.SetTrigger("BounceLeft");
    }

    public void BounceRight()
    {
        anim.SetTrigger("BounceRight");
    }

    public void OpenLevelHolder()
    {
        if (!levelHolderOpen)
        {
            levelHolderOpen = true;
            anim.SetTrigger("LevelHolderPop");
        }
        else {
            levelHolderOpen = false;
            anim.SetTrigger("LevelHolderClose");
        }
    }

    public void CloseLevelHolder()
    {
        anim.SetTrigger("LevelHolderClose");
    }
}
