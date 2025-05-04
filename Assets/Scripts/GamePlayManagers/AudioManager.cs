using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlayManagers
{
    public class AudioManager : MonoBehaviour
    {
        public List<AudioSource> bgmSourceList = new();
        public List<AudioClip> bgmClipList = new ();

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        private void PlayClip(int sourceIndex, int clipIndex, List<AudioSource> sourceList, List<AudioClip> clipList, bool shouldLoop)
        {
            if (sourceList == null || clipList == null || sourceList.Count == 0 || clipList.Count == 0)
                return;
            AudioClip currentAudioClip = null;
            if (clipList[clipIndex] != null)
            {
                currentAudioClip = clipList[clipIndex];
            }

            AudioSource currentAudioSource = null;
            if (sourceList[sourceIndex] != null)
            {
                currentAudioSource = sourceList[sourceIndex];
            }

            if (currentAudioSource == null) return;
            currentAudioSource.loop = shouldLoop;
            currentAudioSource.clip = currentAudioClip;
            currentAudioSource.Play();
        }

        private void PlayBGM(int clipIndex, bool shouldLoop) =>
            PlayClip(clipIndex, clipIndex, bgmSourceList, bgmClipList, shouldLoop);

        private void PlayMenuBGM() => PlayBGM(0, true);
        private void PlayMainLevelBGM() => PlayBGM(1, true);
        private void PlayWinLevelBGM() => PlayBGM(2, true);
        private void PlayLoseLevelBGM() => PlayBGM(3, true);

        private void PlayCurrentSceneBGM()
        {
            var currentSceneName = SceneManager.GetActiveScene().name;
            switch (currentSceneName)
            {
                case "MainMenu":
                    PlayMenuBGM();
                    break;
                case "MainLevel":
                    PlayMainLevelBGM();
                    break;
                case "WinLevel":
                    PlayWinLevelBGM();
                    break;
                case "LoseLevel":
                    PlayLoseLevelBGM();
                    break;
                default: break;
            }
        }

        private void StopAudio(List<AudioSource> sourceList)
        {
            foreach (var audioSource in sourceList)
                if (audioSource != null)
                    audioSource.Stop();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            StopAudio(bgmSourceList);
            PlayCurrentSceneBGM();
        }
    }
}
