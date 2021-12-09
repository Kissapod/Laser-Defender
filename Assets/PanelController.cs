using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PanelController : MonoBehaviour
{
    [HideInInspector]
    public bool isPaused, isShowUpgradeMenu;
    [SerializeField]
    private KeyCode pauseButton;
    [SerializeField]
    private GameObject pausePanel;
    private MoverBonus[] bonusesMove;

    private void Start()
    {
        isShowUpgradeMenu = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            isPaused = !isPaused;
        }
        /*if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            isShowUpgradeMenu = !isShowUpgradeMenu;
        }*/
        if (isPaused)
        {
            bonusesMove = FindObjectsOfType<MoverBonus>();
            foreach (MoverBonus bonus in bonusesMove)
            {
                bonus.enabled = false;
            }
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            bonusesMove = FindObjectsOfType<MoverBonus>();
            foreach (MoverBonus bonus in bonusesMove)
            {
                bonus.enabled = true;
            }
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }
        if (!isShowUpgradeMenu)
        {
            bonusesMove = FindObjectsOfType<MoverBonus>();
            foreach (MoverBonus bonus in bonusesMove)
            {
                bonus.enabled = false;
            }
            //GameObject.Find("UpMenu").GetComponent<Animator>().SetBool("ShowUpgradeMenu", true);
            //Invoke(nameof(StopStartTime), 0.1f);
            Time.timeScale = 0;
        }
        else 
        {
            bonusesMove = FindObjectsOfType<MoverBonus>();
            foreach (MoverBonus bonus in bonusesMove)
            {
                bonus.enabled = true;
            }
            GameObject.Find("UpMenu").GetComponent<Animator>().SetBool("ShowUpgradeMenu", false);
            Invoke(nameof(StopStartTime), 0.1f);
        }
    }

    void StopStartTime()
    {
        if (isPaused || isShowUpgradeMenu)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1f;
        }
    }
}
