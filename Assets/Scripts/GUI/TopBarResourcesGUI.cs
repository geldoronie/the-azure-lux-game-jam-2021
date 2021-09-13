using TMPro;
using UnityEngine;

public class TopBarResourcesGUI : MonoBehaviour
{
    [SerializeField] private GameObject resourcesLayoutGrupo;
    [SerializeField] private TMP_Text _food;
    [SerializeField] private TMP_Text _wood;
    [SerializeField] private TMP_Text _gold;
    [SerializeField] private TMP_Text _stone;
    [SerializeField] private TMP_Text _people;
    [SerializeField] private TMP_Text _military;

    private void Update()
    {
        GameModeBase gm = GameModeBase.Instance;
        Player player = gm.Player;
        _food.SetText(player.Resources.Food.ToString() + " (" + gm.LastTurnRessourcesPerTurn.Food.ToString("+#;-#;0") + ")");
        _wood.SetText(player.Resources.Wood.ToString() + " (" + gm.LastTurnRessourcesPerTurn.Wood.ToString("+#;-#;0") + ")");
        _gold.SetText(player.Resources.Gold.ToString() + " (" + gm.LastTurnRessourcesPerTurn.Gold.ToString("+#;-#;0") + ")");
        _stone.SetText(player.Resources.Stone.ToString() + " (" + gm.LastTurnRessourcesPerTurn.Stone.ToString("+#;-#;0") + ")");
        _people.SetText(player.Resources.People.ToString() + " (" + gm.LastTurnRessourcesPerTurn.People.ToString("+#;-#;0") + ")");
        _military.SetText(player.Resources.Military.ToString() + " (" + gm.LastTurnRessourcesPerTurn.Military.ToString("+#;-#;0") + ")");

        _food.gameObject.SetActive(false);
        _food.gameObject.SetActive(true);
        _wood.gameObject.SetActive(false);
        _wood.gameObject.SetActive(true);
        _gold.gameObject.SetActive(false);
        _gold.gameObject.SetActive(true);
        _stone.gameObject.SetActive(false);
        _stone.gameObject.SetActive(true);
        _people.gameObject.SetActive(false);
        _people.gameObject.SetActive(true);
        _military.gameObject.SetActive(false);
        _military.gameObject.SetActive(true);

        resourcesLayoutGrupo.SetActive(false);
        resourcesLayoutGrupo.SetActive(true);
    }
}
