using System.Linq;

public class TrixProp<T> : M4u.M4uProperty<T>
{

    public TrixProp(T val) : base(val) { }

    public TrixProp() : base() { }

    public new T Value
    {
        get { return base.Value; }
        set
        {
            base.Value = value;
        }
    }

    private string _boundPath = null;
    private string BoundPath
    {
        get
        {
            if (string.IsNullOrEmpty(_boundPath))
            {
                var binding = Bindings.FirstOrDefault();
                if (binding != null && binding.Paths.Length > 0)
                {
                    _boundPath = string.Join(".", binding.Paths);
                }
            }
            return _boundPath;
        }
    }
}
