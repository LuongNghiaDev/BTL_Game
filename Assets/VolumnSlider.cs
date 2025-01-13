using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumnSlider : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        AudioListener.volume = 1.0f;
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    void ChangeVolume()
    {
        // Đặt giá trị âm lượng của AudioListener theo giá trị của Slider
        AudioListener.volume = volumeSlider.value;
    }
}
