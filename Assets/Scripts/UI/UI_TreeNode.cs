using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    private UI ui;
    private RectTransform rectTransform;
    private UI_SkillTree skillTree;
    private UI_TreeConnectHandler connectHandler;

    [Header("Unlock details")]
    public UI_TreeNode[] neededNodes;
    public UI_TreeNode[] conflictNodes;
    public bool isUnlocked;
    public bool isLocked;

    [Header("Skills details")]
    public Skill_DataSO skillData;
    [SerializeField] private string skillName;
    [SerializeField] private Image skillIcon;
    [SerializeField] private int skillCost; //here for debugging
    //[SerializeField] private Color skillLockedColor = Color.gray;
    [SerializeField] private string lockedColorHex = "#808080";
    private Color lastColor;




    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        rectTransform = GetComponent<RectTransform>();
        skillTree = GetComponentInParent<UI_SkillTree>();
        connectHandler = GetComponent<UI_TreeConnectHandler>();

        UpdateIconColor(GetColorByHex(lockedColorHex));

    }

    private void Start()
    {

        if (skillData.unlockedByDefault)
            Unlock();
        
    }

    public void Refund()
    {

        if (isUnlocked == false || skillData.unlockedByDefault)
            return;

        isUnlocked = false;
        isLocked = false;
        UpdateIconColor(GetColorByHex(lockedColorHex));

        skillTree.AddSkillPoints(skillData.skillPointCost);
        connectHandler.UnlockConnectionImage(false);

        //skill manager and reset skill
    }


    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white);
        LockConflictNodes();

        skillTree.RemoveSkillPoints(skillData.skillPointCost);
        connectHandler.UnlockConnectionImage(true);
        skillTree.skillManager.GetSkillByType(skillData.skillType).SetSkillUpgrade(skillData.upgradeData);

    }

    private bool CanBeUnlocked()
    {
        if (isUnlocked || isLocked)
            return false;
            
        if(skillTree.EnoughSkillPoints(skillData.skillPointCost) == false)
            return false;

        foreach (var node in neededNodes)
        {
            if (node.isUnlocked == false)
                return false;
        }

        return true;
    }

    private void LockConflictNodes()
    {
        foreach (var node in conflictNodes)
        {
            node.isLocked = true;
            node.LockChildNodes();
        }
    }

    public void LockChildNodes()
    {
        isLocked = true;

        foreach (var node in connectHandler.GetChildNodes())
            node.LockChildNodes();
    }


    private void UpdateIconColor(Color color)
    {
        if (skillIcon == null)
            return;

        lastColor = skillIcon.color;
        skillIcon.color = color;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanBeUnlocked())
            Unlock();
        else if (isLocked)
            ui.skillToolTip.LockedSkillEffect();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(true, rectTransform, this);

        if (isUnlocked || isLocked)
            return;


        ToggleNodeHighlight(true);
        
}

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(false, rectTransform); //can pass null here

        if (isUnlocked || isLocked)
            return;
            
            ToggleNodeHighlight(false);
    }

    private void ToggleNodeHighlight(bool highlight)
    {
        Color highlightColor = Color.white * .9f; highlightColor.a = 1;
        Color colorToApply = highlight ? highlightColor : lastColor;

        UpdateIconColor(colorToApply);
    }

    private Color GetColorByHex(string hexNumber)
    {
        UnityEngine.ColorUtility.TryParseHtmlString(hexNumber, out Color color);
        return color;
    }

    private void OnDisable()
    {
        if (isLocked)
            UpdateIconColor(GetColorByHex(lockedColorHex));

        if (isUnlocked)
            UpdateIconColor(Color.white);
    }
    

    //onvalidate is called when the inspector is updated so this updates the gmeobject name and icon in the editor when we add the SO
    private void OnValidate()
    {
        if (skillData == null)
            return;

        skillName = skillData.skillName;
        skillIcon.sprite = skillData.icon;
        skillCost = skillData.skillPointCost;
        gameObject.name = "UI_TreeNode - " + skillData.skillName;
    }
}
