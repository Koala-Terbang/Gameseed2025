using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [System.Serializable]
    public class SkillSlot
    {
        public string skillName;
        public GameObject skillObject;
        public bool startActive = true;
    }

    public List<SkillSlot> skills = new List<SkillSlot>();

    void Start()
    {
        foreach (SkillSlot slot in skills)
        {
            if (slot.skillObject != null)
                slot.skillObject.SetActive(slot.startActive);
        }
    }

    public void ActivateSkill(string skillName)
    {
        SkillSlot skill = skills.Find(s => s.skillName == skillName);
        if (skill != null && skill.skillObject != null)
            skill.skillObject.SetActive(true);
    }

    public void DeactivateSkill(string skillName)
    {
        SkillSlot skill = skills.Find(s => s.skillName == skillName);
        if (skill != null && skill.skillObject != null)
            skill.skillObject.SetActive(false);
    }

    public void ToggleSkill(string skillName)
    {
        SkillSlot skill = skills.Find(s => s.skillName == skillName);
        if (skill != null && skill.skillObject != null)
        {
            bool current = skill.skillObject.activeSelf;
            skill.skillObject.SetActive(!current);
        }
    }

    public void ActivateAllSkills()
    {
        foreach (SkillSlot slot in skills)
        {
            if (slot.skillObject != null)
                slot.skillObject.SetActive(true);
        }
    }

    public void DeactivateAllSkills()
    {
        foreach (SkillSlot slot in skills)
        {
            if (slot.skillObject != null)
                slot.skillObject.SetActive(false);
        }
    }
}
