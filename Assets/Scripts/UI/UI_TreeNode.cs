using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    private UI ui;
    private RectTransform rectTransform;
    private UI_SkillTree skillTree;

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
    [SerializeField] private Color skillLockedColor = Color.gray;
    //[SerializeField] private string lockedColorHex = "#808080"; // if want to use hex value for color indead of picker
    private Color lastColor;




    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        rectTransform = GetComponent<RectTransform>();
        skillTree = GetComponentInParent<UI_SkillTree>();

        UpdateIconColor(skillLockedColor);
    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white);
        skillTree.RemoveSkillPoints(skillData.skillPointCost);
        LockConflictNodes();
        
        //find player skill manager
        //unlock skill on skill manager 
        // skill manager unlock skill from skill data skill type
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
            node.isLocked = true;
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
        else
            Debug.Log("Can not unlock skill");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(true, rectTransform, this);

        if (isUnlocked == false)
            UpdateIconColor(Color.white * .9f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(false, rectTransform); //can pass null here

        if (isUnlocked == false)
            UpdateIconColor(lastColor);
    }

    //if I want to pick the loced coolor by hex value can do the below
    // private Color GetColorByHex(string hexNumber){
    //     ColorUtility.TryParseHtmlString(hexNumber, out Color color);
    //     return color;}
    // would then need to update where skillLockedColor is set in Awake with GetColorByHex(lockedColorHex);

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
