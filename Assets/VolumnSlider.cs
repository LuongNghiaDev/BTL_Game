using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumnSlider : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // Đặt giá trị ban đầu của Slider theo giá trị âm lượng hiện tại
        volumeSlider.value = AudioListener.volume;

        // Đăng ký sự kiện để lắng nghe sự thay đổi giá trị của Slider
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    void ChangeVolume()
    {
        // Đặt giá trị âm lượng của AudioListener theo giá trị của Slider
        AudioListener.volume = volumeSlider.value;
    }
}
