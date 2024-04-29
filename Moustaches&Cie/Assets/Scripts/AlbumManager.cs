using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        quitButton.onClick.AddListener(QuitButtonClicked);
        previousButton.onClick.AddListener(PreviousButtonClicked);
        nextButton.onClick.AddListener(NextButtonClicked);
    }

    private void Start()
    {
        // Print first cat
        catNameL.text = cats.cats[0].name;
        if (StatsManager.instance.GetLevel() == cats.cats[0].level)
        {
            drawingL.sprite = cats.cats[0].picture;
        }
        else
        {
            drawingL.sprite = defaultImage;
        }
        if (StatsManager.instance.GetAdoptedCats().Contains(cats.cats[0].name))
        {
            pictureL.sprite = cats.cats[0].reference;
        }
        else
        {
            pictureL.sprite = defaultImage;
        }
        
        // Print second cat
        catNameR.text = cats.cats[1].name;
        if (StatsManager.instance.GetLevel() == cats.cats[1].level)
        {
            drawingR.sprite = cats.cats[1].picture;
        }
        else
        {
            drawingR.sprite = defaultImage;
        }
        if (StatsManager.instance.GetAdoptedCats().Contains(cats.cats[1].name))
        {
            pictureR.sprite = cats.cats[1].reference;
        }
        else
        {
            pictureR.sprite = defaultImage;
        }
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
        throw new NotImplementedException();
    }

    private void PreviousButtonClicked()
    {
        throw new NotImplementedException();
    }

    private void QuitButtonClicked()
    {
        albumPanel.SetActive(false);
    }
}
