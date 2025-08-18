using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSoundOption : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SESlider;

	void Start()
	{
		BGMSlider.value = Managers.Sound.GetBGMVol();
		SESlider.value = Managers.Sound.GetEffectVol();

		BGMSlider.onValueChanged.AddListener(Managers.Sound.SetBGMVol);
		SESlider.onValueChanged.AddListener(Managers.Sound.SetEffectVol);

	}

}
