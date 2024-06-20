using System.Collections;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* Manage the cat album when it is active */
public class AlbumManager : MonoBehaviour
{
    // Panel
    [SerializeField] private GameObject albumPanel;
    
    // Buttons
    [SerializeField] private Button quitButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    
    // Images
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private Image drawingL;
    [SerializeField] private Image pictureL;
    [SerializeField] private Image drawingR;
    [SerializeField] private Image pictureR;
    
    // TMP_Text
    [SerializeField] private TMP_Text catNameL;
    [SerializeField] private TMP_Text catNameR;
    
    // Scriptable object
    [SerializeField] private CatsScriptableObject cats;
    
    // Sfx
    [SerializeField] private AudioClip buttonSfx;
    [SerializeField] private AudioClip pagingSfx;
    
    // Vfx
    [SerializeField] private GameObject catInfos;
    [SerializeField] private GameObject page;
    [SerializeField] private Animator pageAlbum;

    // Private variables
    private int _indexL;
    private int _indexR;

    private void Awake()
    {
        // Set listeners on buttons
        quitButton.onClick.AddListener(QuitButtonClicked);
        previousButton.onClick.AddListener(PreviousButtonClicked);
        nextButton.onClick.AddListener(NextButtonClicked);
    }

    private void Start()
    {
        // Set the first pages
        _indexL = 0;
        _indexR = 1;
        
        // Print first cat
        PrintLeftCat();
        // Print second cat
        PrintRightCat();
    }

    private void Update()
    {
        // Can quit the panel with "escape"
        if (Input.GetKeyDown("escape"))
        {
            albumPanel.SetActive(false);
        }
    }

    private void NextButtonClicked()
    {
        StartCoroutine(Next());
    }
    
    /* Print the next pages when right arrow clicked
     * (loop at the start if at the end)
     */
    private IEnumerator Next()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(pagingSfx);
        catInfos.SetActive(false);
        page.SetActive(true);
        pageAlbum.Play("PageEndroit");
        yield return new WaitForSeconds(0.5f);
        catInfos.SetActive(true);
        page.SetActive(false);
        
        _indexL = (_indexL + 2) % cats.cats.Count;
        _indexR = (_indexR + 2) % cats.cats.Count;
        PrintLeftCat();
        PrintRightCat();
    }

    private void PreviousButtonClicked()
    {
        StartCoroutine(Previous());
    }
    
    /* Print the previous pages when left arrow clicked
     * (loop at the end if at the start)
     */
    private IEnumerator Previous()
    {
        // Sfx
        SfxManager.instance.PlaySfxClip(pagingSfx);
        catInfos.SetActive(false);
        page.SetActive(true);
        pageAlbum.Play("PageReverse");
        yield return new WaitForSeconds(0.5f);
        catInfos.SetActive(true);
        page.SetActive(false);
        
        _indexL = (_indexL - 2) % cats.cats.Count;
        _indexR = (_indexR - 2) % cats.cats.Count;
        if (_indexL < 0)
            _indexL = cats.cats.Count - 2;
        if (_indexR < 1)
            _indexR = cats.cats.Count - 1;
        PrintLeftCat();
        PrintRightCat();
    }

    /* Close the panel */
    private void QuitButtonClicked()
    {
        // Play sfx
        SfxManager.instance.PlaySfxClip(buttonSfx);
        // Switch music fx
        MusicManager.instance.BgBossaNova(false);
        
        albumPanel.SetActive(false);
    }

    /* Print the current cat on the left page */
    private void PrintLeftCat()
    {
        /* Check the max level reached by the player (all runs together)
         to print the name and the drawing only if the cat has been encountered at least once */
        if (PlayerPrefs.GetInt("MaxLevel") >= cats.cats[_indexL].level)
        {
            catNameL.text = cats.cats[_indexL].name;
            drawingL.sprite = cats.cats[_indexL].picture;
        }
        else
        {
            catNameL.text = "???";
            drawingL.sprite = defaultImage;
        }
        
        // Print the reference picture if the cat has been placed in a good family
        pictureL.sprite = StatsManager.instance.GetAlbumCats().Contains(cats.cats[_indexL].name) ? cats.cats[_indexL].reference : defaultImage;
    }

    /* Print the current cat on the right page */
    private void PrintRightCat()
    {
        /* Check the max level reached by the player (all runs together)
         to print the name and the drawing only if the cat has been encountered at least once */
        if (PlayerPrefs.GetInt("MaxLevel") >= cats.cats[_indexR].level)
        {
            catNameR.text = cats.cats[_indexR].name;
            drawingR.sprite = cats.cats[_indexR].picture;
        }
        else
        {
            catNameR.text = "???";
            drawingR.sprite = defaultImage;
        }

        // Print the reference picture if the cat has been placed in a good family
        pictureR.sprite = StatsManager.instance.GetAlbumCats().Contains(cats.cats[_indexR].name) ? cats.cats[_indexR].reference : defaultImage;
    }
}