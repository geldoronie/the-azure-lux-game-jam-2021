using UnityEngine;

public class NextVickingAttackGUI : MonoBehaviour
{
    [SerializeField] private TextIconGUI _militaryRequired;
    [SerializeField] private TextIconGUI _turnsLeft;

    private void Update()
    {
        _militaryRequired.Value = ((GameModeWorldVsYou)GameModeBase.Instance).VickingsAttackMilitaryRequired.ToString();
        _turnsLeft.Value = ((GameModeWorldVsYou)GameModeBase.Instance).VickingsAttackTurnsLeft.ToString();
    }
}
