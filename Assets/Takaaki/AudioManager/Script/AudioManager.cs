using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;


    private void Start()
    {
        DataManger dataManger = new DataManger();
        GameData gameData = dataManger.LoadGameData();

        audioMixer.SetFloat(MasterSlider.mastrtVol, ConvertVolumeToDb(gameData.masterVolume));
        audioMixer.SetFloat(BgmSlider.bgmVol, ConvertVolumeToDb(gameData.bgmVolume));
        audioMixer.SetFloat(SeSlider.seVol, ConvertVolumeToDb(gameData.seVolume));
    }

    public float ConvertVolumeToDb(float volume)
    {
        return Mathf.Clamp(Mathf.Log10(Mathf.Clamp(volume, 0f, 1f)) * 20f, -80f, 0f);
    }
}
