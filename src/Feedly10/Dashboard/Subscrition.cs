using System.Collections.Generic;

namespace App.Dashboard
{
    public class Subscrition
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Website { get; set; }
        public List<Category> Categories { get; set; }

    }
}