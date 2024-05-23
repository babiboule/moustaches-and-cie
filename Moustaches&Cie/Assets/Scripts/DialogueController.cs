using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    // UI
    [SerializeField] private TMP_Text dialogueTMP;
    [SerializeField] private string[] sentences;
    private static TMP_Text _dialogueTMP;
    
    // List
    private static string[] _sentences;
    
    // Private variables
    private static int _index;
    private static readonly float TextSpeed = .03f;
    private static bool _isWriting;
    private static bool _waitInput;
    private Coroutine _co;


    private void Awake()
    {
        _sentences = sentences;
        _dialogueTMP = dialogueTMP;
    }

    // Start is called before the first frame update
    void Start()
    {
        _index = 0;
        dialogueTMP.text = "";
        
        // Write the first sentence of the dialogue
        _co = StartCoroutine(WriteSentence());
    }

    // Update is called once per frame
    void Update()
    {
        // Next sentence when left mouse button clicked
        if (Input.GetButtonDown("Fire1") && !_waitInput)
        {
            // If the sentence is fully printed, go to next sentence
            if(!_isWriting)
            {
                _index++;
                NextSentence();
            }
            // Else stop the coroutine and print the entire current sentence
            else
            {
                StopCoroutine(_co);
                _isWriting = false;
                dialogueTMP.text = sentences[_index];
            }
        }
    }

    /*
     * Go to the next sentence and print it
     */
    private void NextSentence()
    {
        // Clean the current sentence and print the next one if it exists
        if (_index < sentences.Length)
        {
            dialogueTMP.text = "";
            _co = StartCoroutine(WriteSentence());
        }
        else
        {
            dialogueTMP.text = "";
        }
    }

    /*
     * CoRoutine to write the sentences char by char at _textSpeed
     */
    private IEnumerator WriteSentence()
    {
        // Start writing
        _isWriting = true;
        
        // Print a char and wait for _textSpeed seconds before printing the next one
        foreach (var car in sentences[_index].ToCharArray())
        {
            dialogueTMP.text += car;
            yield return new WaitForSeconds(TextSpeed);
        }
        
        // Finish writing
        _isWriting = false;
    }

    public static IEnumerator WriteSentence(string str, TMP_Text tmp)
    {
        foreach (var car in str.ToCharArray())
        {
            tmp.text += car;
            yield return new WaitForSeconds(TextSpeed);
        }
    }

    /*
     * Set the current index to Param i
     */
    public static void SetIndexTo(int i)
    {
        _index = i;
        _dialogueTMP.text = _sentences[i];
    }

    /*
     * Return the current index
     */
    public static int GetIndex()
    {
        return _index;
    }

    /*
     * Set the waitInput bool to Param a
     */
    public static void SetWaitInput(bool a)
    {
        _waitInput = a;
    }

    /*
     * Return the isWriting bool
     */
    public static bool GetIsWriting()
    {
        return _isWriting;
    }
    
}
