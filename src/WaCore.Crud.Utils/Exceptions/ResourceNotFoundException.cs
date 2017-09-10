using System;
using System.Collections.Generic;
using System.Text;

namespace WaCore.Crud.Utils.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        private readonly object _resource;

        public ResourceNotFoundException(object resource)
            : base(String.Format("Resource {0} not found.", resource))
        {
            _resource = resource;
        }

        public ResourceNotFoundException(object resource, string message)
            : base(message)
        {
            _resource = resource;
        }

        public object Resource { get { return _resource; } }

        public string BaseMessage
        {
            get { return "Ressource not found."; }
        }
    }
}
