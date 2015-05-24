using UnityEngine;
using System.Collections;

public static class PlayerPrefsManager {

	public const string ALLOW_ADS = "AllowAds";
	public const string ALLOW_SOUND = "AllowSound";
	public const string TIME_MODE_50_BEST_TIME = "TimeMode50BestTime";
	public const string TIME_MODE_100_BEST_TIME = "TimeMode100BestTime";
	public const string TIME_MODE_150_BEST_TIME = "TimeMode150BestTime";
	public const string ENDLESS_MODE_5_BEST_TIME = "EndlessMode5BestTime";
	public const string ENDLESS_MODE_25_BEST_TIME = "EndlessMode25BestTime";
	public const string ENDLESS_MODE_50_BEST_TIME = "EndlessMode50BestTime";
	public const string NO_BLACK_BUBBLES_COUNT_All_MODES = "NoBlackBubblesCountAllModes";
	public const string NO_BLACK_BUBBLES_COUNT_TIME_MODE_50 = "NoBlackBubblesCountTimeMode50";
	public const string NO_BLACK_BUBBLES_COUNT_TIME_MODE_100 = "NoBlackBubblesCountTimeMode100";
	public const string NO_BLACK_BUBBLES_COUNT_TIME_MODE_150 = "NoBlackBubblesCountTimeMode150";
	public const string NO_BLACK_BUBBLES_COUNT_ENDLESS_MODE_5 = "NoBlackBubblesCountEndlessMode5";
	public const string NO_BLACK_BUBBLES_COUNT_ENDLESS_MODE_25 = "NoBlackBubblesCountEndlessMode25";
	public const string NO_BLACK_BUBBLES_COUNT_ENDLESS_MODE_50 = "NoBlackBubblesCountEndlessMode50";
	public const string TIME_MODE_50_SUCCEEDING_WINS = "TimeMode50SucceedingWins";
	public const string TIME_MODE_100_SUCCEEDING_WINS = "TimeMode100SucceedingWins";
	public const string TIME_MODE_150_SUCCEEDING_WINS = "TimeMode150SucceedingWins";
	public const string ENDLESS_MODE_5_SUCCEEDING_WINS = "EndlessMode5SucceedingWins";
	public const string ENDLESS_MODE_25_SUCCEEDING_WINS = "EndlessMode25SucceedingWins";
	public const string ENDLESS_MODE_50_SUCCEEDING_WINS = "EndlessMode50SucceedingWins";

	public static bool GetBool (string p_key, bool p_defaultValue) {
		if(PlayerPrefs.HasKey(p_key)) {
			return (PlayerPrefs.GetInt(p_key) == 1 ? true : false);
		} else {
			PlayerPrefs.SetInt(p_key, p_defaultValue ? 1 : 0);
			PlayerPrefs.Save();
			return p_defaultValue;
		}
	}

	public static void SetBool (string p_key, bool p_value) {
		PlayerPrefs.SetInt(p_key, p_value ? 1 : 0);
		PlayerPrefs.Save();
	}

	public static float GetFloat (string p_key, float p_defaultValue) {
		if(PlayerPrefs.HasKey(p_key)) {
			return PlayerPrefs.GetFloat(p_key);
		} else {
			PlayerPrefs.SetFloat(p_key, p_defaultValue);
			PlayerPrefs.Save();
			return p_defaultValue;
		}
	}

	public static void SetFloat (string p_key, float p_value) {
		PlayerPrefs.SetFloat(p_key, p_value);
		PlayerPrefs.Save();
	}

	public static int GetInt (string p_key, int p_defaultValue) {
		if(PlayerPrefs.HasKey(p_key)) {
			return PlayerPrefs.GetInt(p_key);
		} else {
			PlayerPrefs.SetInt(p_key, p_defaultValue);
			PlayerPrefs.Save();
			return p_defaultValue;
		}
	}
	
	public static void SetInt (string p_key, int p_value) {
		PlayerPrefs.SetInt(p_key, p_value);
		PlayerPrefs.Save();
	}
}
