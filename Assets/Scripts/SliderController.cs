using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private void Start()
    {
        Slider slider = GetComponentInChildren<Slider>();
        slider.onValueChanged.AddListener((value) => SoundManager.Instance.SetVolume(value));
        slider.onValueChanged.AddListener((value) => GetComponentInChildren<TMPro.TextMeshProUGUI>().text = ((int)(value*100)).ToString());
        slider.onValueChanged.AddListener((value) =>
        {
            if (value == 0f)
            {
                GetComponentInChildren<Image>().color = Color.red;
            }
            else
            {
                GetComponentInChildren<Image>().color = Color.white;
            }
        });
    }

}
