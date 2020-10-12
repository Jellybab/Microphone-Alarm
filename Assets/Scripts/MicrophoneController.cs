using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MicrophoneController : MonoBehaviour {


    //microphone 
    public static float MicLoudness;
    public float miclimit;

    //alarm and timers
    public GameObject alarmGO;
    playaudioclip alarm;
    public float timer = 20;
    private float temp;

    public float volumeTimer;
    private float tempTimer;


    //ui


    //mic initialization
    void InitMic() {
        _clipRecord = Microphone.Start(null, true, 999, 44100);


    }

    private void Start() {
        alarm = alarmGO.GetComponent<playaudioclip>();
        temp = timer;
        tempTimer = volumeTimer;
        TimerText.text =  "5 Secs";
        VolumeLimitText.text =  "-180 db";
    }

    AudioClip _clipRecord;
    int _sampleWindow = 128;

    //get data from microphone into audioclip
    float LevelMax() {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        _clipRecord.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++) {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak) {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }





    void Update() {
        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere
        MicLoudness = LevelMax();
        tempTimer -= Time.deltaTime;
        float db = 20 * Mathf.Log10(Mathf.Abs(MicLoudness));

        if ( tempTimer <=  0) {
            VolumeCurrent.text = db.ToString();
            tempTimer = volumeTimer;
        }
      

        if (db < miclimit || db == Mathf.Infinity) {
            //Debug.Log("counting");
            temp -=Time.deltaTime;
            if (temp <= 0) {
                alarm.playAudioClip();
                temp = 5;
            }
        } else {
            temp = timer;
        }
       // Debug.Log(timer2);
    }

    // start mic when scene starts
    void OnEnable() {
        InitMic();

    }

    //UI Stuff

    public Text VolumeLimitText;
    public Text VolumeCurrent;
    public Text TimerText;


    public void SetVolume(Slider volume) {
        miclimit = volume.value;
        VolumeLimitText.text = volume.value + " db";
    }

    public void SetTimer(Slider Timer) {
        timer = Timer.value;
        TimerText.text = Timer.value + " Secs";
    }



}
    


