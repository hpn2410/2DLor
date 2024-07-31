using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    private bool isSettingOpen;
    public GameObject settingMenu;
    public Slider _musicSlider, _sfxSlider;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Setting") && !isSettingOpen)
        {
            Time.timeScale = 0;
            Debug.Log("Open");
            isSettingOpen = true;
            settingMenu.SetActive(true);
        }
        else if (Input.GetButtonDown("Setting") && isSettingOpen)
        {
            Time.timeScale = 1;
            Debug.Log("Close");
            isSettingOpen = false;
            settingMenu.SetActive(false);
        }
    }

    public void ToggleMusic()
    {
        AudioScript.instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioScript.instance.ToggleSFX();
    }

    public void ChangeVolume()
    {
        AudioScript.instance.ChangeVolume(_musicSlider.value);
    }

    public void ChangeSFX()
    {
        AudioScript.instance.ChangeSFX(_sfxSlider.value);
    }
}
