using UnityEngine;
using UnityEngine.SceneManagement;

public class Koudousuru : MonoBehaviour
{
        public void GoToShopping()
        {
            SceneManager.LoadScene("ShoppingScene");
        }

        public void GoToWork()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
