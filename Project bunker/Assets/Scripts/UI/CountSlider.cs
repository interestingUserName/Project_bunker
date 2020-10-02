using UnityEngine;
using UnityEngine.UI;

public class CountSlider : MonoBehaviour
{
    public Text countText;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void OnChangedValue()
    {
        countText.text = slider.value.ToString();
    }
}
