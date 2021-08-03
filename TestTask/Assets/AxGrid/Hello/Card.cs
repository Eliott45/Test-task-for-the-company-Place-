using UnityEngine;

namespace AxGrid.Hello
{
    public class Card : MonoBehaviour, IClicked
    {
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
            Debug.Log("!");
        }
    }
}
