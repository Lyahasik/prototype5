using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int Value = 15;
    public GameObject PrefabDied;
    public bool Die = false;
    public Text TextHealth;
    public GameObject MenuDie;

    public void TakeDamage(int value)
    {
        Value -= value;
        if (TextHealth)
        {
            TextHealth.text = "x" + Value;
        }

        if (Die == false && Value <= 0)
        {
            Die = true;
            Instantiate(PrefabDied, transform.position + Vector3.up, transform.rotation);

            if (gameObject.name != "Player")
            {
                Invoke("DestroyObject", 5.0f);
            }
            else
            {
                MenuDie.SetActive(true);
            }
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
