using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ISkillTree
{
    void ResetSkillTree();
}

public class SkillTree : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject skillTreePanel;
    public GameObject KatanaPanel;
    public GameObject ZincirPanel;
    public GameObject TirpanPanel;
    public GameObject GswordPanel;
    public GameObject HalberdPanel;
    public GameObject GurzPanel;
    public GameObject Map;
    public GameObject Player;
    public Button KatanaButton;
    public Button ZincirButton;
    public Button TirpanButton;
    public Button GswordButton;
    public Button HalberdButton;
    public Button GurzButton;

    public KeyCode UIopen = KeyCode.K;
    public KeyCode resetKey = KeyCode.R;

    private GameObject currentActivePanel = null;
    
    void Start()
    {
        if (skillTreePanel != null)
        {
            skillTreePanel.SetActive(false);
        }

        KatanaButton.onClick.AddListener(SelectKatana);
        ZincirButton.onClick.AddListener(SelectZincir);
        TirpanButton.onClick.AddListener(SelectTirpan);
        GswordButton.onClick.AddListener(SelectGsword);
        HalberdButton.onClick.AddListener(SelectHalberd);
        GurzButton.onClick.AddListener(SelectGurz);
    }

    void Update()
    {
        if (mainMenuPanel != null && mainMenuPanel.activeSelf)
        {
            return;
        }

        if (Input.GetKeyDown(UIopen))
        {
            ToggleSkillTree();
        }

        if (Input.GetKeyDown(resetKey) && skillTreePanel != null && skillTreePanel.activeSelf)
        {
            ShowSelectionScreen();
        }
    }

    void ToggleSkillTree()
    {
        if (skillTreePanel == null) return;

        bool isOpening = !skillTreePanel.activeSelf;
        skillTreePanel.SetActive(isOpening);
        Time.timeScale = isOpening ? 0f : 1f;

        if (Map != null) Map.SetActive(!isOpening);
        if (Player != null)
        {
            Player.GetComponent<Rigidbody2D>().bodyType = isOpening ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
        }

        if (isOpening)
        {
            if (currentActivePanel == null)
            {
                ShowSelectionScreen();
            }
            else
            {
                ShowSpecificPanel(currentActivePanel);
            }
        }
    }

    public void SelectKatana() { ShowSpecificPanel(KatanaPanel); }
    public void SelectZincir() { ShowSpecificPanel(ZincirPanel); }
    public void SelectTirpan() { ShowSpecificPanel(TirpanPanel); }
    public void SelectGsword() { ShowSpecificPanel(GswordPanel); }
    public void SelectHalberd() { ShowSpecificPanel(HalberdPanel); }
    public void SelectGurz() { ShowSpecificPanel(GurzPanel); }

    void ShowSpecificPanel(GameObject panelToShow)
    {
        currentActivePanel = panelToShow;
        KatanaPanel.SetActive(panelToShow == KatanaPanel);
        ZincirPanel.SetActive(panelToShow == ZincirPanel);
        TirpanPanel.SetActive(panelToShow == TirpanPanel);
        GswordPanel.SetActive(panelToShow == GswordPanel);
        HalberdPanel.SetActive(panelToShow == HalberdPanel);
        GurzPanel.SetActive(panelToShow == GurzPanel);
    }

    void ShowSelectionScreen()
    {
        currentActivePanel = null;
        KatanaPanel.SetActive(true);
        ZincirPanel.SetActive(true);
        TirpanPanel.SetActive(true);
        GswordPanel.SetActive(true);
        HalberdPanel.SetActive(true);
        GurzPanel.SetActive(true);
    }
}
