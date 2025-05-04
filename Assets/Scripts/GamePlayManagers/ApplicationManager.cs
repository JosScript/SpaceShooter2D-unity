using UnityEngine;

namespace GamePlayManagers
{
    public class ApplicationManager : MonoBehaviour
    {
        [SerializeField] private GameObject menuManagerPrefab;
        [SerializeField] private GameObject sceneManagerPrefab;
        [SerializeField] private GameObject audioSystemPrefab;

        private void Awake()
        {
            BuildManagerParent();
        }

        private void BuildManagerParent()
        {

            if (GameObject.FindGameObjectWithTag("ManagersParent") == null)
            {

                GameObject managersParent = new GameObject();
                Instantiate(managersParent, transform.position, transform.rotation);
                managersParent.tag = "ManagersParent";
                managersParent.name = "ManagersParent";

                Instantiate(menuManagerPrefab, managersParent.transform);
                Instantiate(sceneManagerPrefab, managersParent.transform);
                Instantiate(audioSystemPrefab, managersParent.transform);

                DontDestroyOnLoad(managersParent);
            }
        }
    }
}
