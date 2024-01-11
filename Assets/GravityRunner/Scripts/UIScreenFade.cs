using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenFade : MonoBehaviour, ITriggerable
{
    public Image UIBlackBackground;
    public Color initalColor;
    public Color endColor;
    public float duration;
    public bool isFadeIn;
    public void OnTriggered()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Color.Lerp(initalColor, endColor, elapsedTime / duration);
            if (isFadeIn)
            {
                UIBlackBackground.color = Color.Lerp(endColor, initalColor, elapsedTime / duration);
            }
            else
            {
                UIBlackBackground.color = Color.Lerp(initalColor, endColor, elapsedTime / duration);
            }
            yield return null;
        }
        UIBlackBackground.color = endColor;
    }
}
