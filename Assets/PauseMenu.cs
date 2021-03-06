﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
public class PauseMenu : MonoBehaviour {

    private Animator m_animator;

    [SerializeField]
    private Selectable firstSelected;

    [SerializeField]
    private CanvasGroup pausePanel;

    [SerializeField]
    private Text recText;

    private Sequence seq;

    private bool _expanded;
    public bool Expanded
    {
        get
        {
            return _expanded;
        }
        private set
        {
            _expanded = value;
            m_animator.SetBool("Expanded", value);
        }
    }

    void Start()
    {
        m_animator = GetComponent<Animator>();

        Expanded = false;
        seq = DOTween.Sequence();
        seq.Append(pausePanel.DOFade(0f, 0.4f));

        seq.Play();
    }

    void Update()
    {
        if (Input.GetButtonDown("Start1") || Input.GetButtonDown("Start2"))
        {
            Expanded = !Expanded;

            if (Expanded)
            {
                firstSelected.Select();
                recText.text = "REC OFF";
            } else
            {
                recText.text = "REC";
            }
        }
    }

    public void Continue (){
         recText.text = "REC";
         Expanded = false;
    }
}
