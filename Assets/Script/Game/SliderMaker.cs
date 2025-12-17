using UnityEngine;
using UnityEngine.UI;

public class SliderMaker : MonoBehaviour
{
    private Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    
    void Update()
    {
        if (!GameManager.Instance.isRandomBox)
        {
            slider.value = GameManager.Instance.SliderCount;
        }
        else // isRandomBox == true
        {
            if (slider.value > 0f)
            {
                slider.value -= Time.deltaTime * 20f;
            }

            if (slider.value <= 0f)
            {
                slider.value = 0f;
                GameManager.Instance.SliderCount = 0;
            }
        }
    }

}
