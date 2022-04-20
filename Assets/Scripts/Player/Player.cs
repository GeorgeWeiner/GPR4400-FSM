using DefaultNamespace;
using UnityEngine.SceneManagement;

namespace Player
{
    public class Player : Creature
    {
        protected override void OnDeath()
        {
            ReloadScene();
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene("OutdoorsScene");
        }
    }
}