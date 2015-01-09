using System.Runtime.CompilerServices;

namespace Schedule.UniversalApp.BaseTypes
{
    public class Category
    {
        private bool isVisible = true;
        public string filter { get; set; }
        public string _Id { get; set; }
        public string Name { get; set; }

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }
        public override string ToString()
        {
            return Name;
        }  
    }
}
