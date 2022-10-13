using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    public TextMeshProUGUI visualiseVolume;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
            Load();
        }

        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        visualiseVolume.text = "" + ((int)(volumeSlider.value * 100))+"%";
        Save();
    }

    void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        visualiseVolume.text = ""+((int)(volumeSlider.value*100))+"%";
    }
}
