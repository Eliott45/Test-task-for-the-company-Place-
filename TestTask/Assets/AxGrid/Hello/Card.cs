using UnityEngine;

namespace AxGrid.Hello
{
    public class Card : MonoBehaviour
    {
        public void SetLayer(int i)
        {
            GetComponent<SpriteRenderer>().sortingOrder = i;
            foreach (Transform child in gameObject.transform)
            {
                child.GetComponent<SpriteRenderer>().sortingOrder = i;
            }
        }
    }
}
