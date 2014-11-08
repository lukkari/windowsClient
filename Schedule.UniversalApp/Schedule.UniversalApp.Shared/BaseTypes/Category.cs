namespace Schedule.UniversalApp.BaseTypes
{
    public class Category
    {        
        public string _Id { get; set; }
        public string Name { get; set; }       
        public override string ToString()
        {
            return Name;
        }  
    }
}
