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
    
    // Alphabet sfx
    [SerializeField] private AudioClip a;
    [SerializeField] private AudioClip b;
    [SerializeField] private AudioClip c;
    [SerializeField] private AudioClip d;
    [SerializeField] private AudioClip e;
    [SerializeField] private AudioClip f;
    [SerializeField] private AudioClip g;
    [SerializeField] private AudioClip h;
    [SerializeField] private AudioClip i;
    [SerializeField] private AudioClip j;
    [SerializeField] private AudioClip k;
    [SerializeField] private AudioClip l;
    [SerializeField] private AudioClip m;
    [SerializeField] private AudioClip n;
    [SerializeField] private AudioClip o;
    [SerializeField] private AudioClip p;
    [SerializeField] private AudioClip q;
    [SerializeField] private AudioClip r;
    [SerializeField] private AudioClip s;
    [SerializeField] private AudioClip t;
    [SerializeField] private AudioClip u;
    [SerializeField] private AudioClip v;
    [SerializeField] private AudioClip w;
    [SerializeField] private AudioClip x;
    [SerializeField] private AudioClip y;
    [SerializeField] private AudioClip z;
    [SerializeField] private AudioClip point;
    
    private static AudioClip _a;
    private static AudioClip _b;
    private static AudioClip _c;
    private static AudioClip _d;
    private static AudioClip _e;
    private static AudioClip _f;
    private static AudioClip _g;
    private static AudioClip _h;
    private static AudioClip _i;
    private static AudioClip _j;
    private static AudioClip _k;
    private static AudioClip _l;
    private static AudioClip _m;
    private static AudioClip _n;
    private static AudioClip _o;
    private static AudioClip _p;
    private static AudioClip _q;
    private static AudioClip _r;
    private static AudioClip _s;
    private static AudioClip _t;
    private static AudioClip _u;
    private static AudioClip _v;
    private static AudioClip _w;
    private static AudioClip _x;
    private static AudioClip _y;
    private static AudioClip _z;
    private static AudioClip _point;
    


    private void Awake()
    {
        _dialogTMP = dialogueTMP;
        _dialogBox = dialogBox;
        
        dialogueTMP.text = "";
        dialogBox.SetActive(false);
        
        _a = a;
        _b = b;
        _c = c;
        _d = d;
        _e = e;
        _f = f;
        _g = g;
        _h = h;
        _i = i;
        _j = j;
        _k = k;
        _l = l;
        _m = m;
        _n = n;
        _o = o;
        _p = p;
        _q = q;
        _r = r;
        _s = s;
        _t = t;
        _u = u;
        _v = v;
        _w = w;
        _x = x;
        _y = y;
        _z = z;
        _point = point;
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
        for(int i = 0; i<str.ToCharArray().Length; i++)
        {
            var car = str[i];
            _dialogTMP.text += car;
            if(i%2 == 0)
                LetterSpeach(car);
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
            for(int i = 0; i<str.ToCharArray().Length; i++)
            {
                var car = str[i];
                _dialogTMP.text += car;
                if(i%2 == 0)
                    LetterSpeach(car);
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

    private static void LetterSpeach(char c)
    {
        var car = char.ToUpper(c);
        switch (car)
        {
            case 'A' :
                SfxManager.instance.PlaySfxClip(_a);
                break;
            case 'B' :
                SfxManager.instance.PlaySfxClip(_b);
                break;
            case 'C' :
                SfxManager.instance.PlaySfxClip(_c);
                break;
            case 'D' :
                SfxManager.instance.PlaySfxClip(_d);
                break;
            case 'E' :
                SfxManager.instance.PlaySfxClip(_e);
                break;
            case 'F' :
                SfxManager.instance.PlaySfxClip(_f);
                break;
            case 'G' :
                SfxManager.instance.PlaySfxClip(_g);
                break;
            case 'H' :
                SfxManager.instance.PlaySfxClip(_h);
                break;
            case 'I' :
                SfxManager.instance.PlaySfxClip(_i);
                break;
            case 'J' :
                SfxManager.instance.PlaySfxClip(_j);
                break;
            case 'K' :
                SfxManager.instance.PlaySfxClip(_k);
                break;
            case 'L' :
                SfxManager.instance.PlaySfxClip(_l);
                break;
            case 'M' :
                SfxManager.instance.PlaySfxClip(_m);
                break;
            case 'N' :
                SfxManager.instance.PlaySfxClip(_n);
                break;
            case 'O' :
                SfxManager.instance.PlaySfxClip(_o);
                break;
            case 'P' :
                SfxManager.instance.PlaySfxClip(_p);
                break;
            case 'Q' :
                SfxManager.instance.PlaySfxClip(_q);
                break;
            case 'R' :
                SfxManager.instance.PlaySfxClip(_r);
                break;
            case 'S' :
                SfxManager.instance.PlaySfxClip(_s);
                break;
            case 'T' :
                SfxManager.instance.PlaySfxClip(_t);
                break;
            case 'U' :
                SfxManager.instance.PlaySfxClip(_u);
                break;
            case 'V' :
                SfxManager.instance.PlaySfxClip(_v);
                break;
            case 'W' :
                SfxManager.instance.PlaySfxClip(_w);
                break;
            case 'X' :
                SfxManager.instance.PlaySfxClip(_x);
                break;
            case 'Y' :
                SfxManager.instance.PlaySfxClip(_y);
                break;
            case 'Z' :
                SfxManager.instance.PlaySfxClip(_z);
                break;
            
            default:
                SfxManager.instance.PlaySfxClip(_point);
                break;
        }
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