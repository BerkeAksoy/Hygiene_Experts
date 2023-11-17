using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RawImage fillImage;
    private RectTransform maskRectTransform;
    private float maskWidth;
    public Gradient gradient;

    private void Awake()
    {
        maskRectTransform = transform.Find("Mask").GetComponent<RectTransform>();
        fillImage = transform.Find("Mask/Fill").GetComponent<RawImage>();

        maskWidth = maskRectTransform.sizeDelta.x;
    }

    public void updateHealth(Enemy enemy)
    {
        float normalizedHealth = normalizeHealth(enemy);

        Vector2 maskSizeDelta = maskRectTransform.sizeDelta;
        maskSizeDelta.x = normalizedHealth * maskWidth;
        maskRectTransform.sizeDelta = maskSizeDelta;

        fillImage.color = gradient.Evaluate(normalizedHealth);
    }

    private float normalizeHealth(Enemy enemy)
    {
        return enemy.getCurHealth() / enemy.getMaxHealth();
    }



}
