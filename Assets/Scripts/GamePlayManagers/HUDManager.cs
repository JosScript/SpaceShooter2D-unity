using System.Collections;
using GamePlayManagers;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private RawImage[] hearts;
    [SerializeField] private Text scoreText;
    private PlayerManager _player;
    private int _hp;
    private int _score;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        StartCoroutine(WaitPlayerSpawn());
        _hp = hearts.Length;
    }
    
    public void SetPlayer(PlayerManager player) => _player = player;
    
    private IEnumerator WaitPlayerSpawn()
    {
        yield return new WaitUntil(() => _player);
    }
    
    private void CheckHealthPoints()
    {
        var nHearts = _player.GetHp();
        if (nHearts == _hp) return;
        for (var i = 0; i < 5 - nHearts; i++)
            hearts[i].enabled = false;
        _hp = nHearts;
    }

    private void CheckScore()
    {
        var score = _player.GetScore();
        if (score == _score) return;
        scoreText.text = score.ToString("D6");
        _score = score;
    }
    
    // Update is called once per frame
    private void Update()
    {
        CheckHealthPoints();
        CheckScore();
    }
}
