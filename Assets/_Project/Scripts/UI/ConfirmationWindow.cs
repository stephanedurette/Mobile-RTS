using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmationWindow : MonoBehaviour
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    private UnityAction onConfirmSelected, onCancelSelected;

    public void Bind(UnityAction onConfirmSelected, UnityAction onCancelSelected)
    {
        this.onConfirmSelected = onConfirmSelected;
        this.onCancelSelected = onCancelSelected;

        confirmButton.onClick.AddListener(onConfirmSelected);
        cancelButton.onClick.AddListener(onCancelSelected);
    }

    public void Unbind()
    {
        if (onConfirmSelected == null) return;

        confirmButton.onClick?.RemoveListener(onConfirmSelected);
        cancelButton.onClick?.RemoveListener(onCancelSelected);

        onCancelSelected = null;
        onConfirmSelected = null;
    }

    private void OnDisable()
    {
        Unbind();
    }
}
