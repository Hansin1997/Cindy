using Cindy.Control.Cameras;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SimpleTrigger : MonoBehaviour
{
    public string targetName;
    public CameraBehaviourAttachment attachment;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals(targetName))
            attachment.Attach();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals(targetName))
            attachment.Detach();
    }
}
