using OpenTK.Windowing.Common;

namespace GameOpenGL;

public abstract class Component
{
    public Time Time => GameObject.Time;

    public GameObject GameObject { get; internal set; } = null!;
    public Transform Transform => GameObject.Transform;


    
    protected Component()
    {
        
    }

    public virtual void OnLoad()
    {
        
    }

    public virtual void OnResize(ResizeEventArgs e)
    {
        
    }

    public virtual void Update()
    {
        
    }

    public virtual void LateUpdate()
    {
        
    }

    public virtual void Render()
    {
        
    }

    public virtual void OnDestroy()
    {
        
    }

    public TComponent AddComponent<TComponent>(TComponent component) where TComponent : Component
    {
        return GameObject.AddComponent(component);
    }

    public TComponent? GetComponent<TComponent>()
    {
        return GameObject.GetComponent<TComponent>();
    }
    
    public TComponent[] GetComponents<TComponent>()
    {
        return GameObject.GetComponents<TComponent>().ToArray();
    }

    public bool TryGetComponent<TComponent>(out TComponent component)
    {
        return GameObject.TryGetComponent(out component);
    }

    public TComponent[] FindObjectsOfType<TComponent>()
    {
        return GameObject.FindObjectsOfType<TComponent>();
    }

    public TComponent? FindObjectOfType<TComponent>()
    {
        return GameObject.FindObjectOfType<TComponent>();
    }
}