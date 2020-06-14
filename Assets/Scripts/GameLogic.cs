using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private int stage = 1;
    private int lastStage = 3;
    private bool changeLevel;
    private bool gameOver;
    public Camera[] cameras;
    public GameObject restart;
    public GameObject canvas;
    public GameObject gameOverText;
    public GameObject scoreText;
    public GameObject congratulationsText;
    private int score = 0;
    private bool levelDone;
    private UnityEngine.UI.Text scoreTextComponent;

    void Start()
    {
        gameOver = false;
        scoreTextComponent = scoreText.GetComponent<UnityEngine.UI.Text>();
    }

    void Update()
    {
        if (stage < lastStage && GameObject.FindGameObjectsWithTag("points_" + stage).Length == 0)
        {
            changeLevel = true;
            cameras[1].enabled = true;
            cameras[0].enabled = false;
            stage++;
        }
        else if(stage == lastStage && GameObject.FindGameObjectsWithTag("points_" + stage).Length == 0 && !levelDone)
        {
            levelDone = true;
            var congratsText = Instantiate(congratulationsText);

            congratsText.transform.SetParent(canvas.transform);
            congratsText.transform.localPosition = new Vector3(0, 0);
        }

        scoreTextComponent.text = "Score: " + score;

    }

    public int GetStage()
    {
        return stage;
    }
    public bool GetIsChangeLevel()
    {
        return changeLevel;
    }
    public void SetIsChangeLevel(bool changeLevel)
    {
        this.changeLevel = changeLevel;
    }
    public Camera GetEnabledCamera()
    {
        for(int i = 0; i < cameras.Length; i++)
        {
            if(cameras[i].enabled)
            {
                return cameras[i];
            }
        }
        return null;
    }
    public void SetEnabledCamera(int cameraToBeEnabled)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if(cameras[i].enabled)
            {
                cameras[cameraToBeEnabled].transform.position = cameras[i].transform.position;
            }
            if(i == cameraToBeEnabled)
            {
                cameras[i].enabled = true;
            }
            else
            {
                cameras[i].enabled = false;
            }
           
        }
    }
    public void EndGame()
    {
        gameOver = true;

        var restartObj =  Instantiate(restart);

        restartObj.transform.SetParent(canvas.transform);
        restartObj.transform.localPosition = new Vector3(0, 0);

        
        var gameOverObj = Instantiate(gameOverText);

        gameOverObj.transform.SetParent(canvas.transform);
        gameOverObj.transform.localPosition = new Vector3(-510, 290);

    }

    public bool GetGameOver()
    {
        return gameOver;
    }

    public void IncrementScore(int points)
    {
        score += points;
    }

    public bool GetLevelDone()
    {
        return levelDone;
    }

}
