using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueTMP;

    [SerializeField] private string[] sentences;
    private static TMP_Text _dialogueTMP;
    private static string[] _sentences;
    
    private static int m_Index;
    private bool _isWriting;
    private static bool _waitInput;
    private Coroutine _co;
    [SerializeField] private float textSpeed = .1f;

    private void Awake()
    {
        _sentences = sentences;
        _dialogueTMP = dialogueTMP;
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueTMP.text = "";
        _co = StartCoroutine(WriteSentence());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !_waitInput)
        {
            if(!_isWriting)
            {
                m_Index++;
                NextSentence();
            }
            else
            {
                StopCoroutine(_co);
                _isWriting = false;
                dialogueTMP.text = sentences[m_Index];
            }
        }
    }

    private void NextSentence()
    {
        if (m_Index < sentences.Length)
        {
            dialogueTMP.text = "";
            _co = StartCoroutine(WriteSentence());
        }
        else
        {
            dialogueTMP.text = "";
        }
    }

    private IEnumerator WriteSentence()
    {
        _isWriting = true;
        foreach (var car in sentences[m_Index].ToCharArray())
        {
            dialogueTMP.text += car;
            yield return new WaitForSeconds(textSpeed);
        }
        _isWriting = false;
    }

    public static void SetIndexTo(int i)
    {
        m_Index = i;
        _dialogueTMP.text = _sentences[i];
    }

    public static int GetIndex()
    {
        return m_Index;
    }

    public static void SetWaitInput(bool a)
    {
        _waitInput = a;
    }
    
}
