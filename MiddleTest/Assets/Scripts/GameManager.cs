using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

public enum GameState
{
    StartScreen,
    ChapterSelect,
    LevelSelect,
    Playing
}

public class GameManager : MonoBehaviour {

    public GameState State;

    private Tile[,] allTiles;
    private int sideLength;
    private List<Tile> emptyTiles = new List<Tile>();
    private Player player = new Player();
    private GameObject smallGrid;
    private GameObject bigGrid;
    private GameObject menuPanel;
    private GameObject ChapterHolder;
    private GameObject tittleText;
    private GameObject lionChapterPanel;
    private LevelManager lm = new LevelManager();
    private Level activeLevel;
    GameObject playButton;


    void Start () {
        menuPanel = GameObject.Find("MenuPanel");
        smallGrid = GameObject.Find("5x5");
        bigGrid = GameObject.Find("7x7");
        playButton = GameObject.Find("PlayButton");
        tittleText = GameObject.Find("TittleText");
        lionChapterPanel = GameObject.Find("LionChapterPanel");
        ChapterHolder = GameObject.Find("ChapterHolder");
        ChapterHolder.SetActive(false);
        State = GameState.StartScreen;
    }

    void ShowMenu()
    {
        menuPanel.SetActive(true);
    }

    void HideMenu()
    {
        menuPanel.SetActive(false);
    }

    public void toLevelSelect(ChapterManager cm)
    {
        if (cm.State == ChapterState.Lion)
        {
            HideMenu();
            State = GameState.LevelSelect;
        }
    }

    public void BackToLevelSelect()
    {
        player.CurrentTile.HasPlayer = false;
        player.emptyPosStack();
        smallGrid.SetActive(false);
        bigGrid.SetActive(false);
        lionChapterPanel.SetActive(true);
        State = GameState.LevelSelect;
    }

    public void toChapterSelect()
    {
        playButton.SetActive(false);
        tittleText.SetActive(false);
        ChapterHolder.SetActive(true);
        State = GameState.ChapterSelect;
    }

    public void BackToChapterSelect()
    {
        ShowMenu();
        State = GameState.ChapterSelect;
    }

    public void loadLevel(string lName)
    {
        lm.LoadFromJson(lName);
        Level activeLevel = lm.ActiveLevel;
        sideLength = activeLevel.sideLength - 1;
        if(sideLength == 4)
        {
            smallGrid.SetActive(true);
            bigGrid.SetActive(false);
        } else
        {
            smallGrid.SetActive(false);
            bigGrid.SetActive(true);
        }

        allTiles = new Tile[activeLevel.sideLength, activeLevel.sideLength];
        Tile[] AllTilesOneDim = GameObject.FindObjectsOfType<Tile>();
        foreach (Tile t in AllTilesOneDim)
        {
            t.Strength = activeLevel.tileArray[t.indX, t.indY];
            if (t.Strength == -1)
            {
                t.HasPlayer = true;
                player.CurrentTile = t;
            }
            else
                t.HasPlayer = false;

            allTiles[t.indX, t.indY] = t;
            if (t.Strength == 0)
                emptyTiles.Add(t);
        }
        lionChapterPanel.SetActive(false);
        State = GameState.Playing;
    }

    // Update is called once per frame
    void Update () {    
    }

    public void Move(MoveDirection md) {
        Debug.Log(md.ToString() + " move.");

        switch(md) {
            case MoveDirection.Left:
                MoveOneTileLeft();
                break;
            case MoveDirection.Right:
                MoveOneTileRight();
                break;
            case MoveDirection.Up:
                MoveOneTileUp();
                break;
            case MoveDirection.Down:
                MoveOneTileDown();
                break;
        }
        if(isLevelComplete())
            Debug.Log("Level Complete!");
        else if (isGameOver(player.CurrentTile.indX, player.CurrentTile.indY))
            Debug.Log("Game over!");
    }

    public void undoMove()
    {
        if(!player.isPosStackEmpty())
        {
            player.CurrentTile.HasPlayer = false;
            player.CurrentTile = player.getPosFromStack();
            if (player.CurrentTile.Strength != -1)
            {
                player.CurrentTile.Strength++;
            }
            player.CurrentTile.HasPlayer = true;
        }
    }

    /* Moving functions, return values are not yet used in anyway */
    bool MoveOneTileLeft() {
        if(player.CurrentTile.indX != 0 && allTiles[player.CurrentTile.indX-1, player.CurrentTile.indY].Strength != 0) {
            player.addPosToStack(player.CurrentTile);
            if(player.CurrentTile.Strength != -1) {
                player.CurrentTile.Strength--;
            }
            player.CurrentTile.HasPlayer = false;
            player.CurrentTile = allTiles[player.CurrentTile.indX - 1, player.CurrentTile.indY];
            player.CurrentTile.HasPlayer = true;
            Debug.Log("Players currentTile: " + player.CurrentTile.indX + ", " + player.CurrentTile.indY);
            Debug.Log("Last tile's strength: " + allTiles[player.CurrentTile.indX + 1, player.CurrentTile.indY].Strength);
            return true;
        } else 
            return false;
    }

