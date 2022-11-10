using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager instance;
    public AudioSource[] audioSources;

    // 인스턴스 생성
    public static SoundManager Instance{
        get{
            return instance;
        }
    }

    // 입력받은 soundName에 해당하는 이름의 오디오 소스를 찾아서 해당하는게 있다면
    // 해당 오디오 재생 정지
    public void StopSound(string soundName){
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].gameObject.name.CompareTo(soundName) == 0){
                audioSources[i].Stop();
            }
        }
    }

    // 입력받은 soundName에 해당하는 이름의 오디오 소스를 찾아서 해당하는게 있다면
    // 해당 오디오 재생
    public void PlaySound(string soundName){
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].gameObject.name.CompareTo(soundName) == 0)  
            {
                audioSources[i].Play();
            }
        }
    }

    // 모든 오디오 소스 재생 음소거
    public void SoundAllMute(){
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].mute = true;
        }
    }

    // 모든 오디오 소스 음소거 해제
    public void SoundAllOn(){
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].mute = false;
        }
    }
}
