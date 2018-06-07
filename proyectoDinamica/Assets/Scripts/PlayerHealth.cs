using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public int lives = 2;
    public float invulnerabilityTime = 1;

    int _lives;
    float _invulnerabilityTime;
    bool canBeDamaged = true;
    SkinnedMeshRenderer smr;

    void Start()
    {
        _lives = lives;
        _invulnerabilityTime = invulnerabilityTime;
        smr = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Update()
    {
        if (!canBeDamaged)
        {
            StartCoroutine("Fade");
            _invulnerabilityTime -= Time.deltaTime;
            if(_invulnerabilityTime <= 0)
            {
                canBeDamaged = true;
                _invulnerabilityTime = invulnerabilityTime;
            }
        }
    }

    public void Damage(int damage)
    {
        if (_lives <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("demo");
        }

        if (canBeDamaged)
        {
            _lives--;
            canBeDamaged = false;
        }
    }

    IEnumerator Fade()
    {
        while (!canBeDamaged)
        {
            smr.enabled = false;
            yield return new WaitForSeconds(0.15f);
            smr.enabled = true;
            yield return new WaitForSeconds(0.15f);
        }

    }
}
