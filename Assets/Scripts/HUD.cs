using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Text Health;
    [SerializeField] Text Ammo;
    [SerializeField] Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        Health.text = player.CurrentHealth.ToString();
    }
}
