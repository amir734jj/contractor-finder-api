namespace Core.Tests
{
    public class DummyModel
    {
        public string Prop1 { get; set; }
        
        public string Prop2 { get; set; }
        
        public NestedDummyModel Prop3 { get; set; }
        
        public static string StaticProp {get; set; }
    }
    
    public class NestedDummyModel
    {
        public string Nested { get; set; }
    }
}