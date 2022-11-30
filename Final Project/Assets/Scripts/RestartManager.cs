using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
    public static RestartManager instance { get; private set; }

    public bool canRestart = false;

    private void Awake()
    {
        instance = this;
        canRestart = false;
    }

    private void Update()
    {
        if (canRestart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
                canRestart = false;
            }
        }
    }
}
