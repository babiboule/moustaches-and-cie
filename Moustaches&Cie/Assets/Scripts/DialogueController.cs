using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    // UI
    [SerializeField] private TMP_Text dialogueTMP;
    [SerializeField] private GameObject dialogBox;
    private static TMP_Text _dialogTMP;
    private static GameObject _dialogBox;
    
    // Private variables
    private const float TextSpeed = .03f;
    private static bool _isWriting;
    private static bool _skipClic;
    private static bool _waitInput;
    private Coroutine _co;


    private void Awake()
    {
        _dialogTMP = dialogueTMP;
        _dialogBox = dialogBox;
        
        dialogueTMP.text = "";
        dialogBox.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && _isWriting && !_waitInput)
        {
            _skipClic = true;
        }
    }

    public static IEnumerator WriteDialog(string str)
    {
        _dialogTMP.text = "";
        _dialogBox.SetActive(true);
        _isWriting = true;
        foreach (var car in str.ToCharArray())
        {
            _dialogTMP.text += car;
            yield return new WaitForSeconds(TextSpeed);
            if(_skipClic)
            {
                SkipWriting(str);
                break;
            }
        }

        _waitInput = true;
        while (!Input.GetButtonDown("Fire1"))
        {
            yield return null;
        }

        _waitInput = false;

        _isWriting = false;
        yield return CloseDialogBox();
    }
    
    public static IEnumerator WriteDialog(string[] strArray)
    {
        _dialogBox.SetActive(true);
        _isWriting = true;
        foreach (var str in strArray)
        {
            _dialogTMP.text = "";
            foreach (var car in str.ToCharArray())
            {
                _dialogTMP.text += car;
                yield return new WaitForSeconds(TextSpeed);
                if (_skipClic)
                {
                    SkipWriting(str);
                    break;
                }
            }

            _waitInput = true;
            while (!Input.GetButtonDown("Fire1"))
            {
                yield return null;
            }

            _waitInput = false;
        }
        
        _waitInput = true;
        while (!Input.GetButtonDown("Fire1"))
        {
            yield return null;
        }

        _waitInput = false;
        _isWriting = false;
        yield return CloseDialogBox();
    }
   
    private static IEnumerator CloseDialogBox()
    {
        while (!Input.GetButtonDown("Fire1"))
        {
            yield return null;
        }
        _dialogBox.SetActive(false);
    }

    private static void SkipWriting(string str)
    {
        _dialogTMP.text = str;
        _skipClic = false;
    }

    /*
     * Return the isWriting bool
     */
    public static bool GetIsWriting()
    {
        return _isWriting;
    }
    
}
