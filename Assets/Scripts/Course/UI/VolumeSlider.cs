using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _mixergruop;

    private void Start()
    {
        var slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(SliderValueChanged);
        var val = PlayerPrefs.GetFloat(_mixergruop, 0.75f);
        slider.value = val;

    }

    private void SliderValueChanged(float newValue)
    {
        _mixer.SetFloat(_mixergruop, Mathf.Log10(newValue) * 20);
        PlayerPrefs.SetFloat("bgm", newValue);
        PlayerPrefs.Save();
    }
}
