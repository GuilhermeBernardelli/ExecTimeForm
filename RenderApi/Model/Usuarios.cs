//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RenderApi.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuarios
    {
        public Usuarios()
        {
            this.Renderizar = new HashSet<Renderizar>();
        }
    
        public int id { get; set; }
        public string nome { get; set; }
        public Nullable<int> registro { get; set; }
        public string senha { get; set; }
    
        public virtual ICollection<Renderizar> Renderizar { get; set; }
    }
}