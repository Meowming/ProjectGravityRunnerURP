using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGravityIndicator : MonoBehaviour
{
    public RectTransform gravityIndicator;

    private void Start()
    {
        PlayManager.gravityIndicator = this;
    }
    public void OnGravityFlip()
    {
        Vector3 invertedScale = gravityIndicator.localScale;
        invertedScale.y *= -1;
        gravityIndicator.localScale = invertedScale;
    }
}
