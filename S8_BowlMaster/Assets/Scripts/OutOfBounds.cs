using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class OutOfBounds : MonoBehaviour {
    [SerializeField] private Ball ball;
    [SerializeField] private DragLaunch ballController;

    private BoxCollider boundsCollider;
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ball.gameObject)
        {
            ballController.ResetBall();
        }
    }
}
