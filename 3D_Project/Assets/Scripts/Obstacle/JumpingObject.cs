using UnityEngine;

public class JumpingObject : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }


}
