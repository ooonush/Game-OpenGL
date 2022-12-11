using OpenTK.Windowing.Common;

namespace GameOpenGL;

public class Scene
{
    private readonly HashSet<GameObject> _gameObjects = new();

    public GameObject CreateGameObject()
    {
        var gameObject = new GameObject();
        _gameObjects.Add(gameObject);
        return gameObject;
    }

    public GameObject CreateGameObject(Transform transform)
    {
        var gameObject = new GameObject(transform);
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
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.Update(args);
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