using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class GameHandler : MonoBehaviour {

      private GameObject player;
        public GameOverScreen _gameOverScreen;
      // public GameObject item;
      // private bool item_active = false;
      public int playerHealth = 150;
      public int StartPlayerHealth = 150;
      public GameObject Canvas;
      private HealthBarThirdPerson healthBarScript;

      public static bool GameisPaused = false;
      public GameObject pauseMenuUI;
      public AudioMixer mixer;
      public static float volumeLevel = 1.0f;
      private Slider sliderVolumeCtrl;

    //   public static int gotTokens = 0;
    //   public GameObject tokensText;

      public bool isDefending = false;

      public static bool stairCaseUnlocked = false;
      //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

      private string sceneName;

      public CameraShake cameraShake;

      // public CameraShake cameraShake;
      // public GameObject item2;
      // private bool item2_active = false;
      // private int Deaths = 0;

      private GameObject[] enemies;
      private GameObject[] S_Enemies;
      int enemiesLeft = 0;
      private bool noEnemies = false;
      public Text enemyCountText;

      public openDoor doorOpen;

      public bool damaged = true;

      void Awake (){
                SetLevel (volumeLevel);
                GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
                if (sliderTemp != null){
                        sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                        sliderVolumeCtrl.value = volumeLevel;
                }
      }

      void Start(){
            player = GameObject.FindWithTag("Player");
            sceneName = SceneManager.GetActiveScene().name;
            //if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
                  playerHealth = StartPlayerHealth;
            //}
            updateStatsDisplay();

            pauseMenuUI.SetActive(false);
                GameisPaused = false;

            // item_active = false;
      }

    //   public void playerGetTokens(int newTokens){
    //         gotTokens += newTokens;
    //         updateStatsDisplay();
    //   }

    void Update (){
                if (Input.GetKeyDown(KeyCode.Escape)){
                        if (GameisPaused){
                                Resume();
                        }
                        else{
                                Pause();
                        }
                }

            //     if (playerHealth <= 50 && item_active == false) {
            //         item_active = true;
            //         item.SetActive(true);
            //     }

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] S_Enemies = GameObject.FindGameObjectsWithTag("S_Enemy");
            enemiesLeft = enemies.Length + S_Enemies.Length;
            enemyCountText.text = "Enemies Left: " + enemiesLeft.ToString();

            if (enemiesLeft == 0) {
                  noEnemies = true; 
                  doorOpen.GetComponent<openDoor>().Open();
            }
     }

     public bool NoMoreEnemies() {
           return noEnemies;
     }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
        cameraShake.disableShake();
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
        cameraShake.enableShake();
    }

    public void SetLevel (float sliderValue){
        mixer.SetFloat("MasterVolume", Mathf.Log10 (sliderValue) * 20);
        volumeLevel = sliderValue;
    }
        

      public void playerGetHit(int damage){
            if (damaged == true) {
                  cameraShake.ShakeCamera(0.15f, 0.3f);

                  // Debug.Log("Player Got hit!");
                  if (isDefending == false){
                        playerHealth -= damage;
                        // cameraShake.ShakeCamera(0.15f, 0.3f);
                        if (playerHealth >=0){
                              updateStatsDisplay();
                        }
                        if (damage > 0){
                              player.GetComponent<PlayerHurt>().playerHit();       //play GetHit animation
                        }
                  }

                  if (playerHealth > StartPlayerHealth){
                        playerHealth = StartPlayerHealth;
                        updateStatsDisplay();
                  }

                  if (playerHealth <= 0){
                        playerHealth = 0;
                        updateStatsDisplay();
                        playerDies();
                  }

                  Debug.Log(playerHealth);
            }
      }

      public void enable_damage() {
            damaged = true;
      }

      public void disable_damage() {
            damaged = false;
      }

      public void updateStatsDisplay(){
            healthBarScript = Canvas.GetComponent<HealthBarThirdPerson>();
            healthBarScript.TakeDamage();

            // Text tokensTextTemp = tokensText.GetComponent<Text>();
            // tokensTextTemp.text = "GOLD: " + gotTokens;
      }

      // public void DeathCount() {
      //     Deaths++;
      //     Debug.Log(Deaths);

      //     if (Deaths >= 2 && item2_active == false) {
      //         item2_active = true;
      //         item2.SetActive(true);
      //   }

      //   if (Deaths > 4) {
      //       SceneManager.LoadScene("WinScene");
      //       Debug.Log("Goin to WinScene");
      //   }
      // }

      public void playerDies(){
            // player.GetComponent<PlayerHurt>().playerDead();       
            //play Death animation
            StartCoroutine(DeathPause());
      }

      IEnumerator DeathPause(){
            // player.GetComponent<PlayerMove>().isAlive = false;
            // player.GetComponent<PlayerJump>().isAlive = false;
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
      }

     

      public void StartGame() {
            SceneManager.LoadScene("SampleScene");
      }

      public void RestartGame() {
          Time.timeScale = 1f;
            SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
            playerHealth = StartPlayerHealth;
      }

      public void QuitGame() {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
      }

      public void Credits() {
            SceneManager.LoadScene("Credits");
      }
}