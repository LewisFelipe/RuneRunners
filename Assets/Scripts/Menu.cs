using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private static Menu mManager;
    public static Menu Singleton
    {
        get
        {
            if (mManager == null)
                mManager = FindObjectOfType<Menu>();
            return mManager;
        }
    }

    public float tutorialTime;
    public static bool isPaused;
    private GameObject pauseScreen;
    private GameObject optionsScreen;
    private GameObject information;
    private GameObject tutorialScreen;
    private GameObject credits;

    public Sprite[] muteImagesUI;
    private bool musicMuted;
    private bool soundMuted;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("PauseScreen") != null)
        {
            pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen");
            pauseScreen.SetActive(false);
        }
        if (GameObject.FindGameObjectWithTag("OptionsScreen") != null)
        {
            optionsScreen = GameObject.FindGameObjectWithTag("OptionsScreen");
            optionsScreen.SetActive(false);
        }
        if (GameObject.FindGameObjectWithTag("Information") != null)
        {
            information = GameObject.FindGameObjectWithTag("Information");
            information.SetActive(false);
        }
        if (GameObject.FindGameObjectWithTag("TutorialScreen1") != null)
        {
            tutorialScreen = GameObject.FindGameObjectWithTag("TutorialScreen1");
            tutorialScreen.SetActive(false);
        }
        if (GameObject.FindGameObjectWithTag("Credits") != null)
        {
            credits = GameObject.FindGameObjectWithTag("Credits");
            credits.SetActive(false);
        }
    }

    private void Start()
    {
        isPaused = false;
    }

    public void StartGame()
    {
        AudioManager.Singleton.PlaySoundEffect(0);

        if (PlayerPrefs.GetInt("HighestScore") == 0)
        {
            StartCoroutine("TutorialStart");
        }
        else
            SceneManager.LoadScene("GamePlay");
    }

    private IEnumerator TutorialStart()
    {
        tutorialScreen.SetActive(true);
        yield return new WaitForSeconds(tutorialTime);
        SceneManager.LoadScene("GamePlay");
    }


    public void QuitGame()
    {
        AudioManager.Singleton.PlaySoundEffect(0);
        Application.Quit();
    }

    public void Options()
    {
        AudioManager.Singleton.PlaySoundEffect(0);
        optionsScreen.SetActive(true);
    }

    public void PauseGame()
    {
        isPaused = true;
        AudioManager.Singleton.PlaySoundEffect(0);
        pauseScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        AudioManager.Singleton.PlaySoundEffect(0);

        if (HealthSystem.isDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        pauseScreen.SetActive(false);
        isPaused = false;
    }

    public void BackToMenu()
    {
        AudioManager.Singleton.PlaySoundEffect(0);
        SceneManager.LoadScene("Menu");
    }

    IEnumerator InformationManager()
    {
        yield return new WaitUntil(() => (Input.touchCount > 0 || Input.GetMouseButtonUp(0)));
        information.SetActive(true);
        yield return new WaitUntil(() => Input.touchCount == 0);
        yield return new WaitUntil(() => (Input.touchCount > 0 || Input.GetMouseButtonUp(0)));
        AudioManager.Singleton.PlaySoundEffect(0);
        tutorialScreen.SetActive(true);
        yield return new WaitUntil(() => Input.touchCount == 0);
        yield return new WaitUntil(() => (Input.touchCount > 0 || Input.GetMouseButtonUp(0)));
        AudioManager.Singleton.PlaySoundEffect(0);
        credits.SetActive(true);
        yield return new WaitUntil(() => Input.touchCount == 0);
        yield return new WaitUntil(() => (Input.touchCount > 0 || Input.GetMouseButtonUp(0)));
        AudioManager.Singleton.PlaySoundEffect(0);
        information.SetActive(false);
        tutorialScreen.SetActive(false);
        credits.SetActive(false);
    }

    public void Informations()
    {
        AudioManager.Singleton.PlaySoundEffect(0);
        StartCoroutine("InformationManager");
    }

    public void Options_Back()
    {
        AudioManager.Singleton.PlaySoundEffect(0);
        optionsScreen.SetActive(false);
    }

    public void MusicMuteButton()
    {
        AudioManager.Singleton.PlaySoundEffect(0);
        if (!musicMuted)
        {
            AudioManager.Singleton.MuteMusic(musicMuted);
            GameObject.FindGameObjectWithTag("MusicMuteButton").GetComponent<Image>().sprite = muteImagesUI[0];
            musicMuted = true;
        }
        else if (musicMuted)
        {
            AudioManager.Singleton.MuteMusic(musicMuted);
            GameObject.FindGameObjectWithTag("MusicMuteButton").GetComponent<Image>().sprite = muteImagesUI[1];
            musicMuted = false;
        }
    }

    public void SoundMuteButton()
    {
        AudioManager.Singleton.PlaySoundEffect(0);
        if (!soundMuted)
        {
            AudioManager.Singleton.MuteSound(soundMuted);
            GameObject.FindGameObjectWithTag("SoundMuteButton").GetComponent<Image>().sprite = muteImagesUI[2];
            soundMuted = true;
        }
        else if (soundMuted)
        {
            AudioManager.Singleton.MuteSound(soundMuted);
            GameObject.FindGameObjectWithTag("SoundMuteButton").GetComponent<Image>().sprite = muteImagesUI[3];
            soundMuted = false;
        }
    }

    public void MusicChangeVolumeSlider()
    {
        if (!musicMuted)
        {
            AudioManager.Singleton.ChangeMusicVolume(GameObject.FindGameObjectWithTag("MusicVolumeSlider").GetComponent<Slider>().value);
        }
    }

    public void SoundChangeVolumeSlider()
    {
        if (!soundMuted)
        {
            AudioManager.Singleton.ChangeSoundVolume(GameObject.FindGameObjectWithTag("SoundVolumeSlider").GetComponent<Slider>().value);
        }
    }
}