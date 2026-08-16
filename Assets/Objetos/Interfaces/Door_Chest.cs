using UnityEngine;
using System.Collections;

public interface IInteractable
{
    void Interact(GameObject Interactor);
}

public interface IDestructible
{
    void MagicDestroy(GameObject Interactor);
}
