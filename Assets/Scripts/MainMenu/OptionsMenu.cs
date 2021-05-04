using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	[SerializeField] private AudioMixer audioMixer;
	[SerializeField] private GameObject lowQualityMessage;
	[SerializeField] private Dropdown resolutionDropdown;

	private Resolution[] resolutions;

	void Start() {
		lowQualityMessage.SetActive(false);

		//Populates the dropdown menu with all compatible screen resolutions
		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions();

		List<string> options = new List<string>();

		int currResolutionIndex = 0;
		for(int i = 0; i < resolutions.Length; i++) {
			string option = resolutions[i].width + " x " + resolutions[i].height;
			options.Add(option);

			if(resolutions[i].width == Screen.currentResolution.width &&
			   resolutions[i].height == Screen.currentResolution.height) {

				currResolutionIndex = i;
			}
		}

		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value = currResolutionIndex;
		resolutionDropdown.RefreshShownValue();
	}

	public void SetResolution(int resolutionIndex) {
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetVolume(float volume) {
		audioMixer.SetFloat("volume", volume);
	}

	public void SetQuality(int qualityIndex) {
		QualitySettings.SetQualityLevel(qualityIndex);

		//Warn player that lighting effects will be ruined if using Low or Very Low quality setting
		if(qualityIndex >= 4) {
			lowQualityMessage.SetActive(true);
		} else {
			lowQualityMessage.SetActive(false);
		}
	}

	public void SetFullscreen(bool isFullscreen) {
		Screen.fullScreen = isFullscreen;
	}

}
