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

    public void UpdateActionBar(List<Action> actions)
    {
        for (int i = 0; i < actionButtons.Count; i++) {
            if (i >= actions.Count) { 
                actionButtons[i].gameObject.SetActive(false);
            } else
            {
                actionButtons[i].gameObject.SetActive(true);
                actionButtons[i].Action = actions[i];
            }
        }

        Show(actions.Count > 0);
    }

    public void Show(bool show)
    {
        parentTransform.gameObject.SetActive(show);
    }
}
