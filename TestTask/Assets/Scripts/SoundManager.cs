using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public event Action ChangedSettings;
    private float _volume;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _volume = PlayerPrefs.GetFloat("SoundVolume", 1f);
        DontDestroyOnLoad(this);
    }
    
    /// <param name="value">integer value in range [0, 100]</param>
    public void SetVolume(int value)
    {
        if(value < 0)
            value = 0;
        else if(value > 100)
            value = 100;

        _volume = value / 100f;

        PlayerPrefs.SetFloat("SoundVolume", _volume);
        PlayerPrefs.Save();
        ChangedSettings?.Invoke();
    }
    public float GetVolume() => _volume;
    
}
