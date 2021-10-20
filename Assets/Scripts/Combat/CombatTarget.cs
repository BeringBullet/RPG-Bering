using RPG.Attribute;
using UnityEngine;
using RPG.Control;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        public bool HangleRaycast(PlayerController callingControler)
        {
            if (!callingControler.GetComponent<Fighter>().CanAttack(gameObject)) return false;

            if (Input.GetMouseButton(0))
            {
                callingControler.GetComponent<Fighter>().Attack(gameObject);
            }
            return true;
        }
    }
}