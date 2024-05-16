using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlbumManager : MonoBehaviour
{
    // Panel
    public GameObject albumPanel;
    
    // Buttons
    public Button quitButton;
    public Button previousButton;
    public Button nextButton;
    
    // Images
    public Sprite defaultImage;
    public Image drawingL;
    public Image pictureL;
    public Image drawingR;
    public Image pictureR;
    
    // TMP_Text
    public TMP_Text catNameL;
    public TMP_Text catNameR;
    
    // Scriptable object
    public CatsScriptableObject cats;
    
    // Sfx
    [SerializeField] private AudioClip buttonSfx;
    [SerializeField] private AudioClip pagingSfx;

    private int m_IndexL;
    private int m_IndexR;

    private void Awake()
    {
        quitButton.onClick.AddListener(QuitButtonClicked);
        previousButton.onClick.AddListener(PreviousButtonClicked);
        nextButton.onClick.AddListener(NextButtonClicked);
    }

    private void Start()
    {
        m_IndexL = 0;
        m_IndexR = 1;
        
        // Print first cat
        PrintLeftCat();
        // Print second cat
        PrintRightCat();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            albumPanel.SetActive(false);
        }
    }

    private void NextButtonClicked()
    {
        SfxManager.instance.PlaySfxClip(pagingSfx);
        m_IndexL = (m_IndexL + 2) % cats.cats.Count;
        m_IndexR = (m_IndexR + 2) % cats.cats.Count;
        PrintLeftCat();
        PrintRightCat();
    }

    private void PreviousButtonClicked()
    {
        SfxManager.instance.PlaySfxClip(pagingSfx);
        m_IndexL = (m_IndexL - 2) % cats.cats.Count;
        m_IndexR = (m_IndexR - 2) % cats.cats.Count;
        if (m_IndexL < 0)
        {
            m_IndexL = cats.cats.Count - 2;
        }
        if (m_IndexR < 1)
        {
            m_IndexR = cats.cats.Count - 1;
        }
        PrintLeftCat();
        PrintRightCat();
    }

    private void QuitButtonClicked()
    {
        SfxManager.instance.PlaySfxClip(buttonSfx);
        albumPanel.SetActive(false);
        MusicManager.instance.SwitchBossaNova(0);
    }

    private void PrintLeftCat()
    {
        if (PlayerPrefs.GetInt("MaxLevel") >= cats.cats[m_IndexL].level)
        {
            catNameL.text = cats.cats[m_IndexL].name;
            drawingL.sprite = cats.cats[m_IndexL].picture;
        }
        else
        {
            catNameL.text = "???";
            drawingL.sprite = defaultImage;
        }
        if (StatsManager.instance.GetAlbumCats().Contains(cats.cats[m_IndexL].name))
        {
            pictureL.sprite = cats.cats[m_IndexL].reference;
        }
        else
        {
            pictureL.sprite = defaultImage;
        }
    }

    private void PrintRightCat()
    {
        if (PlayerPrefs.GetInt("MaxLevel") >= cats.cats[m_IndexR].level)
        {
            catNameR.text = cats.cats[m_IndexR].name;
            drawingR.sprite = cats.cats[m_IndexR].picture;
        }
        else
        {
            catNameR.text = "???";
            drawingR.sprite = defaultImage;
        }

        if (StatsManager.instance.GetAlbumCats().Contains(cats.cats[m_IndexR].name))
        {
            pictureR.sprite = cats.cats[m_IndexR].reference;
        }
        else
        {
            pictureR.sprite = defaultImage;
        }
    }
}
