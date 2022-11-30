using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UIFixedRobots : MonoBehaviour
{
    public static UIFixedRobots instance { get; private set; }

    private int fixedRobots = 0;

    public int numberOfRobots;

    public TextMeshProUGUI fixedText = null;

    public GameObject winMessage;

    public AudioSource musicSource;
    public AudioClip winMusic;

    public bool wonLevel = false;

    public bool lastLevel = false;

    private void Awake()
    {
        instance = this;
        winMessage.SetActive(false);
    }

    private void Start()
    {
        fixedRobots = 0;
        fixedText.text = "Fixed Robots: " + fixedRobots.ToString() + "/" + numberOfRobots.ToString();
    }

    public void FixedRobot()
    {
        fixedRobots++;

        fixedText.text = "Fixed Robots: " + fixedRobots.ToString() + "/" + numberOfRobots.ToString();

        if (fixedRobots == numberOfRobots)
        {
            winMessage.SetActive(true);

            if (lastLevel)
            {
                musicSource.clip = winMusic;
                musicSource.Play();
                RestartManager.instance.canRestart = true;
            }


            wonLevel = true;
        }
    }
}
