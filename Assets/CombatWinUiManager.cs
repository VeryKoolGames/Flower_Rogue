using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Events;
using DG.Tweening;
using UnityEngine;

public class CombatWinUiManager : MonoBehaviour
{
    [SerializeField] private GameObject combatWinUi;
    public void OnCombatWin()
    {
        Vector3 targetScale = combatWinUi.transform.localScale;
        combatWinUi.transform.localScale = Vector3.zero;
        combatWinUi.SetActive(true);
        combatWinUi.transform.DOScale(targetScale, 1f).SetEase(Ease.InOutBounce);
    }
}
