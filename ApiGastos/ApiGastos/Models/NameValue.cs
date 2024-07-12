namespace ApiGastos.Models
{
    public class NameValue<T>
    {
        public string Name { get; set; } = string.Empty;
        public T Value { get; set; }    
    }
}
