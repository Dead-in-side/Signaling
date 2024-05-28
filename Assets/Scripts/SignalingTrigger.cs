using System;
using UnityEngine;

[RequireComponent (typeof(Collider))]

public class SignalingTrigger : MonoBehaviour
{
    private Collider _collider;

    public event Action ThiefEnter;
    public event Action ThiefExit;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        ThiefEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        ThiefExit?.Invoke();
    }
}
