using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace GameOpenGL;

public sealed class GameObject
{
    public readonly Transform Transform;
    
    private readonly List<Component> _components = new();

    public GameObject()
    {
        Transform = AddComponent(new Transform());
    }
    
    public GameObject(Transform transform)
    {
        Transform = AddComponent(transform);
    }
    
    public GameObject(Vector3 position)
    {
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

    public void Update(FrameEventArgs args)
    {
        foreach (Component component in _components)
        {
            component.DeltaTime = (float)args.Time;
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

    public TComponent GetComponent<TComponent>() where TComponent : Component
    {
        foreach (Component component in _components)
        {
            if (component is TComponent result)
            {
                return result;
            }
        }

        return null!;
    }
    
    public bool TryGetComponent<TComponent>(out TComponent component) where TComponent : Component
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
}