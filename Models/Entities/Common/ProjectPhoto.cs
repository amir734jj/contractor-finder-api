using System;
using Models.Abstracts;
using Models.Interfaces;

namespace Models.Entities.Common
{
    public class ProjectPhoto : AbstractFileEntity
    {
        public string Description { get; set; }
    }
}