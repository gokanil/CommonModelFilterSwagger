using Process.SwaggerFilter;
using System.ComponentModel;

namespace Data.Models
{
    public class TestModel
    {
        [SwaggerIgnore]
        public string MyFuture { get; set; }

        [SwaggerIgnore(true)]
        public int MyId { get; set; }

        public string MyName { get; set; }

        public string Description { get; set; }
        
    }
}
