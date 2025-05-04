using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlayManagers
{
    public class SceneFlowManager : MonoBehaviour
    {
        public static SceneFlowManager Instance;

        private void Awake() => InitializeInstance();

        private void InitializeInstance() => Instance ??= this;
        
        public void LoadMainLevel()
        {
            SceneManager.LoadSceneAsync("MainLevel");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void LoadConfigMenu()
        {
            SceneManager.LoadSceneAsync("ConfigMenu");
        }

        public void LoadOptionMenu()
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }

        public void LoadWinLevel()
        {
            SceneManager.LoadSceneAsync("WinLevel");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void LoadLoseLevel()
        {
            SceneManager.LoadSceneAsync("LoseLevel");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
