using Unity.VisualScripting;
using UnityEngine;

public class Colllision : MonoBehaviour
{
    public int value = 1;

    public GameManagerScript game;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            game.AddScore(value);
            Destroy(gameObject);
            
        }
    }
}
