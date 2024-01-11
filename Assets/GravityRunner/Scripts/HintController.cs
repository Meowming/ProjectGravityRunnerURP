using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintController : MonoBehaviour
{
    public float fadeInTime = 1f;
    public float fadeOutTime = 5f;
    public bool isShowingHint = false;
    public bool isFadingIn = false;
    public float alphaStrength = 0f;
    public List<GameObject> overrideHintGO = new List<GameObject>();
    public Material hintMaterial;
    //public Renderer shareMaterialRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) //TODO: move to player input
        {
            if (!isShowingHint)
            {
                ShowHint();
            }
        }
        if (isShowingHint)
        {
            if (isFadingIn)
            {
                alphaStrength += 1 / fadeInTime * Time.deltaTime;
            }
            else
            {
                alphaStrength -= 1 / fadeOutTime * Time.deltaTime;
            }
            UpdateMaterial();
        }
    }

    public void ShowHint()
    {
        StopAllCoroutines();
        alphaStrength = 0f;
        StartCoroutine(ShowHintCoroutine());
    }

    void UpdateMaterial()
    {
        Color hintColor = new Color(hintMaterial.color.r, hintMaterial.color.g, hintMaterial.color.b, alphaStrength);
        hintMaterial.color = hintColor;
    }

    IEnumerator ShowHintCoroutine()
    {
        foreach (GameObject go in overrideHintGO)
        {
            go.layer = LayerMask.NameToLayer("Highlighted");
        }
        alphaStrength = 0f;
        UpdateMaterial();
        isShowingHint = true;
        isFadingIn = true;
        yield return new WaitForSeconds(fadeInTime);
        isFadingIn = false;
        yield return new WaitForSeconds(fadeOutTime);
        isShowingHint = false;
        alphaStrength = 0f;
        UpdateMaterial();
        foreach (GameObject go in overrideHintGO)
        {
            go.layer = LayerMask.NameToLayer("Highlighted");
        }
    }
}
