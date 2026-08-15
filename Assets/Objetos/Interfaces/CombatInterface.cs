using UnityEngine;
using System.Collections;

public interface IKillable
{
    void Kill();
}

public interface IDamageable
{
    void TakeDamage(float damage);
}