    bool MoveOneTileRight()
    {
        if (player.CurrentTile.indX != sideLength && allTiles[player.CurrentTile.indX + 1, player.CurrentTile.indY].Strength != 0)
        {
            player.addPosToStack(player.CurrentTile);
            if (player.CurrentTile.Strength != -1)
            {
                player.CurrentTile.Strength--;
            }
            player.CurrentTile.HasPlayer = false;
            player.CurrentTile = allTiles[player.CurrentTile.indX + 1, player.CurrentTile.indY];
            player.CurrentTile.HasPlayer = true;
            Debug.Log("Players currentTile: " + player.CurrentTile.indX + ", " + player.CurrentTile.indY);
            Debug.Log("Last tile's strength: " + allTiles[player.CurrentTile.indX - 1, player.CurrentTile.indY].Strength);
            return true;
        }
        else
            return false;
    }

    bool MoveOneTileUp()
    {
        if (player.CurrentTile.indY != 0 && allTiles[player.CurrentTile.indX, player.CurrentTile.indY-1].Strength != 0)
        {
            player.addPosToStack(player.CurrentTile);
            if (player.CurrentTile.Strength != -1)
            {
                player.CurrentTile.Strength--;
            }
            player.CurrentTile.HasPlayer = false;
            player.CurrentTile = allTiles[player.CurrentTile.indX, player.CurrentTile.indY-1];
            player.CurrentTile.HasPlayer = true;
            Debug.Log("Players currentTile: " + player.CurrentTile.indX + ", " + player.CurrentTile.indY);
            Debug.Log("Last tile's strength: " + allTiles[player.CurrentTile.indX, player.CurrentTile.indY+1].Strength);
            return true;
        }
        else
            return false;
    }

    bool MoveOneTileDown()
    {
        if (player.CurrentTile.indY != sideLength && allTiles[player.CurrentTile.indX, player.CurrentTile.indY + 1].Strength != 0)
        {
            player.addPosToStack(player.CurrentTile);
            if (player.CurrentTile.Strength != -1)
            {
                player.CurrentTile.Strength--;
            }
            player.CurrentTile.HasPlayer = false;
            player.CurrentTile = allTiles[player.CurrentTile.indX, player.CurrentTile.indY + 1];
            player.CurrentTile.HasPlayer = true;
            Debug.Log("Players currentTile: " + player.CurrentTile.indX + ", " + player.CurrentTile.indY);
            Debug.Log("Last tile's strength: " + allTiles[player.CurrentTile.indX, player.CurrentTile.indY - 1].Strength);
            return true;
        }
        else
            return false;
    }

    /* Checks if level is completed */
    bool isLevelComplete()
    {
        if (player.CurrentTile.Strength == -1)
        {
            bool isComplete = true;
            for(int x = 0; x < sideLength; ++x)
            {
                for (int y = 0; y < sideLength; ++y)
                {
                    if (allTiles[x, y].Strength > 0)
                    {
                        isComplete = false;
                        x = sideLength;
                        y = sideLength;
                    }
                }
            }
            return isComplete;
        }
        else
            return false;
    }

    /* Checks if game is over */
    bool isGameOver(int x, int y)
    {   if (x == 0) {
            if (y == 0) {
                if (allTiles[x + 1, y].Strength == 0 && allTiles[x, y + 1].Strength == 0)
                    return true;
            } else if(y == sideLength) {
                if (allTiles[x + 1, y].Strength == 0 && allTiles[x, y - 1].Strength == 0)
                    return true;
            } else {
                if (allTiles[x + 1, y].Strength == 0 && allTiles[x, y - 1].Strength == 0 && allTiles[x, y + 1].Strength == 0)
                    return true;
            }
        } else if(x == sideLength) {
            if (y == 0)
            {
                if (allTiles[x - 1, y].Strength == 0 && allTiles[x, y + 1].Strength == 0)
                    return true;
            }
            else if (y == sideLength)
            {
                if (allTiles[x - 1, y].Strength == 0 && allTiles[x, y - 1].Strength == 0)
                    return true;
            }
            else {
                if (allTiles[x - 1, y].Strength == 0 && allTiles[x, y - 1].Strength == 0 && allTiles[x, y + 1].Strength == 0)
                    return true;
            }
        } else {
            if (y == 0)
            {
                if (allTiles[x - 1, y].Strength == 0 && allTiles[x + 1, y].Strength == 0 && allTiles[x, y + 1].Strength == 0)
                    return true;
            }
            else if (y == sideLength)
            {
                if (allTiles[x - 1, y].Strength == 0 && allTiles[x + 1, y].Strength == 0 && allTiles[x, y - 1].Strength == 0)
                    return true;
            }
            else {
                if (allTiles[x - 1, y].Strength == 0 && allTiles[x + 1, y].Strength == 0 && allTiles[x, y - 1].Strength == 0
                    && allTiles[x, y + 1].Strength == 0 && allTiles[x, y - 1].Strength == 0)
                    return true;
            }
        }
        return false;
    }
}



