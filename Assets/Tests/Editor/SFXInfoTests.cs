using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class SFXInfoTests
{
    private readonly string testAudioClipPath = "Assets/Audio/Sfx/ForestSpirit.wav";

    [Test]
    [TestCase(0.31f, 1.5f)]
    [TestCase(1.2f, 4f)]
    [TestCase(-0.6f, -0.2f)]
    public void TestGlobalSFX(float volume, float pitch) {
        GameObject audioSourceGameObject = new GameObject();
        AudioSource audioSource = audioSourceGameObject.AddComponent<AudioSource>();

        AudioClip audioClip = (AudioClip)AssetDatabase.LoadMainAssetAtPath(testAudioClipPath);
        Assert.IsNotNull(audioClip, "Missing audioClip");

        SFXInfo sfxInfo = new SFXInfo(audioClip, volume, pitch);
        sfxInfo.ConfigureAudioSource(audioSource);

        Assert.AreEqual(audioClip.length, sfxInfo.ClipDuration);
        Assert.AreEqual(Mathf.Clamp01(volume), audioSource.volume);
        Assert.AreEqual(Mathf.Clamp(pitch, -3, 3), audioSource.pitch);
        Assert.AreEqual(0, audioSource.spatialBlend);
    }

    [Test]
    [TestCase(0.31f, 1.5f, 12, -40)]
    [TestCase(1.2f, 4f, -5, 23)]
    [TestCase(-0.6f, -0.2f, 36, 55)]
    public void TestLocalSFX(float volume, float pitch, float x, float y) {
        GameObject audioSourceGameObject = new GameObject();
        AudioSource audioSource = audioSourceGameObject.AddComponent<AudioSource>();

        AudioClip audioClip = (AudioClip)AssetDatabase.LoadMainAssetAtPath(testAudioClipPath);
        Assert.IsNotNull(audioClip, "Missing audioClip");

        SFXInfo sfxInfo = new SFXInfo(audioClip, volume, pitch, new Vector2(x,y));
        sfxInfo.ConfigureAudioSource(audioSource);

        Assert.AreEqual(audioClip.length, sfxInfo.ClipDuration);
        Assert.AreEqual(Mathf.Clamp01(volume), audioSource.volume);
        Assert.AreEqual(Mathf.Clamp(pitch, -3, 3), audioSource.pitch);
        Assert.AreEqual(1, audioSource.spatialBlend);
        Assert.AreEqual(new Vector2(x, y), (Vector2)audioSourceGameObject.transform.position);
    }
}
