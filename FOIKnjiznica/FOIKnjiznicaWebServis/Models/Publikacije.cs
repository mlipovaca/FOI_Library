//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FOIKnjiznicaWebServis.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Publikacije
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Publikacije()
        {
            this.Je_Autor = new HashSet<Je_Autor>();
            this.Je_Favorit = new HashSet<Je_Favorit>();
            this.Kopija_Publikacije = new HashSet<Kopija_Publikacije>();
        }
    
        public int id { get; set; }
        public string naziv { get; set; }
        public string isbn { get; set; }
        public string udk { get; set; }
        public string signatura { get; set; }
        public string jezik { get; set; }
        public int stranice { get; set; }
        public string sadrzaj { get; set; }
        public string slika_url { get; set; }
        public int godina_izdanja { get; set; }
        public string izdanje { get; set; }
        public int KategorijeId { get; set; }
        public int IzdavaciId { get; set; }
    
        public virtual Izdavaci Izdavaci { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Je_Autor> Je_Autor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Je_Favorit> Je_Favorit { get; set; }
        public virtual Kategorije Kategorije { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kopija_Publikacije> Kopija_Publikacije { get; set; }
    }
}