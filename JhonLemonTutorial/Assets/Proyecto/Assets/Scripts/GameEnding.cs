using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float FadeDuration = 1f;   
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBacgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
 

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }
    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }
    void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBacgroundImageCanvasGroup, false);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true);
        }
    }
    void EndLevel(CanvasGroup ImageCanvasGroup, bool doRestart)
    {
        m_Timer += Time.deltaTime;
        ImageCanvasGroup.alpha = m_Timer / FadeDuration;

        if (m_Timer > FadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
