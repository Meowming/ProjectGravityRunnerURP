using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerChangeLevel : MonoBehaviour, ITriggerable
{
    public float delay = 3f;
    public string targetLevel = "";

    
    public void OnTriggered()
    {
        StartCoroutine(ChangeLevelDelayed());
    }

    IEnumerator ChangeLevelDelayed()
    {
        yield return new WaitForSeconds(delay);
        ChangeLevel();
    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene(targetLevel);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
