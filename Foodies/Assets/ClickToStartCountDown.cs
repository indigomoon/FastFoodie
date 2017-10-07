using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickToStartCountDown : MonoBehaviour
{

	private GameObject[] recipes;
	private string[] letters;
	public GameObject CountDownTimer;
	private float CountDownTime;
	public bool trigger;

	void Start()
	{
		//Find allthe recipes;
		recipes = GameObject.FindGameObjectsWithTag("Recipe");
		//Debug.Log("CountDownTime is " + CountDownTime);
		//Debug.Log(recipes.Length);



	}

	void Awake()
	{
		//trigger = true;

	}

	void Update()
	{
		//Trigger the InitialiseTimer function
		if (!trigger)
		{
			return;
		}
		else
		{
			trigger = false;
			InitialiseTimer();

		}



	}

	public void TrigerCountDown() {
		trigger = true;
	}

	//Initialise the time, find the cooking time in the recipe's steps
	void InitialiseTimer()
	{

		//if there is no time keywords in the step, skip it
		foreach (GameObject recipe in recipes)
		{
			string ThisStep = recipe.GetComponent<Text>().text;
			//string ThisStep = "Cook it for 2 minute..";
			//print(ThisStep);
			if (!ThisStep.Contains("min") && !ThisStep.Contains("sec") &&
				!ThisStep.Contains("second") && !ThisStep.Contains("minute")
				&& !ThisStep.Contains("hour"))
			{
				return;
			}
			else
			{
				//Find out if there is numbers in the recipe. If there is some numbers,
				//it means with the time key word the chance of having a cooking time is high

				float Number;

				//TryParse is the way to find if the string is a number.
				//If the string is not a number, it will retun 0.
				//Normal recipe will not ask for 0 minute, 0 second, or 0 hour,
				//so if the number is 0, there is no need for the countdown timer

				float.TryParse(ThisStep, out Number);
				//print(letters[i] + "Number is+ " +Number);
				if (
					Number != 0
				)
				{
					return;
				}
				else
				{

					FindCookTime(ThisStep);

					SendTime();
				}
			}



		}
	}

	void FindCookTime(string CookStepText)
	{

		//Extract the numbers and the time keywords

		//print(CookStepText);
		char[] stringSeparators = new char[] { ',', '.', ' ', '!', ')' };
		letters = CookStepText.Split(stringSeparators);
		//print(letters.Length);
		for (int i = 1; i < letters.Length; i++)
		{
			//Find the numbers
			//print(letters[i]);
			float Number;
			float.TryParse(letters[i], out Number);
			//print(letters[i] + "Number is+ " +Number);
			if (
				Number != 0
			)
			{
				//if there is a time keyword after numbers, it mean it's a discription for a certain amount of time
				//In normal recipes, this discription means there is a need for a countdown timer

				if (letters[i + 1] == "minute" || letters[i + 1] == "minutes")
				{
					CountDownTime = int.Parse(letters[i]) * 60f + 1f;
					print("Sent");
				}

				if (letters[i + 1] == "hour" || letters[i + 1] == "hours")
				{
					CountDownTime = int.Parse(letters[i]) * 3600f + 1f;
				}
				if (letters[i + 1] == "second" || letters[i + 1] == "seconds")
				{
					CountDownTime = int.Parse(letters[i]) + 1f;
				}
				if (letters[i + 1] == "min" || letters[i + 1] == "mins")
				{
					CountDownTime = int.Parse(letters[i]) * 60f + 1f;
				}
				if (letters[i + 1] == "sec" || letters[i + 1] == "secs")
				{

					CountDownTime = int.Parse(letters[i]) + 1f;
					//print(CountDownTime);
				}

			}

		}

	}

	void SendTime()
	{
		//CountDownTimer.SetActive(true);
		CountDownTimer.GetComponent<CountDownTimer>().Timer = CountDownTime;
		CountDownTimer.GetComponent<CountDownTimer>().StartToCountDown = false;
		CountDownTimer.GetComponent<CountDownTimer>().StartToCountDown = true;
	}
}
