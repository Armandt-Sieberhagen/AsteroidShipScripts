using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{

    public EnemySpawner Spawner;
    public TextMeshProUGUI texts;
    void FixedUpdate()
    {
        texts.text = Spawner.CurrentRocksBroken.ToString();
    }
}
