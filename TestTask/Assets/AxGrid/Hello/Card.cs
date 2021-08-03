using UnityEngine;

namespace AxGrid.Hello
{
    public class Card : MonoBehaviour, IClicked
    {
        public enum ECollection
        {
            A,
            B
        };
        
        public int CardID;
        public ECollection collection;
        
        public void SetLayer(int i)
        {
            GetComponent<SpriteRenderer>().sortingOrder = i;
            foreach (Transform child in gameObject.transform)
            {
                child.GetComponent<SpriteRenderer>().sortingOrder = i;
            }
        }

        public void ONClickAction()
        {
            Settings.Fsm.Invoke("SwapCard", gameObject);
        }
    }
}
