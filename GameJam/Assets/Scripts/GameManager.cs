using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static event Action<CharacterData, int> OnWin;
    public List<GameObject> SpawnPoints;
    public List<GameObject> SpawnWalls;
    public float StartDelay;
    public float RestartDelay;
    public int MaxPlayers; 
    public static int PlayerCount;
    public int PlayersAlive;
    PlayerIndicators playerIndicators;
    private Dictionary<int, GameObject> playerSet= new();
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
        playerSet.Remove(id);
        if (PlayersAlive == 1)
        {
            GameObject go = playerSet.First().Value;
            CharacterData data = go.GetComponent<CharacterMain>().CharacterData;
            go.GetComponent<CharacterHealt>().Kill();
            FindAnyObjectByType<InGameUI>(FindObjectsInactive.Include).gameObject.SetActive(false);
            FindAnyObjectByType<EndGameUI>(FindObjectsInactive.Include).gameObject.SetActive(true);
            OnWin?.Invoke(data, go.GetComponent<CharacterMain>().ID);
            //Invoke(nameof(RestartGame), RestartDelay);
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
        playerSet.Add(PlayerCount - 1, cxt.transform.gameObject);
        FindAnyObjectByType<PlayerSelectionUI>().PlayerJoined();
        cxt.GetComponent<CharacterMain>().ID = PlayerCount - 1;
        if (PlayerCount == MaxPlayers)
        {
            Invoke(nameof(StartGame), StartDelay);
        }
    }
    void StartGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
        InGameUI inGameUI = FindAnyObjectByType<InGameUI>(FindObjectsInactive.Include);
        PlayerSelectionUI selUi = FindAnyObjectByType<PlayerSelectionUI>();
        selUi.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(true);
        foreach (GameObject w in SpawnWalls)
        {
            Destroy(w);
        }
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
