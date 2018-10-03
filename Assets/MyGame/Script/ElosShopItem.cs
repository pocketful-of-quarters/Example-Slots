using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using QuartersSDK;

namespace Elona.Slot {
	public class ElosShopItem : MonoBehaviour {

		public Text textName;
		public Text textCost;
		public Image icon;
        public ShopItemData item;
		public Button button;
		public Image bg;
		public Color colorValid;
		public Color colorInvalid;

        public void ApplyData(ShopItemData item, ElosShop shop) {
            this.item = item;
            item.actor = this;

            button.onClick.AddListener(() => { shop.Buy(item); });
		}

		public void Refresh(bool canAfford) {
            textName.text = item.name;
            textCost.text = item.localizedCost;
            //icon.sprite = product.sprite;
			textCost.color = canAfford ? colorValid : colorInvalid;
		}
	}
}