using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotInventario : MonoBehaviour
{
    public GameObject objetoSlot;
    private _GameController _GameController;
    private PainelItemInfo painelItemInfo;

    [Header("Id Slots")]
    public int idSlot;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        painelItemInfo = FindObjectOfType(typeof(PainelItemInfo)) as PainelItemInfo;


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void usarItem() {
        // print("Usei Item");
        if (objetoSlot != null) {
            // objetoSlot.SendMessage("usarItem", SendMessageOptions.DontRequireReceiver);

            painelItemInfo.objetoSlot=objetoSlot;
            painelItemInfo.idSlot = idSlot;

            painelItemInfo.carregarInfoItem();
            _GameController.openItemInfo();

        }
    }



}
