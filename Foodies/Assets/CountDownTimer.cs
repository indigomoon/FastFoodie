using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{

	public float Timer;
	private string Minute;
	private string Second;
	private AudioSource AlarmSound;
	private bool CountDownRunning;
	private bool AlarmIsPlaying;
	public Transform CountDownClock;
	public bool StartToCountDown;


	void Awake() {
		StartToCountDown = false;
		AlarmSound = GetComponent<AudioSource>();
		CountDownRunning = true;
		AlarmIsPlaying = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (!StartToCountDown)
		{
			return;
		}
		else {
			CountDown();
			StartToCountDown = true;
		}
	}

	private void CountDown() {

		//Countdown then destroy this gameObject
		//Debug.Log(Timer);
		if (CountDownRunning)
		{
			Timer -= Time.deltaTime;
		}
		else
		{
			Timer = 0f;
		}



		Minute = Mathf.Floor((int)Timer / 60).ToString("00");
		Second = Mathf.Floor((int)Timer % 60).ToString("00");
		//print((int)Timer);
		GetComponent<Text>().text = Minute + " : " + Second;

		if ((int)Timer == 0 && !AlarmIsPlaying)
		{
			CountDownRunning = false;
			AlarmSound.Play();
			AlarmIsPlaying = true;
		}

		if (AlarmIsPlaying)
		{
			//transform.Rotate(Vector3.forward * Mathf.Clamp((Mathf.Cos(Time.time * 60f) * 180) / Mathf.PI * 0.5f, -2f, 2f), Space.Self);
		}
		else
		{
			if (!CountDownRunning)
			{
				Destroy(gameObject);
			}

		}
	}

	public void SilenceAlarm() {
		AlarmIsPlaying = false;
	}
}
