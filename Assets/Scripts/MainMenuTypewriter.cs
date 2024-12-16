using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainMenuTypewriter : MonoBehaviour
{
    [SerializeField] private List<MainMenuTypewriterKey> keys;
    private List<string> heldedIds;

    private void Awake()
    {
        keys = GetComponentsInChildren<MainMenuTypewriterKey>().ToList();
    }

    private void Start()
    {
        CheckKeys();
    }

    private void CheckKeys()
    {
        heldedIds = GameManager.instance.Data.typewritesIds;
        foreach (var key in keys)
            key.gameObject.SetActive(heldedIds.Contains(key.KeyId));
    }
}