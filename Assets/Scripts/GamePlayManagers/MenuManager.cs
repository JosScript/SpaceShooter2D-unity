using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GamePlayManagers
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        
        [SerializeField] private List<Button> _levelButtonList = new List<Button>();
        
        private void Awake()
        {
            InitializeButtonList();
            InitializeInstance();
        }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start() => SceneManager.sceneLoaded += OnSceneLoaded;

        private void InitializeInstance() => Instance ??= this;
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) => InitializeButtonList();

        private void AssingButtonEvent(Button button)
        {
            switch (button.gameObject.tag)
            {
                case "MainLevelButton":
                    button.onClick.AddListener(
                        delegate
                        {
                            Debug.Log("MainLevel Button Clicked");
                            SceneFlowManager.Instance.LoadMainLevel();
                        });
                    break;
                case "ConfigMenuButton":
                    button.onClick.AddListener(
                        delegate
                        {
                            SceneFlowManager.Instance.LoadConfigMenu();
                        });
                    break;
                case "OptionMenuButton":
                    button.onClick.AddListener(
                        delegate
                        {
                            SceneFlowManager.Instance.LoadOptionMenu();
                        });
                    break;
                case "QuitButton":
                    button.onClick.AddListener(
                        delegate
                        {
#if UNITY_EDITOR
                            UnityEditor.EditorApplication.isPlaying = false;
#else
		                Application.Quit();
#endif
                        });
                    break;
            }
        }

        private void AssignButtonListEvent()
        {
            foreach (var button in _levelButtonList)
            {
                AssingButtonEvent(button);
            }
        }

        private void PopulateButtonList()
        {
            foreach (var button in GameObject.FindObjectsOfType<Button>())
            {
                _levelButtonList.Add(button);
            }
        }

        private void InitializeButtonList()
        {
            _levelButtonList.Clear();
            PopulateButtonList();
            AssignButtonListEvent();
        }
    }
}