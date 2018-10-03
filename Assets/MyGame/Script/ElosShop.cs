using System;
using System.Collections.Generic;
using CSFramework;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Purchasing;
using QuartersSDK;
using UnityEngine.UI;
using UIToolkit.UI;

namespace Elona.Slot {
	[ExecuteInEditMode]
	public class ElosShop : MonoBehaviour {
		public Elos elos;
        public Wait waitModal;

        public GridLayoutGroup layoutItems;
		public Transform window;
		public Image background;
		public Transform mascot;

        //data only, this is dumb, TODO change this to utilize Product class
        public List<ShopItemData> items;


        public List<ElosShopItem> cells;

		public ElosShopItem itemMold;
		public int minCheatBalance;

		private int balance { get { return elos.slot.gameInfo.balance; } }
		private Elos.Assets assets { get { return elos.assets; } }

        private void Awake()
        {
        //    if (!Application.isPlaying) {
        //    	items.Sort((x, y) => { return (int)y.cost - (int)x.cost; });
        //    	return;
        //    }
            List<string> productIds = new List<string>();
            productIds.Add("400quarters");
            productIds.Add("800quarters");
            productIds.Add("1200quarters");
            InitializeIAP(productIds);

            //move to center
            this.transform.localPosition = Vector3.zero;
        }

        private void InitializeIAP(List<string> productIds)
        {
            Debug.Log("Product IDs: " + productIds[0]);
            QuartersIAP.Instance.Initialize(productIds, delegate (Product[] products) {
                items = new List<ShopItemData>();

                int i = 0;
                // loop trough the array of products we fetched from the server
                foreach (Product product in products)
                {
                    // create a ShopItemData object using the data of each fetched iAP product...
                    ShopItemData d = new ShopItemData();
                    //d.id = "test" + i;
                    d.id = product.definition.storeSpecificId;
                    d.name = product.metadata.localizedTitle;
                    Debug.Log("Product title: " + product.metadata.localizedTitle);

                    d.cost = product.metadata.localizedPrice;
                    d.localizedCost = product.metadata.localizedPriceString;
                    string quantityAsString = d.id.Replace("Quarters", string.Empty);
                    int quantity;
                    int.TryParse(quantityAsString, out quantity);
                    d.quantity = quantity;

                    // ...and add it to the temp items array
                    items.Add(d);

                    i++;
                }

                 //refresh the Shop


            }, delegate (InitializationFailureReason reason) {
                Debug.LogError(reason.ToString());
            });
        }



        public void Activate() {






			gameObject.SetActive(true);
			assets.audioClick.Play();
			background.color = new Color(0, 0, 0, 0);
			background.DOFade(0.5f, 1f);
			mascot.transform.localPosition = window.transform.localPosition = new Vector3(0, 1200, 0);
			window.DOLocalMoveY(0, 1f).SetEase(Ease.OutBounce);
			Util.Tween(0.35f, null, () => {
				assets.audioImpact.Play();
				Camera.main.DOShakePosition(1.2f, 6, 12);
			});
			mascot.DOLocalMoveY(0, 2f).SetEase(Ease.OutBounce);


            foreach (ElosShopItem cell in cells) Destroy(cell.gameObject);
            cells.Clear();

            Refresh();

        }

		public void Deactivate() {
			if (DOTween.IsTweening(background)) return;
			assets.audioClick.Play();
			background.DOFade(0f, 0.8f).OnComplete(_Deactivate);
			window.DOLocalMoveY(-1200, 0.8f).SetEase(Ease.InBack);
		}

		public void _Deactivate() { gameObject.SetActive(false); }




        public void Buy(ShopItemData item)
        {
            Debug.Log("Selected Quantity" + item.quantity);
            Debug.Log("Selected " + item.id);

            foreach (Product p in QuartersIAP.Instance.products)
            {
                if (p.definition.storeSpecificId == item.id)
                {
                    Debug.Log("Buying... " + p.metadata.localizedTitle);

                    // Buy iAP product
                    QuartersIAP.Instance.BuyProduct(p, (Product product, string txId) => {
                        Debug.Log("Purchase complete");
                        Debug.Log("Quantity: " + item.quantity);

                        //elos.slot.gameInfo.AddBalance(item.quantity);
                        waitModal.Activate();
                        this.Deactivate();

                    }, (string error) => {
                        Debug.LogError("Purchase error: " + error);

                    });
                    break;
                }
            }

            //test
            //LoadingView.instance.Show();

        }

        public void Refresh() {
            //if (itemMold.gameObject.activeSelf) {
                foreach (ShopItemData item in items) {
                ElosShopItem cell = Util.InstantiateAt<ElosShopItem>(itemMold, layoutItems.transform);
                cell.ApplyData(item, this);

                cells.Add(cell);

                item.actor.gameObject.SetActive(true);
                    item.actor.Refresh(true);

                    Debug.Log("Item id.. " + item.id);
                }
                itemMold.gameObject.SetActive(false);
            //}
            //foreach (ShopItemData item in items) {
            //  if (item.cost < 0) item.actor.gameObject.SetActive(balance < minCheatBalance);
            //  item.actor.Refresh(balance >= item.cost);
            //}
		}
	}
}