using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    public AudioSource tindeck;
    public GameObject exclamation;

    bool m_IsPlayerInRange;
    float m_Timer;
    public float m_TimerEnd = 2f;
    bool m_ColisionDetected;
    bool m_HasAudioPlayed;

    private void Start()
    {
        exclamation.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            m_ColisionDetected = true;
        }
        else
        {
            m_ColisionDetected = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
            m_ColisionDetected = false;
            m_Timer = 0;
            exclamation.SetActive(false);
            m_HasAudioPlayed = false;
        }
    }
    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.collider.transform == player)
                {
                    exclamation.SetActive(true);
                    if (!m_HasAudioPlayed)
                    {
                        tindeck.Play();
                        m_HasAudioPlayed = true;
                    }

                    m_Timer += Time.deltaTime;
                    if(m_Timer > m_TimerEnd)
                    {
                        gameEnding.CaughtPlayer();
                    }
                }
            }
        }
        if (m_ColisionDetected == true)
        {
            Observed();
        }
    }

    void Observed()
    {
        m_IsPlayerInRange = true;
    }
}
