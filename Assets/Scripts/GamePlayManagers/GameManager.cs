using System.Collections;
using UnityEngine;

namespace GamePlayManagers
{
    public class GameManager : MonoBehaviour
    {
        private float _secondsToLoadScene = 1;
        private bool _alreadyWon;
        private bool _alreadyLost;
        private bool _bossInstantiate;
        private PlayerManager _playerManager;
        
        public GameObject player;
        public Transform playerSpawnPoint;
        public EnemyManager enemyManager;
        public HUDManager hudManager;

        private void Awake()
        {
            player = Instantiate(player, playerSpawnPoint.transform.position, playerSpawnPoint.transform.rotation);
        }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _playerManager = player.GetComponent<PlayerManager>();
            enemyManager.SetPlayer(_playerManager);
            hudManager.SetPlayer(_playerManager);
        }
        
        public PlayerManager GetPlayerManager() => _playerManager;

        private IEnumerator LoadWinLevel()
        {
            yield return new WaitForSeconds(_secondsToLoadScene);
            SceneFlowManager.Instance.LoadWinLevel();
        }
        
        private IEnumerator LoadLoseLevel()
        {
            yield return new WaitForSeconds(_secondsToLoadScene);
            SceneFlowManager.Instance.LoadLoseLevel();
        }
        
        private bool ValidatePlayerDead() => _playerManager.IsPlayerDead();

        private bool ValidateTotalScore() => _playerManager.GetScore () >= 1e2;

        private bool ValidateBossDead()
        {
            if (!ValidateTotalScore()) return false;
            if (_bossInstantiate) return enemyManager.IsBossDead();
            _bossInstantiate = true;
            StartCoroutine(enemyManager.InstantiateBoss());
            return false;
        }
        
        private void VerifyWinningCondition()
        {
            if (!ValidateBossDead() || _alreadyWon) return;
            _alreadyWon = true;
            StartCoroutine(LoadWinLevel());
        }

        private void VerifyLosingCondition()
        {
            if (!ValidatePlayerDead() || _alreadyLost) return;
            _alreadyLost = true;
            StartCoroutine(LoadLoseLevel());
        }

        // Update is called once per frame
        private void Update()
        {
            VerifyWinningCondition();
            VerifyLosingCondition();
        }
    }
}