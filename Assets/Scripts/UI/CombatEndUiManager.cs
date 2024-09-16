using DG.Tweening;
using UnityEngine;

public class CombatEndUiManager : MonoBehaviour
{
    [SerializeField] private GameObject combatWinUi;
    [SerializeField] private GameObject combatLoseUi;
    public void OnCombatWin()
    {
        Vector3 targetScale = combatWinUi.transform.localScale;
        combatWinUi.transform.localScale = Vector3.zero;
        combatWinUi.SetActive(true);
        combatWinUi.transform.DOScale(targetScale, 1f).SetEase(Ease.InOutBounce);
    }
    
    public void OnCombatLose()
    {
        Vector3 targetScale = combatLoseUi.transform.localScale;
        combatLoseUi.transform.localScale = Vector3.zero;
        combatLoseUi.SetActive(true);
        combatLoseUi.transform.DOScale(targetScale, 1f).SetEase(Ease.InOutBounce);
    }
    
}
