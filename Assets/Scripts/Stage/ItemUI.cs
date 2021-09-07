using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Sprite[] item_Sprite = new Sprite[6];
    public GameObject keycard;
    public GameObject Item_UI;
    public GameObject ITEMS;
    Color temp;

    int item; // 들고 있는 아이템
    int[] cards = new int[6];
    GameObject[] card_instant = new GameObject[6];

    void Update()
    {
        item = ITEMS.GetComponent<ItemController>().Item;
        cards = ITEMS.GetComponent<ItemController>().Cards;
        if(item != 0){
          temp = Item_UI.GetComponent<Image>().color;
          temp.a = 1f;
          Item_UI.GetComponent<Image>().color = temp;
          Item_UI.GetComponent<Image>().sprite = item_Sprite[item-1];
          UseItem();
        }
        else {
          Item_UI.GetComponent<Image>().sprite = null;
          temp = Item_UI.GetComponent<Image>().color;
          temp.a = 0f;
          Item_UI.GetComponent<Image>().color = temp;
        }
        cardUI();
    }

    void cardUI(){
      int n = 0;
      for(int i = 0; i < cards.Length; i++){
        if(cards[i] == 0) break;
        else if(cards[i] == 99){
          Destroy(card_instant[i]);
          break;
        }
        cardInst(i);
        n++;
      }
      if(n > 0){
        if(Input.GetButtonDown("Jump")){
          ITEMS.GetComponent<ItemController>().CardKey();
        }
      }
    }

    void UseItem()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
          ITEMS.GetComponent<ItemController>().ItemUse(item);
        }
    }

    void cardInst(int a){
      if(card_instant[a] == null){
        GameObject cardPrefab = Instantiate(keycard, new Vector3(0, 0, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        cardPrefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(-110, -300 - (100 * a));
        card_instant[a] = cardPrefab;
      }
    }
}
