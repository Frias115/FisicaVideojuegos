using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public int Lives;

    void Update()
    {
        if (Lives <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("demo");
        }
    }
}
