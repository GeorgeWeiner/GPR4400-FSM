using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI
{
    public class AIGroupManager : MonoBehaviour
    {
        public List<Npc> npcsInGroup;
        private void Awake()
        {
            var allNpcGameObjects = GameObject.FindGameObjectsWithTag("NPC").ToList();
            foreach (var npcGameObject in allNpcGameObjects)
            {
                npcsInGroup.Add(npcGameObject.GetComponent<Npc>());
            }
        }
    }
}
