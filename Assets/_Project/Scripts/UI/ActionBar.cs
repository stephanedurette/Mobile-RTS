using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<ActionButton> actionButtons;
    [SerializeField] private Transform parentTransform;

    [Header("Events")]
    [SerializeField] private UnityEvent<ActionButton> OnActionButtonClicked;

    private void OnEnable()
    {
        foreach (var button in actionButtons) { 
            button.OnActionButtonClicked.AddListener((actionButton) => OnActionButtonClicked?.Invoke(button));
        }
    }

    public void OnPlayerUnitSelected(Unit selectedUnit)
    {
        Show(selectedUnit.ActionList.Count > 0);

        for (int i = 0; i < actionButtons.Count; i++)
        {
            if (i >= selectedUnit.ActionList.Count)
            {
                actionButtons[i].gameObject.SetActive(false);
                actionButtons[i].Unbind();
            }
            else
            {
                actionButtons[i].gameObject.SetActive(true);
                actionButtons[i].Bind(selectedUnit.ActionList[i], selectedUnit.Owner);
            }
        }
    }

    public void Show(bool show)
    {
        parentTransform.gameObject.SetActive(show);
    }
}
