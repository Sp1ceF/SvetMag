//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityTest
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tovar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<int> Quantity { get; set; }
        public byte[] Photo { get; set; }
    }
}
