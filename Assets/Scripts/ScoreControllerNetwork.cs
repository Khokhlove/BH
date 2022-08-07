using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.SceneManagement;

public class ScoreControllerNetwork : Singleton<ScoreControllerNetwork>
{
    [SyncVar]
    public List<PlayerScore> playerScore;
    public Text winText;
    public void AddScore(Player player)
    {
        PlayerScore score = playerScore.Find(playerScore => playerScore.player == player);
        if (score == null)
            return;
        score.score++;
        player.scoreText.text = score.score.ToString();

        if (score.score == Settings.GetInstance().ScoreToWin)
            StartCoroutine(FinishGame(player));
    }
    public void AddPlayerScore(Player player)
    {
        playerScore.Add(new PlayerScore() { player = player, score = 0});
    }

    public IEnumerator FinishGame(Player player)
    {
        float time = Settings.GetInstance().TimeRestart;
        winText.text = $"Победитель {player.name}. Игра перезапуститься через: {time}";
        winText.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        StartGame();
        winText.gameObject.SetActive(false);
    }
    [Command(requiresAuthority = false)]
    private void StartGame()
    {
        NetworkManager.singleton.ServerChangeScene(SceneManager.GetActiveScene().name);
    }
}
[Serializable]
public class PlayerScore
{
    [SerializeField]
    public Player player;
    [SerializeField]
    public int score;
}
