using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UnityEngine.UI.Text scoreLable;
    public UnityEngine.UI.Text healthLable;
    public UnityEngine.UI.Text scoreRecordLable;
    public UnityEngine.UI.Image menu;
    public UnityEngine.UI.Button startButton;
    public UnityEngine.UI.Button mainMenuButton;
    public UnityEngine.UI.Button restartButton;
    public UnityEngine.UI.Button closeCredits;
    public UnityEngine.UI.Button openCredits;
    public UnityEngine.UI.Button burgerButton;
    public UnityEngine.UI.Button exitGameButton;
    public UnityEngine.UI.Button closeMenuButton; //кнопка выхода из бургер меню

    public GameObject player;
    public GameObject gameMenu;
    public GameObject burgerMenu;
    public GameObject creditsGrid;
    public GameObject handlers; //папка элементов управления персонажем
    public GameObject leftJoystick; //левый джойстик для перемещений

    public bool isStarted = false;
    public bool isBurgerMenuOpened = false; //нужно для правильной установки игры в паузу
    private bool paused = true; //буль для отслеживания состояния паузы игры
    
    SaveGame saveGame = new SaveGame();
    LoadGame loadGame = new LoadGame();

    int score = 0;
    int maxScore; //записывается последний лучший результат
    //int health = 0;

    public static GameController instance;

    public void IncrementScore(int increment)
    {
        score += increment;
        scoreLable.text = "Score: " + score;
        if (score > maxScore)
        {
            maxScore = score;
            scoreRecordLable.text = "Best Score: " + maxScore;
            saveGame.MaxScore = maxScore;
        } 
    }

    public void ChangeHealth(int health) //этот метод лишь отображает здоровье. подсчет идет непосредственно в объекте игрока
    {
        //health += change;
        healthLable.text = "Health: " + health; 
    }

    public void ActiveMenuButton()
    {
        handlers.gameObject.SetActive(false);
        gameMenu.gameObject.SetActive(true);
    }

    void RestartGame() //очищает сцену перед запуском новой игры
    {
        saveGame.MaxScore = maxScore;
        saveGame.Save();

        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid"); // поиск массива объектов
        foreach (GameObject item in asteroids)
        {
            Destroy(item);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // поиск массива объектов
        foreach (GameObject item in enemies)
        {
            Destroy(item);
        }
        GameObject[] lasers = GameObject.FindGameObjectsWithTag("RedEnemyLaser"); // поиск массива объектов
        foreach (GameObject item in lasers)
        {
            Destroy(item);
        }
        GameObject[] shields = GameObject.FindGameObjectsWithTag("Shield"); // поиск массива объектов
        foreach (GameObject item in shields)
        {
            Destroy(item);
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); // поиск массива объектов
        foreach (GameObject item in players)
        {
            Destroy(item);
        }

        Instantiate(player, player.transform.position, Quaternion.identity); //добавляем игрока только по нажатию на кнопку Start
        IncrementScore(-score);
    }

    // Start is called before the first frame update
    void Start()
    {
        loadGame.LoadSavedGame();
        maxScore = loadGame.MaxScore;
        scoreRecordLable.text = "Best Score: " + maxScore; //принудительно изменяет отображаемое количество очков при старте, после загрузки

        Debug.Log("maxScore in GameController after load game = " + maxScore.ToString());
        
        menu.gameObject.SetActive(true); //при запуске активируем главное меню
        burgerMenu.SetActive(false); //и деактивируем меню Бургера
        creditsGrid.gameObject.SetActive(false);
        handlers.gameObject.SetActive(false);
        instance = this;

        startButton.onClick.AddListener(delegate { //запуск игры из главного меню
            menu.gameObject.SetActive(false);
            isStarted = true;
            paused = false;
            Time.timeScale = 1;
            gameMenu.gameObject.SetActive(false);
            handlers.gameObject.SetActive(true); //активируем джойстик ПЕРЕД созданием игрока (игрок создается в RestartGame())
            RestartGame();
        });

        openCredits.onClick.AddListener(delegate { //открыть Кредитс
            creditsGrid.gameObject.SetActive(true);
        });

        closeCredits.onClick.AddListener(delegate { //закрыть Кредитс
            creditsGrid.gameObject.SetActive(false);
        });

        mainMenuButton.onClick.AddListener(delegate { //выход в главное меню
            isStarted = false;
            RestartGame(); // делаем рестарт, чтобы уничтожить всех оставшихся врагов и астероидов
            Debug.Log("Before Destroy player");
            //Destroy(player); // после чего удаляем игрока, т.к. он создается в методе RestartGame()
            Debug.Log("After Destroy player");
            gameMenu.gameObject.SetActive(false);
            handlers.gameObject.SetActive(false); // отключаем джойстик ПОСЛЕ игрока, иначе возникнет ошибка при поиске объекта джойстика
            Time.timeScale = 0;
            paused = true;
            menu.gameObject.SetActive(true);
        });

        burgerButton.onClick.AddListener(delegate { //выход в главное меню
            //isStarted = false;
            isBurgerMenuOpened = true; //нужно будет продумать как паузить игру
            Time.timeScale = 0; //ставим игру на паузу
			paused = true; //пока не знаю нужно ли отслеживать это состояние. Не проще ли считывать из Time.timeScale
            //handlers.gameObject.SetActive(false); // отключаем джойстик ПОСЛЕ игрока, иначе возникнет ошибка при поиске объекта джойстика
            burgerMenu.SetActive(true);
            creditsGrid.gameObject.SetActive(false);
        });

        restartButton.onClick.AddListener(delegate { //перезапуск игры
            menu.gameObject.SetActive(false);
            gameMenu.gameObject.SetActive(false);
            handlers.gameObject.SetActive(true); //активируем Джойстик ДО создания игрока
            RestartGame();
            //isBurgerMenuOpened = true; //нужно будет продумать как паузить игру
            //Time.timeScale = 0; //ставим игру на паузу
            isStarted = true;
        });

        exitGameButton.onClick.AddListener(delegate { //выход из игры
            Debug.Log("ExitGame button clicked");
            burgerMenu.SetActive(false);
            Application.Quit();
        });

        closeMenuButton.onClick.AddListener(delegate { //закрыть бургер меню
            burgerMenu.SetActive(false);
            isBurgerMenuOpened = false;
            Time.timeScale = 1;
            paused = false;
        });
    }

}
