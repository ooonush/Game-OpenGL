using OpenTK.Windowing.Common;

namespace GameOpenGL;

public class Scene
{
    private readonly HashSet<GameObject> _gameObjects = new();
    public readonly Time Time = new Time();

    public GameObject[] GetAllGameObjects()
    {
        return _gameObjects.ToArray();
    }
    
    public GameObject CreateGameObject()
    {
        var gameObject = new GameObject(this);
        _gameObjects.Add(gameObject);
        return gameObject;
    }

    public GameObject CreateGameObject(Transform transform)
    {
        var gameObject = new GameObject(transform, this);
        _gameObjects.Add(gameObject);
        return gameObject;
    }
    
    public bool RemoveGameObject(GameObject gameObject)
    {
        return _gameObjects.Remove(gameObject);
    }
    
    public void OnLoad()
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.OnLoad();
        }
    }

    public void OnResize(ResizeEventArgs resizeEventArgs)
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.OnResize(resizeEventArgs);
        }
    }

    public void Update(FrameEventArgs args)
    {
        Time.TimeInSeconds += (float)args.Time;
        Time.DeltaTime = (float)args.Time;
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.Update();
        }
    }
    
    public void Render()
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.Render();
        }
    }

    public void Unload()
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.OnDestroy();
        }
        _gameObjects.Clear();
    }
}