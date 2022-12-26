//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GIBDD.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cars
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cars()
        {
            this.Drivers = new HashSet<Drivers>();
        }
    
        public int Id { get; set; }
        public string VIN { get; set; }
        public string Manufact { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Weight { get; set; }
        public Nullable<int> Color { get; set; }
        public Nullable<int> EngineTypeId { get; set; }
        public string TypeOfDrive { get; set; }
    
        public virtual CarColors CarColors { get; set; }
        public virtual EngineTypes EngineTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Drivers> Drivers { get; set; }

        public override string ToString()
        {            
            return $"Машина №{this.Id}: VIN - {this.VIN}, производитель - {this.Manufact}, модель - {this.Model}, год - {this.Year} г, вес - {this.Weight} кг, привод - {this.TypeOfDrive}";
        }
    }
}
