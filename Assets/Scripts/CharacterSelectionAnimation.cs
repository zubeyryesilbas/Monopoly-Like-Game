using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _animator.SetTrigger("Spin");
    }
}
