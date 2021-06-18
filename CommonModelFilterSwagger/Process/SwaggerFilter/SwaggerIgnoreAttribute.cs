using System;

namespace Process.SwaggerFilter
{
    public class SwaggerIgnoreAttribute : Attribute
    {
        public bool ExceptUpdate { get; set; }

        public SwaggerIgnoreAttribute(bool ExceptUpdate = false)
        {
            this.ExceptUpdate = ExceptUpdate;
        }
    }
}
