//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lech_Poznań.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web;

    public partial class Sztab
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sztab()
        {
            this.Nagrody_dla_sztabu = new HashSet<Nagrody_dla_sztabu>();
        }
    
        public int ID { get; set; }
        public string Imię { get; set; }
        public string Nazwisko { get; set; }
        public Nullable<int> Id_Funkcji { get; set; }
        [DisplayName("Zdjęcie")]
        public string Zdjęcie { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

        public virtual Funckje Funckje { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Nagrody_dla_sztabu> Nagrody_dla_sztabu { get; set; }

        public string FullName
        {
            get
            {
                return String.Format("{0} {1}", Imię, Nazwisko);
            }
        }
    }
}
