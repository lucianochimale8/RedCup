using TMPro;
using UnityEngine;

public class BreathingText : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float minAlpha = 0.3f;
    [SerializeField] private float maxAlpha = 1f;

    private TMP_Text text;
    private Color color;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        color = text.color;
    }

    private void Update()
    {
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(Time.time * speed) + 1f) / 2f);

        color.a = alpha;
        text.color = color;
    }
}
