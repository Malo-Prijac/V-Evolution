using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    private Upgrade _upgrade;
    private GameObject _playerGameObject;
    private Weapon _weapon;
    private Button _button;
    private TextMeshProUGUI _textDescription;
    // Start is called before the first frame update
    void Start()
    {
        _playerGameObject = FindObjectOfType<PlayerController>().gameObject;
        _textDescription = GetComponentInChildren<TextMeshProUGUI>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ClickButtonUpgrade);

        UpgradeManager.Instance.UpgradeButtons.Add(this);
        DrawUpgrade();
    }

    // Update is called once per frame
    void Update()
    {
        _button.interactable = (UpgradeManager.Instance.CanPurchaseUpgrade(_upgrade.Price));
    }

    public void DrawUpgrade()
    {
        _upgrade = UpgradeManager.Instance.DrawUpgrade();
        _textDescription.SetText(_upgrade.Description);
        
        TextMeshProUGUI[] texts = transform.parent.gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var text in texts)
        {
            if (text != _textDescription)
            {
                text.SetText(_upgrade.Price.ToString("000"));
            }
        }
    }
    
    
    public void ClickButtonUpgrade()
    {
        if (UpgradeManager.Instance.CanPurchaseUpgrade(_upgrade.Price) == false)
            return;
        
        _upgrade.UseUpgrade(_playerGameObject);
        UpgradeManager.Instance.UseMoney(_upgrade.Price);
    }
    
}
