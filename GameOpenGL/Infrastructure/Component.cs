using OpenTK.Windowing.Common;

namespace GameOpenGL;

public abstract class Component
{
    public float DeltaTime { get; internal set; }
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

    public TComponent GetComponent<TComponent>() where TComponent : Component
    {
        return GameObject.GetComponent<TComponent>();
    }
    
    public TComponent[] GetComponents<TComponent>() where TComponent : Component
    {
        return GameObject.GetComponents<TComponent>().ToArray();
    }

    public bool TryGetComponent<TComponent>(out TComponent component) where TComponent : Component
    {
        return GameObject.TryGetComponent(out component);
    }
    
    public List<Component> GetAllGameObjects()
    {
        return GameObject.GetAllGameObjects();
    }
}