using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuTopBarGUI : MonoBehaviour
{

    [SerializeField] private TMP_Text _playerDisplayName;
    // Start is called before the first frame update

    [SerializeField] private PlayerProfilePlayfab _playerProfilePlayfab;

    void Awake()
    {
        this._playerProfilePlayfab = GameObject.Find("PlayerProfile").GetComponent<PlayerProfilePlayfab>();
        this._playerProfilePlayfab.OnProfileLoadedSuccess += this.OnPlayerProfile;
    }

    void Start()
    {
        if(this._playerProfilePlayfab.IsLoaded)
            this._playerDisplayName.text = this._playerProfilePlayfab.Profile.displayName;
    }

    void OnPlayerProfile(){
        this._playerProfilePlayfab.OnProfileLoadedSuccess -= this.OnPlayerProfile;
        this._playerDisplayName.text = this._playerProfilePlayfab.Profile.displayName;
    }
}
