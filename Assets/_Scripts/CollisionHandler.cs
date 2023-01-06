using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private const string FriendlyTag = "Friendly";
    private const string FinishPadTag = "FinishPad";
    private const string FuelTag = "Fuel";


    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case FriendlyTag:
                Debug.LogFormat("This thing is {0}", FriendlyTag);
                break;
            case FinishPadTag:
                Debug.LogFormat("Congrats, you finished!");
                break;
            case FuelTag:
                Debug.LogFormat("You picked up a {0}", FuelTag);
                break;
            default:
                Debug.LogFormat("Sorry you blew up");
                break;
        }
    }
}
