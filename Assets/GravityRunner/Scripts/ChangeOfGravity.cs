using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FPSController))]
public class ChangeOfGravity : MonoBehaviour
{
    public bool isReversed = false;
    public float normalGravity = -10;
    public float reversedGravity = 10;
    public FPSController fpsControl;
    public float flipDuration = 2f;
    Quaternion upRotation, downRotation;

    // Start is called before the first frame update
    void Start()
    {
        fpsControl = GetComponent<FPSController>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnChangeGravity();
        }
    }

    public void OnChangeGravity()
    {
        StopAllCoroutines();
        if (isReversed)
        {
            StartCoroutine(ChangeToNormalGravity());
        }
        else
        {
            StartCoroutine(ChangeToReversedGravity());
        }
        isReversed = !isReversed;
    }

    public IEnumerator ChangeToNormalGravity()
    {
        upRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        downRotation = Quaternion.Euler(180f, transform.rotation.eulerAngles.y, 0f);
        fpsControl.gravity = 0;
        float elapsedTime = 0f;
        while (elapsedTime < flipDuration)
        {
            elapsedTime += Time.deltaTime;
            fpsControl.gameObject.transform.localRotation = Quaternion.Lerp(downRotation, upRotation, elapsedTime / flipDuration);
            yield return null;
        }
        transform.localRotation = upRotation;
        fpsControl.gravity = normalGravity;
    }

    public IEnumerator ChangeToReversedGravity()
    {
        //TODO: revise boiler plate code
        upRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        downRotation = Quaternion.Euler(180f, transform.rotation.eulerAngles.y, 0f);
        fpsControl.gravity = 0;
        float elapsedTime = 0f;
        while (elapsedTime < flipDuration)
        {
            elapsedTime += Time.deltaTime;
            fpsControl.gameObject.transform.localRotation = Quaternion.Lerp(upRotation, downRotation,  elapsedTime / flipDuration);
            yield return null;
        }      
        transform.localRotation = downRotation;
        fpsControl.gravity = reversedGravity;
    }
}
