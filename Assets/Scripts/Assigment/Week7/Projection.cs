
using UnityEngine;
using UnityEngine.SceneManagement;
using Transform = UnityEngine.Transform;

public class Projection : MonoBehaviour
{
   private Scene _simulationScene;
   private PhysicsScene _physicsScene;
   [SerializeField] private Transform[] map;

   private void Start()
   {
      CreateProjection();
   }

   void CreateProjection()
   {
      _simulationScene = SceneManager.CreateScene("Simulation2", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
      _physicsScene = _simulationScene.GetPhysicsScene();
      foreach (Transform obj in map)
      {
         var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
         ghostObj.GetComponent<Renderer>().enabled = false;
         SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
      }
      
   }

   [SerializeField] private LineRenderer _line;
   [SerializeField] private int _maxPhysicsFrameIterations = 100;

   public void SimulateTrajectory(GameObject BallPrefab, Transform pos, Vector3 velocity)
   {
      var ghostObj = Instantiate(BallPrefab, pos.position, Quaternion.identity).GetComponent<Rigidbody>();
      ghostObj.GetComponent<Renderer>().enabled = false;
      ghostObj.transform.GetChild(0).gameObject.SetActive(false);
      ghostObj.gameObject.GetComponent<BulletTeleport>().enabled = false;
      SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);
      ghostObj.AddForce(velocity, ForceMode.Impulse);

      _line.positionCount = _maxPhysicsFrameIterations;
      for (int i = 0; i < _maxPhysicsFrameIterations; i++)
      {
         _physicsScene.Simulate(Time.fixedDeltaTime);
         _line.SetPosition(i, ghostObj.transform.position);
      }
      Destroy(ghostObj);
   }
}
