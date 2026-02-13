using System.Collections;
using UnityEngine;

public class SelectionCursor : MonoBehaviour
{
    [SerializeField] private float activeDuration;

    private void OnEnable()
    {
        StartCoroutine(SetActiveFalseCoroutine());
    }

    private IEnumerator SetActiveFalseCoroutine()
    {
        yield return new WaitForSeconds(activeDuration);
        gameObject.SetActive(false);
    }
}
