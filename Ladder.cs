using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [HideInInspector] GameObject _Ladder;
    [HideInInspector] public GameObject topTransform, botTransform;
    [HideInInspector] public GameObject BotPlatform, TopPlatform;
    [HideInInspector] public GameObject TopDrop, BotDrop;
    void Awake()
    {
        _Ladder = transform.Find("Ladder").gameObject;

        topTransform = transform.Find("TopTransform").gameObject;
        botTransform = transform.Find("BotTransform").gameObject;

        TopPlatform = transform.Find("TopPlatform").gameObject;
        BotPlatform = transform.Find("BotPlatform").gameObject;

        TopDrop = transform.Find("TopDrop").gameObject;
        BotDrop = transform.Find("BotDrop").gameObject;
    }
}
