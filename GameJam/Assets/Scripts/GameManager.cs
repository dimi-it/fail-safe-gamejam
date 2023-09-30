using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public float StartDelay;
    public float RestartDelay;
    public int MaxPlayers; 
    public static int PlayerCount;
    public int PlayersAlive;
    PlayerIndicators playerIndicators;
    private void OnEnable()
    {
        CharacterHealt.OnDeath += CharacterHealt_OnDeath;
    }
    private void OnDisable()
    {
        CharacterHealt.OnDeath -= CharacterHealt_OnDeath;

    }
    private void CharacterHealt_OnDeath(int id)
    {
        PlayersAlive--;
        if (PlayersAlive == 1)
        {
            FindAnyObjectByType<CharacterHealt>(FindObjectsInactive.Exclude).Kill();
            FindAnyObjectByType<InGameUI>(FindObjectsInactive.Include).gameObject.SetActive(false);
            FindAnyObjectByType<EndGameUI>(FindObjectsInactive.Include).gameObject.SetActive(true);
            Invoke(nameof(RestartGame), RestartDelay);
        }
    }

    private void Start()
    {
        playerIndicators = FindAnyObjectByType<PlayerIndicators>(FindObjectsInactive.Include);
        PlayersAlive = MaxPlayers;
    }
    public void OnPlayerJoined(PlayerInput cxt)
    {
        if (PlayerCount == MaxPlayers)
        {
            Destroy(cxt.gameObject);
            return;
        }
        PlayerCount++;
        PlayerIndicator indicator = playerIndicators.Indicators[PlayerCount - 1];
        indicator.gameObject.SetActive(true);
        indicator.Unit = cxt.transform;
        FindAnyObjectByType<PlayerSelectionUI>().PlayerJoined();
        cxt.GetComponent<CharacterMain>().ID = PlayerCount - 1;
        if (PlayerCount == MaxPlayers)
        {
            //Invoke(nameof(StartGame), StartDelay);
        }
    }
    void StartGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
        InGameUI inGameUI = FindAnyObjectByType<InGameUI>(FindObjectsInactive.Include);
        PlayerSelectionUI selUi = FindAnyObjectByType<PlayerSelectionUI>();
        selUi.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(true);
    }
    void RestartGame()
    {
        SceneManager.UnloadSceneAsync("UI");
        SceneManager.UnloadSceneAsync("SampleScene");
        Restart();
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("UI"));
        FindAnyObjectByType<PlayerSelectionUI>(FindObjectsInactive.Include).gameObject.SetActive(true);
        FindAnyObjectByType<MainMenuUi>(FindObjectsInactive.Include).gameObject.SetActive(false);

    }
    public void Restart()
    {
        PlayersAlive = MaxPlayers;
        PlayerCount = 0;
    }
}
