using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopBarResourcesGUI : MonoBehaviour
{
    [SerializeField] Player Player;
    // Start is called before the first frame update
    
    [SerializeField] TextIconGUI Food;
    [SerializeField] TextIconGUI Wood;
    [SerializeField] TextIconGUI Gold;
    [SerializeField] TextIconGUI Stone;
    [SerializeField] TextIconGUI People;
    [SerializeField] TextIconGUI Military;

    // Update is called once per frame
    void Update()
    {
        Food.Value = this.Player.FoodAmount.ToString();
        Wood.Value = this.Player.WoodAmount.ToString();
        Gold.Value = this.Player.GoldAmount.ToString();
        Stone.Value = this.Player.StoneAmount.ToString();
        People.Value = this.Player.PeopleAmount.ToString();
        Military.Value = this.Player.MilitaryAmount.ToString();
    }
}
