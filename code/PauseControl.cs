using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public  bool ClickedExit = false;
    public UI ui;

    GameObject MenuPanel;
    GameObject StartButton;
    GameObject StartButtonText;

    GameObject ExitButton;
    GameObject ExitButtonText;


    private void Start()
    {
        MenuPanel = new GameObject();
        StartButton = new GameObject();
        StartButtonText = new GameObject();

        ExitButton = new GameObject();
        ExitButtonText = new GameObject();

        ui = GetComponent<UI>();
        ui.CreatePanel(
                     MenuPanel,
                    "MenuPanel",
                     ui.myGO,
                     ui.UIbackgroud,
                     0.5f,
                     new Vector3(0f, 0f, 0f),
                     new Vector2(1f, 1f),
                     new Vector2(0f, 0f),
                     new Vector2(0f, 0f),
                     new Vector2(0f, 0f),
                     new Vector2(0f, 0f),
                     ui);


        ui.createButton(StartButton,
                       MenuPanel,
                       "StartButton",
                       new Vector3(0f, 0f, 0f),
                       new Vector2(0.66f, 0.8f),
                       new Vector2(0.33f, 0.6f),
                       new Vector2(0f, 0f),
                       new Vector2(0f, 0f),
                       new Vector2(0f, 0f));

        ui.CreateText(StartButtonText,
                      StartButton,
                     "Start Game",
                     ui.ArialFont,
                     new Vector2(1f, 1f),
                     new Vector2(0f, 0f),
                     new Vector2(0f, 0f),
                     new Vector2(0f, 0f)
                     );

        ui.createButton(ExitButton,
                      MenuPanel,
                      "StartButton",
                      new Vector3(0f, 0f, 0f),
                      new Vector2(0.66f, 0.5f),
                      new Vector2(0.33f, 0.3f),
                      new Vector2(0f, 0f),
                      new Vector2(0f, 0f),
                      new Vector2(0f, 0f));

        ui.CreateText(ExitButtonText,
                      ExitButton,
                     "Exit Game",
                     ui.ArialFont,
                     new Vector2(1f, 1f),
                     new Vector2(0f, 0f),
                     new Vector2(0f, 0f),
                     new Vector2(0f, 0f)
                     );


        StartButton.GetComponent<Button>().onClick.AddListener(() => { Time.timeScale = 1f; });
        ExitButton.GetComponent<Button>().onClick.AddListener(() => { ClickedExit = true; });

        gameIsPaused = !gameIsPaused;
        PauseGame();
                
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }

        if (Time.timeScale == 1f)
        {
            gameIsPaused = false;
            MenuPanel.SetActive(false);
            StartButtonText.GetComponent<Text>().text = "Resume Game";
        }

        if (GetComponent<GameControls>().EnemyBuildings.Count == 0)
        {
            StartButtonText.GetComponent<Text>().text = "Victory!!!";
        }

        if (ClickedExit)
        {
            Application.Quit();
        }
        
    }

   public void PauseGame()
    {
        if (gameIsPaused)
        {
            MenuPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            MenuPanel.SetActive(false);
            Time.timeScale = 1;
            StartButtonText.GetComponent<Text>().text = "Resume Game";
        }
    }
}
