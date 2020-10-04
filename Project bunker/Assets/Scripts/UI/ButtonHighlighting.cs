using UnityEngine;

public class ButtonHighlighting : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void OnMouseEnter()
    {
        rectTransform.localScale *= 1.05f;
    }

    public void OnMouseExit()
    {
        rectTransform.localScale = new Vector2(1, 1);
    }
}
