using System;
using System.Collections.Generic;

namespace Models.Abstracts
{
    public abstract class AbstractFileEntity
    {
        public string Name { get; set; }
        
        public string ContentType { get; set; }
        
        public IReadOnlyDictionary<string, string> Metadata { get; set; }
        
        public Uri Uri { get; set; }
    }
}