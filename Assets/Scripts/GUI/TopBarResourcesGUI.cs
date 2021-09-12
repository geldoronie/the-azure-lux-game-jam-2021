using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopBarResourcesGUI : MonoBehaviour
{
    [SerializeField] Player Player;
    // Start is called before the first frame update
    
    [SerializeField] ResourceIconGUI Food;
    [SerializeField] ResourceIconGUI Wood;
    [SerializeField] ResourceIconGUI Gold;
    [SerializeField] ResourceIconGUI Stone;
    [SerializeField] ResourceIconGUI People;
    [SerializeField] ResourceIconGUI Military;

    // Update is called once per frame
    void Update()
    {
        Food.Value = this.Player.FoodAmount;
        Wood.Value = this.Player.WoodAmount;
        Gold.Value = this.Player.GoldAmount;
        Stone.Value = this.Player.StoneAmount;
        People.Value = this.Player.PeopleAmount;
        Military.Value = this.Player.MilitaryAmount;
    }
}
