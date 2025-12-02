using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KatanaSkills : MonoBehaviour, ISkillTree
{
    public Button BSkill;
    public Button MSkill1;
    public Button MSkill2;
    public Button MSkill3;
    public Button MSkill4;
    public Button LSkill1;
    public Button LSkill2;
    public Button LSkill3;
    public Button LSkill4;
    public Button RSkill1;
    public Button RSkill2;
    public Button RSkill3;
    public Button RSkill4;

    public Color activeColor = Color.cyan;
    public Color inactiveColor = Color.gray;
    public KeyCode resetKey = KeyCode.R;

    public SkillTreeLines skillTreeLines;

    private List<Button> pressedButtons = new List<Button>();

    void Start()
    {
        if (skillTreeLines == null)
        {
            skillTreeLines = FindObjectOfType<SkillTreeLines>();
        }

        MSkill1.interactable = false;
        MSkill2.interactable = false;
        MSkill3.interactable = false;
        MSkill4.interactable = false;
        LSkill1.interactable = false;
        LSkill2.interactable = false;
        LSkill3.interactable = false;
        LSkill4.interactable = false;
        RSkill1.interactable = false;
        RSkill2.interactable = false;
        RSkill3.interactable = false;
        RSkill4.interactable = false;

        UpdateAllButtonColors();

        BSkill.onClick.AddListener(OnBSkillClick);
        MSkill1.onClick.AddListener(OnMSkill1Click);
        MSkill2.onClick.AddListener(OnMSkill2Click);
        MSkill3.onClick.AddListener(OnMSkill3Click);
        MSkill4.onClick.AddListener(OnMSkill4Click);
        LSkill1.onClick.AddListener(OnLSkill1Click);
        LSkill2.onClick.AddListener(OnLSkill2Click);
        LSkill3.onClick.AddListener(OnLSkill3Click);
        LSkill4.onClick.AddListener(OnLSkill4Click);
        RSkill1.onClick.AddListener(OnRSkill1Click);
        RSkill2.onClick.AddListener(OnRSkill2Click);
        RSkill3.onClick.AddListener(OnRSkill3Click);
        RSkill4.onClick.AddListener(OnRSkill4Click);
    }

    void Update()
    {
        if (gameObject.activeInHierarchy && Input.GetKeyDown(resetKey))
        {
            ResetSkillTree();
        }
    }

    public void ResetSkillTree()
    {
        if (skillTreeLines != null)
        {
            skillTreeLines.ClearLines();
        }

        pressedButtons.Clear();

        BSkill.interactable = true;
        MSkill1.interactable = false;
        MSkill2.interactable = false;
        MSkill3.interactable = false;
        MSkill4.interactable = false;
        LSkill1.interactable = false;
        LSkill2.interactable = false;
        LSkill3.interactable = false;
        LSkill4.interactable = false;
        RSkill1.interactable = false;
        RSkill2.interactable = false;
        RSkill3.interactable = false;
        RSkill4.interactable = false;

        UpdateAllButtonColors();
    }

    void OnBSkillClick()
    {
        BSkill.interactable = false;
        pressedButtons.Add(BSkill);
        MSkill1.interactable = true;
        LSkill1.interactable = true;
        RSkill1.interactable = true;
        UpdateAllButtonColors();

        skillTreeLines.DrawLine(BSkill.GetComponent<RectTransform>(), MSkill1.GetComponent<RectTransform>());
        skillTreeLines.DrawLine(BSkill.GetComponent<RectTransform>(), LSkill1.GetComponent<RectTransform>());
        skillTreeLines.DrawLine(BSkill.GetComponent<RectTransform>(), RSkill1.GetComponent<RectTransform>());
    }

    void OnMSkill1Click()
    {
        MSkill1.interactable = false;
        pressedButtons.Add(MSkill1);
        MSkill2.interactable = true;
        UpdateAllButtonColors();
        skillTreeLines.DrawLine(MSkill1.GetComponent<RectTransform>(), MSkill2.GetComponent<RectTransform>());
    }

    void OnMSkill2Click()
    {
        MSkill2.interactable = false;
        pressedButtons.Add(MSkill2);
        MSkill3.interactable = true;
        UpdateAllButtonColors();
        skillTreeLines.DrawLine(MSkill2.GetComponent<RectTransform>(), MSkill3.GetComponent<RectTransform>());
    }

    void OnMSkill3Click()
    {
        MSkill3.interactable = false;
        pressedButtons.Add(MSkill3);
        MSkill4.interactable = true;
        UpdateAllButtonColors();
        skillTreeLines.DrawLine(MSkill3.GetComponent<RectTransform>(), MSkill4.GetComponent<RectTransform>());
    }

    void OnMSkill4Click()
    {
        MSkill4.interactable = false;
        pressedButtons.Add(MSkill4);
        UpdateAllButtonColors();
    }

    void OnLSkill1Click()
    {
        RSkill1.interactable = false;
        LSkill1.interactable = false;
        pressedButtons.Add(LSkill1);
        RSkill2.interactable = true;
        LSkill2.interactable = true;
        UpdateAllButtonColors();
        skillTreeLines.DrawLine(LSkill1.GetComponent<RectTransform>(), LSkill2.GetComponent<RectTransform>());
        skillTreeLines.DrawLine(LSkill1.GetComponent<RectTransform>(), RSkill2.GetComponent<RectTransform>());
    }

    void OnLSkill2Click()
    {
        RSkill2.interactable = false;
        LSkill2.interactable = false;
        pressedButtons.Add(LSkill2);
        RSkill3.interactable = true;
        LSkill3.interactable = true;
        UpdateAllButtonColors();
        skillTreeLines.DrawLine(LSkill2.GetComponent<RectTransform>(), LSkill3.GetComponent<RectTransform>());
        skillTreeLines.DrawLine(LSkill2.GetComponent<RectTransform>(), RSkill3.GetComponent<RectTransform>());
    }

    void OnLSkill3Click()
    {
        RSkill3.interactable = false;
        LSkill3.interactable = false;
        pressedButtons.Add(LSkill3);
        RSkill4.interactable = true;
        LSkill4.interactable = true;
        UpdateAllButtonColors();
        skillTreeLines.DrawLine(LSkill3.GetComponent<RectTransform>(), LSkill4.GetComponent<RectTransform>());
        skillTreeLines.DrawLine(LSkill3.GetComponent<RectTransform>(), RSkill4.GetComponent<RectTransform>());
    }

    void OnLSkill4Click()
    {
        RSkill4.interactable = false;
        LSkill4.interactable = false;
        pressedButtons.Add(LSkill4);
        UpdateAllButtonColors();
        skillTreeLines.DrawLine(LSkill4.GetComponent<RectTransform>(), RSkill4.GetComponent<RectTransform>());
    }

    void OnRSkill1Click()
    {
        LSkill1.interactable = false;
        RSkill1.interactable = false;
        pressedButtons.Add(RSkill1);
        LSkill2.interactable = true;
        RSkill2.interactable = true;
        UpdateAllButtonColors();
        skillTreeLines.DrawLine(RSkill1.GetComponent<RectTransform>(), RSkill2.GetComponent<RectTransform>());
        skillTreeLines.DrawLine(RSkill1.GetComponent<RectTransform>(), LSkill2.GetComponent<RectTransform>());
    }

    void OnRSkill2Click()
    {
        LSkill2.interactable = false;
        RSkill2.interactable = false;
        pressedButtons.Add(RSkill2);
        LSkill3.interactable = true;
        RSkill3.interactable = true;
        UpdateAllButtonColors();
        skillTreeLines.DrawLine(RSkill2.GetComponent<RectTransform>(), RSkill3.GetComponent<RectTransform>());
        skillTreeLines.DrawLine(RSkill2.GetComponent<RectTransform>(), LSkill3.GetComponent<RectTransform>());
    }

    void OnRSkill3Click()
    {
        LSkill3.interactable = false;
        RSkill3.interactable = false;
        pressedButtons.Add(RSkill3);
        LSkill4.interactable = true;
        RSkill4.interactable = true;
        UpdateAllButtonColors();
        skillTreeLines.DrawLine(RSkill3.GetComponent<RectTransform>(), RSkill4.GetComponent<RectTransform>());
        skillTreeLines.DrawLine(RSkill3.GetComponent<RectTransform>(), LSkill4.GetComponent<RectTransform>());
    }

    void OnRSkill4Click()
    {
        LSkill4.interactable = false;
        RSkill4.interactable = false;
        pressedButtons.Add(RSkill4);
        UpdateAllButtonColors();
        skillTreeLines.DrawLine(LSkill4.GetComponent<RectTransform>(), RSkill4.GetComponent<RectTransform>());
    }

    void UpdateAllButtonColors()
    {
        UpdateButtonColor(BSkill);
        UpdateButtonColor(MSkill1);
        UpdateButtonColor(MSkill2);
        UpdateButtonColor(MSkill3);
        UpdateButtonColor(MSkill4);
        UpdateButtonColor(LSkill1);
        UpdateButtonColor(LSkill2);
        UpdateButtonColor(LSkill3);
        UpdateButtonColor(LSkill4);
        UpdateButtonColor(RSkill1);
        UpdateButtonColor(RSkill2);
        UpdateButtonColor(RSkill3);
        UpdateButtonColor(RSkill4);
    }

    void UpdateButtonColor(Button button)
    {
        ColorBlock colors = button.colors;
        if (pressedButtons.Contains(button))
        {
            colors.normalColor = activeColor;
            colors.disabledColor = activeColor;
        }
        else
        {
            colors.normalColor = inactiveColor;
            colors.disabledColor = inactiveColor;
        }
        button.colors = colors;
    }
}
