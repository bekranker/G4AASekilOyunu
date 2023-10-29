using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class CreateAudio
{
    public static AudioClip FindClip(string ClipName, string AudioMixerName = "General", string AudioMixerGroupName = "Sound")
    {
        AudioClip audioClip = Resources.Load("Audio/" + ClipName) as AudioClip;
        if (audioClip == null) { Debug.LogWarning(ClipName + " Not Found"); return null; }
        return audioClip;
    }

    public static void PlayAudio(string ClipName, float volume = 1f, string AudioMixerName = "General", string AudioMixerGroupName = "Sound")
    {
        if (ClipName == "") return;
        AudioClip audioClip = Resources.Load("Audio/" + ClipName) as AudioClip;
        if (audioClip == null) { Debug.LogWarning(ClipName + " Not Found"); return; }
        PlayAudio(audioClip, volume, AudioMixerName, AudioMixerGroupName);
    }
    public static void PlayAudio(AudioClip clip, float volume = 1f, string AudioMixerName = "General", string AudioMixerGroupName = "Sound")
    {
        if (clip == null) return;
        // iOS can be problem 
        AudioMixer audioMixer = Resources.Load("Audio/" + AudioMixerName) as AudioMixer;
        AudioMixerGroup group = audioMixer.FindMatchingGroups(AudioMixerGroupName)[0];

        PlayAudio(clip, group, volume);
    }

    public static void PlayAudio(AudioClip clip, AudioMixerGroup group, float volume = 1f)
    {
        if (clip == null) return;
        GameObject gameObject = new GameObject("One shot audio");
        gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);
        gameObject.transform.position = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        if (group != null)
            audioSource.outputAudioMixerGroup = group;
        audioSource.clip = clip;
        audioSource.spatialBlend = 1f;
        audioSource.volume = volume;
        audioSource.loop = false;
        audioSource.Play();
        Object.Destroy(gameObject, clip.length);
    }
}