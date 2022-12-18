using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace GameOpenGL;

public class Time
{
    public float TimeInSeconds { get; internal set; }
    public float DeltaTime;
}

public sealed class GameObject
{
    public readonly Transform Transform;
    public Time Time => _scene.Time;
    
    private readonly List<Component> _components = new();
    private readonly Scene _scene;

    public GameObject(Scene scene)
    {
        _scene = scene;
        Transform = AddComponent(new Transform());
    }
    
    public GameObject(Transform transform, Scene scene)
    {
        _scene = scene;
        Transform = AddComponent(transform);
    }
    
    public GameObject(Vector3 position, Scene scene)
    {
        _scene = scene;
        Transform = AddComponent(new Transform(position));
    }
    
    public void OnLoad()
    {
        foreach (Component component in _components)
        {
            component.OnLoad();
        }
    }

    public void OnResize(ResizeEventArgs resizeEventArgs)
    {
        foreach (Component component in _components)
        {
            component.OnResize(resizeEventArgs);
        }
    }

    public void Update()
    {
        foreach (Component component in _components)
        {
            component.Update();
        }

        foreach (Component component in _components)
        {
            component.LateUpdate();
        }
    }

    public void Render()
    {
        foreach (Component component in _components)
        {
            component.Render();
        }
    }

    public TComponent AddComponent<TComponent>(TComponent component) where TComponent : Component
    {
        if (_components.Contains(component))
        {
            return component;
        }

        component.GameObject?.RemoveComponent(component);
        _components.Add(component);
        component.GameObject = this;
        return component;
    }
    
    public TComponent AddComponent<TComponent>() where TComponent : Component, new()
    {
        var component = new TComponent();
        _components.Add(component);
        component.GameObject = this;

        return component;
    }

    public TComponent? GetComponent<TComponent>()
    {
        foreach (Component component in _components)
        {
            if (component is TComponent result)
            {
                return result;
            }
        }

        return default;
    }

    public TComponent? FindObjectOfType<TComponent>()
    {
        var result = new List<TComponent>();
        
        foreach (GameObject gameObject in GetAllGameObjects())
        {
            if (gameObject.TryGetComponent(out TComponent component))
            {
                return component;
            }
        }

        return default;
    }

    public TComponent[] FindObjectsOfType<TComponent>()
    {
        var result = new List<TComponent>();
        
        foreach (GameObject gameObject in GetAllGameObjects())
        {
            if (gameObject.TryGetComponent(out TComponent component))
            {
                result.Add(component);
            }
        }

        return result.ToArray();
    }

    public IEnumerable<TComponent> GetComponents<TComponent>()
    {
        foreach (Component component in _components)
        {
            if (component is TComponent result)
            {
                yield return result;
            }
        }
    }

    public bool TryGetComponent<TComponent>(out TComponent component)
    {
        foreach (Component component1 in _components)
        {
            if (component1 is not TComponent result) continue;
            component = result;
            return true;
        }

        component = default!;
        return false;
    }

    public bool RemoveComponent(Component component)
    {
        return _components.Remove(component);
    }

    public void OnDestroy()
    {
        foreach (Component component in _components)
        {
            component.OnDestroy();
        }
    }

    public GameObject[] GetAllGameObjects()
    {
        return _scene.GetAllGameObjects();
    }
}