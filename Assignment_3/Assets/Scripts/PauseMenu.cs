using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject confirmMenu;
    public AudioClip onClickClip;
    public AudioSource audioSource;
    public Gun gun;
    public GameObject upgradeButton;
    private int upgradeCost = 50;

    void Start()
    {
        pauseMenu.SetActive(false);
        confirmMenu.SetActive(false);
        upgradeButton.GetComponent<TextMeshProUGUI>().text = "+Damage (" + upgradeCost + ")";
        Debug.Log("+Damage (" + upgradeCost + ")");
    }

    void Update()
    {
        if(Keyboard.current.pKey.isPressed){
            Pause();
        }
    }

    //Methods for buttons
    public void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gun.enabled = false;
        Debug.Log(upgradeButton);
        upgradeButton.GetComponent<TextMeshProUGUI>().text = "+Damage (" + upgradeCost + ")";
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gun.enabled = true;
    }


    public void ClickAndLoadMainMenu(){
        audioSource.PlayOneShot(onClickClip);
        Time.timeScale = 1;
        Invoke("LoadMainMenu", 0.3f);
    }
    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToPauseMenu(){

        confirmMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void GoToConfirmMenu(){
        pauseMenu.SetActive(false);
        confirmMenu.SetActive(true);
    }

    public void playClickSound(){
        audioSource.PlayOneShot(onClickClip);
    }

    public void UpgradeDamage()
    {
        if (GameObject.FindFirstObjectByType<RoundHandler>().money > upgradeCost)
        {
            GameObject.FindFirstObjectByType<RoundHandler>().money -= upgradeCost;
            GameObject.Find("assault1").GetComponent<Gun>().damage += 2;

            upgradeCost += 150;
            upgradeButton.GetComponent<TextMeshProUGUI>().text = "+Damage (" + upgradeCost + ")";
        }
    }
}
