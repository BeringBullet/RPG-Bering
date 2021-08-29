using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Resources;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerControler : MonoBehaviour
    {
        Health health;
        enum CursorType
        {
            None,
            Movement,
            Combat
        }

        [Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings;
        private void Awake()
        {
            health = GetComponent<Health>();
        }
        void Update()
        {
            if (InteractWithIU()) return;
            if (health.isDead) 
            {
                SetCursor(CursorType.None);
                return;
            }
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            SetCursor(CursorType.None);
        }

        private bool InteractWithIU()
        {
           return EventSystem.current.IsPointerOverGameObject();
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                SetCursor(CursorType.Combat);
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hadHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hadHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }
        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }
        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                    return mapping;
            }
            return cursorMappings[0];
        }
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}