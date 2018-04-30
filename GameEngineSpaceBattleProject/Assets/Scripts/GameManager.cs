using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public Camera openSceneCam;
    public Camera asteroidCam;
    public Camera overheadCam;
    public Camera oncomingCam;
    public Camera distanceCam;
    public Camera relayLongCam;
    public Camera normandyCam;
    public Camera lastShotCam;
    [Space(10)]
    public float cameraChange1;
    public float cameraChange2;
    public float cameraChange3;
    public float cameraChange4;
    public float cameraChange5;
    public float cameraChange6;
    public float cameraChange7;
    [Space(10)]
    public float audioTiming1;
    [Space(10)]
    public GameObject normandy;
    public GameObject panel1;
    public GameObject endPanel;
    //public Text endText;
    float alpha1;
    float endAlpha;
    [Space(10)]
    public AudioClip reaperSiren;
    public AudioSource repearAudioSource;
    public AudioSource thisAS;
    bool endScene;
    Color endBlue;
    Color endBlack;
    SpriteRenderer blueSprite;
    SpriteRenderer blackSprite;

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    // Use this for initialization
    void Start () 
	{
        alpha1 = panel1.GetComponent<SpriteRenderer>().color.a;
        endBlue = panel1.GetComponent<SpriteRenderer>().color;
        blueSprite = panel1.GetComponent<SpriteRenderer>();
        endAlpha = endPanel.GetComponent<SpriteRenderer>().color.a;
        endBlack = endPanel.GetComponent<SpriteRenderer>().color;
        blueSprite = endPanel.GetComponent<SpriteRenderer>();
        asteroidCam.gameObject.SetActive(false);
        overheadCam.gameObject.SetActive(false);
        oncomingCam.gameObject.SetActive(false);
        distanceCam.gameObject.SetActive(false);
        relayLongCam.gameObject.SetActive(false);
        normandyCam.gameObject.SetActive(false);
        lastShotCam.gameObject.SetActive(false);
        StartCoroutine("StartScene");
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (endScene)
        {
            EndScene();
        }
    }

    public IEnumerator StartScene()
    {
        print("Coroutine Start");

        yield return new WaitForSeconds(cameraChange1);

        openSceneCam.gameObject.SetActive(false);
        asteroidCam.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(audioTiming1);

        repearAudioSource.clip = reaperSiren;
        repearAudioSource.Play();

        yield return new WaitForSeconds(cameraChange2);

        asteroidCam.gameObject.SetActive(false);
        overheadCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(cameraChange3);

        overheadCam.gameObject.SetActive(false);
        oncomingCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(cameraChange4);

        oncomingCam.gameObject.SetActive(false);
        distanceCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(cameraChange5);

        distanceCam.gameObject.SetActive(false);
        relayLongCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(cameraChange6);

        relayLongCam.gameObject.SetActive(false);
        normandyCam.gameObject.SetActive(true);
        normandy.GetComponent<Boid>().maxSpeed = 14;

        yield return new WaitForSeconds(cameraChange6);

        normandyCam.gameObject.SetActive(false);
        lastShotCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(cameraChange7);

        endScene = true;
        StartCoroutine("FadeOut");

        print("Coroutine End");
    }

    void EndScene()
    {
        alpha1 += 0.3f * Time.deltaTime;
        endBlue.a = alpha1;
        blueSprite.color = endBlue;

        if (alpha1 >= 1)
        {
            endAlpha += 0.3f * Time.deltaTime;
            endBlack.a = endAlpha;
            blackSprite.color = endBlack;
        }
    }
}