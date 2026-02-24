using System;

public class ObservableInt
{
    public Action<int> OnValueChanged = delegate { };

    private int value;

    public int Value { get {  return value; }
        set {
            if (value == this.value) return;
            this.value = value;
            OnValueChanged?.Invoke(this.value);
        }
    }

    public ObservableInt(int startingValue)
    {
        value = startingValue;
    }
}
