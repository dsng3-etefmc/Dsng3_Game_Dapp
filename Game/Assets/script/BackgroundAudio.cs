using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundAudio : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    void Start()
    {
        GameObject[] other = GameObject.FindGameObjectsWithTag("BackgroundMusic");

        foreach (GameObject oneOther in other) {
            if (oneOther.scene.buildIndex == -1) {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(transform.gameObject);
        this.audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (this.audioSource.isPlaying) return;
        this.audioSource.Play();
    }

    public void StopMusic()
    {
        this.audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
